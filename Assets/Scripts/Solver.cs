using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Solver {

    protected Board _board;
    protected Dictionary<Board, int> _minBoardMoves = new();
    protected int _maxDepth;
    protected int _maxVisits;
    protected int _nodeVisits;
    protected List<Solution> _solutions = new();


    protected Solver(Board board, int maxDepth = 15, int maxVisits = 1000) {
        _board = board;
        _maxDepth = maxDepth;
        _maxVisits = maxVisits;
    }

    public abstract Solution Solve();

    public abstract void VisitNode(Node node);
    public abstract List<Node> FindPossibleNodes(Node node, int priorMoveCar);
}

 
public class BFSSolver : Solver {

    private Queue<Node> _queue;

    public BFSSolver(Board board, int maxDepth, int maxVisits) : base(board, maxDepth, maxVisits) {
    }

    public override Solution Solve() {
        
        // Initialize queue with FindPossibleNodes on base board wihtout prior moves
        // While queue is not empty, visit nodes in queue in order
        _nodeVisits = 0;
        _queue = new Queue<Node>(FindInitialMoves());
        
        while (_queue.Count > 0 && _nodeVisits < _maxVisits) {
            Node node = _queue.Dequeue();
            VisitNode(node);
        }

        // After completing all visits, review solutions
        if (_solutions.Count < 1) {
            // TODO: decide if none, return null or empty list
            Debug.Log("Number of solutions found: " + _solutions.Count.ToString());
            return null;
        } else if (_solutions.Count == 1) {
            return _solutions[0];
        } else {
            // Returning the best move, first one with lowest value
            // Arbitrarily take the first appearing smallest solution
            int minMoves = 1000;
            Solution minSolution = null;

            Debug.Log("Number of candidate solutions: " + _solutions.Count.ToString());

            foreach(Solution solution in _solutions) {
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

        // Debug.Log("Number of stored boards with moves: " + _minBoardMoves.Count.ToString());
        _nodeVisits += 1;

        // Check if board configuration was seen before:
        Board nodeBoard = node.GetBoard();
        // Debug.Log("Node Depth: " + node.GetDepth().ToString());


        // We only ever up with one of these
        // Maybe we are messing up when updating the boards for new nodes? We need to make sure they're updated properly 

        if (_minBoardMoves.TryGetValue(nodeBoard, out int boardDepth)) {
            // If so and movecount is > than stored one, we can stop going down this tree and do nothing
            if (node.GetDepth() >= boardDepth) {
                Debug.Log("Found prior board with more moves. Stopping search from this node.");
                return;
            } else if (node.GetDepth() < boardDepth) {
                // If so and movecount is <, update the stored depth
                _minBoardMoves[nodeBoard] = node.GetDepth();
            }
        } else {
            // If doesn't exist, add it to the tracker
            // Debug.Log("Adding new board to board/move tracker.");
            _minBoardMoves.Add(nodeBoard, node.GetDepth());
        }

        // Check if board could win, add solution move and add to solutions
        if (nodeBoard.IsWinCondition()) {
            // Debug.Log("Found Solution!");
            _solutions.Add(node.GenerateSolution());
            return;
        }

        // Check if maxdepth has been passed, if so, do nothing
        if (boardDepth > _maxDepth) {
            Debug.Log("Reached maximum depth on node. Stopping search from this node.");
            return;
        }

        // If any of these haven't happened, find all possible nodes and add them to the queue
        List<int> carIndices = node.CarIndices();
        int lastCarIndex = carIndices[^1];

        // TODO: push the min board moves censoring here?
        List<Node> newNodes = FindPossibleNodes(node, lastCarIndex);

        foreach(Node newNode in newNodes) {
            _queue.Enqueue(newNode);
        }
    }

    public override List<Node> FindPossibleNodes(Node node, int priorMoveCar) {
        // Check all cars on the board and all their moves
        // If they weren't the priormovecar, for each of the cars and each of their moves
        // create a new node and add to the queue

        Board board = node.GetBoard();
        List<Node> nodes = new();
        
        for (int i = 0; i < board.CountCars(); i++) {
            if (i != priorMoveCar) {
                List<GridSpace> nodeMoves = board.GetAvailableMoves(i);

                foreach(GridSpace move in nodeMoves) {
                    nodes.Add(node.GenerateNewNode(i, move));
                }
            }
        }

        return nodes;
    }

    public List<Node> FindInitialMoves() {
        Board board = _board;
        List<Node> nodes = new();

        for (int i = 0; i < board.CountCars(); i++) {

            List<GridSpace> nodeMoves = board.GetAvailableMoves(i);

            
            foreach (GridSpace move in nodeMoves) {

                // TODO: check if this is working as expected
                Board tempBoard = board.DeepCopy();
                tempBoard.MoveCar(i, move);

                nodes.Add(new Node(
                    tempBoard,
                    1,
                    i,
                    move
                ));
            }

        }

        return nodes;
    }
}

public class Node {

    private List<int> _allCarMoveIndices = new();
    private List<GridSpace> _allCarMoveSpaces = new();
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

        if (allCarMoveIndices is null) {
            _allCarMoveIndices.Add(carMoveIndex);
        } else {
            _allCarMoveIndices = allCarMoveIndices;
        }

        if (allCarMovespaces is null) {
            _allCarMoveSpaces.Add(moveSpace);
        } else {
            _allCarMoveSpaces = allCarMovespaces;
        }
        
    }

    public Node GenerateNewNode(int carMoveIndex, GridSpace moveSpace) {
        // Utility function to generate new nodes from existing, given the car and the move

        Node node = DeepCopy();
        Board board = node.GetBoard().DeepCopy();

        board.MoveCar(carMoveIndex, moveSpace);

        List<int> allCarMoveIndices = node.CarIndices();
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

    public Solution GenerateSolution() {
        _allCarMoveIndices.Add(0);
        _allCarMoveSpaces.Add(new GridSpace(_board.WinSpace().X, _board.WinSpace().Y));

        return new Solution(_allCarMoveIndices, _allCarMoveSpaces);
    }

    public Board GetBoard() {
        return _board;
    }

    public int GetDepth() {
        return _depth;
    }

    public List<int> CarIndices() {
        return _allCarMoveIndices;
    }

    public List<GridSpace> CarSpaces() {
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

    private List<int> _carIndices;
    private List<GridSpace> _moveSpaces;

    public Solution(List<int> carIndices, List<GridSpace> moveSpaces) {
        _carIndices = carIndices;
        _moveSpaces = moveSpaces;
    }

    public int CountMoves() {
        return _moveSpaces.Count;
    }
}
