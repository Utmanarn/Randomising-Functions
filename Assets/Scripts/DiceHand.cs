using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.UI;
using UnityEngine.UI;

public class DiceHand : MonoBehaviour
{
    private PRNGAlgorithms _pRNGAlgorithms;
    private int[] _dice;
    private List<int> reRoll;
    private int numberOfRolls;

    [SerializeField] private bool _debugLogging;
    [SerializeField] private bool _debugTesting;

    [SerializeField] private Button dice1;
    [SerializeField] private Button dice2;
    [SerializeField] private Button dice3;
    [SerializeField] private Button dice4;
    [SerializeField] private Button dice5;

    private void Awake()
    {
        _pRNGAlgorithms = GetComponent<PRNGAlgorithms>();

        _dice = new int[5];
        reRoll = new List<int>();
        numberOfRolls = 3;
    }

    private void Start()
    {
        if (!_debugTesting) return;
        RollAllDice();
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

        numberOfRolls--;

        if(numberOfRolls < 1)
        {
            numberOfRolls = 3;
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

        if (_debugLogging)
        {
            Debug.Log("The dice numbers are: " + _dice[0] + " " + _dice[1] + " " + _dice[2] + " " + _dice[3] + " " + _dice[4]);
        }
    }

    private void RollSelectedDice(List<int> dicePositionsToRoll)
    {
        if (_debugLogging) Debug.Log("Dice Positions to Roll lenght: " + dicePositionsToRoll.Count);

        int[] randomNumbersRolled = _pRNGAlgorithms.ChooseAlgorithm(dicePositionsToRoll.Count);

        for (int i = 0; i < dicePositionsToRoll.Count; i++)
        {
            _dice[dicePositionsToRoll[i]] = randomNumbersRolled[i];

            if (_debugLogging) Debug.Log("Dice position " + dicePositionsToRoll[i] + " is being re-rolled.");
        }
        

        if (_debugLogging)
        {
            Debug.Log("The dice numbers are: " + _dice[0] + " " + _dice[1] + " " + _dice[2] + " " + _dice[3] + " " + _dice[4]);
        }

        dice1.gameObject.GetComponent<Image>().color = Color.white;
        dice2.gameObject.GetComponent<Image>().color = Color.white;
        dice3.gameObject.GetComponent<Image>().color = Color.white;
        dice4.gameObject.GetComponent<Image>().color = Color.white;
        dice5.gameObject.GetComponent<Image>().color = Color.white;
        
        reRoll.Clear();
    }
}
