using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using HighEnergy.Collections;

namespace HighEnergy.Tree.Test
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void ComplexInitialState()
        {
            var property = new Space { Name = "Industrial Property", SquareFeet = 150000 };

            var buildingA = property.ChildNodes.Add(new Space { Name = "Building A", SquareFeet = 50000 });
            var laundryRoom = buildingA.ChildNodes.Add(new Space { Name = "Laundry Room", SquareFeet = 300 });
            var bathroomA = buildingA.ChildNodes.Add(new Space { Name = "Bathroom", SquareFeet = 150 });
            var storageA = buildingA.ChildNodes.Add(new Space { Name = "Storage", SquareFeet = 450 });

            var buildingB = property.ChildNodes.Add(new Space { Name = "Building B", SquareFeet = 50000 });
            var bathroomB = buildingB.ChildNodes.Add(new Space { Name = "Bathroom", SquareFeet = 50000 });
            var storageB = buildingB.ChildNodes.Add(new Space { Name = "Storage", SquareFeet = 500 });

            var buildingC = property.ChildNodes.Add(new Space { Name = "Building C", SquareFeet = 15000 });
            var meetingC100 = buildingC.ChildNodes.Add(new Space { Name = "Meeting Room 100", SquareFeet = 1200 });
            var meetingC200 = buildingC.ChildNodes.Add(new Space { Name = "Meeting Room 200", SquareFeet = 1600 });
            var meetingC200closet = meetingC200.ChildNodes.Add(new Space { Name = "Meeting Room 200 - Closet", SquareFeet = 150 });
            var meetingC300 = buildingC.ChildNodes.Add(new Space { Name = "Meeting Room 300", SquareFeet = 1550 });
            var kitchen = buildingC.ChildNodes.Add(new Space { Name = "Kitchen", SquareFeet = 600 });

            Assert.AreEqual(3, property.Height);
            Assert.AreEqual(0, property.Depth);
            Assert.AreEqual(1, buildingA.Height);
            Assert.AreEqual(1, buildingA.Depth);
            Assert.AreEqual(0, laundryRoom.Height);
            Assert.AreEqual(2, laundryRoom.Depth);
            Assert.AreEqual(0, meetingC200closet.Height);
            Assert.AreEqual(3, meetingC200closet.Depth);

            var ancestorList = new List<string>();
            foreach (ITreeNode node in laundryRoom.Ancestors)
                ancestorList.Add((node as Space).Name);

            Assert.AreEqual(2, ancestorList.Count);
            Assert.Contains((buildingA as Space).Name, ancestorList);
            Assert.Contains((property as Space).Name, ancestorList);

            ancestorList = new List<string>();
            foreach (ITreeNode node in meetingC200closet.Ancestors)
                ancestorList.Add((node as Space).Name);

            Assert.AreEqual(3, ancestorList.Count);
            Assert.Contains((meetingC200 as Space).Name, ancestorList);
            Assert.Contains((buildingC as Space).Name, ancestorList);
            Assert.Contains((property as Space).Name, ancestorList);

            var descendantList = new List<string>();
            foreach (ITreeNode node in buildingC.Descendants)
                descendantList.Add((node as Space).Name);

            Assert.AreEqual(5, descendantList.Count);
            Assert.Contains((meetingC100 as Space).Name, descendantList);
            Assert.Contains((meetingC200 as Space).Name, descendantList);
            Assert.Contains((meetingC200closet as Space).Name, descendantList);
            Assert.Contains((meetingC300 as Space).Name, descendantList);
            Assert.Contains((kitchen as Space).Name, descendantList);
        }

        [Test]
        public void IncrementalChanges()
        {
            var property = new Space
            {
                Name = "Industrial Property",
                SquareFeet = 150000
            };

            Console.WriteLine("first Space ok");

            Assert.AreEqual(0, property.Depth);
            Assert.AreEqual(0, property.Height);

            var buildingA = property.ChildNodes.Add(
                new Space
                {
                    Name = "Building A",
                    SquareFeet = 900
                });

            Console.WriteLine("buildingA");

            Assert.AreEqual(0, property.Depth);
            Assert.AreEqual(1, property.Height);

            Assert.AreEqual(1, buildingA.Depth);
            Assert.AreEqual(0, buildingA.Height);

            var parentOfBuildingA = (Space)buildingA.Parent;
            Assert.IsNotNull(parentOfBuildingA);
            Assert.AreEqual("Industrial Property", parentOfBuildingA.Name);

            property.ChildNodes.Remove(buildingA);

            Assert.AreEqual(0, property.Depth);
            Assert.AreEqual(0, property.Height);

            Assert.AreEqual(0, buildingA.Depth);
            Assert.AreEqual(0, buildingA.Height);
        }

        [Test]
        public void TestNodeChangeType()
        {
            NodeChangeType change = NodeChangeType.AncestorInserted | NodeChangeType.NewRoot;

            Assert.IsTrue((change & NodeChangeType.AncestorInserted) == NodeChangeType.AncestorInserted);
            Assert.IsTrue((change & NodeChangeType.NewRoot) == NodeChangeType.NewRoot);

            Assert.IsFalse((change & NodeChangeType.ChildRemoved) == NodeChangeType.ChildRemoved);
            Assert.IsFalse((change & NodeChangeType.DescendantRemoved) == NodeChangeType.DescendantRemoved);
        }
    }
}