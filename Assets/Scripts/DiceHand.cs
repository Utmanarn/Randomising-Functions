using UnityEngine;

public class DiceHand : MonoBehaviour
{
    private PRNGAlgorithms _pRNGAlgorithms;
    private int[] dice;

    [SerializeField] private bool _debugLogging;
    [SerializeField] private bool _debugTesting;

    private void Awake()
    {
        _pRNGAlgorithms = GetComponent<PRNGAlgorithms>();
        dice = new int[5];
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
        dice = _pRNGAlgorithms.LinearCongruentialMethod(5);

        if (_debugLogging)
        {
            Debug.Log("The dice numbers are: " + dice[0] + " " + dice[1] + " " + dice[2] + " " + dice[3] + " " + dice[4]);
        }
    }

    private void RollSelectDice(int[] dicePositionsToRoll)
    {
        if (_debugLogging) Debug.Log("Dice Positions to Roll lenght: " + dicePositionsToRoll.Length);

        int[] randomNumbersRolled = _pRNGAlgorithms.LinearCongruentialMethod(dicePositionsToRoll.Length);

        
        for (int i = 0; i < dicePositionsToRoll.Length; i++)
        {
            dice[dicePositionsToRoll[i]] = randomNumbersRolled[i];

            if (_debugLogging) Debug.Log("Dice position " + dicePositionsToRoll[i] + " is being re-rolled.");
        }
        

        if (_debugLogging)
        {
            Debug.Log("The dice numbers are: " + dice[0] + " " + dice[1] + " " + dice[2] + " " + dice[3] + " " + dice[4]);
        }
    }
}
