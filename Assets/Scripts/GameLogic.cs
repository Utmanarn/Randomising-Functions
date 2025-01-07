using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private DiceHand _playerHand;
    [SerializeField] private Button _rollButton;

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

    public void DebugWriteSelectedToConsole()
    {
        Debug.Log("Dice list contains the current selected dice: " + _diceSelected[0] + " " + _diceSelected[1] + " " + _diceSelected[2] + " " + _diceSelected[3] + " " + _diceSelected[4]);
    }
}
