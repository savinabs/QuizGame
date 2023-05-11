using TMPro;
using UnityEngine;

public class FinalScoreScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScore;
    Score score;

    void Awake()
    {
        score = FindObjectOfType<Score>();
    }

    public void ShowFinalScore()
    {
        finalScore.text = "Congratulations!\nYou got a score of " + score.CalculateFinalScore() + "%";
    }
}
