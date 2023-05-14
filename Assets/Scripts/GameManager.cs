using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Quiz quiz;
    [SerializeField]
    FinalScoreScreen finalScoreScreen;

    void Start()
    {
        if (quiz != null && finalScoreScreen != null)
        {
            quiz.gameObject.SetActive(true);
            finalScoreScreen.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        CloseGame();
        if (quiz != null && finalScoreScreen != null)
        {
            if (quiz.isComplete)
            {
                quiz.gameObject.SetActive(false);
                ActiveFinalScoreScreen();
            }
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

    void CloseGame()
    {
        if (Input.GetKeyDown("escape"))
            Application.Quit();
    }
}
