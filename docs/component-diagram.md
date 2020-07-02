# Component diagram

Following on from a Container Diagram, next you can zoom in and decompose each container further to identify the major structural building blocks and their interactions.

The Component diagram shows how a container is made up of a number of components, what each of those components are, their responsibilities and the technology/implementation details.

## Example

As an example, a Component diagram for the Web Application of a simplified, fictional Internet Banking System might look something like this. In summary, it shows the components that make up the Web Application, and the relationships between them.

![An example Component diagram](images/component-diagram-1.png)

With Structurizr for .NET, you can create this diagram with code like the following:

```c#
Component homePageController = webApplication.AddComponent("Home Page Controller", "Serves up the home page.", "Spring MVC Controller");
Component signinController = webApplication.AddComponent("Sign In Controller", "Allows users to sign in to the Internet Banking System.", "Spring MVC Controller");
Component accountsSummaryController = webApplication.AddComponent("Accounts Summary Controller", "Provides customers with an summary of their bank accounts.", "Spring MVC Controller");
Component securityComponent = webApplication.AddComponent("Security Component", "Provides functionality related to signing in, changing passwords, etc.", "Spring Bean");
Component mainframeBankingSystemFacade = webApplication.AddComponent("Mainframe Banking System Facade", "A facade onto the mainframe banking system.", "Spring Bean");

webApplication.Components.Where(c => "Spring MVC Controller".Equals(c.Technology)).ToList().ForEach(c => customer.Uses(c, "Uses", "HTTPS"));
signinController.Uses(securityComponent, "Uses");
accountsSummaryController.Uses(mainframeBankingSystemFacade, "Uses");
securityComponent.Uses(database, "Reads from and writes to", "JDBC");
mainframeBankingSystemFacade.Uses(mainframeBankingSystem, "Uses", "XML/HTTPS");

ComponentView componentView = views.CreateComponentView(webApplication, "Components", "The components diagram for the Web Application");
componentView.AddAllContainers();
componentView.AddAllComponents();
componentView.Add(customer);
componentView.Add(mainframeBankingSystem);
```

See [BigBankPlc.cs](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Examples/BigBankPlc.cs) for the full code, and [https://structurizr.com/share/36141#Components](https://structurizr.com/share/36141#Components) for the diagram.

### Extracting components automatically

Please note that, in a real-world scenario, you would probably want to extract components automatically from a codebase with the "component finder", using static analysis and reflection techniques. 