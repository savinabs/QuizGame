using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    FinalScoreScreen finalScoreScreen;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        finalScoreScreen = FindObjectOfType<FinalScoreScreen>();
    }

    void Start()
    {
        quiz.gameObject.SetActive(true);
        finalScoreScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            ActiveFinalScoreScreen();
        }
    }

    void ActiveFinalScoreScreen()
    {
        finalScoreScreen.gameObject.SetActive(true);
        finalScoreScreen.ShowFinalScore();
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
