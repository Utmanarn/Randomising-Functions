using UnityEngine;
using System;
using System.Linq;

public class PRNGAlgorithms : MonoBehaviour
{
    [SerializeField] private Type type = new Type();

    long baseSeed;

    enum Type
    {
        ComputerTicks,
        LinearCongruential,
        MiddleSquare
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public int[] ChooseAlgorithm(int diceToRoll)
    {
        long dateTime = DateTime.Now.Ticks;

        string shortenSeed = dateTime.ToString().Substring(8);

        baseSeed = long.Parse(shortenSeed);

        Debug.Log("Seed: " + baseSeed);

        switch (type)
        {
            case Type.ComputerTicks:
                return ComputerTicksMethod(diceToRoll);
            case Type.LinearCongruential:
                return LinearCongruentialMethod(diceToRoll);
            case Type.MiddleSquare:
                return MiddleSquareMethod(diceToRoll);
        }

        return null;
    }

    public int[] ComputerTicksMethod(int diceToRoll)
    {
        long seed;
        int[] randomNumbers = new int[diceToRoll];
        string allRandomNumbers = "";
        int safetyCount = 0;
     
        do
        {
            seed = DateTime.Now.Ticks;

            for (int i = 0; i < diceToRoll; i++)
            {
                randomNumbers[i] = int.Parse(seed.ToString().Substring(17 - i, 1));
            }

            safetyCount++;

        } while (randomNumbers.Contains(0) || randomNumbers.Contains(7) || randomNumbers.Contains(8) || randomNumbers.Contains(9));
        

        for (int i = 0; i < randomNumbers.Length; i++)
        {
            allRandomNumbers += randomNumbers[i] + " ";
        }

        return randomNumbers;
    }

    public int[] LinearCongruentialMethod(int diceToRoll)
    {
        long seed = baseSeed;

        //multiplier
        long a = 1103515245;
        //increment
        long c = 12345;
        //modulus m (which is also the maximum possible random value)
        long m = (long)Mathf.Pow(2f, 31f);

        //To display the random numbers when the loop is finished
        string allRandomNumbers = "";

        //Array that will store the random numbers so we can display them
        int[] randomNumbers = new int[diceToRoll];

        for (int i = 0; i < diceToRoll; i++)
        {
            int safetyCount = 0;
            do
            {
                //Basic idea: seed = (a * seed + c) mod m
                seed = (a * seed + c) % m;

                //To get a value between 0 and 1
                //float randomValue = seed / (float)m;
                int randomValue = Mathf.RoundToInt(Mathf.Floor((seed / (float)m) * 10));

                randomNumbers[i] = randomValue;
                safetyCount++;
            } while (randomNumbers[i] < 1 || randomNumbers[i] > 6 && safetyCount < 10);
        }

        for (int i = 0; i < randomNumbers.Length; i++)
        {
            allRandomNumbers += randomNumbers[i] + " ";
        }

        return randomNumbers;
    }

    public int[] MiddleSquareMethod(int diceToRoll)
    {
        long seed = baseSeed;

        //How many digits in the seed?
        int digits = seed.ToString().Length;

        //To display the random numbers when the loop is finished
        string allRandomNumbers = "";

        //Array that will store the random numbers so we can display them
        int[] randomNumbers = new int[diceToRoll];

        for (int i = 0; i < diceToRoll; i++)
        {
            int safetyCount = 0;
            do
            {
                //Square the seed
                long seedSqr = seed * seed;

                //Make it a string
                string randomString = seedSqr.ToString();

                //If the string has less than digits * 2 characters, add zeros to the front of the string
                while (randomString.Length < digits * 2)
                {
                    randomString = 0 + randomString;
                }

                //Get the middle characters of this string
                int start = Mathf.FloorToInt(digits / 2);

                string middleCharacters = randomString.Substring(start, digits);

                //The next seed in the next loop is these middle characters
                seed = long.Parse(middleCharacters);

                //If we want a float between 0 and 1 we divide the maximum number with 9999 if we have 4 digits
                float divisor = (Mathf.Pow(10f, digits - 1)) - 1f;

                int randomValue = Mathf.RoundToInt(Mathf.Floor(seed / divisor));

                randomNumbers[i] = randomValue;
                safetyCount++;

            } while (randomNumbers[i] < 1 || randomNumbers[i] > 6 && safetyCount < 10);
        }

        for (int i = 0; i < randomNumbers.Length; i++)
        {
            allRandomNumbers += randomNumbers[i] + " ";
        }

        return randomNumbers;
    }
}
