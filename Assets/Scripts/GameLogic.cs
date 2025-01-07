using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLogic : MonoBehaviour
{
    private DiceHand _playerHand;
    [SerializeField] private Button _rollButton;
    [SerializeField] private TMP_Dropdown _selectScoreMenu;

    private bool[] _diceSelected;

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

    public void SelectScore() // TODO: Change this to use the ClickHandler that was implemented on every item in the dropdown!
    {
        //int value = _selectScoreMenu.value;

        //_selectScoreMenu.options.RemoveAt(value);
    }

    public void DebugWriteSelectedToConsole()
    {
        Debug.Log("Dice list contains the current selected dice: " + _diceSelected[0] + " " + _diceSelected[1] + " " + _diceSelected[2] + " " + _diceSelected[3] + " " + _diceSelected[4]);
    }
}
