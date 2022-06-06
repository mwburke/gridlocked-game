using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] public static int _boardWidth = 6;
    [SerializeField] public static GridSpace _goalSpace = new(5, 2);
    [SerializeField] public static GridSpace _carStartSpace = new(0, 2);
    [SerializeField] private Node _nodePrefab;
    [SerializeField] private List<Car> _cars;
    private List<Node> _nodes;

    public Car GoalCar = new(CarType.Goal, 2, _carStartSpace, Orientation.Horizontal, Color.red);

    //[SerializeField] private Block _blockPrefab;
    //[SerializeField] private SpriteRenderer _boardPrefab;
    //[SerializeField] private List<BlockType> _types;
    //[SerializeField] private float _travelTime = 0.2f;
    //[SerializeField] private int _winCondition = 2048;
    //[SerializeField] private GameObject _winScreen;
    //[SerializeField] private GameObject _loseScreen;


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