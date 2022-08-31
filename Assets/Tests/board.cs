using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class board {

    [Test]
    public void board_checks_win_condition_true_0() {
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



    [Test]
    public void board_checks_string_equal() {
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

        Assert.IsTrue(test_board_1.ToString() == test_board_2.ToString());
    }


    [Test]
    public void board_checks_string_not_equal() {
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

        Assert.IsTrue(test_board_1.ToString() != test_board_2.ToString());
    }


    [Test]
    public void board_checks_hashcode_equal() {
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

        Assert.IsTrue(test_board_1.GetHashCode() == test_board_2.GetHashCode());
    }


    [Test]
    public void board_checks_hashcode_not_equal() {
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

        Assert.IsTrue(test_board_1.GetHashCode() != test_board_2.GetHashCode());
    }


    [Test]
    public void board_checks_get_occupied_spaces_row_multiple() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        List<GridSpace> occupiedSpaces = new List<GridSpace> {
            new GridSpace(0, 2),
            new GridSpace(1, 2),
            new GridSpace(4, 2),
        };

        Assert.IsTrue(test_board.GetOccupiedSpacesRow(2).SequenceEqual(occupiedSpaces));
    }

    [Test]
    public void board_checks_get_occupied_spaces_row_none() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Assert.IsTrue(test_board.GetOccupiedSpacesRow(0).Count == 0);
    }

    [Test]
    public void board_checks_get_occupied_spaces_col_multiple() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        List<GridSpace> occupiedSpaces = new List<GridSpace> {
            new GridSpace(0, 2),
            new GridSpace(0, 3),
            new GridSpace(0, 4)
        };

        Assert.IsTrue(test_board.GetOccupiedSpacesCol(0).SequenceEqual(occupiedSpaces));
    }

    [Test]
    public void board_checks_get_occupied_spaces_col_none() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Assert.IsTrue(test_board.GetOccupiedSpacesCol(5).Count == 0);
    }

    [Test]
    public void board_checks_not_gridlocked() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        Assert.IsFalse(test_board.IsGridlocked());
    }

    [Test]
    public void board_checks_is_gridlocked() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Regular, 3, new GridSpace(0, 0), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(0, 1), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(0, 3), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(0, 4), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(0, 5), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(3, 0), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(3, 1), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(3, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(3, 3), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(3, 4), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(3, 5), Orientation.Horizontal, Color.red),
        });

        Assert.IsTrue(test_board.IsGridlocked());
    }


    [Test]
    public void board_checks_move_car_0() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        GridSpace newStartSpace = new GridSpace(1, 2);
        GridSpace newStartSpaceCopy = new GridSpace(1, 2);

        test_board.MoveCar(
            0,
            newStartSpace
        );

        Assert.IsTrue(test_board.GetCar(0).GetStartSpace() == newStartSpaceCopy);
    }

    [Test]
    public void board_checks_move_car_1() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        GridSpace newStartSpace = new GridSpace(1, 2);

        test_board.MoveCar(
            0,
            newStartSpace
        );

        List<GridSpace> newOccupiedSpaces = new List<GridSpace> {
           new GridSpace(1, 2),
           new GridSpace(2, 2)
        };

        Assert.IsTrue(test_board.GetCar(0).GetOccupiedSpaces().SequenceEqual(newOccupiedSpaces));
    }

    [Test]
    public void board_checks_get_available_moves_no_obstructions_vertical_0() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        List<GridSpace> actualAvailableMoves = new List<GridSpace> {
            new GridSpace(4, 0),
            new GridSpace(4, 2),
            new GridSpace(4, 3)
        };

        List<GridSpace> carMoves = test_board.GetAvailableMoves(1);

        Assert.IsTrue(carMoves.SequenceEqual(actualAvailableMoves));
    }

    [Test]
    public void board_checks_get_available_moves_no_obstructions_vertical_1() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 0), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green)
        });

        List<GridSpace> actualAvailableMoves = new List<GridSpace> {
            new GridSpace(4, 1),
            new GridSpace(4, 2),
            new GridSpace(4, 3)
        };

        List<GridSpace> carMoves = test_board.GetAvailableMoves(1);

        Assert.IsTrue(carMoves.SequenceEqual(actualAvailableMoves));
    }

    [Test]
    public void board_checks_get_available_moves_no_obstructions_horizontal_0() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green),
            new Car(CarType.Goal, 2, new GridSpace(0, 5), Orientation.Horizontal, Color.cyan),
        });

        List<GridSpace> actualAvailableMoves = new List<GridSpace> {
            new GridSpace(1, 5),    
            new GridSpace(2, 5),
            new GridSpace(3, 5),
            new GridSpace(4, 5)
        };

        List<GridSpace> carMoves = test_board.GetAvailableMoves(3);

        Assert.IsTrue(carMoves.SequenceEqual(actualAvailableMoves));
    }

    [Test]
    public void board_checks_get_available_moves_no_obstructions_horizontal_1() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green),
            new Car(CarType.Goal, 2, new GridSpace(1, 5), Orientation.Horizontal, Color.cyan),
        });

        List<GridSpace> actualAvailableMoves = new List<GridSpace> {
            new GridSpace(0, 5),
            new GridSpace(2, 5),
            new GridSpace(3, 5),
            new GridSpace(4, 5)
        };

        List<GridSpace> carMoves = test_board.GetAvailableMoves(3);

        Assert.IsTrue(carMoves.SequenceEqual(actualAvailableMoves));
    }

    [Test]
    public void board_checks_get_available_moves_no_obstructions_horizontal_2() {
        Board test_board = new(
        6,
        new GridSpace(5, 2),
        new List<Car> {
            new Car(CarType.Goal, 2, new GridSpace(0, 2), Orientation.Horizontal, Color.red),
            new Car(CarType.Regular, 3, new GridSpace(4, 1), Orientation.Vertical, Color.blue),
            new Car(CarType.Regular, 2, new GridSpace(0, 3), Orientation.Vertical, Color.green),
            new Car(CarType.Goal, 2, new GridSpace(4, 5), Orientation.Horizontal, Color.cyan),
        });

        List<GridSpace> actualAvailableMoves = new List<GridSpace> {
            new GridSpace(3, 5),
            new GridSpace(2, 5),
            new GridSpace(1, 5),
            new GridSpace(0, 5),
            
        };

        List<GridSpace> carMoves = test_board.GetAvailableMoves(3);

        Assert.IsTrue(carMoves.SequenceEqual(actualAvailableMoves));
    }

}