namespace Structurizr.GraphViz
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public class DotFileWriter
    {
        private const int CLUSTER_INTERNAL_MARGIN = 25;

        private File path;
        private RankDirection rankDirection;
        private double rankSeparation;
        private double nodeSeparation;

        public DotFileWriter(File path, RankDirection rankDirection, double rankSeparation, double nodeSeparation)
        {
            this.path = path;
            this.rankDirection = rankDirection;
            this.rankSeparation = rankSeparation;
            this.nodeSeparation = nodeSeparation;
        }

        public CultureInfo locale { get; set; } = CultureInfo.InvariantCulture;

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

        private void Write(View view, ViewSet viewSet, bool isEnterpriseBoundaryVisible)
        {
            var directory = "$HOME";
            var fileName = Path.Combine(directory, view.Key + ".dot");
            Console.WriteLine("Processing system landscape view: " + view.Key);
            Console.WriteLine(" - Writing " + fileName);

            using(var stream = new FileStream(fileName, FileMode.Create))
            using(var writer = new StreamWriter(stream))
            {
                this.WriteHeader(writer);

                if (isEnterpriseBoundaryVisible)
                {
                    writer.Write("  subgraph cluster_enterprise {\n");
                    writer.Write("    margin=" + CLUSTER_INTERNAL_MARGIN + "\n");
                    foreach (var elementView in view.Elements)
                    {
                        if (elementView.Element is Person personElement
                            && personElement.Location == Location.Internal)
                        {
                            this.WriteElement(view, viewSet, "    ", elementView.Element, fileWriter);
                        }
                        if (elementView.Element is SoftwareSystem softwareSystemElement
                            && softwareSystemElement.Location == Location.Internal)
                        {
                            this.WriteElement(view, viewSet,"    ", elementView.Element, fileWriter);
                        }
                    }
                    fileWriter.write("  }\n\n");

                    foreach (var elementView in view.Elements)
                    {
                        if (elementView.Element is Person && ((Person)elementView.Element).Location != Location.Internal) {
                            this.WriteElement(view, viewSet, "  ", elementView.Element, fileWriter);
                        }
                        if (elementView.Element is SoftwareSystem && ((SoftwareSystem)elementView.Element).Location != Location.Internal) {
                            this.WriteElement(view, viewSet, "  ", elementView.Element, fileWriter);
                        }
                    }
                } else {
                    foreach (var elementView in view.Elements)
                    {
                        this.WriteElement(view, viewSet, "  ", elementView.Element, fileWriter);
                    }
                }

                this.WriteRelationships(view, fileWriter);
                this.WriteFooter(fileWriter);
            }
        }

    void write(SystemContextView view) {
        File file = new File(path, view.getKey() + ".dot");
        System.out.println("Processing system context view: " + view.getKey());
        System.out.println(" - Writing " + file.getAbsolutePath());

        FileWriter fileWriter = new FileWriter(file);
        writeHeader(fileWriter);

        if (view.isEnterpriseBoundaryVisible()) {
            fileWriter.write("  subgraph cluster_enterprise {\n");
            fileWriter.write("    margin=" + CLUSTER_INTERNAL_MARGIN + "\n");
            for (var elementView in view.Elements) {
                if (elementView.Element is Person && ((Person)elementView.Element).Location == Location.Internal) {
                    this.WriteElement(view, viewSet, "    ", elementView.Element, fileWriter);
                }
                if (elementView.Element is SoftwareSystem && ((SoftwareSystem)elementView.Element).Location == Location.Internal) {
                    this.WriteElement(view, viewSet, "    ", elementView.Element, fileWriter);
                }
            }
            fileWriter.write("  }\n\n");

            for (var elementView in view.Elements) {
                if (elementView.Element is Person && ((Person)elementView.Element).Location != Location.Internal) {
                    this.WriteElement(view, viewSet, "  ", elementView.Element, fileWriter);
                }
                if (elementView.Element is SoftwareSystem && ((SoftwareSystem)elementView.Element).Location != Location.Internal) {
                    this.WriteElement(view, viewSet, "  ", elementView.Element, fileWriter);
                }
            }
        } else {
            for (var elementView in view.Elements) {
                this.WriteElement(view, viewSet, "  ", elementView.Element, fileWriter);
            }
        }

        writeRelationships(view, fileWriter);

        writeFooter(fileWriter);
        fileWriter.close();
    }

    void write(ContainerView view) {
        File file = new File(path, view.getKey() + ".dot");
        System.out.println("Processing container view: " + view.getKey());
        System.out.println(" - Writing " + file.getAbsolutePath());

        FileWriter fileWriter = new FileWriter(file);
        writeHeader(fileWriter);

        SoftwareSystem softwareSystem = view.getSoftwareSystem();

        fileWriter.write(String.format(locale, "  subgraph cluster_%s {\n", softwareSystem.getId()));
        fileWriter.write("    margin=" + CLUSTER_INTERNAL_MARGIN + "\n");
        for (var elementView in view.Elements) {
            if (elementView.Element.getParent() == softwareSystem) {
                this.WriteElement(view, viewSet, "    ", elementView.Element, fileWriter);
            }
        }
        fileWriter.write("  }\n");

        for (var elementView in view.Elements) {
            if (elementView.Element.getParent() != softwareSystem) {
                this.WriteElement(view, viewSet, "  ", elementView.Element, fileWriter);
            }
        }

        writeRelationships(view, fileWriter);

        writeFooter(fileWriter);
        fileWriter.close();
    }

    void write(ComponentView view) {
        File file = new File(path, view.getKey() + ".dot");
        System.out.println("Processing component view: " + view.getKey());
        System.out.println(" - Writing " + file.getAbsolutePath());

        FileWriter fileWriter = new FileWriter(file);
        writeHeader(fileWriter);

        Container container = view.getContainer();

        fileWriter.write(String.format(locale, "  subgraph cluster_%s {\n", container.getId()));
        fileWriter.write("    margin=" + CLUSTER_INTERNAL_MARGIN + "\n");
        for (var elementView in view.Elements) {
            if (elementView.Element.getParent() == container) {
                this.WriteElement(view, viewSet, "    ", elementView.Element, fileWriter);
            }
        }
        fileWriter.write("  }\n");

        for (var elementView in view.Elements) {
            if (elementView.Element.getParent() != container) {
                this.WriteElement(view, viewSet, "  ", elementView.Element, fileWriter);
            }
        }

        writeRelationships(view, fileWriter);

        writeFooter(fileWriter);
        fileWriter.close();
    }

    void write(DynamicView view) {
        File file = new File(path, view.getKey() + ".dot");
        System.out.println("Processing dynamic view: " + view.getKey());
        System.out.println(" - Writing " + file.getAbsolutePath());

        FileWriter fileWriter = new FileWriter(file);
        writeHeader(fileWriter);

        Element element = view.Element;

        if (element == null) {
            for (var elementView in view.Elements) {
                this.WriteElement(view, viewSet, "  ", elementView.Element, fileWriter);
            }
        } else {
            fileWriter.write(String.format(locale, "  subgraph cluster_%s {\n", element.getId()));
            fileWriter.write("    margin=" + CLUSTER_INTERNAL_MARGIN + "\n");
            for (var elementView in view.Elements) {
                if (elementView.Element.getParent() == element) {
                    this.WriteElement(view, viewSet, "    ", elementView.Element, fileWriter);
                }
            }
            fileWriter.write("  }\n");

            for (var elementView in view.Elements) {
                if (elementView.Element.getParent() != element) {
                    this.WriteElement(view, viewSet, "  ", elementView.Element, fileWriter);
                }
            }
        }

        writeRelationships(view, fileWriter);

        writeFooter(fileWriter);
        fileWriter.close();
    }

     void write(DeploymentView view) throws Exception {
        File file = new File(path, view.getKey() + ".dot");
        System.out.println("Processing deployment view: " + view.getKey());
        System.out.println(" - Writing " + file.getAbsolutePath());

        FileWriter fileWriter = new FileWriter(file);
        writeHeader(fileWriter);

        for (var elementView in view.Elements) {
            if (elementView.Element is DeploymentNode && elementView.Element.getParent() == null) {
                write(view, (DeploymentNode)elementView.Element, fileWriter, "");
            }
        }

        writeRelationships(view, fileWriter);

        writeFooter(fileWriter);
        fileWriter.close();
    }

    private void write(DeploymentView view, DeploymentNode deploymentNode, FileWriter fileWriter, String indent) {
        fileWriter.write(String.format(locale, indent + "subgraph cluster_%s {\n", deploymentNode.getId()));
        fileWriter.write(indent + "  margin=" + CLUSTER_INTERNAL_MARGIN + "\n");
        fileWriter.write(String.format(locale, indent + "  label=\"%s: %s\"\n", deploymentNode.getId(), deploymentNode.getName()));

        for (DeploymentNode child : deploymentNode.getChildren()) {
            if (view.isElementInView(child)) {
                write(view, child, fileWriter, indent + "  ");

            }
        }

        for (InfrastructureNode infrastructureNode : deploymentNode.getInfrastructureNodes()) {
            if (view.isElementInView(infrastructureNode)) {
                this.WriteElement(view, viewSet, indent + "  ", infrastructureNode, fileWriter);
            }
        }

        for (ContainerInstance containerInstance : deploymentNode.getContainerInstances()) {
            if (view.isElementInView(containerInstance)) {
                this.WriteElement(view, viewSet, indent + "  ", containerInstance, fileWriter);
            }
        }

        fileWriter.write(indent + "}\n");
    }
}
}

