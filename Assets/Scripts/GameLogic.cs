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
                foreach (var dice in _playerHand.DiceList())
                {
                    if (dice == 1)
                    {
                        diceScore += 1;
                    }
                }
                break;
            case 7:
                // TWO PAIRS
                for (int i = 0; i < _playerHand.DiceList().Length; i++)
                {
                    if (_playerHand.DiceListIndex(0) == 1)
                    {

                    }
                }
                foreach (var dice in _playerHand.DiceList())
                {
                    if (dice == 1)
                    {
                        diceScore += 1;
                    }
                }
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
                foreach (var dice in _playerHand.DiceList())
                {
                    if (dice == 1)
                    {
                        diceScore += 1;
                    }
                }
                break;
            case 11:
                // LARGE STRAIGHT
                foreach (var dice in _playerHand.DiceList())
                {
                    if (dice == 1)
                    {
                        diceScore += 1;
                    }
                }
                break;
            case 12:
                // FULL HOUSE
                foreach (var dice in _playerHand.DiceList())
                {
                    if (dice == 1)
                    {
                        diceScore += 1;
                    }
                }
                break;
            case 13:
                // CHANCE
                foreach (var dice in _playerHand.DiceList())
                {
                    if (dice == 1)
                    {
                        diceScore += 1;
                    }
                }
                break;
            case 14:
                // YATZY
                //if (_playerHand.DiceListIndex(0) == _playerHand.DiceListIndex(1) == _playerHand.DiceListIndex(2) == _playerHand.DiceListIndex(3) == _playerHand.DiceListIndex(4))
                {

                }
                break;
            }
        }

    public void DebugWriteSelectedToConsole()
    {
        Debug.Log("Dice list contains the current selected dice: " + _diceSelected[0] + " " + _diceSelected[1] + " " + _diceSelected[2] + " " + _diceSelected[3] + " " + _diceSelected[4]);
    }
}
