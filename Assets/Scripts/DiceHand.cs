using UnityEngine;

public class DiceHand : MonoBehaviour
{
    private PRNGAlgorithms _pRNGAlgorithms;
    private int[] _dice;

    [SerializeField] private bool _debugLogging;
    [SerializeField] private bool _debugTesting;

    private void Awake()
    {
        _pRNGAlgorithms = GetComponent<PRNGAlgorithms>();
        _dice = new int[5];
    }

    private void Start()
    {
        if (!_debugTesting) return;
        RollAllDice();

        int[] reRoll = { 4, 2, 1 };

        RollSelectDice(reRoll);
    }

    private void RollAllDice()
    {
        _dice = _pRNGAlgorithms.LinearCongruentialMethod(5);

        if (_debugLogging)
        {
            Debug.Log("The dice numbers are: " + _dice[0] + " " + _dice[1] + " " + _dice[2] + " " + _dice[3] + " " + _dice[4]);
        }
    }

    private void RollSelectDice(int[] dicePositionsToRoll)
    {
        if (_debugLogging) Debug.Log("Dice Positions to Roll lenght: " + dicePositionsToRoll.Length);

        int[] randomNumbersRolled = _pRNGAlgorithms.LinearCongruentialMethod(dicePositionsToRoll.Length);

        
        for (int i = 0; i < dicePositionsToRoll.Length; i++)
        {
            _dice[dicePositionsToRoll[i]] = randomNumbersRolled[i];

            if (_debugLogging) Debug.Log("Dice position " + dicePositionsToRoll[i] + " is being re-rolled.");
        }
        

        if (_debugLogging)
        {
            Debug.Log("The dice numbers are: " + _dice[0] + " " + _dice[1] + " " + _dice[2] + " " + _dice[3] + " " + _dice[4]);
        }
    }
}
