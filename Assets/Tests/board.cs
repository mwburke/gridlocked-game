using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class board
{

    [Test]
    public void board_checks_win_condition_true_0()
    {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red)
        }
        );

        Assert.IsTrue(test_board.IsWinCondition());
    }

    public void board_checks_win_condition_true_1() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Goal, 2, new GridSpace(4, 3), Orientation.Vertical, Color.blue)
        }
        );

        Assert.IsTrue(test_board.IsWinCondition());
    }

    public void board_checks_win_condition_false() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Goal, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue)
        }
        );

        Assert.IsTrue(!test_board.IsWinCondition());
    }

}
