# Structurizr documentation template

Structurizr for .NET includes an implementation of the "software guidebook" from Simon Brown's [Software Architecture for Developers](https://leanpub.com/visualising-software-architecture) book, which can be used to document your software architecture.

## Example

To use this template, create an instance of the [StructurizrDocumentationTemplate](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Core/Documentation/StructurizrDocumentationTemplate.cs) class.
You can then add documentation sections as needed, each associated with a software system in your software architecture model, using Markdown or AsciiDoc. For example:

```c#
StructurizrDocumentationTemplate template = new StructurizrDocumentationTemplate(workspace);

DirectoryInfo documentationRoot = new DirectoryInfo("Documentation" + Path.DirectorySeparatorChar + "structurizr" + Path.DirectorySeparatorChar + "markdown");
template.AddContextSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "01-context.md")));
template.AddFunctionalOverviewSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "02-functional-overview.md")));
template.AddQualityAttributesSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "03-quality-attributes.md")));
template.AddConstraintsSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "04-constraints.md")));
template.AddPrinciplesSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "05-principles.md")));
template.AddSoftwareArchitectureSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "06-software-architecture.md")));
template.AddDataSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "07-data.md")));
template.AddInfrastructureArchitectureSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "08-infrastructure-architecture.md")));
template.AddDeploymentSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "09-deployment.md")));
template.AddDevelopmentEnvironmentSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "10-development-environment.md")));
template.AddOperationAndSupportSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "11-operation-and-support.md")));
template.AddDecisionLogSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "12-decision-log.md")));
```

Structurizr will create navigation controls based upon the the sections in the documentation, and the software systems they have been associated with.
See [StructurizrDocumentationExample.cs](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Examples/StructurizrDocumentationExample.cs) for the full code, and [https://structurizr.com/share/14181/documentation](https://structurizr.com/share/14181/documentation) to see the rendered documentation.

## More information

See [Help - Documentation](https://structurizr.com/help/documentation) for more information about how headings are rendered, and how to embed diagrams from you workspace into the documentation.