namespace Structurizr.Core.View
{
    public class ProcessView : StaticView
    {
        public ProcessView(Model model, string key, string description) : base(null, key, description)
        {
            this._processModel = model;
        }

        private Model _processModel;

        public override Model Model
        {
            get => this._processModel;
            set { }
        }

        public override string Name => " - Processes";

        public override void AddAllElements()
        {
            AddAllSoftwareSystems();
            AddAllPeople();
            AddAllProcesses();
        }

        /// <summary>
        /// Adds people, software systems and containers that are directly related to the given element.
        /// </summary>
        public override void AddNearestNeighbours(Element element)
        {
            AddNearestNeighbours(element, typeof(Person));
            AddNearestNeighbours(element, typeof(SoftwareSystem));
            AddNearestNeighbours(element, typeof(Container));
            AddNearestNeighbours(element, typeof(Process));
        }
    }
}
