# Container diagram

Once you understand how your system fits in to the overall IT environment, a really useful next step is to illustrate the high-level technology choices with a Container diagram. A "container" is something like a web application, desktop application, mobile app, database, file system, etc. Essentially, a container is a separately deployable unit that executes code or stores data.

The Container diagram shows the high-level shape of the software architecture and how responsibilities are distributed across it. It also shows the major technology choices and how the containers communicate with one another. It's a simple, high-level technology focussed diagram that is useful for software developers and support/operations staff alike.

## Example

As an example, a Container diagram for a simplified, fictional Internet Banking System might look something like this. In summary, it shows that the Internet Banking System is made up a Web Application and a Database. It also shows the relationship between the Web Application and the Mainframe Banking System.

![An example Container diagram](images/container-diagram-1.png)

With Structurizr for .NET, you can create this diagram with code like the following:

```c#
Container webApplication = internetBankingSystem.AddContainer("Web Application", "Provides all of the Internet banking functionality to customers.", "Java and Spring MVC");
Container database = internetBankingSystem.AddContainer("Database", "Stores interesting data.", "Relational Database Schema");

customer.Uses(webApplication, "HTTPS");
webApplication.Uses(database, "Reads from and writes to", "JDBC");
webApplication.Uses(mainframeBankingSystem, "Uses", "XML/HTTPS");

ContainerView containerView = views.CreateContainerView(internetBankingSystem, "Containers", "The container diagram for the Internet Banking System.");
containerView.Add(customer);
containerView.AddAllContainers();
containerView.Add(mainframeBankingSystem);
```

See [BigBankPlc.cs](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Examples/BigBankPlc.cs) for the full code, and [https://structurizr.com/share/36141#Containers](https://structurizr.com/share/36141#Containers) for the diagram.