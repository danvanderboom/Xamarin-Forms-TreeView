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
            var storage = building.Children.Add(new Space { Name = "Storage", SquareFeet = 1200 });
            var bin = storage.Children.Add(new Space { Name = "Bin", SquareFeet = 4 });

            Assert.AreEqual(0, building.Ancestors.Count());
            Assert.AreEqual(1, building.Children.Count);
            Assert.AreEqual(1, building.ChildNodes.Count());
            Assert.AreEqual(2, building.Descendants.Count());
            Assert.AreEqual(2, building.Height);
            Assert.AreEqual(0, building.Depth);
            Assert.IsNull(building.Parent);

            Assert.AreEqual(1, storage.Ancestors.Count());
            Assert.AreEqual(1, storage.Children.Count);
            Assert.AreEqual(1, storage.ChildNodes.Count());
            Assert.AreEqual(1, storage.Descendants.Count());
            Assert.AreEqual(1, storage.Height);
            Assert.AreEqual(1, storage.Depth);
            Assert.AreSame(storage.Parent, building);

            Assert.AreEqual(2, bin.Ancestors.Count());
            Assert.AreEqual(0, bin.Children.Count);
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
            var storage = building.Children.Add(new Space { Name = "Storage", SquareFeet = 1200 });
            var bin = storage.Children.Add(new Space { Name = "Bin", SquareFeet = 4 });

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
            var storage = building.Children.Add(new Space { Name = "Storage", SquareFeet = 1200 });
            var kitchen = building.Children.Add(new Space { Name = "Kitchen", SquareFeet = 1800 });
            var bin = storage.Children.Add(new Space { Name = "Bin", SquareFeet = 4 });

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
            var storage = building.Children.Add(new Space { Name = "Storage", SquareFeet = 1200 });
            var bin = storage.Children.Add(new Space { Name = "Bin", SquareFeet = 4 });

            storage.Children.Remove(bin);

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

            var buildingA = property.Children.Add(new Space { Name = "Building A", SquareFeet = 50000 });
            var laundryRoom = buildingA.Children.Add(new Space { Name = "Laundry Room", SquareFeet = 300 });
            var bathroomA = buildingA.Children.Add(new Space { Name = "Bathroom", SquareFeet = 150 });
            var storageA = buildingA.Children.Add(new Space { Name = "Storage", SquareFeet = 450 });

            var buildingB = property.Children.Add(new Space { Name = "Building B", SquareFeet = 50000 });
            var bathroomB = buildingB.Children.Add(new Space { Name = "Bathroom", SquareFeet = 50000 });
            var storageB = buildingB.Children.Add(new Space { Name = "Storage", SquareFeet = 500 });
            var meetingRoom = buildingB.Children.Add(new Space { Name = "Meeting Room", SquareFeet = 1600 });
            var meetingRoomCloset = meetingRoom.Children.Add(new Space { Name = "Meeting Room Closet", SquareFeet = 150 });

            Assert.AreEqual(0, property.Ancestors.Count());
            Assert.AreEqual(2, property.Children.Count());
            Assert.AreEqual(9, property.Descendants.Count());
            Assert.AreEqual(3, property.Height);
            Assert.AreEqual(0, property.Depth);
            Assert.IsNull(property.Parent);

            Assert.AreEqual(1, buildingA.Ancestors.Count());
            Assert.AreEqual(3, buildingA.Children.Count());
            Assert.AreEqual(3, buildingA.Descendants.Count());
            Assert.AreEqual(1, buildingA.Height);
            Assert.AreEqual(1, buildingA.Depth);
            Assert.AreSame(property, buildingA.Parent);

            Assert.AreEqual(2, laundryRoom.Ancestors.Count());
            Assert.AreEqual(0, laundryRoom.Children.Count());
            Assert.AreEqual(0, laundryRoom.Descendants.Count());
            Assert.AreEqual(0, laundryRoom.Height);
            Assert.AreEqual(2, laundryRoom.Depth);
            Assert.AreSame(buildingA, laundryRoom.Parent);

            Assert.AreEqual(2, bathroomA.Ancestors.Count());
            Assert.AreEqual(0, bathroomA.Children.Count());
            Assert.AreEqual(0, bathroomA.Descendants.Count());
            Assert.AreEqual(0, bathroomA.Height);
            Assert.AreEqual(2, bathroomA.Depth);
            Assert.AreSame(buildingA, bathroomA.Parent);

            Assert.AreEqual(2, storageA.Ancestors.Count());
            Assert.AreEqual(0, storageA.Children.Count());
            Assert.AreEqual(0, storageA.Descendants.Count());
            Assert.AreEqual(0, storageA.Height);
            Assert.AreEqual(2, storageA.Depth);
            Assert.AreSame(buildingA, storageA.Parent);

            Assert.AreEqual(1, buildingB.Ancestors.Count());
            Assert.AreEqual(3, buildingB.Children.Count());
            Assert.AreEqual(4, buildingB.Descendants.Count());
            Assert.AreEqual(2, buildingB.Height);
            Assert.AreEqual(1, buildingB.Depth);
            Assert.AreSame(property, buildingB.Parent);

            Assert.AreEqual(2, bathroomB.Ancestors.Count());
            Assert.AreEqual(0, bathroomB.Children.Count());
            Assert.AreEqual(0, bathroomB.Descendants.Count());
            Assert.AreEqual(0, bathroomB.Height);
            Assert.AreEqual(2, bathroomB.Depth);
            Assert.AreSame(buildingB, bathroomB.Parent);

            Assert.AreEqual(2, storageB.Ancestors.Count());
            Assert.AreEqual(0, storageB.Children.Count());
            Assert.AreEqual(0, storageB.Descendants.Count());
            Assert.AreEqual(0, storageB.Height);
            Assert.AreEqual(2, storageB.Depth);
            Assert.AreSame(buildingB, storageB.Parent);

            Assert.AreEqual(2, meetingRoom.Ancestors.Count());
            Assert.AreEqual(1, meetingRoom.Children.Count());
            Assert.AreEqual(1, meetingRoom.Descendants.Count());
            Assert.AreEqual(1, meetingRoom.Height);
            Assert.AreEqual(2, meetingRoom.Depth);
            Assert.AreSame(buildingB, meetingRoom.Parent);

            Assert.AreEqual(3, meetingRoomCloset.Ancestors.Count());
            Assert.AreEqual(0, meetingRoomCloset.Children.Count());
            Assert.AreEqual(0, meetingRoomCloset.Descendants.Count());
            Assert.AreEqual(0, meetingRoomCloset.Height);
            Assert.AreEqual(3, meetingRoomCloset.Depth);
            Assert.AreSame(meetingRoom, meetingRoomCloset.Parent);

            var ancestorList = new List<string>();
            foreach (ITreeNode node in meetingRoomCloset.Ancestors)
                ancestorList.Add((node as Space).Name);

            Assert.AreEqual(3, ancestorList.Count);
            Assert.Contains((meetingRoom as Space).Name, ancestorList);
            Assert.Contains((buildingB as Space).Name, ancestorList);
            Assert.Contains((property as Space).Name, ancestorList);

            ancestorList = new List<string>();
            foreach (ITreeNode node in laundryRoom.Ancestors)
                ancestorList.Add((node as Space).Name);

            Assert.AreEqual(2, ancestorList.Count);
            Assert.Contains((buildingA as Space).Name, ancestorList);
            Assert.Contains((property as Space).Name, ancestorList);

            var descendantList = new List<string>();
            foreach (ITreeNode node in buildingA.Descendants)
                descendantList.Add((node as Space).Name);

            Assert.AreEqual(3, descendantList.Count);
            Assert.Contains((laundryRoom as Space).Name, descendantList);
            Assert.Contains((bathroomA as Space).Name, descendantList);
            Assert.Contains((storageA as Space).Name, descendantList);

            descendantList = new List<string>();
            foreach (ITreeNode node in property.Descendants)
                descendantList.Add((node as Space).Name);

            Assert.AreEqual(9, descendantList.Count);
            Assert.Contains((buildingA as Space).Name, descendantList);
            Assert.Contains((laundryRoom as Space).Name, descendantList);
            Assert.Contains((bathroomA as Space).Name, descendantList);
            Assert.Contains((storageA as Space).Name, descendantList);
            Assert.Contains((buildingB as Space).Name, descendantList);
            Assert.Contains((bathroomB as Space).Name, descendantList);
            Assert.Contains((storageB as Space).Name, descendantList);
            Assert.Contains((meetingRoom as Space).Name, descendantList);
            Assert.Contains((meetingRoomCloset as Space).Name, descendantList);
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

            var buildingA = property.Children.Add(
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

            property.Children.Remove(buildingA);

            Assert.AreEqual(0, property.Depth);
            Assert.AreEqual(0, property.Height);

            Assert.AreEqual(0, buildingA.Depth);
            Assert.AreEqual(0, buildingA.Height);
        }
    }
}