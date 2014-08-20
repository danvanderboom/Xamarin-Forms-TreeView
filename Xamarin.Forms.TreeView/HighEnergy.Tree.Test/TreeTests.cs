using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using HighEnergy.Collections;

namespace HighEnergy.Tree.Test
{
    [TestFixture]
    public class TreeTests
    {
        [Test]
        public void SingleNode()
        {
            var building = new Space { Name = "Science Building", SquareFeet = 30000 };

            Assert.AreEqual(0, building.Height);
            Assert.AreEqual(0, building.Depth);
            Assert.AreEqual(0, building.Ancestors.Count());
            Assert.AreEqual(0, building.Descendants.Count());
            Assert.AreEqual(0, building.ChildNodes.Count());
            Assert.IsNull(building.Parent);
        }

        [Test]
        public void AddChildNodes()
        {
            var building = new Space { Name = "Science Building", SquareFeet = 30000 };
            var storage = building.ChildNodes.Add(new Space { Name = "Storage", SquareFeet = 1200 });
            var bin = storage.ChildNodes.Add(new Space { Name = "Bin", SquareFeet = 4 });

            Assert.AreEqual(0, building.Ancestors.Count());
            Assert.AreEqual(1, building.ChildNodes.Count());
            Assert.AreEqual(2, building.Descendants.Count());
            Assert.AreEqual(2, building.Height);
            Assert.AreEqual(0, building.Depth);
            Assert.IsNull(building.Parent);

            Assert.AreEqual(1, storage.Ancestors.Count());
            Assert.AreEqual(1, storage.ChildNodes.Count());
            Assert.AreEqual(1, storage.Descendants.Count());
            Assert.AreEqual(1, storage.Height);
            Assert.AreEqual(1, storage.Depth);
            Assert.AreSame(storage.Parent, building);

            Assert.AreEqual(2, bin.Ancestors.Count());
            Assert.AreEqual(0, bin.ChildNodes.Count());
            Assert.AreEqual(0, bin.Descendants.Count());
            Assert.AreEqual(0, bin.Height);
            Assert.AreEqual(2, bin.Depth);
            Assert.AreSame(bin.Parent, storage);
        }

        [Test]
        public void RemoveNodeBySettingParentToNull()
        {
            var building = new Space { Name = "Science Building", SquareFeet = 30000 };
            var storage = building.ChildNodes.Add(new Space { Name = "Storage", SquareFeet = 1200 });
            var bin = storage.ChildNodes.Add(new Space { Name = "Bin", SquareFeet = 4 });

            bin.Parent = null;

            Assert.AreEqual(0, building.Ancestors.Count());
            Assert.AreEqual(1, building.ChildNodes.Count());
            Assert.AreEqual(1, building.Descendants.Count());
            Assert.AreEqual(1, building.Height);
            Assert.AreEqual(0, building.Depth);
            Assert.IsNull(building.Parent);

            Assert.AreEqual(1, storage.Ancestors.Count());
            Assert.AreEqual(0, storage.ChildNodes.Count());
            Assert.AreEqual(0, storage.Descendants.Count());
            Assert.AreEqual(0, storage.Height);
            Assert.AreEqual(1, storage.Depth);
            Assert.AreSame(storage.Parent, building);

            Assert.AreEqual(0, bin.Ancestors.Count());
            Assert.AreEqual(0, bin.ChildNodes.Count());
            Assert.AreEqual(0, bin.Descendants.Count());
            Assert.AreEqual(0, bin.Height);
            Assert.AreEqual(0, bin.Depth);
            Assert.IsNull(bin.Parent);
        }

        [Test]
        public void SwapNodeParent()
        {
            var building = new Space { Name = "Science Building", SquareFeet = 30000 };
            var storage = building.ChildNodes.Add(new Space { Name = "Storage", SquareFeet = 1200 });
            var kitchen = building.ChildNodes.Add(new Space { Name = "Kitchen", SquareFeet = 1800 });
            var bin = storage.ChildNodes.Add(new Space { Name = "Bin", SquareFeet = 4 });

            bin.Parent = kitchen;

            Assert.AreEqual(0, building.Ancestors.Count());
            Assert.AreEqual(2, building.ChildNodes.Count());
            Assert.AreEqual(3, building.Descendants.Count());
            Assert.AreEqual(2, building.Height);
            Assert.AreEqual(0, building.Depth);
            Assert.IsNull(building.Parent);

            Assert.AreEqual(1, storage.Ancestors.Count());
            Assert.AreEqual(0, storage.ChildNodes.Count());
            Assert.AreEqual(0, storage.Descendants.Count());
            Assert.AreEqual(0, storage.Height);
            Assert.AreEqual(1, storage.Depth);
            Assert.AreSame(storage.Parent, building);

            Assert.AreEqual(1, kitchen.Ancestors.Count());
            Assert.AreEqual(1, kitchen.ChildNodes.Count());
            Assert.AreEqual(1, kitchen.Descendants.Count());
            Assert.AreEqual(1, kitchen.Height);
            Assert.AreEqual(1, kitchen.Depth);
            Assert.AreSame(kitchen.Parent, building);

            Assert.AreEqual(2, bin.Ancestors.Count());
            Assert.AreEqual(0, bin.ChildNodes.Count());
            Assert.AreEqual(0, bin.Descendants.Count());
            Assert.AreEqual(0, bin.Height);
            Assert.AreEqual(2, bin.Depth);
            Assert.AreSame(bin.Parent, kitchen);
        }

        [Test]
        public void RemoveNodeByCallingRemove()
        {
            var building = new Space { Name = "Science Building", SquareFeet = 30000 };
            var storage = building.ChildNodes.Add(new Space { Name = "Storage", SquareFeet = 1200 });
            var bin = storage.ChildNodes.Add(new Space { Name = "Bin", SquareFeet = 4 });

            storage.ChildNodes.Remove(bin);

            Assert.AreEqual(0, building.Ancestors.Count());
            Assert.AreEqual(1, building.ChildNodes.Count());
            Assert.AreEqual(1, building.Descendants.Count());
            Assert.AreEqual(1, building.Height);
            Assert.AreEqual(0, building.Depth);
            Assert.IsNull(building.Parent);

            Assert.AreEqual(1, storage.Ancestors.Count());
            Assert.AreEqual(0, storage.ChildNodes.Count());
            Assert.AreEqual(0, storage.Descendants.Count());
            Assert.AreEqual(0, storage.Height);
            Assert.AreEqual(1, storage.Depth);
            Assert.AreSame(storage.Parent, building);

            Assert.AreEqual(0, bin.Ancestors.Count());
            Assert.AreEqual(0, bin.ChildNodes.Count());
            Assert.AreEqual(0, bin.Descendants.Count());
            Assert.AreEqual(0, bin.Height);
            Assert.AreEqual(0, bin.Depth);
            Assert.IsNull(bin.Parent);
        }

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

            Assert.AreEqual(0, property.Depth);
            Assert.AreEqual(0, property.Height);

            var buildingA = property.ChildNodes.Add(
                new Space
                {
                    Name = "Building A",
                    SquareFeet = 900
                });

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
    }
}