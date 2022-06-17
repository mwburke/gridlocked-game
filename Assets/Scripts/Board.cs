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
        if (cars == null) {
            _cars = new();
        } else {
            _cars = cars;
        }
        
    }

    public List<GridSpace> GetOccupiedSpaces() {
        List<GridSpace> occupiedSpaces = new();
        foreach (Car car in _cars) {
            // TODO: find out what's causing this to error
            occupiedSpaces = (List<GridSpace>)occupiedSpaces.Concat(car.GetOccupiedSpaces()).ToList();
            // occupiedSpaces = occupiedSpaces.Concat(car.GetOccupiedSpaces()).ToList();
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

    // TODO: review this and see if we should swap to passing in car object?
    // TODO: right now, this doesn't include length? 
    public List<GridSpace> GetAvailableMoves(int carIndex) {
        List<GridSpace> availableMoves = new();

        Car car = _cars[carIndex];
        Orientation orientation = car.GetOrientation();
        GridSpace startSpace = car.GetStartSpace();
        int length = car.GetLength();

        int carStart;
        int carEnd;

        // Set starting point to check from
        // Either X or Y since the car can only move in one direction 
        // depending on the orientation
        if (orientation == Orientation.Horizontal) {
            carStart = startSpace.X;
            carEnd = startSpace.X + car.GetLength() - 1;
        } else {
            carStart = startSpace.Y;
            carEnd = startSpace.Y + car.GetLength() - 1;
        }

        List<GridSpace> occupiedSpaces = new();
        List<GridSpace> freeSpaces = new();

        // Find all available slots within the row/column
        if (orientation == Orientation.Horizontal) {
            occupiedSpaces = GetOccupiedSpacesRow(startSpace.Y);
            
            for (int i = 0; i < _boardWidth; i++) {
                GridSpace tempSpace = new GridSpace(i, startSpace.Y);
                if (!occupiedSpaces.Contains(tempSpace)) {
                    freeSpaces.Add(tempSpace);
                }
            }
        }

        // Check prior to car
        bool hasAvailableSpace = true;
        int checkIndex = carStart - 1;
        while (checkIndex > 0 && hasAvailableSpace) {
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
        checkIndex = carEnd + 1;
        while (checkIndex < _boardWidth && hasAvailableSpace) {
            GridSpace tempSpace;

            // TODO: modify this to account for length, check multiple spaces?
            if (orientation == Orientation.Horizontal) {
                tempSpace = new(checkIndex, startSpace.Y);
            } else {
                tempSpace = new(startSpace.X, checkIndex);
            }

            if (freeSpaces.Contains(tempSpace)) {
                // Actual start move space
                GridSpace startMoveSpace;
                if (orientation == Orientation.Horizontal) {
                    startMoveSpace = new(checkIndex - (length - 1), startSpace.Y);
                } else {
                    startMoveSpace = new(startSpace.X, checkIndex - (length - 1));
                }

                // TODO: double check this is right
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

        return true;
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

    public void LogBoardConsole() {
        List<GridSpace> occupiedSpaces = GetOccupiedSpaces();
        List<GridSpace> goalCarSpaces = occupiedSpaces.Where((item, index) => index < 2).ToList();
        occupiedSpaces = occupiedSpaces.Skip(2).ToList();

        string printStr = "";

        for (int y = 0; y < _boardWidth; y++) {
            for (int x = 0; x < _boardWidth; x++) {
                GridSpace tempSpace = new(x, y);
                if (goalCarSpaces.Contains(tempSpace)) {
                    printStr += "X";
                } else if (occupiedSpaces.Contains(tempSpace)) {
                    printStr += "O";
                } else {
                    printStr += "-";
                }
            }
            printStr += "\n";
        }

        Debug.Log(printStr);
    }
}
