# Deployment diagram

A Deployment diagram allows you to illustrate how containers in the static model are mapped to infrastructure at deployment time. It's based upon the [UML deployment diagram](https://en.wikipedia.org/wiki/Deployment_diagram).

> Note: this page describes a feature that is not available to use with Structurizr's Free Plan. You can, however, render Deployment diagrams using the [PlantUMLWriter](plantuml.md).

## Example

As an example, a Deployment diagram for the live environment of a simplified, fictional Internet Banking System might look something like this. In summary, it shows the deployment of the Web Application and the Database, with a secondary Database being used for failover purposes.

![An example Deployment diagram](images/deployment-diagram-1.png)

With Structurizr for .NET, you can create this diagram with code like the following:

```c#
DeploymentNode liveWebServer = model.AddDeploymentNode("bigbank-web***", "A web server residing in the web server farm, accessed via F5 BIG-IP LTMs.", "Ubuntu 16.04 LTS", 8, DictionaryUtils.Create("Location=London"));
liveWebServer.AddDeploymentNode("Apache Tomcat", "An open source Java EE web server.", "Apache Tomcat 8.x", 1, DictionaryUtils.Create("Xmx=512M", "Xms=1024M", "Java Version=8"))
    .Add(webApplication);
    
DeploymentNode primaryDatabaseServer = model.AddDeploymentNode("bigbank-db01", "The primary database server.", "Ubuntu 16.04 LTS", 1, DictionaryUtils.Create("Location=London"))
    .AddDeploymentNode("Oracle - Primary", "The primary, live database server.", "Oracle 12c");
primaryDatabaseServer.Add(database);
    
DeploymentNode secondaryDatabaseServer = model.AddDeploymentNode("bigbank-db02", "The secondary database server.", "Ubuntu 16.04 LTS", 1, DictionaryUtils.Create("Location=Reading"))
    .AddDeploymentNode("Oracle - Secondary", "A secondary, standby database server, used for failover purposes only.", "Oracle 12c");
ContainerInstance secondaryDatabase = secondaryDatabaseServer.Add(database);


model.Relationships.Where(r => r.Destination.Equals(secondaryDatabase)).ToList().ForEach(r => r.AddTags("Failover"));
Relationship dataReplicationRelationship = primaryDatabaseServer.Uses(secondaryDatabaseServer, "Replicates data to", "");
secondaryDatabase.AddTags("Failover");

DeploymentView liveDeploymentView = views.CreateDeploymentView(internetBankingSystem, "LiveDeployment", "An example live deployment scenario for the Internet Banking System.");
liveDeploymentView.Add(liveWebServer);
liveDeploymentView.Add(primaryDatabaseServer);
liveDeploymentView.Add(secondaryDatabaseServer);
liveDeploymentView.Add(dataReplicationRelationship);
```

See [BigBankPlc.cs](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Examples/BigBankPlc.cs) for the full code, and [https://structurizr.com/share/36141#LiveDeployment](https://structurizr.com/share/36141#LiveDeployment) for the diagram.