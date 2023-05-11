using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Answers")]
    [SerializeField]
    List<GameObject> answerButtons;



    public void OpenNewScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
