# Filtered views

A filtered view represents a view on top of another view, which can be used to include or exclude specific elements and/or relationships, based upon their tag. The benefit of using filtered views is that element and relationship positions are shared between the views.

Filtered views can be created on top of static views only; i.e. Enterprise Context, System Context, Container and Component views.

## Example

As an example, let's imagine an organisation where a User uses Software System A for tasks 1 and 2.

![A diagram showing the current state](images/filtered-views-1.png)

And, in the future, Software System B will be introduced to fulfil task 2.

![A diagram showing the future state](images/filtered-views-2.png)

With Structurizr for .NET, you can illustrate this by defining a single context diagram with two filtered views on top; one showing the current state and the other showing future state.

```c#
Person user = model.AddPerson("User", "A description of the user.");
SoftwareSystem softwareSystemA = model.AddSoftwareSystem("Software System A", "A description of software system A.");
SoftwareSystem softwareSystemB = model.AddSoftwareSystem("Software System B", "A description of software system B.");
softwareSystemB.AddTags(FutureState);

user.Uses(softwareSystemA, "Uses for tasks 1 and 2").AddTags(CurrentState);
user.Uses(softwareSystemA, "Uses for task 1").AddTags(FutureState);
user.Uses(softwareSystemB, "Uses for task 2").AddTags(FutureState);

ViewSet views = workspace.Views;
EnterpriseContextView enterpriseContextView = views.CreateEnterpriseContextView("EnterpriseContext", "An example Enterprise Context diagram.");
enterpriseContextView.AddAllElements();

views.CreateFilteredView(enterpriseContextView, "CurrentState", "The current context.", FilterMode.Exclude, FutureState);
views.CreateFilteredView(enterpriseContextView, "FutureState", "The future state context after Software System B is live.", FilterMode.Exclude, CurrentState);
```

In summary, you create a view with all of the elements and relationships that you want to show, and then create one or more filtered views on top, specifying the tags that you'd like to include or exclude.

See [FilteredViews.cs](https://github.com/structurizr/dotnet/tree/master/AtlasEngine.Modelling.C5.Examples/FilteredViews.cs) for the full code, and [https://structurizr.com/share/19911](https://structurizr.com/share/19911) for the diagram.