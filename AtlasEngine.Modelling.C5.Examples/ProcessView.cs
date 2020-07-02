namespace AtlasEngine.Modelling.C5.Examples
{
    using System.IO;

    using AtlasEngine.Modelling.C5.GraphViz;

    public sealed class ProcessView
    {
        private const string EXPORT_FILE_PATH = "./data.json";

        public static void Main()
        {
            var workspace = new Workspace("5Minds", "This is my Workspace!");
            var model = workspace.Model;

            var customer = model.AddPerson(Location.Unspecified, "Kunde", "Dieser Kunde kauft im Shop ein.");
            var webShop = model.AddSoftwareSystem(Location.Internal, "5Minds Webshop", "Das ist unser toller WebShop");
            var apiShop = model.AddSoftwareSystem(Location.Internal, "5Minds Webshop Api", "Das ist unsere tolle API");
            var dataBase = model.AddSoftwareSystem(Location.Internal, "Database", "postgress01");
            var deploymentNode = model.AddDeploymentNode("mainNode", "Meine Test Node", "master_node_1");

            customer.Uses(webShop, "kauft ein");
            webShop.Uses(apiShop, "ruft auf");
            apiShop.Uses(dataBase, "ruft auf");

            var views = workspace.Views;
            var systemLandscapeView = views.CreateSystemLandscapeView( "System_Land_Scape", "Eine komplette Übersicht über alle Systeme.");
            systemLandscapeView.AddAllElements();

            var containerView = views.CreateContainerView(webShop, "Container_View", "Eine Übersicht über die Shop API");
            containerView.Add(apiShop);
            containerView.Add(dataBase);

            var deploymentView = views.CreateDeploymentView(webShop, "Deployment_View", "Test Deployment View");
            deploymentView.Add(deploymentNode);

            var systemContextView = views.CreateSystemContextView(webShop, "SystemContext_View", "Test SystemContext View");
            systemContextView.Add(apiShop);
            systemContextView.Add(dataBase);

            var styles = views.Configuration.Styles;
            styles.Add(new ElementStyle(Tags.Element) { Background = "#CCCCCC", Color = "#000000", Width = 600, Height = 300});

            var layout = new GraphvizAutomaticLayout("./");
            layout.Apply(workspace);

            CleanUp();
            workspace.ExportToFile(EXPORT_FILE_PATH);
        }

        private static void CleanUp()
        {
            if (File.Exists(EXPORT_FILE_PATH))
            {
                File.Delete(EXPORT_FILE_PATH);
            }
        }
    }
}
