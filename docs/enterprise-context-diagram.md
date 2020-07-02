# Enterprise Context diagram

An Enterprise Context diagram is really the same as the [System Context diagram](system-context-diagram.md), without a focus on a specific software system. It can help to provide a broader view of the people and software systems that are related to and reside within a given enterprise context (e.g. a business or organisation).

## Example

As an example, an Enterprise Context diagram for a simplified, fictional Internet Banking System might look something like this. In summary, it shows more than just the immediate relationships of the Internet Banking System.

![An example Enterprise Context diagram](images/enterprise-context-diagram-1.png)

With Structurizr for .NET, you can create this diagram with code like the following:

```c#
Person customer = model.AddPerson(Location.External, "Customer", "A customer of the bank.");
    
SoftwareSystem internetBankingSystem = model.AddSoftwareSystem(Location.Internal, "Internet Banking System", "Allows customers to view information about their bank accounts and make payments.");
customer.Uses(internetBankingSystem, "Uses");
    
SoftwareSystem mainframeBankingSystem = model.AddSoftwareSystem(Location.Internal, "Mainframe Banking System", "Stores all of the core banking information about customers, accounts, transactions, etc.");
internetBankingSystem.Uses(mainframeBankingSystem, "Uses");
    
SoftwareSystem atm = model.AddSoftwareSystem(Location.Internal, "ATM", "Allows customers to withdraw cash.");
atm.Uses(mainframeBankingSystem, "Uses");
customer.Uses(atm, "Withdraws cash using");
    
Person bankStaff = model.AddPerson(Location.Internal, "Bank Staff", "Staff within the bank.");
bankStaff.Uses(mainframeBankingSystem, "Uses");

EnterpriseContextView enterpriseContextView = views.CreateEnterpriseContextView("EnterpriseContext", "The system context diagram for the Internet Banking System.");
enterpriseContextView.AddAllElements();
```

See [BigBankPlc.cs](https://github.com/structurizr/dotnet/blob/master/AtlasEngine.Modelling.C5.Examples/BigBankPlc.cs) for the full code, and [https://structurizr.com/share/36141#EnterpriseContext](https://structurizr.com/share/36141#EnterpriseContext) for the diagram.