using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Solver {

    protected Board _board;
    protected Dictionary<Board, int> _minBoardMoves = new();
    protected int _maxDepth;
    protected int _maxVisits;
    protected int _nodeVisits;


    protected Solver(Board board, int maxDepth = 15, int maxVisits = 1000) {
        _board = board;
        _maxDepth = maxDepth;
        _maxVisits = maxVisits;
    }

    public abstract Solution Solve();

    public abstract void VisitNode(Node node);
    public abstract List<Node> FindPossibleNodes(Board board, int priorMoveCar);
}


public class BFSSolver : Solver {

    private Queue<Node> _queue;

    public BFSSolver(Board board, int maxDepth, int maxVisits) : base(board, maxDepth, maxVisits) {
    }

    public override Solution Solve() {
        List<Solution> solutions = new();

        // Initialize queue with FindPossibleNodes on base board wihtout prior moves
        // While queue is not empty, visit nodes in queue in order
        _nodeVisits = 0;
        _queue = new Queue<Node>(FindPossibleNodes(_board, _maxDepth));
        

        while (_queue.Count > 0 && _nodeVisits < _maxVisits) {
            Node node = _queue.Dequeue();
            VisitNode(node);
        }

        // After completing all visits, review solutions
        if (solutions.Count != 1) {
            // TODO: decide if none, return null or empty list
            return null;
        } else {
            // Returning the best move, first one with lowest value
            // TODO: decide if this is what we want
            int minMoves = 1000;
            Solution minSolution = null;

            foreach(Solution solution in solutions) {
                int countMoves = solution.CountMoves();
                if (countMoves < minMoves) {
                    minMoves = countMoves;
                    minSolution = solution;
                }
            }

            return minSolution;
        }
    }

    public override void VisitNode(Node node) {
        // Check if board configuration was seen before:
        // If not, continue
        // If so and movecount is >= than stored one, do nothing
        // If so and movecount is <, update stored one, continue adding nodes

        // Check if board could win, add solution move and add to solutions

        // Check if maxdepth has been reached, if so, do nothing
        
        // If any of these haven't happened, find all possible nodes and add them to the queue

    }

    public override List<Node> FindPossibleNodes(Board board, int priorMoveCar) {
        // Check all cars on the board and all their moves
        // If they weren't the priormovecar, for each of the cars and each of their moves
        // create a new node and add to the queue

        List<Node> moves = new();
        


        return moves;
    }
}

public class Node {

    private List<int> _allCarMoveIndices;
    private List<GridSpace> _allCarMoveSpaces;
    private int _depth;
    private Board _board;
    private int _carMoveIndex;
    private GridSpace _moveSpace;

    public Node(Board board, int depth, int carMoveIndex, GridSpace moveSpace, List<int> allCarMoveIndices = null, List<GridSpace> allCarMovespaces = null) {
        _board = board;
        _depth = depth;
        _board = board;
        _carMoveIndex = carMoveIndex;
        _moveSpace = moveSpace;

        if (allCarMoveIndices == null) {
            _allCarMoveIndices.Add(carMoveIndex);
        } else {
            _allCarMoveIndices = allCarMoveIndices;
        }

        if (allCarMovespaces == null) {
            _allCarMoveSpaces.Add(moveSpace);
        } else {
            _allCarMoveSpaces = allCarMovespaces;
        }
        
    }

    public Node GenerateNewNode(int carMoveIndex, GridSpace moveSpace) {
        // Utility function to generate new nodes from existing, given the car and the move

        Node node = DeepCopy();
        // MoveCar(int carIndex, GridSpace moveSpace)
        Board board = node.GetBoard();
        board.MoveCar(carMoveIndex, moveSpace);

        List<int> allCarMoveIndices = node.MoveNums();
        allCarMoveIndices.Add(carMoveIndex);

        List<GridSpace> allCarMoveSpaces = node.CarSpaces();
        allCarMoveSpaces.Add(moveSpace);

        return new Node(
            board,
            _depth + 1,
            carMoveIndex,
            moveSpace,
            allCarMoveIndices,
            allCarMoveSpaces
        );
    }

    private Board GetBoard() {
        return _board;
    }

    private List<int> MoveNums() {
        return _allCarMoveIndices;
    }

    private List<GridSpace> CarSpaces() {
        return _allCarMoveSpaces;
    }

    public Node DeepCopy() {
        return new Node(
            _board,
            _depth,
            _carMoveIndex,
            _moveSpace,
            _allCarMoveIndices,
            _allCarMoveSpaces
        );
    }
}

public class Solution {

    private List<Node> _moves;

    public Solution(List<Node> moves) {
        _moves = moves;
    }

    public int CountMoves() {
        return _moves.Count;
    }
}
