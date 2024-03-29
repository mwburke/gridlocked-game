using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board
{

    private int _boardWidth;
    private GridSpace _goalSpace;
    private List<Car> _cars;

    public Board(int boardWidth, GridSpace goalSpace, List<Car> cars = null) {
        _boardWidth = boardWidth;
        _goalSpace = goalSpace;
        if (cars is null) {
            _cars = new();
        } else {
            _cars = cars;
        }
        
    }

    public Board DeepCopy() {
        return new Board(
            _boardWidth,
            _goalSpace,
            _cars
        );
    }

    public List<GridSpace> GetOccupiedSpaces() {
        List<GridSpace> occupiedSpaces = new();
        foreach (Car car in _cars) {
            occupiedSpaces = (List<GridSpace>)occupiedSpaces.Concat(car.GetOccupiedSpaces()).ToList();
        }
        return occupiedSpaces;
    }

    public List<GridSpace> GetOccupiedSpacesRow(int row) {
        List<GridSpace> occupiedSpaces = new();
        foreach (Car car in _cars) {
            List<GridSpace> carSpaces = car.GetOccupiedSpaces();
            foreach (GridSpace space in carSpaces) {
                if (space.Y == row) {
                    occupiedSpaces.Add(space);
                }
            }
            
        }
        return occupiedSpaces;
    }

    // Yes, this is kind of repeated. Who cares
    public List<GridSpace> GetOccupiedSpacesCol(int col) {
        List<GridSpace> occupiedSpaces = new();
        foreach (Car car in _cars) {
            List<GridSpace> carSpaces = car.GetOccupiedSpaces();
            foreach (GridSpace space in carSpaces) {
                if (space.X == col) {
                    occupiedSpaces.Add(space);
                }
            }

        }
        return occupiedSpaces;
    }

    public void MoveCar(int carIndex, GridSpace moveSpace) {
        _cars[carIndex].UpdateLocation(moveSpace);
    }

    public List<GridSpace> GetAvailableMoves(int carIndex) {
        List<GridSpace> availableMoves = new();

        Car car = _cars[carIndex];
        List<GridSpace> carOccupiedSpaces = car.GetOccupiedSpaces();
        Orientation orientation = car.GetOrientation();
        GridSpace startSpace = car.GetStartSpace();
        int length = car.GetLength();

        int carStart;

        // Set starting point to check from
        // Either X or Y since the car can only move in one direction 
        // depending on the orientation
        if (orientation == Orientation.Horizontal) {
            carStart = startSpace.X;
        } else {
            carStart = startSpace.Y;
        }

        List<GridSpace> occupiedSpaces;
        List<GridSpace> freeSpaces = new();

        // Find all available slots within the row/column
        if (orientation == Orientation.Horizontal) {
            occupiedSpaces = GetOccupiedSpacesRow(startSpace.Y);
            // Remove all current car spaces from the occupied spaces
            occupiedSpaces.RemoveAll(t => carOccupiedSpaces.Contains(t));

            for (int i = 0; i < _boardWidth; i++) {
                GridSpace tempSpace = new(i, startSpace.Y);
                if (!occupiedSpaces.Contains(tempSpace)) {
                    freeSpaces.Add(tempSpace);
                }
            }
        } else {
            occupiedSpaces = GetOccupiedSpacesCol(startSpace.X);
            // Remove all current car spaces from the occupied spaces
            occupiedSpaces.RemoveAll(t => carOccupiedSpaces.Contains(t));

            for (int i = 0; i < _boardWidth; i++) {
                GridSpace tempSpace = new(startSpace.X, i);
                if (!occupiedSpaces.Contains(tempSpace)) {
                    freeSpaces.Add(tempSpace);
                }
            }
        }

        // Check prior to car
        bool hasAvailableSpace = true;
        int checkIndex = carStart - 1;
        while (checkIndex >= 0 && hasAvailableSpace) {
            GridSpace tempSpace;

            if (orientation == Orientation.Horizontal) {
                tempSpace = new(checkIndex, startSpace.Y);
            }  else {
                tempSpace = new(startSpace.X, checkIndex);
            }

            if (freeSpaces.Contains(tempSpace)) {
                availableMoves.Add(tempSpace);
                checkIndex -= 1;
            } else {
                hasAvailableSpace = false;
            }
        }


        // Check after car
        hasAvailableSpace = true;
        checkIndex = carStart + 1;
        while (checkIndex < (_boardWidth - (length - 1)) && hasAvailableSpace) {
            List<GridSpace> tempSpaces = new();

            if (orientation == Orientation.Horizontal) {
                for (int i = 0; i < length; i++) {
                    tempSpaces.Add(new GridSpace(checkIndex + i, startSpace.Y));
                }
            } else {
                for(int i = 0; i < length; i++) {
                    tempSpaces.Add(new(startSpace.X, checkIndex + i));
                }
            }

            if (!tempSpaces.Except(freeSpaces).Any()) {

                // Actual start move space
                GridSpace startMoveSpace;
                if (orientation == Orientation.Horizontal) {
                    startMoveSpace = new(checkIndex, startSpace.Y);
                } else {
                    startMoveSpace = new(startSpace.X, checkIndex);
                }

                availableMoves.Add(startMoveSpace);
                checkIndex += 1;
            } else {
                hasAvailableSpace = false;
            }
        }

        return availableMoves;
    }

    public bool IsGridlocked() {
        for (int i = 0; i < _cars.Count; i++) {
            List<GridSpace> moves = GetAvailableMoves(i);
            if (moves.Count > 0) {
                return false;
            }
        }
        return true;
    }

    // Check if the goal car could reach the destination in 
    // a single move
    public bool IsWinCondition() {
        // The goal car is always first in the list
        List<GridSpace> moves = GetAvailableMoves(0);

        // The goal car is always 2 spaces long
        GridSpace winSpace = WinSpace();

        return moves.Contains(winSpace) || _cars[0].GetStartSpace() == _goalSpace;
    }

    public GridSpace WinSpace() {
        GridSpace winSpace = new(_goalSpace.X - 1, _goalSpace.Y);
        return winSpace;
    }
        
    public bool NewCarOverlapsWithExisting(Car car) {
        // Check if a new car we want to add overlaps with any existing
        // Just compare the new car's occupied spaces with all existing occupied spaces

        List<GridSpace> carSpaces = car.GetOccupiedSpaces();
        List<GridSpace> boardSpaces = GetOccupiedSpaces();

        return boardSpaces.Intersect(carSpaces).Any();
    }

    public void AddCar(Car car) {
        _cars.Add(car);
    }

    public int CountCars() {
        return _cars.Count;
    }

    public int CountCarsInRowCol(Orientation orientation, int rowColNum) {
        int countCars = 0;

        for (int i = 0; i < _cars.Count(); i++) {
            if (orientation == Orientation.Vertical) {
                if (_cars[i].GetStartSpace().X == rowColNum) {
                    countCars += 1;
                }
            } else {
                if (_cars[i].GetStartSpace().Y == rowColNum) {
                    countCars += 1;
                }
            }
        }

        return countCars;
    }

    public Car GetCar(int carIndex) {
        return _cars[carIndex];
    }

    public override string ToString() {
        int numCars = _cars.Count();

        string printStr = "";

        int[,] array = new int[_boardWidth, _boardWidth];

        for (int i = 0; i < numCars; i++) {
            Car car = _cars[i];
            
            List<GridSpace> occupiedSpaces = car.GetOccupiedSpaces();
            int printNum = i + 1;

            // TODO: figure out why this is throwing an out of bounds exception
            // Maybe it has to do with length checking and illegal moves?
            for (int j = 0; j < occupiedSpaces.Count(); j++) {
                array[occupiedSpaces[j].X, occupiedSpaces[j].Y] = printNum; 
            }
        }
        
        for (int y = 0; y < _boardWidth; y++) {
            for (int x = 0; x < _boardWidth; x++) {
                if (array[x, y] == 0) {
                    printStr += "0";
                } else {
                    printStr += (Convert.ToChar(array[x, y] + 64));
                }
                
            }
            printStr += "\n";
        }

        return printStr;
    }



    public static bool operator ==(Board b1, Board b2) {
        // Return false if either is null
        // This doesn't follow conventions of null comparisons.. oh well
        if (b1 is null | b2 is null) {
            return false;
        }

        // If either board has a different number of cars, theydon't match
        if (b1.CountCars() != b2.CountCars()) {
            return false;
        }

        // Want to compare all of the cars
        for (int i = 0; i < b1.CountCars(); i++) {
            if (b1.GetCar(i) != b2.GetCar(i)) {
                return false;
            }
        }
        return true;
    }

    public static bool operator !=(Board b1, Board b2) {
        if (b1 is null | b2 is null) {
            return true;
        }

        if (b1.CountCars() != b2.CountCars()) {
            return true;
        }

        int matchCount = 0;
        for (int i = 0; i < b1.CountCars(); i++) {
            if (b1.GetCar(i) == b2.GetCar(i)) {
                matchCount += 1;
            }
        }

        return matchCount == b1.CountCars();
    }

    public bool Equals(Board b1) {
        if (this is null | b1 is null) {
            return false;
        }

        if (b1.CountCars() != this.CountCars()) {
            return false;
        }

        for (int i = 0; i < b1._cars.Count(); i++) {
            if (b1.GetCar(i) != this.GetCar(i)) {
                return false;
            }
        }
        return true;
    }

    public static bool Equals(Board b1, Board b2) {
        if (b1 is null | b2 is null) {
            return false;
        }

        if (b1.CountCars() != b2.CountCars()) {
            return false;
        }

        for (int i = 0; i < b1._cars.Count(); i++) {
            if (b1.GetCar(i) != b2.GetCar(i)) {
                return false;
            }
        }
        return true;
    }

    public override int GetHashCode() {
        return this.ToString().GetHashCode();
    }
}
