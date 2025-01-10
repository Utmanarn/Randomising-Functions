using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Http;

public class GameLogic : MonoBehaviour
{
    private DiceHand _playerHand;
    [SerializeField] private Button _rollButton;
    [SerializeField] private TMP_Dropdown _selectScoreMenu;

    private bool[] _diceSelected;

    private int diceScore;
    private int totalScore;

    private void Awake()
    {
        _playerHand = GetComponent<DiceHand>();
        _diceSelected = new bool[5];
    }

    public void Roll()
    {
        _rollButton.interactable = false;
    }

    public void SelectDice(int diceSelected)
    {
        _diceSelected[diceSelected] = !_diceSelected[diceSelected];
    }

    public void SelectScore(int itemIndex) // TODO: Change this to use the ClickHandler that was implemented on every item in the dropdown!
    {
        CalculateDices(itemIndex);

        Debug.Log(diceScore);

        totalScore += diceScore;

        // TODO: Write out the dice score and the total score to the currently players score list!

        diceScore = 0;

        //int value = _selectScoreMenu.value;

        //_selectScoreMenu.options.RemoveAt(value);
    }

    private void CalculateDices(int itemIndex)
    {
        if(itemIndex >= 0 && itemIndex <= 5)
        {
            foreach (var dice in _playerHand.DiceList())
            {
                if (dice == itemIndex + 1)
                {
                    diceScore += itemIndex + 1;
                }
            }
        }

        switch (itemIndex)
        {
            case 6:
                // ONE PAIR
                int score = 0;

                for (int i = 0; i < _playerHand.DiceList().Length; i++)
                {
                    for (int j = 0; j < _playerHand.DiceList().Length; j++)
                    {
                        if (i == j) continue; // We don't want to check a dice with itself.
                        if (_playerHand.DiceList()[i] != _playerHand.DiceList()[j]) continue;

                        int number = _playerHand.DiceList()[i] + _playerHand.DiceList()[j];

                        score = (number > score) ? number : score;
                    }
                }

                diceScore = score;
                break;
            case 7:
                // TWO PAIRS
                score = 0;
                int secondScore = 0;
                int checkedDice1 = -1, checkedDice2 = -1;

                for (int i = 0; i < _playerHand.DiceList().Length; i++)
                {
                    for (int j = 0; j < _playerHand.DiceList().Length; j++)
                    {
                        if (i == j) continue; // We don't want to check a dice with itself.
                        if (_playerHand.DiceList()[i] != _playerHand.DiceList()[j]) continue;

                        int number = _playerHand.DiceList()[i] + _playerHand.DiceList()[j];

                        if (number > score)
                        {
                            checkedDice1 = i;
                            checkedDice2 = j;

                            score = number;
                        }
                    }
                }

                for (int i = 0; i < _playerHand.DiceList().Length; i++)
                {
                    for (int j = 0; j < _playerHand.DiceList().Length; j++)
                    {
                        if (i == j || i == checkedDice1 || i == checkedDice2 || j == checkedDice1 || j == checkedDice2) continue; // We don't want to check a dice with itself.
                        if (_playerHand.DiceList()[i] != _playerHand.DiceList()[j]) continue;

                        int number = _playerHand.DiceList()[i] + _playerHand.DiceList()[j];

                        if (number > secondScore)
                        {
                            secondScore = number;
                        }
                    }
                }

                if (score <= 0 || secondScore <= 0) 
                { 
                    diceScore = 0;
                    return;
                }

                diceScore = score + secondScore;
                break;
            case 8:
                // THREE OF A KIND
                foreach (var dice in _playerHand.DiceList())
                {
                    if (dice == 1)
                    {
                        diceScore += 1;
                    }
                }
                break;
            case 9:
                // FOUR OF A KIND
                foreach (var dice in _playerHand.DiceList())
                {
                    if (dice == 1)
                    {
                        diceScore += 1;
                    }
                }
                break;
            case 10:
                // SMALL STRAIGHT
                int smallStraight = 0;

                foreach (var dice in _playerHand.DiceList())
                {
                    if (dice == 1)
                    {
                        smallStraight++;
                    }
                    else if (dice == 2)
                    {
                        smallStraight++;
                    }
                    else if (dice == 3)
                    {
                        smallStraight++;
                    }
                    else if (dice == 4)
                    {
                        smallStraight++;
                    }
                    else if (dice == 5)
                    {
                        smallStraight++;
                    }

                    if (smallStraight == 5)
                    {
                        diceScore += 15;
                    }
                }
                break;
            case 11:
                // LARGE STRAIGHT
                int largeStraight = 0;

                foreach (var dice in _playerHand.DiceList())
                {
                    if (dice == 2)
                    {
                        largeStraight++;
                    }
                    else if (dice == 3)
                    {
                        largeStraight++;
                    }
                    else if (dice == 4)
                    {
                        largeStraight++;
                    }
                    else if (dice == 5)
                    {
                        largeStraight++;
                    }
                    else if (dice == 6)
                    {
                        largeStraight++;
                    }

                    if (largeStraight == 5)
                    {
                        diceScore += 20;
                    }
                }
                break;
            case 12:
            // FULL HOUSE

            case 13:
                // CHANCE
                foreach (var dice in _playerHand.DiceList())
                {
                    diceScore += dice;
                }
                break;
            case 14:
                // YATZY
                if (_playerHand.DiceList()[0] == _playerHand.DiceList()[1]
                    && _playerHand.DiceList()[1] == _playerHand.DiceList()[2]
                    && _playerHand.DiceList()[2] == _playerHand.DiceList()[3]
                    && _playerHand.DiceList()[3] == _playerHand.DiceList()[4])
                {
                    foreach (var dice in _playerHand.DiceList())
                    {
                        diceScore += dice;
                    }
                }
                break;
        }
    }

    public void DebugWriteSelectedToConsole()
    {
        Debug.Log("Dice list contains the current selected dice: " + _diceSelected[0] + " " + _diceSelected[1] + " " + _diceSelected[2] + " " + _diceSelected[3] + " " + _diceSelected[4]);
    }
}
