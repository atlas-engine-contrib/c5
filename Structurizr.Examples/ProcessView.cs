namespace Structurizr.Examples
{
    using System.IO;

    using Structurizr.GraphViz;
    using Structurizr.IO.Json;
    using Structurizr.Util;

    public sealed class ProcessView
    {
        public static void Main()
        {
            var workspace = new Workspace("5Minds", "This is my Workspace!");
            var model = workspace.Model;

            var customer = model.AddPerson(Location.Unspecified, "Kunde", "Dieser Kunde kauft im Shop ein.");
            var webShop = model.AddSoftwareSystem(Location.Internal, "5Minds Webshop", "Das ist unser toller WebShop");

            customer.Uses(webShop, "kauft ein");

            var views = workspace.Views;
            var view = views.CreateSystemLandscapeView( "FullView", "Eine komplette Übersicht über alle Systeme.");
            view.AddAllElements();

            var styles = views.Configuration.Styles;
            styles.Add(new ElementStyle(Tags.Element) { Background = "#CCCCCC", Color = "#000000", Width = 600, Height = 300});

            var workspaceAsJson = "";

            var layout = new GraphvizAutomaticLayout("./");
            layout.Apply(workspace);

            using (var stringWriter = new StringWriter())
            {
                var jsonWriter = new JsonWriter(false);
                jsonWriter.Write(workspace, stringWriter);

                stringWriter.Flush();
                workspaceAsJson = stringWriter.ToString();
                System.Console.WriteLine(workspaceAsJson);
            }
        }
    }
}
