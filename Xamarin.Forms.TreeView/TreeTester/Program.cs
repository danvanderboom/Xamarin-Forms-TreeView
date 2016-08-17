using System;
using HighEnergy.Tree.Test;

namespace TreeTester
{
    class Assert
    {
        public static void AreEqual<T>(T expected, T actual)
        {
            if (!expected.Equals(actual))
                throw new Exception("expected " + expected + ", actual " + actual);
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            var property = new Space { Name = "Industrial Property", SquareFeet = 150000 };
            //var buildingA = property.ChildNodes.Add(new Space { Name = "Building A", SquareFeet = 50000 });
            //var laundryRoom = buildingA.ChildNodes.Add(new Space { Name = "Laundry Room", SquareFeet = 300 });

//            var bathroomA = buildingA.ChildNodes.Add(new Space { Name = "Bathroom", SquareFeet = 150 });
//            var storageA = buildingA.ChildNodes.Add(new Space { Name = "Storage", SquareFeet = 450 });
//            var buildingB = property.ChildNodes.Add(new Space { Name = "Building B", SquareFeet = 50000 });
//            var bathroomB = buildingB.ChildNodes.Add(new Space { Name = "Bathroom", SquareFeet = 50000 });
//            var storageB = buildingB.ChildNodes.Add(new Space { Name = "Storage", SquareFeet = 500 });
//            var buildingC = property.ChildNodes.Add(new Space { Name = "Building C", SquareFeet = 15000 });
//            var meetingC100 = buildingC.ChildNodes.Add(new Space { Name = "Meeting Room 100", SquareFeet = 1200 });
//            var meetingC200 = buildingC.ChildNodes.Add(new Space { Name = "Meeting Room 200", SquareFeet = 1600 });
//            var meetingC200closet = meetingC200.ChildNodes.Add(new Space { Name = "Meeting Room 200 - Closet", SquareFeet = 150 });
//            var meetingC300 = buildingC.ChildNodes.Add(new Space { Name = "Meeting Room 300", SquareFeet = 1550 });
//            var kitchen = buildingC.ChildNodes.Add(new Space { Name = "Kitchen", SquareFeet = 600 });

            //laundryRoom.Parent = null;
        }
    }
}