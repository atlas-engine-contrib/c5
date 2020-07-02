# arc42 documentation template

Structurizr for .NET includes an implementation of the [arc42 documentation template](http://arc42.org), which can be used to document your software architecture.

## Example

To use this template, create an instance of the [Arc42DocumentationTemplate](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Core/Documentation/Arc42DocumentationTemplate.cs) class.
You can then add documentation sections as needed, each associated with a software system in your software architecture model, using Markdown or AsciiDoc. For example:

```c#
Arc42DocumentationTemplate template = new Arc42DocumentationTemplate(workspace);

DirectoryInfo documentationRoot = new DirectoryInfo("Documentation" + Path.DirectorySeparatorChar + "arc42" + Path.DirectorySeparatorChar + "markdown");
template.AddIntroductionAndGoalsSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "01-introduction-and-goals.md")));
template.AddConstraintsSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "02-architecture-constraints.md")));
template.AddContextAndScopeSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "03-system-scope-and-context.md")));
template.AddSolutionStrategySection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "04-solution-strategy.md")));
template.AddBuildingBlockViewSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "05-building-block-view.md")));
template.AddRuntimeViewSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "06-runtime-view.md")));
template.AddDeploymentViewSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "07-deployment-view.md")));
template.AddCrosscuttingConceptsSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "08-crosscutting-concepts.md")));
template.AddArchitecturalDecisionsSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "09-architecture-decisions.md")));
template.AddRisksAndTechnicalDebtSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "10-quality-requirements.md")));
template.AddQualityRequirementsSection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "11-risks-and-technical-debt.md")));
template.AddGlossarySection(softwareSystem, new FileInfoPath.Combine(documentationRoot.FullName, "12-glossary.md")));
```

Structurizr will create navigation controls based upon the the sections in the documentation, and the software systems they have been associated with.
See [Arc42DocumentationExample.cs](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Examples/Arc42DocumentationExample.cs) for the full code, and [https://structurizr.com/share/27791/documentation](https://structurizr.com/share/27791/documentation) to see the rendered documentation.

## More information

See [Help - Documentation](https://structurizr.com/help/documentation) for more information about how headings are rendered, and how to embed diagrams from you workspace into the documentation.