namespace Structurizr.GraphViz
{
    public static class Constants
    {
        /// <summary>
        /// diagrams created by the Structurizr cloud service/on-premises installation are sized for 300dpi.
        /// </summary>
        public const double StructurizrDpi = 300.0;

        /// <summary>
        /// graphviz uses 72dpi by default
        /// </summary>
        public const double GraphVizDpi = 72.0;

        /// <summary>
        /// this is needed to convert coordinates provided by graphviz, to those used by Structurizr
        /// </summary>
        public const double DpiRatio = StructurizrDpi / GraphVizDpi;
    }
}
