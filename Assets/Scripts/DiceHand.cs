using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.UI;
using UnityEngine.UI;
using TMPro;

public class DiceHand : MonoBehaviour
{
    private PRNGAlgorithms _pRNGAlgorithms;
    private int[] _dice;
    private List<int> reRoll;
    private int numberOfRolls;

    private bool playerOneTurn;

    [SerializeField] private Button dice1;
    [SerializeField] private Button dice2;
    [SerializeField] private Button dice3;
    [SerializeField] private Button dice4;
    [SerializeField] private Button dice5;

    [SerializeField] private TMP_Text number1;
    [SerializeField] private TMP_Text number2;
    [SerializeField] private TMP_Text number3;
    [SerializeField] private TMP_Text number4;
    [SerializeField] private TMP_Text number5;

    private void Awake()
    {
        _pRNGAlgorithms = GetComponent<PRNGAlgorithms>();

        _dice = new int[5];
        reRoll = new List<int>();
        numberOfRolls = 3;
        playerOneTurn = true;

        Debug.Log("Player Ones's Turn");
    }

    public void Roll()
    {
        if(numberOfRolls < 3)
        {
            RollSelectedDice(reRoll);
        } 
        else
        {
            RollAllDice();
        }

        number1.text = _dice[0].ToString();
        number2.text = _dice[1].ToString();
        number3.text = _dice[2].ToString();
        number4.text = _dice[3].ToString();
        number5.text = _dice[4].ToString();

        Debug.Log("Dice Rolled: " + _dice[0] + " " + _dice[1] + " " + _dice[2] + " " + _dice[3] + " " + _dice[4]);

        numberOfRolls--;

        if(numberOfRolls < 1)
        {
            numberOfRolls = 3;

            if(playerOneTurn == true)
            {
                playerOneTurn = false;

                Debug.Log("Player Two's Turn");
            } 
            else
            {
                playerOneTurn = true;
                Debug.Log("Player Ones's Turn");
            }
        }
    }

    public void reRollDice()
    {
        string clickedButton = EventSystem.current.currentSelectedGameObject.name;

        int dice = int.Parse(clickedButton);

        if(dice == 1)
        {
            dice1.gameObject.GetComponent<Image>().color = Color.gray;
        }
        if (dice == 2)
        {
            dice2.gameObject.GetComponent<Image>().color = Color.gray;
        }
        if (dice == 3)
        {
            dice3.gameObject.GetComponent<Image>().color = Color.gray;
        }
        if (dice == 4)
        {
            dice4.gameObject.GetComponent<Image>().color = Color.gray;
        }
        if (dice == 5)
        {
            dice5.gameObject.GetComponent<Image>().color = Color.gray;
        }

        if (!reRoll.Contains(dice - 1))
        {
            reRoll.Add(dice - 1);
        } 
        else
        {
            if (dice == 1)
            {
                dice1.gameObject.GetComponent<Image>().color = Color.white;
            }
            if (dice == 2)
            {
                dice2.gameObject.GetComponent<Image>().color = Color.white;
            }
            if (dice == 3)
            {
                dice3.gameObject.GetComponent<Image>().color = Color.white;
            }
            if (dice == 4)
            {
                dice4.gameObject.GetComponent<Image>().color = Color.white;
            }
            if (dice == 5)
            {
                dice5.gameObject.GetComponent<Image>().color = Color.white;
            }

            reRoll.Remove(dice - 1);
        }
    }

    private void RollAllDice()
    {
        _dice = _pRNGAlgorithms.ChooseAlgorithm(5);
    }

    private void RollSelectedDice(List<int> dicePositionsToRoll)
    {

        int[] randomNumbersRolled = _pRNGAlgorithms.ChooseAlgorithm(dicePositionsToRoll.Count);

        for (int i = 0; i < dicePositionsToRoll.Count; i++)
        {
            _dice[dicePositionsToRoll[i]] = randomNumbersRolled[i];
        }

        dice1.gameObject.GetComponent<Image>().color = Color.white;
        dice2.gameObject.GetComponent<Image>().color = Color.white;
        dice3.gameObject.GetComponent<Image>().color = Color.white;
        dice4.gameObject.GetComponent<Image>().color = Color.white;
        dice5.gameObject.GetComponent<Image>().color = Color.white;
        
        reRoll.Clear();
    }
}
