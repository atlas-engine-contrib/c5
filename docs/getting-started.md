# Getting started

Here is a quick overview of how to get started with Structurizr for .NET so that you can create a software architecture model as code. You can find the code at [GettingStarted.cs](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Examples/GettingStarted.cs) and the live example workspace at [https://structurizr.com/share/25441](https://structurizr.com/share/25441).

For more examples, please see [AtlasEngine.Modelling.C5.Examples](https://github.com/structurizr/dotnet/tree/master/AtlasEngine.Modelling.C5.Examples).

## 1. Dependencies

The "Structurizr for .NET" assemblies are available on NuGet as follows:

Name                    | Description
---------------------   | ---------------------------------------------------------------------------------------------------------------------------
AtlasEngine.Modelling.C5.Core        | The basic library that can used to create software architecture models.
AtlasEngine.Modelling.C5.Client		| The API client for publishing models on the Structurizr cloud service and on-premises installation.

To install AtlasEngine.Modelling.C5.Client, use the following command in the NuGet Package Manager Console:

```powershell
Install-Package AtlasEngine.Modelling.C5.Client
```

## 2. Create a model

The first step is to create a workspace in which the software architecture model will reside.

```c#
Workspace workspace = new Workspace("Getting Started", "This is a model of my software system.");
Model model = workspace.Model;
```

Now let's add some elements to the model to describe a user using a software system.

```c#
Person user = model.AddPerson("User", "A user of my software system.");
SoftwareSystem softwareSystem = model.AddSoftwareSystem("Software System", "My software system.");
user.Uses(softwareSystem, "Uses");
```

## 3. Create some views

With the model created, we need to create some views with which to visualise it.

```c#
ViewSet viewSet = workspace.Views;
SystemContextView contextView = viewSet.CreateSystemContextView(softwareSystem, "SystemContext", "An example of a System Context diagram.");
contextView.AddAllSoftwareSystems();
contextView.AddAllPeople();
```

## 4. Add some colour

Elements and relationships can be styled by specifying colours, sizes and shapes.

```c#
Styles styles = viewSet.Configuration.Styles;
styles.Add(new ElementStyle(Tags.SoftwareSystem) { Background = "#1168bd", Color = "#ffffff" });
styles.Add(new ElementStyle(Tags.Person) { Background = "#08427b", Color = "#ffffff", Shape = Shape.Person });
```

## 5. Upload to Structurizr

Structurizr provides a web API to get and put workspaces.

```c#
StructurizrClient structurizrClient = new StructurizrClient("key", "secret");
structurizrClient.PutWorkspace(25441, workspace);
```

> In order to upload your model to Structurizr using the web API, you'll need to [sign up for free](https://structurizr.com/signup) to get your own API key and secret. See [Workspaces](https://structurizr.com/help/workspaces) for information about finding your workspace ID, API key and secret.

## 6. Open the workspace in Structurizr

Once you've run your program to create and upload the workspace, you can now sign in to your Structurizr account, and open the workspace from [your dashboard](https://structurizr.com/dashboard). The result should be a diagram like this:

![Getting Started with Structurizr for .NET](images/getting-started-1.png)

By default, Structurizr does not auto-layout your diagram elements. The diagram layout can be modified by dragging the elements around the diagram canvas in the diagram editor, and the layout saved using the "Save workspace" button. See [Structurizr - Help - Diagram editor](https://structurizr.com/help/diagram-editor) for more information. 

![Getting Started with Structurizr for .NET](images/getting-started-2.png)

A diagram key is automatically generated based upon the styles in the model. Click the "i" button on the toolbar (or press the 'i' key) to display the diagram key.

![A diagram key](images/getting-started-diagram-key.png)

When you upload a new version of the same workspace, the Structurizr client will try to retain the diagram layout information. See [API client](api-client.md) for more details.