using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    long seed1;
    long seed2;

    long seed3;

    // Start is called before the first frame update
    void Start()
    {
        seed1 = DateTime.Now.Ticks;
        seed2 = DateTime.Now.Millisecond;

        string shortenSeed = seed1.ToString().Substring(8);

        seed3 = long.Parse(shortenSeed);

        Debug.Log(seed1);
        Debug.Log(seed2);
        Debug.Log(seed3);

        GetRandomLinearCongruential();
        GetRandomMiddleSquare();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetRandomLinearCongruential()
    {
        long seed = seed3;

        //Needs the following parameters, which can be found on Wikipedia for different implementations
        //https://en.wikipedia.org/wiki/Linear_congruential_generator
        //multiplier
        long a = 1103515245;
        //increment
        long c = 12345;
        //modulus m (which is also the maximum possible random value)
        long m = (long)Mathf.Pow(2f, 31f);

        //To display the random numbers when the loop is finished
        string allRandomNumbers = "";

        //How many random numbers do we want to generate?
        int amountOfNumbers = 5;

        //Array that will store the random numbers so we can display them
        float[] randomNumbers = new float[amountOfNumbers];

        for (int i = 0; i < amountOfNumbers; i++)
        {
            //Basic idea: seed = (a * seed + c) mod m
            seed = (a * seed + c) % m;

            //To get a value between 0 and 1
            //float randomValue = seed / (float)m;
            float randomValue = Mathf.Floor((seed / (float)m) * 10);

            //Remove this line if you want to speed up the testing of the algorithm
            allRandomNumbers += randomValue + " ";

            randomNumbers[i] = randomValue;
        }


        Debug.Log(allRandomNumbers);
    }

    void GetRandomMiddleSquare()
    {
        //We cant use the regular int32, which max value is 2147483647
        //So we have to use long, which max value is 9,223,372,036,854,775,807
        long seed = seed3;

        //How many digits in the seed?
        int digits = seed.ToString().Length;
        Debug.Log(digits);

        //To display the random numbers when the loop is finished
        string allRandomNumbers = "";

        //How many random numbers do we want to generate?
        int amountOfNumbers = 5;

        //Array that will store the random numbers so we can display them
        float[] randomNumbers = new float[amountOfNumbers];

        for (int i = 0; i < amountOfNumbers; i++)
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
            //float divisor = (Mathf.Pow(10f, digits)) - 1f;
            float divisor = (Mathf.Pow(10f, digits - 1)) - 1f;

            float randomValue = Mathf.Floor(seed / divisor);

            //Remove this line if you want to speed up the testing of the algorithm
            allRandomNumbers += randomValue + " ";

            randomNumbers[i] = randomValue;
        }

        Debug.Log(allRandomNumbers);
    }
}
