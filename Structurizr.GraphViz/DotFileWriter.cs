namespace Structurizr.GraphViz
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public class DotFileWriter
    {
        private const int CLUSTER_INTERNAL_MARGIN = 25;

        private string directory;
        private double rankSeparation;
        private double nodeSeparation;
        private RankDirection rankDirection;
        private CultureInfo locale;

        public DotFileWriter(
            string directory,
            RankDirection rankDirection,
            double rankSeparation,
            double nodeSeparation,
            CultureInfo locale = null)
        {
            this.directory = directory;
            this.rankDirection = rankDirection;
            this.rankSeparation = rankSeparation;
            this.nodeSeparation = nodeSeparation;
            this.locale = locale ?? CultureInfo.InvariantCulture;
        }

        public void Write(View view, ViewSet viewSet)
        {
            var fileName = Path.Combine(directory, view.Key + ".dot");
            var viewName = view.GetType().Name;

            Console.WriteLine("Processing " + viewName + ": " + view.Key);
            Console.WriteLine(" - Writing " + fileName);

            using(var stream = new FileStream(fileName, FileMode.Create))
            using(var writer = new StreamWriter(stream))
            {
                this.WriteHeader(writer);

                this.WriteViewElements(view, viewSet, writer);

                this.WriteRelationships(view, writer);

                this.WriteFooter(writer);
            }
        }

        private void WriteViewElements(View view, ViewSet viewSet, StreamWriter writer)
        {
            switch (view)
            {
                case SystemLandscapeView systemLandscapeView:
                    this.WriteSystemLandscapeView(systemLandscapeView, viewSet, writer);
                    break;

                case SystemContextView systemContextView:
                    this.WriteSystemContextView(systemContextView, viewSet, writer);
                    break;

                case ContainerView containerView:
                    this.WriteContainerView(containerView, viewSet, writer);
                    break;

                case ComponentView componentView:
                    this.WriteComponentView(componentView, viewSet, writer);
                    break;

                case DynamicView dynamicView:
                    this.WriteDynamicView(dynamicView, viewSet, writer);
                    break;

                case DeploymentView deploymentView:
                    this.WriteDeploymentView(deploymentView, viewSet, writer);
                    break;
            }
        }

        private void WriteSystemLandscapeView(SystemLandscapeView view, ViewSet viewSet, StreamWriter writer)
        {
            if (view.EnterpriseBoundaryVisible == true)
            {
                writer.Write("  subgraph cluster_enterprise {\n");
                writer.Write("    margin=" + CLUSTER_INTERNAL_MARGIN + "\n");
                foreach (var elementView in view.Elements)
                {
                    if (elementView.Element is Person personElement
                        && personElement.Location == Location.Internal)
                    {
                        this.WriteElement(view, viewSet, "    ", elementView.Element, writer);
                    }
                    if (elementView.Element is SoftwareSystem softwareSystemElement
                        && softwareSystemElement.Location == Location.Internal)
                    {
                        this.WriteElement(view, viewSet,"    ", elementView.Element, writer);
                    }
                }
                writer.Write("  }\n\n");

                foreach (var elementView in view.Elements)
                {
                    if (elementView.Element is Person && ((Person)elementView.Element).Location != Location.Internal) {
                        this.WriteElement(view, viewSet, "  ", elementView.Element, writer);
                    }
                    if (elementView.Element is SoftwareSystem && ((SoftwareSystem)elementView.Element).Location != Location.Internal) {
                        this.WriteElement(view, viewSet, "  ", elementView.Element, writer);
                    }
                }
            } else {
                foreach (var elementView in view.Elements)
                {
                    this.WriteElement(view, viewSet, "  ", elementView.Element, writer);
                }
            }
        }

        private void WriteSystemContextView(SystemContextView view, ViewSet viewSet, StreamWriter writer)
        {
            if (view.EnterpriseBoundaryVisible == true)
            {
                writer.Write("  subgraph cluster_enterprise {\n");
                writer.Write("    margin=" + CLUSTER_INTERNAL_MARGIN + "\n");
                foreach (var elementView in view.Elements)
                {
                    if (elementView.Element is Person personElement
                        && personElement.Location == Location.Internal)
                    {
                        this.WriteElement(view, viewSet, "    ", elementView.Element, writer);
                    }
                    if (elementView.Element is SoftwareSystem softwareSystemElement
                        && softwareSystemElement.Location == Location.Internal)
                    {
                        this.WriteElement(view, viewSet,"    ", elementView.Element, writer);
                    }
                }
                writer.Write("  }\n\n");

                foreach (var elementView in view.Elements)
                {
                    if (elementView.Element is Person && ((Person)elementView.Element).Location != Location.Internal) {
                        this.WriteElement(view, viewSet, "  ", elementView.Element, writer);
                    }
                    if (elementView.Element is SoftwareSystem && ((SoftwareSystem)elementView.Element).Location != Location.Internal) {
                        this.WriteElement(view, viewSet, "  ", elementView.Element, writer);
                    }
                }
            }
            else
            {
                foreach (var elementView in view.Elements)
                {
                    this.WriteElement(view, viewSet, "  ", elementView.Element, writer);
                }
            }
        }

        private void WriteContainerView(ContainerView view, ViewSet viewSet, StreamWriter writer)
        {
            var softwareSystem = view.SoftwareSystem;

            writer.Write($"  subgraph cluster_{softwareSystem.Id} {{\n");
            writer.Write($"    margin={CLUSTER_INTERNAL_MARGIN}\n");

            foreach (var elementView in view.Elements)
            {
                var element = elementView.Element;
                var elementParent = element.Parent;
                if (elementParent?.Equals(softwareSystem) == true)
                {
                    this.WriteElement(view, viewSet, "    ", elementView.Element, writer);
                }
            }

            writer.Write("  }\n");

            foreach (var elementView in view.Elements)
            {
                var element = elementView.Element;
                var elementParent = element.Parent;
                if (elementParent?.Equals(softwareSystem) == false)
                {
                    this.WriteElement(view, viewSet, "  ", elementView.Element, writer);
                }
            }
        }

        private void WriteComponentView(ComponentView view, ViewSet viewSet, StreamWriter writer)
        {
            var container = view.Container;
            writer.Write($"  subgraph cluster_{container.Id} {{\n");
            writer.Write("    margin=" + CLUSTER_INTERNAL_MARGIN + "\n");

            foreach (var elementView in view.Elements)
            {
                if (elementView.Element.Parent.Equals(container))
                {
                    this.WriteElement(view, viewSet, "    ", elementView.Element, writer);
                }
            }

            writer.Write("  }\n");

            foreach (var elementView in view.Elements) {
                if (!elementView.Element.Parent.Equals(container))
                {
                    this.WriteElement(view, viewSet, "  ", elementView.Element, writer);
                }
            }
        }

        private void WriteDynamicView(DynamicView view, ViewSet viewSet, StreamWriter writer)
        {
            var element = view.Element;
            if (element == null)
            {
                foreach (var elementView in view.Elements)
                {
                    this.WriteElement(view, viewSet, "  ", elementView.Element, writer);
                }
            }
            else
            {
                writer.Write($"  subgraph cluster_{element.Id} {{\n");
                writer.Write("    margin=" + CLUSTER_INTERNAL_MARGIN + "\n");
                foreach (var elementView in view.Elements)
                {
                    if (elementView.Element.Parent.Equals(element))
                    {
                        this.WriteElement(view, viewSet, "    ", elementView.Element, writer);
                    }
                }
                writer.Write("  }\n");

                foreach (var elementView in view.Elements)
                {
                    if (!elementView.Element.Parent.Equals(element))
                    {
                        this.WriteElement(view, viewSet, "  ", elementView.Element, writer);
                    }
                }
            }
        }

        private void WriteDeploymentView(DeploymentView view, ViewSet viewSet, StreamWriter writer)
        {
            foreach (var elementView in view.Elements)
            {
                if (elementView.Element is DeploymentNode deploymentNode
                    && elementView.Element.Parent == null)
                {
                    this.Write(view, viewSet, (DeploymentNode)elementView.Element, writer, "");
                }
            }
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

        private void WriteHeader(StreamWriter writer)
        {
            writer.Write("digraph {");
            writer.Write("\n");
            writer.Write(string.Format("  graph [splines=polyline,rankdir={0},ranksep={1},nodesep={2},fontsize=5]", RankDirectionUtil.GetCode(rankDirection), rankSeparation, nodeSeparation));
            writer.Write("\n");
            writer.Write("  node [shape=box,fontsize=5]");
            writer.Write("\n");
            writer.Write("  edge []");
            writer.Write("\n");
            writer.Write("\n");
        }

        private void WriteFooter(StreamWriter writer)
        {
            writer.Write("}");
        }

        private void WriteElement(View view, ViewSet viewSet, String padding, Element element, StreamWriter writer)
        {
            writer.Write("{0}{1} [width={2},height={3},fixedsize=true,id={4},label=\"{5}: {6}\"]",
                padding,
                element.Id,
                GetElementWidth(view, viewSet, element.Id) / Constants.StructurizrDpi, // convert Structurizr dimensions to inches
                GetElementHeight(view, viewSet, element.Id) / Constants.StructurizrDpi, // convert Structurizr dimensions to inches
                element.Id,
                element.Id,
                element.Name
            );
            writer.Write("\n");
        }

        private void WriteRelationships(View view, StreamWriter writer) {
            writer.Write("\n");

            foreach(var relationshipView in view.Relationships)
            {
                var relationShip = relationshipView.Relationship;
                if (relationShip.Source is DeploymentNode
                    || relationShip.Destination is DeploymentNode)
                {
                    // todo: relationships to/from deployment nodes (graphviz clusters)
                    continue;
                }

                writer.Write(string.Format("  {0} -> {1} [id={2}]",
                    relationShip.SourceId,
                    relationShip.DestinationId,
                    relationshipView.Id
                ));
                writer.Write("\n");
            }
        }

        private void Write(DeploymentView view, ViewSet viewSet, DeploymentNode deploymentNode, StreamWriter writer, string indent)
        {
            writer.Write($"{indent}subgraph cluster_{deploymentNode.Id} {{\n");
            writer.Write($"{indent}  margin={CLUSTER_INTERNAL_MARGIN}\n");
            writer.Write($"{indent}  label=\"{deploymentNode.Id}: {deploymentNode.Name}\"\n");

            foreach (var child in deploymentNode.Children)
            {
                if (view.IsElementInView(child))
                {
                    this.Write(view, viewSet, child, writer, indent + "  ");
                }
            }

            foreach (var containerInstance in deploymentNode.ContainerInstances)
            {
                if (view.IsElementInView(containerInstance))
                {
                    this.WriteElement(view, viewSet, indent + "  ", containerInstance, writer);
                }
            }

            writer.Write(indent + "}\n");
        }
    }
}

