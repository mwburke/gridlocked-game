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

    [Test]
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

    [Test]
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

    [Test]
    public void board_checks_count_cars() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Goal, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue)
        });

        Assert.IsTrue(test_board.CountCars() == 2);
    }

        [Test]
    public void board_checks_equal_0() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Goal, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue)
        });
        Board test_board_2 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Goal, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue)
        });

        Assert.IsTrue(test_board_1 == test_board_2);
    }

    [Test]
    public void board_checks_equal_1() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });
        Board test_board_2 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Assert.IsTrue(test_board_1 == test_board_2);
    }

    [Test]
    public void board_checks_equal_2() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });
        Board test_board_2 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Assert.IsTrue(test_board_1.Equals(test_board_2));
    }


    [Test]
    public void board_checks_get_car_equals_0() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });
        Board test_board_2 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Assert.IsTrue(test_board_1.GetCar(0) == test_board_2.GetCar(0));
    }

    [Test]
    public void board_checks_get_car_equals_1() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Car tempCar = new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red);

        Assert.IsTrue(test_board_1.GetCar(0) == tempCar);
    }

    [Test]
    public void board_checks_get_car_not_equals_0() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Car tempCar = new Car(CarType.Regular, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red);

        Assert.IsTrue(test_board_1.GetCar(0) != tempCar);
    }



    [Test]
    public void board_checks_not_equal_car_count_diff() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });
        Board test_board_2 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue)
        });

        Assert.IsFalse(test_board_1 == test_board_2);
    }


    [Test]
    public void board_checks_not_equal_comparison_car_count_diff() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });
        Board test_board_2 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue)
        });

        Assert.IsTrue(test_board_1 != test_board_2);
    }

    [Test]
    public void board_checks_not_equal_null_comparison() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });
        Board test_board_2 = null;

        Assert.IsTrue(test_board_1 != test_board_2);
    }

    [Test]
    public void board_checks_not_equal_diff_feature_goal() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Board test_board_2 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Goal, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Assert.IsTrue(test_board_1 != test_board_2);
    }

    [Test]
    public void board_checks_not_equal_diff_feature_start_space() {
        Board test_board_1 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });
        Board test_board_2 = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Goal, 3, new GridSpace(4, 5), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Assert.IsTrue(test_board_1 != test_board_2);
    }


    [Test]
    public void board_checks_occupied_spaces_0() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
                    new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
                    new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
                    new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        List<GridSpace> actualOccupiedSpaces = new List<GridSpace> {
            new GridSpace(0, 2),
            new GridSpace(1, 2),
            new GridSpace(4, 1),
            new GridSpace(4, 2),
            new GridSpace(4, 3),
            new GridSpace(0, 3),
            new GridSpace(0, 4)
        };

        List<GridSpace> boardOccupiedSpaces = test_board.GetOccupiedSpaces();

        Assert.IsTrue(actualOccupiedSpaces.SequenceEqual(boardOccupiedSpaces));
    }


}
