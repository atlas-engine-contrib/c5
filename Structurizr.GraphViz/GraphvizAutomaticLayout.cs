namespace AtlasEngine.Modelling.C5.GraphViz
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using Structurizr;

    using SystemProcess = System.Diagnostics.Process;

    public class GraphvizAutomaticLayout
    {
        private readonly string _outputDirectory;
        private readonly double _rankSeparation;
        private readonly double _nodeSeparation;
        private readonly int _margin;
        private readonly bool _changePaperSize;
        private readonly RankDirection _rankDirection;

        public GraphvizAutomaticLayout(
            string outputDirectory,
            RankDirection rankDirection = RankDirection.TopBottom,
            double rankSeparation = 1.0d,
            double nodeSeparation = 1.0d,
            int margin = 10,
            bool changePaperSize = true)
        {
            this._outputDirectory = outputDirectory;
            this._margin = margin;
            this._nodeSeparation = nodeSeparation;
            this._rankDirection = rankDirection;
            this._rankSeparation = rankSeparation;
            this._changePaperSize = changePaperSize;
        }

        /// <summary>
        /// Applies this auto layout to the given workspace.
        /// </summary>
        /// <param name="workspace">This work space will be auto layouted. The elementViews should contain x and y coordinates after layouting.</param>
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

        private DotFileWriter CreateDotFileWriter()
        {
            var dotFileWriter = new DotFileWriter(
                _outputDirectory,
                this._rankDirection,
                this._rankSeparation,
                this._nodeSeparation);

            return dotFileWriter;
        }

        private SVGReader CreateSvgReader()
        {
            return new SVGReader(this._outputDirectory, this._margin, this._changePaperSize);
        }

        private void RunGraphviz(View view)
        {
            Console.WriteLine(" - Running graphviz");

            var startInfo = new ProcessStartInfo("dot");
            startInfo.Arguments = Path.Combine(this._outputDirectory, view.Key + ".dot") + " -Tsvg -O";

            var process = new SystemProcess();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private void Apply(View view, ViewSet viewSet)
        {
            this.CreateDotFileWriter().Write(view, viewSet);
            this.RunGraphviz(view);
            this.CreateSvgReader().ParseAndApplyLayout(view, viewSet);
        }
    }
}
