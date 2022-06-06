using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private int _boardWidth;
    private GridSpace _goalSpace;
    private int _numCars;
    private float _shortCarFraction;
    private float _verticalOrientationFraction;
    private Car _goalCar;
    private Color _goalCarColor;
    private List<Color> _carColors;
    private int _boardGenRetries;
    private int _carPlaceRetries;

    public Generator(int boardWidth, GridSpace goalSpace, Car goalCar, int numCars, float shortCarFraction, Color goalCarColor, List<Color> carColors, float verticalOrientationFraction, int boardGenRetries, int carPlaceRetries) {
        _boardWidth = boardWidth;
        _goalSpace = goalSpace;
        _numCars = numCars;
        _shortCarFraction = shortCarFraction;
        _verticalOrientationFraction = verticalOrientationFraction;
        _goalCar = goalCar;
        _goalCarColor = goalCarColor;
        _carColors = carColors;
        _boardGenRetries = boardGenRetries;
        _carPlaceRetries = carPlaceRetries;
    }

    public Board GenerateBoard() {
        /* Function to generate a random game board using a set of heuristics and hard rules.
         * Always places goalCar first in board cars, and always in the same starting spot. 
         * Each subsequent car has a random orientation, length and color. 
         * They are generated, and random spaces are chosen on the board until the car would not occupy the space of an existing car.
         * Then the car is added to the board. 
         * This is repeated until the designed number of cars are placed, or if it is not possible, the board is cancelled. 
         * 
         * Params:
         * _boardWidth: integer of spaces for width and height of board, assumes same for both
         * _goalSpace: GridSpace of where the goalCar needs to reach to complete the level
         * _boardGenRetries: integer of how many attempts this function will go through creating boards before it gives up and returns a null board
         * _carPlaceRetries: integer of how many attempts this function will go through attempting to find a place to place a car until it gives up on the board.
         */
        int boardGenTries = 0;
        
        while (boardGenTries < _boardGenRetries) {
            Board board = new(_boardWidth, _goalSpace);
            board.AddCar(_goalCar);

            while (board.CountCars() <= _numCars) {
                Car tempCar = GenerateRandomCar();
                bool carAdded = false;
                int carTries = 0;

                while (!carAdded) {
                    if (IsValidCarAddition(board, tempCar)) {
                        board.AddCar(tempCar);
                        carAdded = true;
                    } else {
                        carTries += 1;
                    }

                    if (carTries >= _carPlaceRetries) {
                        // Something went wrong, just end this and return null
                        Debug.Log("Failed to place car after multiple retries");
                        return null;
                    }
                }
            }

            // If we make it here, then we should have added all of the cars succesfully
            // We can return the board
            return board;
        }

        // Failed to successfully generate board, returning null
        Debug.Log("Failed to generate board after multiple retries");
        return null;
    }

    public Car GenerateRandomCar() {
        CarType cartype = CarType.Regular;
        int length = Random.Range(0f, 1f) < _shortCarFraction ? 2 : 3;
        int x = Random.Range(0, _boardWidth - 1);
        int y = Random.Range(0, _boardWidth - 1);
        GridSpace startSpace = new(x, y);
        Orientation orientation = Random.Range(0f, 1f) < 0.5f ? Orientation.Horizontal : Orientation.Vertical;
        Color color = _carColors[Random.Range(0, _carColors.Count - 1)];

        Car newCar = new(cartype, length, startSpace, orientation, color);

        return newCar;
    }

    public bool IsValidCarAddition(Board board, Car car) {

        Orientation orientation = car.GetOrientation();

        // No other horizontal cars in the goal row
        if (car.GetStartSpace().Y == 2 && orientation == Orientation.Horizontal) {
            return false;
        }

        // Count other cars with the same orientation in the same row/column
        // Only want a maximum of two in any given row/column
        int rowColNum = orientation == Orientation.Horizontal ? car.GetStartSpace().Y : car.GetStartSpace().X;
        int countAlignedCars = board.CountCarsInRowCol(orientation, rowColNum);

        if (countAlignedCars >= 2) {
            return false;
        }

        return true;
    }


}
