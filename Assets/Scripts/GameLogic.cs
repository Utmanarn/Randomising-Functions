using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Http;

public class GameLogic : MonoBehaviour
{
    private DiceHand _playerHand;
    [SerializeField] private Button _rollButton;
    [SerializeField] private TMP_Dropdown _selectScoreMenu;

    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;

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

        ChangeText(itemIndex); 
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

    private void ChangeText(int itemIndex)
    {
        if (_playerHand.playerOnesTurn() == true)
        {
            switch (itemIndex)
            {
                case 0:
                    Transform ones = playerOne.transform.Find("Ones");
                    ones.GetComponent<TMP_Text>().text = "Ones: " + diceScore;
                    break;
                case 1:
                    Transform twos = playerOne.transform.Find("Twos");
                    twos.GetComponent<TMP_Text>().text = "Twos: " + diceScore;
                    break;
                case 2:
                    Transform threes = playerOne.transform.Find("Threes");
                    threes.GetComponent<TMP_Text>().text = "Threes: " + diceScore;
                    break;
                case 3:
                    Transform fours = playerOne.transform.Find("Fours");
                    fours.GetComponent<TMP_Text>().text = "Fours: " + diceScore;
                    break;
                case 4:
                    Transform fives = playerOne.transform.Find("Fives");
                    fives.GetComponent<TMP_Text>().text = "Fives: " + diceScore;
                    break;
                case 5:
                    Transform sixes = playerOne.transform.Find("Sixes");
                    sixes.GetComponent<TMP_Text>().text = "Sixes: " + diceScore;
                    break;
                case 6:
                    Transform onePair = playerOne.transform.Find("One Pair");
                    onePair.GetComponent<TMP_Text>().text = "One Pair: " + diceScore;
                    break;
                case 7:
                    Transform twoPairs = playerOne.transform.Find("Two Pairs");
                    twoPairs.GetComponent<TMP_Text>().text = "Two Pairs: " + diceScore;
                    break;
                case 8:
                    Transform threeofakind = playerOne.transform.Find("Three of a kind");
                    threeofakind.GetComponent<TMP_Text>().text = "Three of a kind: " + diceScore;
                    break;
                case 9:
                    Transform fourofakind = playerOne.transform.Find("Four of a kind");
                    fourofakind.GetComponent<TMP_Text>().text = "Four of a kind: " + diceScore;
                    break;
                case 10:
                    Transform smallStraight = playerOne.transform.Find("Small Straight");
                    smallStraight.GetComponent<TMP_Text>().text = "Small Straight: " + diceScore;
                    break;
                case 11:
                    Transform largeStraight = playerOne.transform.Find("Large Straight");
                    largeStraight.GetComponent<TMP_Text>().text = "Large Straight: " + diceScore;
                    break;
                case 12:
                    Transform fullHouse = playerOne.transform.Find("Full House");
                    fullHouse.GetComponent<TMP_Text>().text = "Full House: " + diceScore;
                    break;
                case 13:
                    Transform chance = playerOne.transform.Find("Chance");
                    chance.GetComponent<TMP_Text>().text = "Chance: " + diceScore;
                    break;
                case 14:
                    Transform yatzy = playerOne.transform.Find("Yatzy");
                    yatzy.GetComponent<TMP_Text>().text = "Yatzy: " + diceScore;
                    break;
            }
        }

        if (_playerHand.playerOnesTurn() == false)
        {
            switch (itemIndex)
            {
                case 0:
                    Transform ones = playerTwo.transform.Find("Ones");
                    ones.GetComponent<TMP_Text>().text = "Ones: " + diceScore;
                    break;
                case 1:
                    Transform twos = playerTwo.transform.Find("Twos");
                    twos.GetComponent<TMP_Text>().text = "Twos: " + diceScore;
                    break;
                case 2:
                    Transform threes = playerTwo.transform.Find("Threes");
                    threes.GetComponent<TMP_Text>().text = "Threes: " + diceScore;
                    break;
                case 3:
                    Transform fours = playerTwo.transform.Find("Fours");
                    fours.GetComponent<TMP_Text>().text = "Fours: " + diceScore;
                    break;
                case 4:
                    Transform fives = playerTwo.transform.Find("Fives");
                    fives.GetComponent<TMP_Text>().text = "Fives: " + diceScore;
                    break;
                case 5:
                    Transform sixes = playerTwo.transform.Find("Sixes");
                    sixes.GetComponent<TMP_Text>().text = "Sixes: " + diceScore;
                    break;
                case 6:
                    Transform onePair = playerTwo.transform.Find("One Pair");
                    onePair.GetComponent<TMP_Text>().text = "One Pair: " + diceScore;
                    break;
                case 7:
                    Transform twoPairs = playerTwo.transform.Find("Two Pairs");
                    twoPairs.GetComponent<TMP_Text>().text = "Two Pairs: " + diceScore;
                    break;
                case 8:
                    Transform threeofakind = playerTwo.transform.Find("Three of a kind");
                    threeofakind.GetComponent<TMP_Text>().text = "Three of a kind: " + diceScore;
                    break;
                case 9:
                    Transform fourofakind = playerTwo.transform.Find("Four of a kind");
                    fourofakind.GetComponent<TMP_Text>().text = "Four of a kind: " + diceScore;
                    break;
                case 10:
                    Transform smallStraight = playerTwo.transform.Find("Small Straight");
                    smallStraight.GetComponent<TMP_Text>().text = "Small Straight: " + diceScore;
                    break;
                case 11:
                    Transform largeStraight = playerTwo.transform.Find("Large Straight");
                    largeStraight.GetComponent<TMP_Text>().text = "Large Straight: " + diceScore;
                    break;
                case 12:
                    Transform fullHouse = playerTwo.transform.Find("Full House");
                    fullHouse.GetComponent<TMP_Text>().text = "Full House: " + diceScore;
                    break;
                case 13:
                    Transform chance = playerTwo.transform.Find("Chance");
                    chance.GetComponent<TMP_Text>().text = "Chance: " + diceScore;
                    break;
                case 14:
                    Transform yatzy = playerTwo.transform.Find("Yatzy");
                    yatzy.GetComponent<TMP_Text>().text = "Yatzy: " + diceScore;
                    break;
            }
        }
    }

    public void DebugWriteSelectedToConsole()
    {
        Debug.Log("Dice list contains the current selected dice: " + _diceSelected[0] + " " + _diceSelected[1] + " " + _diceSelected[2] + " " + _diceSelected[3] + " " + _diceSelected[4]);
    }
}
