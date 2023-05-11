using UnityEngine;

public class Score : MonoBehaviour
{
    int numberOfCorrectAnswers = 0;
    int seenQuestions = 0;

    public int GetNumberOfCorrectAnswers()
    {
        return numberOfCorrectAnswers;
    }

    public int GetSeenQuestions()
    {
        return seenQuestions;
    }

    public void IncrementNumberOfCorrectAnswers()
    {
        numberOfCorrectAnswers++;
    }

    public void IncrementSeenQuestions()
    {
        seenQuestions++;
    }

    public int CalculateFinalScore()
    {
        return Mathf.RoundToInt(numberOfCorrectAnswers / ((float)seenQuestions) * 100);
    }
}
