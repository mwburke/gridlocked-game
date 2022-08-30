using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car{
    private CarType _carType;
    private int _length;
    private GridSpace _startSpace;
    private Color _color;
    private Orientation _orientation;
    private List<GridSpace> _occupiedSpaces;

    public Car(CarType cartype, int length, GridSpace startSpace, Orientation orientation, Color color) {
        _carType = cartype;
        _length = length;
        _startSpace = startSpace;
        _orientation = orientation;
        _color = color;
        UpdateOccupiedSpaces();
    }

    void UpdateOccupiedSpaces() {
        _occupiedSpaces = new List<GridSpace>();

        if (_orientation == Orientation.Horizontal) {
            for (int x = 0; x < _length; x++) {
                _occupiedSpaces.Add(new GridSpace(_startSpace.X + x, _startSpace.Y));
            }
        } else {
            for (int y = 0; y < _length; y++) {
                _occupiedSpaces.Add(new GridSpace(_startSpace.X, _startSpace.Y + y));
            }
        }
    }

    public void UpdateLocation(GridSpace newSpace) {
        _startSpace = newSpace;
        UpdateOccupiedSpaces();
    }

    public List<GridSpace> GetOccupiedSpaces() {
        return _occupiedSpaces;
    }

    public CarType GetCarType() {
        return _carType;
    }

    public bool IsGoalCar() {
        return _carType == CarType.Goal;
    }

    public Orientation GetOrientation() {
        return _orientation;
    }

    public GridSpace GetStartSpace() {
        return _startSpace;
    }

    public int GetLength() {
        return _length;
    }

    public static bool operator ==(Car c1, Car c2) {
       if (c1.GetCarType() != c2.GetCarType()) {
            if (c1.GetStartSpace() != c2.GetStartSpace()) {
                if (c1.GetLength() != c2.GetLength()) {
                    if (c1.GetOrientation() != c2.GetOrientation()) {
                        return false;
                    }
                }
            }
       }
       return true;
    }

    public static bool operator !=(Car c1, Car c2) {
        // Want to compare all of the cars

        if (c1.GetCarType() == c2.GetCarType()) {
            if (c1.GetStartSpace() == c2.GetStartSpace()) {
                if (c1.GetLength() == c2.GetLength()) {
                    if (c1.GetOrientation() == c2.GetOrientation()) {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public static bool Equals(Car c1, Car c2) {
        if (c1.GetCarType() != c2.GetCarType()) {
            if (c1.GetStartSpace() != c2.GetStartSpace()) {
                if (c1.GetLength() != c2.GetLength()) {
                    if (c1.GetOrientation() != c2.GetOrientation()) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public bool Equals(Car c2) {
        if (this.GetCarType() != c2.GetCarType()) {
            if (this.GetStartSpace() != c2.GetStartSpace()) {
                if (this.GetLength() != c2.GetLength()) {
                    if (this.GetOrientation() != c2.GetOrientation()) {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}


// Goal is the car we are trying to move to the exit
// Regular is every other car
public enum CarType {
    Goal,
    Regular
}

// Represents the direction a car is facing
public enum Orientation {
    Vertical,
    Horizontal
}

// Represents a single grid location on the board
public struct GridSpace {
    public GridSpace(int x, int y) {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }

    public override string ToString() => $"({X}, {Y})";

    public static bool operator ==(GridSpace s1, GridSpace s2) {
        return s1.X == s2.X && s1.Y == s2.Y;
    }

    public static bool operator !=(GridSpace s1, GridSpace s2) {
        return s1.X != s2.X || s1.Y != s2.Y;
    }

    public static bool Equals(GridSpace s1, GridSpace s2) {
        return s1.X == s2.X && s1.Y == s2.Y;
    }

    //public override bool Equals(object obj) => this.Equals(obj as TwoDPoint);
    public bool Equals(GridSpace s1) {
        return s1.X == this.X && s1.Y == this.Y;
    }

    public override int GetHashCode() {
        return this.ToString().GetHashCode();
    }
}