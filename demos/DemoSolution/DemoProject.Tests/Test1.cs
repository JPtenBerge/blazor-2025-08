using AngleSharp.Dom;
using AwesomeAssertions;
using Bunit;
using Demo.Shared.Entities;
using DemoProject.Components;

namespace DemoProject.Tests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void TestMethod1()
    {
        var destinations = new List<Destination>
        {
            new() {Location = "N.E.C.-megen", Rating = 2,PhotoUrl = string.Empty },
            new() {Location = "Den Bosch", Rating = 3,PhotoUrl = string.Empty },
            new() {Location = "Ede", Rating = 8,PhotoUrl = string.Empty },
            new() {Location = "Veenendaal", Rating = 7,PhotoUrl = string.Empty },
            new() {Location = "Utrecht", Rating = 2,PhotoUrl = string.Empty },
            new() {Location = "Assen", Rating = 7,PhotoUrl = string.Empty },
            new() {Location = "Bolsward", Rating = 7,PhotoUrl = string.Empty },
            new() {Location = "Tilburg", Rating = 7,PhotoUrl = string.Empty },
            new() {Location = "Groningen", Rating = 5,PhotoUrl = string.Empty },
            new() {Location = "Groningen provincie", Rating = 8,PhotoUrl = string.Empty },
            new() {Location = "Utrecht provincie", Rating = 7,PhotoUrl = string.Empty },
        };

        var ctx = new Bunit.TestContext();
        //ctx.ComponentFactories.add
        var fixture = ctx.RenderComponent<Autocompleter<Destination>>(parameters =>
        {
            parameters.Add(x => x.Data, destinations);
            parameters.Add(x => x.ItemTemplate, item => $"{item.Location} krijgt een {item.Rating}");
        });
        var sut = fixture.Instance;
        sut.Query = "provincie";
        sut.Autocomplete();
        fixture.Render();

        var lis = fixture.FindAll("li");
        //Assert.AreEqual(2, lis.Count);
        var expected = new List<string> { "Groningen provincie krijgt een 8", "Utrecht provincie krijgt een 7" };
        //Assert.AreEqual(expected, lis.Select(x => x.GetInnerText()));
        //CollectionAssert.AreEquivalent(expected, lis.Select(x => x.GetInnerText()).ToList());

        var obj = new { Name = "JP", Age = 34 };
        var obj2 = new { Name = "JP", Age = 38 };
        obj.Should().BeEquivalentTo(obj2, opts => opts.Including(x => x.Name));
        lis.Select(x => x.GetInnerText()).Should().BeEquivalentTo(expected);
    }
}
