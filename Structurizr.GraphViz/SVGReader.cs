namespace Structurizr.GraphViz
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class SVGReader
    {
        private string directory;
        private bool changePaperSize;

        private int margin;

        public SVGReader(string directory, int margin, bool changePaperSize)
        {
            this.directory = directory;
            this.margin = margin;
            this.changePaperSize = changePaperSize;
        }

        public void ParseAndApplyLayout(View view, ViewSet viewSet)
        {
            var filePath = Path.Combine(directory, view.Key + ".dot.svg");
            Console.WriteLine(" - Parsing " + filePath);

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var document = new XmlDocument();
                document.Load(stream);

                var namespaceManager = new XmlNamespaceManager(document.NameTable);
                namespaceManager.AddNamespace("a", "http://www.w3.org/2000/svg");

                var nodeList = document.SelectNodes("/a:svg/a:g[@class=\"graph\"]", namespaceManager);
                var transform = nodeList[0].Attributes.GetNamedItem("transform").Value;
                var translate = transform.Substring(transform.IndexOf("translate"));
                var getNumbersRegex = "\\d+";
                var numbers = Regex.Matches(translate, getNumbersRegex, RegexOptions.CultureInvariant);

                var transformX = int.Parse(numbers[0].Value);
                var transformY = int.Parse(numbers[1].Value);

                var minimumX = int.MaxValue;
                var minimumY = int.MaxValue;
                var maximumX = int.MinValue;
                var maximumY = int.MinValue;

                foreach (var elementView in view.Elements)
                {
                    if (elementView.Element is DeploymentNode)
                    {
                        // deployment nodes are clusters, so positioned automatically
                        continue;
                    }

                    var selectElementExpression = $"/a:svg/a:g/a:g[@id=\"{elementView.Id}\"]/a:polygon";
                    nodeList = document.SelectNodes(selectElementExpression, namespaceManager);
                    if (nodeList.Count == 0)
                    {
                        continue;
                    }

                    var pointsAsString = nodeList[0].Attributes.GetNamedItem("points").Value;
                    var points = pointsAsString.Split(' ');
                    var coordinates = points[1].Split(',');

                    var x = double.Parse(coordinates[0]) + transformX;
                    var y = double.Parse(coordinates[1]) + transformY;

                    elementView.X = (int) (x * Constants.DpiRatio);
                    elementView.Y = (int) (y * Constants.DpiRatio);

                    minimumX = Math.Min(elementView.X.Value, minimumX);
                    minimumY = Math.Min(elementView.Y.Value, minimumY);
                    maximumX = Math.Max(elementView.X.Value + this.GetElementWidth(view, viewSet, elementView.Id).Value,
                        maximumX);
                    maximumY = Math.Max(
                        elementView.Y.Value + this.GetElementHeight(view, viewSet, elementView.Id).Value, maximumY);
                }

                foreach (var relationshipView in view.Relationships)
                {
                    var selectRelationshipExpression = $"/a:svg/a:g/a:g[@id=\"{relationshipView.Id}\"]/a:path";
                    nodeList = document.SelectNodes(selectRelationshipExpression, namespaceManager);
                    if (nodeList.Count == 0)
                    {
                        continue;
                    }

                    var dAsString = nodeList[0].Attributes.GetNamedItem("d").Value;
                    var d = dAsString.Split(' ');

                    var vertices = new List<Vertex>();

                    if (d.Length == 3)
                    {
                        relationshipView.Vertices = vertices;
                    }
                    else
                    {
                        for (int i = 1; i < d.Length - 2; i++)
                        {
                            var x = double.Parse(d[i].Split(',')[0]) + transformX;
                            var y = double.Parse(d[i].Split(',')[1]) + transformY;
                            var vertex = new Vertex((int)(x * Constants.DpiRatio), (int)(y * Constants.DpiRatio));
                            vertices.Add(vertex);

                            minimumX = Math.Min(vertex.X.Value, minimumX);
                            minimumY = Math.Min(vertex.Y.Value, minimumY);
                            maximumX = Math.Max(vertex.X.Value, maximumX);
                            maximumY = Math.Max(vertex.Y.Value, maximumY);
                        }

                        relationshipView.Vertices = vertices;
                    }
                }

                // also take into account any clusters that might be rendered outside the nodes
                var selectClusterExpression = "/a:svg/a:g/a:g[@class=\"cluster\"]/a:polygon";
                nodeList = document.SelectNodes(selectClusterExpression, namespaceManager);
                for (int i = 0; i < nodeList.Count; i++)
                {
                    var points = nodeList[i].Attributes.GetNamedItem("points").Value.Split(' ');
                    foreach (var point in points)
                    {
                        int x = (int)((double.Parse(point.Split(',')[0]) + transformX) * Constants.DpiRatio);
                        int y = (int)((double.Parse(point.Split(',')[1]) + transformY) * Constants.DpiRatio);

                        minimumX = Math.Min(x, minimumX);
                        minimumY = Math.Min(y, minimumY);
                        maximumX = Math.Max(x, maximumX);
                        maximumY = Math.Max(y, maximumY);
                    }
                }

                if (changePaperSize)
                {
                    var orientation = (maximumX > maximumY) ? Orientation.Landscape : Orientation.Portrait;

                    foreach (var paperSize in PaperSize.GetOrderedPaperSizes(orientation))
                    {
                        if (paperSize.width > (maximumX + margin + margin) && paperSize.height > (maximumY + margin + margin))
                        {
                            view.PaperSize = paperSize;
                            break;
                        }
                    }
                }

                var deltaX = (view.PaperSize.width - maximumX + minimumX) / 2;
                var deltaY = (view.PaperSize.height - maximumY + minimumY) / 2;

                // move everything relative to 0,0
                foreach (var elementView in view.Elements)
                {
                    elementView.X = elementView.X - minimumX;
                    elementView.Y = elementView.Y - minimumY;
                }

                foreach (RelationshipView relationshipView in view.Relationships)
                {
                    foreach (var vertex in relationshipView.Vertices)
                    {
                        vertex.X = vertex.X - minimumX;
                        vertex.Y = vertex.Y - minimumY;
                    }
                }

                // and now centre everything
                foreach (var elementView in view.Elements)
                {
                    elementView.X = elementView.X + deltaX;
                    elementView.Y = elementView.Y + deltaY;
                }

                foreach (var relationshipView in view.Relationships)
                {
                    foreach (var vertex in relationshipView.Vertices)
                    {
                        vertex.X = vertex.X + deltaX;
                        vertex.Y = vertex.Y + deltaY;
                    }
                }
            }

            Console.WriteLine(" - Done");
    }

        private int? GetElementWidth(View view, ViewSet viewSet, string elementId)
        {
            var element = view.Model.GetElement(elementId);
            return viewSet
                .Configuration
                .Styles
                .Elements
                .First(e =>
                    element
                        .Tags
                        .Contains(e.Tag))
                .Width;
        }

        private int? GetElementHeight(View view, ViewSet viewSet, string elementId) {
            var element = view.Model.GetElement(elementId);
            return viewSet
                .Configuration
                .Styles
                .Elements
                .First(e =>
                    element
                        .Tags
                        .Contains(e.Tag))
                .Height;
        }
    }
}
