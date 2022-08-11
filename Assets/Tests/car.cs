using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class car
{
    // Use the Assert class to test conditions
    [Test]
    public void gridspace_equality_test()
    {
        
        GridSpace gs1 = new(3, 2);
        GridSpace gs2 = new(3, 2);

        Assert.IsTrue(gs1 == gs2);
    }

    [Test]
    public void car_equality_test() {
        Car c1 = new (CarType.Regular, 2, new GridSpace(1, 1), Orientation.Horizontal, Color.red);
        Car c2 = new (CarType.Regular, 2, new GridSpace(1, 1), Orientation.Horizontal, Color.red);

        Assert.IsTrue(c1 == c2);
    }

    [Test]
    public void car_inequality_test_0() {
        Car c1 = new(CarType.Regular, 2, new GridSpace(1, 1), Orientation.Horizontal, Color.red);
        Car c2 = new(CarType.Goal, 2, new GridSpace(1, 1), Orientation.Horizontal, Color.red);

        Assert.IsTrue(c1 != c2);
    }

    public void car_inequality_test_1() {
        Car c1 = new(CarType.Regular, 2, new GridSpace(1, 1), Orientation.Horizontal, Color.red);
        Car c2 = new(CarType.Regular, 2, new GridSpace(1, 2), Orientation.Horizontal, Color.red);

        Assert.IsTrue(c1 != c2);
    }

    [Test]
    public void car_occupied_spaces_horizontal() {
        Car c1 = new(CarType.Regular, 2, new GridSpace(3, 5), Orientation.Horizontal, Color.red);
        List<GridSpace> trueOccupiedSpaces = new List<GridSpace> { new GridSpace(3, 5), new GridSpace(4, 5) };

        Assert.IsTrue(c1.GetOccupiedSpaces().SequenceEqual(trueOccupiedSpaces));
    }

    [Test]
    public void car_occupied_spaces_vertical() {
        Car c1 = new(CarType.Regular, 2, new GridSpace(3, 1), Orientation.Vertical, Color.red);
        List<GridSpace> trueOccupiedSpaces = new List<GridSpace> { new GridSpace(3, 1), new GridSpace(3, 2) };

        Assert.IsTrue(c1.GetOccupiedSpaces().SequenceEqual(trueOccupiedSpaces));
    }

    [Test]
    public void car_occupied_spaces_vertical_3() {
        Car c1 = new(CarType.Regular, 3, new GridSpace(3, 1), Orientation.Vertical, Color.red);
        List<GridSpace> trueOccupiedSpaces = new List<GridSpace> { new GridSpace(3, 1), new GridSpace(3, 2), new GridSpace(3, 3) };

        Assert.IsTrue(c1.GetOccupiedSpaces().SequenceEqual(trueOccupiedSpaces));
    }

    [Test]
    public void car_update_location_space() {
        Car c1 = new(CarType.Regular, 2, new GridSpace(3, 1), Orientation.Vertical, Color.red);
        GridSpace newSpace = new GridSpace(3, 4);
        GridSpace newSpaceCopy = new GridSpace(3, 4);
        c1.UpdateLocation(newSpace);

        Assert.IsTrue(c1.GetStartSpace() == newSpaceCopy);
    }

    [Test]
    public void car_update_location_occupied_spaces() {
        Car c1 = new(CarType.Regular, 2, new GridSpace(3, 1), Orientation.Vertical, Color.red);
        GridSpace newSpace = new GridSpace(3, 4);
        c1.UpdateLocation(newSpace);

        List<GridSpace> trueOccupiedSpaces = new List<GridSpace> { new GridSpace(3, 4), new GridSpace(3, 5) };

        Assert.IsTrue(c1.GetOccupiedSpaces().SequenceEqual(trueOccupiedSpaces));
    }
}
