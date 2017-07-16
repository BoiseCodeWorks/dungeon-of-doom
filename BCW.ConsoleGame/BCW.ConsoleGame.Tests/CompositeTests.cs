using BCW.ConsoleGame.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Tests
{
    [TestFixture]
    public class CompositeTests
    {
        private TestClass inventory;
        private TestClass ruby;

        [SetUp]
        public void Setup()
        {
            inventory = new TestClass(){ Name = "Inventory" };
            ruby = new TestClass() { Name = "Ruby" };

            inventory.AddItem("treasure/gems/rubies", ruby);
            inventory.AddItem("treasure/coins", new TestClass() { Name = "Doubloon" });
        }

        [Test]
        public void CountOnlyIncludeLeafNodes()
        {
            Assert.That(inventory.Count, Is.EqualTo(2));
        }

        [Test]
        public void PruneTest()
        {
            inventory.RemoveItem("treasure/gems/rubies", ruby);

            Assert.That(inventory.Count, Is.EqualTo(1));
        }
    }

    class TestClass : Composite
    {
        public TestClass()
        {
            items = new List<IComposite>();
        }
    }
}
