# Viewpoints and Perspectives documentation template

Structurizr for .NET includes an implementation of the [Viewpoints and Perspectives documentation template](http://www.viewpoints-and-perspectives.info), which can be used to document your software architecture.

## Example

To use this template, create an instance of the [ViewpointsAndPerspectivesDocumentationTemplate](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Core/Documentation/ViewpointsAndPerspectivesDocumentationTemplate.cs) class.
You can then add documentation sections as needed, each associated with a software system in your software architecture model, using Markdown or AsciiDoc. For example:

```c#
ViewpointsAndPerspectivesDocumentationTemplate template = new ViewpointsAndPerspectivesDocumentationTemplate(workspace);

DirectoryInfo documentationRoot = new DirectoryInfo("Documentation" + Path.DirectorySeparatorChar + "viewpointsandperspectives" + Path.DirectorySeparatorChar + "markdown");
template.AddIntroductionSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "01-introduction.md")));
template.AddGlossarySection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "02-glossary.md")));
template.AddSystemStakeholdersAndRequirementsSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "03-system-stakeholders-and-requirements.md")));
template.AddArchitecturalForcesSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "04-architectural-forces.md")));
template.AddArchitecturalViewsSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "05-architectural-views")));
template.AddSystemQualitiesSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "06-system-qualities.md")));
template.AddAppendicesSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "07-appendices.md")));
```

Structurizr will create navigation controls based upon the the sections in the documentation, and the software systems they have been associated with.
See [ViewpointsAndPerspectivesDocumentationExample.cs](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Examples/ViewpointsAndPerspectivesDocumentationExample.cs) for the full code, and [https://structurizr.com/share/36371/documentation](https://structurizr.com/share/36371/documentation) to see the rendered documentation.

## More information

See [Help - Documentation](https://structurizr.com/help/documentation) for more information about how headings are rendered, and how to embed diagrams from you workspace into the documentation.