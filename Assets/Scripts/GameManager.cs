using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] public int boardWidth = 6;
    [SerializeField] public GridSpace goalSpace = new(5, 2);
    [SerializeField] public static GridSpace carStartSpace = new(0, 2);
    [SerializeField] public int numCars = 10;
    [SerializeField] public float shortCarFraction = 0.5f;
    [SerializeField] public Color goalCarColor = Color.red;
    private static readonly List<Color> colors = new List<Color> { Color.black, Color.blue, Color.cyan, Color.green, Color.white, Color.yellow, };
    [SerializeField] public List<Color> carColors = colors;
    [SerializeField] public float verticalOrientationFraction = 0.55f;
    [SerializeField] public int boardGenRetries = 50;
    [SerializeField] public int carPlaceRetries = 50;

    public Board board;
    public Generator generator;

    public Car goalCar = new(CarType.Goal, 2, carStartSpace, Orientation.Horizontal, Color.red);

    //[SerializeField] private Block _blockPrefab;
    //[SerializeField] private SpriteRenderer _boardPrefab;
    //[SerializeField] private float _travelTime = 0.2f;
    //[SerializeField] private GameObject _winScreen;
    //[SerializeField] private GameObject _loseScreen;

    void Start() {
        Debug.Log("Initializing generator");

        generator = new (boardWidth, goalSpace, goalCar, numCars, shortCarFraction, goalCarColor, carColors, verticalOrientationFraction, boardGenRetries, carPlaceRetries);

        Debug.Log("Starting generation");
        board = generator.GenerateBoard();
        board.LogBoardConsole();
        Debug.Log("Completed generation");


        // Do I need to create new UI elements for cars and attach the scripts?
    }
}


public enum GameState
{
    GenerateLevel,
    Solving,
    SpawningCars,
    WaitingInput,
    Moving,
    Win,
    Lose
}   