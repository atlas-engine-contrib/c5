namespace Structurizr.Examples
{
    using System.IO;

    using Structurizr.IO.Json;
    using Structurizr.Util;

    public sealed class ProcessView
    {
        public static void Main()
        {
            var workspace = new Workspace("Corporate Branding", "This is a model of my software system.");
            var model = workspace.Model;

            var processParent = model.AddProcess("TestProcessParent", "Das ist ein 5Minds Test Process");
            var processChild = model.AddProcess("TestProcessChild", "Das ist ein 5Minds Test Process");

            var system =
                model.AddSoftwareSystem(Location.Internal, "Email-FremdSystem", "Das ist ein Test FremdSystem.s");

            processParent.Uses(system, "Sendet Daten an das FremdSystem");

            processParent.Uses(processChild, "Uses");

            var eventElementChild = model.AddEvent("TestEvent", "Das ist ein 5Minds Test Event");

            var views = workspace.Views;
            var view = views.CreateSystemLandscapeView( "Processes", "An example of a System Context diagram.");
            view.AddAllElements();

            var workspaceAsJson = "";

            using (StringWriter stringWriter = new StringWriter())
            {
                JsonWriter jsonWriter = new JsonWriter(false);
                jsonWriter.Write(workspace, stringWriter);

                stringWriter.Flush();
                workspaceAsJson = stringWriter.ToString();
                System.Console.WriteLine(workspaceAsJson);
            }
        }
    }
}
