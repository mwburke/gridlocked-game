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
    [SerializeField] public int maxLongCars = 4;
    [SerializeField] public int maxDepth = 20;
    [SerializeField] public int maxVisits = 1000;

    public Board board;
    public Solver solver;
    public Solution solution;
    public Generator generator;

    //[SerializeField] private Block _blockPrefab;
    //[SerializeField] private SpriteRenderer _boardPrefab;
    //[SerializeField] private float _travelTime = 0.2f;
    //[SerializeField] private GameObject _winScreen;
    //[SerializeField] private GameObject _loseScreen;

    void Start() {
        Debug.Log("Initializing generator");

        generator = new (boardWidth, goalSpace, numCars, shortCarFraction, goalCarColor, carColors, verticalOrientationFraction, boardGenRetries, carPlaceRetries, maxLongCars);

        GenerateAndSolve();
    }

    public void GenerateAndSolve() {
        Debug.Log("Starting generation");

        int boardTries = 0;

        board = generator.GenerateBoard();

        while (board.IsWinCondition() && boardTries < boardGenRetries) {
            board = generator.GenerateBoard();
            boardTries++;
            Debug.Log("Board Tries: " + boardTries.ToString());
        }

        Debug.Log(board.ToString());
        Debug.Log("Completed generation");

        solver = new BFSSolver(board, maxDepth, maxVisits);
        Debug.Log("Starting solver");
        solution = solver.Solve();
        Debug.Log("Completed solution");
        Debug.Log(solution);
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