namespace Structurizr.GraphViz
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    public class GraphvizAutomaticLayout
    {
        private string outputDirectory;

        private CultureInfo locale;

        public GraphvizAutomaticLayout(string outputDirectory)
        {
            this.outputDirectory = outputDirectory;
        }

        public RankDirection RankDirection { get; set; } = RankDirection.TopBottom;

        public double RankSeparation { get; set; } = 1.0d;

        public double NodeSeparation { get; set; } = 1.0d;

        public int Margin { get; set; } = 10;

        public bool ChangePaperSize { get; set; } = true;

        private DotFileWriter CreateDotFileWriter()
        {
            var dotFileWriter = new DotFileWriter(
                outputDirectory,
                this.RankDirection,
                this.RankSeparation,
                this.NodeSeparation,
                this.locale);

            return dotFileWriter;
        }

        private SVGReader CreateSVGReader()
        {
            return new SVGReader(this.outputDirectory, this.Margin, this.ChangePaperSize);
        }

        private void RunGraphviz(View view)
        {
            Console.WriteLine(" - Running graphviz");

            var startInfo = new ProcessStartInfo("dot");
            startInfo.Arguments = Path.Combine(this.outputDirectory, view.Key + ".dot") + " -Tsvg -O";

            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        public void Apply(View view, ViewSet viewSet)
        {
            this.CreateDotFileWriter().Write(view, viewSet);
            this.RunGraphviz(view);
            this.CreateSVGReader().ParseAndApplyLayout(view, viewSet);
        }

        public void Apply(Workspace workspace)
        {
            var viewSet = workspace.Views;

            foreach (SystemLandscapeView view in workspace.Views.SystemLandscapeViews)
            {
                this.Apply(view, viewSet);
            }

            foreach (SystemContextView view in workspace.Views.SystemContextViews)
            {
                this.Apply(view, viewSet);
            }

            foreach (ContainerView view in workspace.Views.ContainerViews)
            {
                this.Apply(view, viewSet);
            }

            foreach (ComponentView view in workspace.Views.ComponentViews)
            {
                this.Apply(view, viewSet);
            }

            foreach (DynamicView view in workspace.Views.DynamicViews)
            {
                this.Apply(view, viewSet);
            }

            foreach (DeploymentView view in workspace.Views.DeploymentViews)
            {
                this.Apply(view, viewSet);
            }
        }
    }
}
