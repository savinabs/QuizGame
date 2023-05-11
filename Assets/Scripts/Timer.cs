using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float timeToAnswerQuestion = 30f;

    [SerializeField]
    float timeToShowAnswer = 5f;

    float timerValue;

    public bool loadNextQuestion;
    public bool answeringQuestion;
    public float fill;
    bool isPaused = false;


    // Update is called once per frame
    void Update()
    {
        UpdateTimerValue();
    }

    public void SetTimerToNull()
    {
        timerValue = 0f;
    }

    void UpdateTimerValue()
    {
        if (!isPaused)
        {
            timerValue -= Time.deltaTime;

            if (answeringQuestion)
            {
                if (timerValue > 0) fill = timerValue / timeToAnswerQuestion;
                else
                {
                    answeringQuestion = false;
                    timerValue = timeToShowAnswer;
                }
            }
            else
            {
                if (timerValue > 0) fill = timerValue / timeToShowAnswer;
                else
                {
                    answeringQuestion = true;
                    timerValue = timeToAnswerQuestion;
                    loadNextQuestion = true;
                }
            }
        }
    }

    public void SetIsPause()
    {
        isPaused = !isPaused;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }
}
