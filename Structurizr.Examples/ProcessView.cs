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

            var processParent = model.AddProcess("TestProcessParent", "Das ist unsere 5Minds Test");
            var processChild = model.AddProcess("TestProcessChild", "Das ist unsere 5Minds Test");

            var views = workspace.Views;
            var processView = views.CreateProcessView( "Processes", "An example of a System Context diagram.");
            processView.AddAllElements();

            var styles = views.Configuration.Styles;
            styles.Add(new ElementStyle(Tags.Process) { Shape = Shape.Process });

            var branding = views.Configuration.Branding;
            branding.Logo = ImageUtils.GetImageAsDataUri(new FileInfo("structurizr-logo.png"));

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
