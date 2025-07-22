using UnityEngine;
using System.Collections.Generic;

public class EquationGenerator : MonoBehaviour
{
    public enum Difficulty { EASY, MEDIUM, HARD}
    public Difficulty difficulty;

    public BV.Range easyRange;
    public BV.Range mediumRange;
    public BV.Range hardRange;

    public int numberOne;
    public int numberTwo;
    public int correctAnswer;
    public List<int> dummyAnswers;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            GenerateMultiplation();
    }

    public void GenerateMultiplation()
    {
        GenerateRandomNumbers();
        correctAnswer = numberOne * numberTwo;
        GenerateDummyAnswers();
        Debug.Log(numberOne + " x " + numberTwo + " = " + correctAnswer);
    }

    public void GenerateAddition()
    {
        GenerateRandomNumbers();
        correctAnswer = numberOne + numberTwo;
        GenerateDummyAnswers();
        Debug.Log(numberOne + " + " + numberTwo + " = " + correctAnswer);
    }

    public void GenerateSubtraction()
    {
        GenerateRandomNumbers();
        correctAnswer = numberOne - numberTwo;
        GenerateDummyAnswers();
        Debug.Log(numberOne + " - " + numberTwo + " = " + correctAnswer);
    }

    public void GenerateDivision()
    {
        GenerateRandomNumbers();
        float tempAnswer = numberOne / numberTwo;
        correctAnswer = Mathf.RoundToInt(tempAnswer);
        GenerateDummyAnswers();
        Debug.Log(numberOne + " / " + numberTwo + " = " + correctAnswer);
    }

    private void GenerateRandomNumbers()
    {
        numberOne = GetRandomNumber();
        numberTwo = GetRandomNumber();
    }

    private int GetRandomNumber()
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                return (int)Random.Range(easyRange.min, easyRange.max);
            case Difficulty.MEDIUM:
                return (int)Random.Range(mediumRange.min, mediumRange.max);
            case Difficulty.HARD:
                return (int)Random.Range(hardRange.min, hardRange.max);
            default:
                return (int)Random.Range(easyRange.min, easyRange.max);
        }
    }

    private void GenerateDummyAnswers()
    {
        for(int i = 0; i < dummyAnswers.Count; i++)
        {
            int dummy;
            do
            {
                dummy = Random.Range(correctAnswer - 10, correctAnswer + 10);
            }
            while (dummy == correctAnswer || dummyAnswers.Contains(dummy));
            dummyAnswers[i] = dummy;
            Debug.Log("Dummy Answer: " + dummyAnswers[i]);
        }
    }
}
