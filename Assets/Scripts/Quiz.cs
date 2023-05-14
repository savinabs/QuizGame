using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField]
    int numberOfQuestions;

    [SerializeField]
    List<ListCountries> listOfLists;
    readonly string[] continents = { "Africa", "Asia", "Antarctica", "Australia", "Europe", "North America", "South America" };

    [SerializeField]
    List<CountrySO> countries = new();
    CountrySO currentCountry;

    [SerializeField]
    TextMeshProUGUI questionText;

    [SerializeField]
    Image flag;

    [Header("Answers")]
    [SerializeField]
    List<GameObject> answerButtons;
    int correctAnswerIndex;
    bool answeredEarly = true;

    [Header("Button Sprites")]
    [SerializeField]
    Sprite defaultSprite;
    [SerializeField]
    Sprite correctSprite;
    [SerializeField]
    Sprite inactiveSprite;

    [Header("Timer")]
    [SerializeField] Image timerSprite;
    Timer timer;

    [Header("Progress")]
    [SerializeField]
    Slider progress;

    [Header("Score")]
    [SerializeField]
    TextMeshProUGUI scoreText;
    Score score;

    [Header("Pause Timer")]
    [SerializeField]
    Sprite pauseSprite;
    [SerializeField]
    Sprite resumeSprite;
    [SerializeField]
    Button buttonToPause;

    [Header("Hint")]
    [SerializeField]
    Button hint;

    [HideInInspector]
    public bool isComplete;

    List<string> random;

    void Awake()
    {
        timer = FindAnyObjectByType<Timer>();
        score = FindAnyObjectByType<Score>();

        if (numberOfQuestions > NumberOfCountriesInList() || numberOfQuestions <=0 ) numberOfQuestions = NumberOfCountriesInList();


        int rand1, rand2;
        for (int k = 0; k < Mathf.Abs(numberOfQuestions); k++)
        {
            rand1 = Random.Range(0, listOfLists.Count);
            rand2 = Random.Range(0, listOfLists[rand1].countries.Count);
            countries.Add(listOfLists[rand1].countries[rand2]);

        }
        /* if (SceneManager.GetActiveScene().name == "Continents")
         {
             int rand1, rand2;
             for (int k = 0; k < Mathf.Abs(numberOfQuestions); k++)
             {
                 rand1 = Random.Range(0, listOfLists.Count);
                 rand2 = Random.Range(0, listOfLists[rand1].countries.Count);
                 countries.Add(listOfLists[rand1].countries[rand2]);

             }
         }
         else
         {
             for (int i = 0; i < listOfLists.Count; i++)
             {
                 for (int j = 0; j < listOfLists[i].countries.Count; j++)
                     countries.Add(listOfLists[i].countries[j]);
             }
         }*/
        progress.maxValue = countries.Count;
        progress.value = 0;
    }

    void Update()
    {
        timerSprite.fillAmount = timer.fill;

        if (timer.loadNextQuestion)
        {
            if (progress.value == progress.maxValue)
            {
                isComplete = true;
                return;
            }
            answeredEarly = false;
            NextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!answeredEarly && !timer.answeringQuestion)
        {
            IsAnswerCorrect(-1);
            SetButtonInteractability(false);
        }

    }

    void GetRandomCountry()
    {
        int index = Random.Range(0, countries.Count);
        currentCountry = countries[index];
    }

    void GenerateRandomAnswers()
    {
        random = new List<string>();
        int rand;
        int randList;
        int randContinent;
        while (random.Count < 3)
        {
            //rand = Random.Range(0, listCountries.countries.Count);
            randList = Random.Range(0, listOfLists.Count);
            rand = Random.Range(0, listOfLists[randList].countries.Count);
            randContinent = Random.Range(0, continents.Length);

            if (SceneManager.GetActiveScene().name.Contains("flags"))
            {
                if (!random.Contains(listOfLists[randList].countries[rand].GetCountryName()) && listOfLists[randList].countries[rand].GetCountryName() != currentCountry.GetCountryName())
                {
                    random.Add(listOfLists[randList].countries[rand].GetCountryName());
                }
            }
            else if (SceneManager.GetActiveScene().name.Contains("currency"))
            {
                if (!random.Contains(listOfLists[randList].countries[rand].GetCountryCurrency()) && listOfLists[randList].countries[rand].GetCountryCurrency() != currentCountry.GetCountryCurrency())
                {
                    random.Add(listOfLists[randList].countries[rand].GetCountryCurrency());
                }
            }
            else if (SceneManager.GetActiveScene().name.Contains("Continents"))
            {
                if (!random.Contains(continents[randContinent]) && continents[randContinent] != currentCountry.GetCountryContinent())
                {
                    random.Add(continents[randContinent]);
                }
            }
            else
            {
                //if (!random.Contains(listCountries.countries[rand].GetCountryCapital()) && rand != countries.IndexOf(currentCountry))
                if (!random.Contains(listOfLists[randList].countries[rand].GetCountryCapital()) && listOfLists[randList].countries[rand].GetCountryCapital() != currentCountry.GetCountryCapital())
                {
                    random.Add(listOfLists[randList].countries[rand].GetCountryCapital());
                }
            }
        }
    }

    void DisplayCountry()
    {
        GenerateRandomAnswers();
        int tmp = 0;

        TextMeshProUGUI answerButtonsText = new();

        correctAnswerIndex = Random.Range(0, answerButtons.Count);

        if (SceneManager.GetActiveScene().name.Contains("flags"))
        {
            questionText.text = "Which countrie's flag is this?";
            flag.sprite = currentCountry.GetCountryFlag();

            for (int i = 0; i < answerButtons.Count; i++)
            {
                answerButtonsText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (i == correctAnswerIndex)
                {
                    answerButtonsText.text = currentCountry.GetCountryName();
                }
                else
                {
                    answerButtonsText.text = random[tmp];
                    tmp++;
                }
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("currency"))
        {
            questionText.text = "What is the currency of " + currentCountry.GetCountryName();

            for (int i = 0; i < answerButtons.Count; i++)
            {
                answerButtonsText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (i == correctAnswerIndex)
                {
                    answerButtonsText.text = currentCountry.GetCountryCurrency();
                }
                else
                {
                    answerButtonsText.text = random[tmp];
                    tmp++;
                }
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("Continents"))
        {
            questionText.text = "What continent is " + currentCountry.GetCountryName() + " part of?";

            for (int i = 0; i < answerButtons.Count; i++)
            {
                answerButtonsText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (i == correctAnswerIndex)
                {
                    answerButtonsText.text = currentCountry.GetCountryContinent();
                }
                else
                {
                    answerButtonsText.text = random[tmp];
                    tmp++;
                }
            }
        }
        else
        {
            questionText.text = "What is the capital of " + currentCountry.GetCountryName();

            for (int i = 0; i < answerButtons.Count; i++)
            {
                answerButtonsText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (i == correctAnswerIndex)
                {
                    answerButtonsText.text = currentCountry.GetCountryCapital();
                }
                else
                {
                    answerButtonsText.text = random[tmp];
                    tmp++;
                }
            }
        }
    }

    void NextQuestion()
    {
        if (countries.Count > 0)
        {
            SetButtonInteractability(true);
            SetHintButtonInteractability(true);
            SetDefaultSpriteForButtons();
            GetRandomCountry();
            DisplayCountry();
            score.IncrementSeenQuestions();
        }
    }

    public void OnAnswerSelected(int index)
    {
        answeredEarly = true;
        IsAnswerCorrect(index);
        SetButtonInteractability(false);
        SetHintButtonInteractability(false);
        timer.SetTimerToNull();
        scoreText.text = "Score: " + score.CalculateFinalScore() + "%";
    }

    void IsAnswerCorrect(int index)
    {
        Image spriteButton;
        if (index == correctAnswerIndex)
        {
            questionText.text = "Bravo!";
            spriteButton = answerButtons[index].GetComponent<Image>();
            spriteButton.sprite = correctSprite;
            progress.value++;
            if (countries.Contains(currentCountry)) countries.Remove(currentCountry);
            score.IncrementNumberOfCorrectAnswers();
        }
        else
        {

            if (SceneManager.GetActiveScene().name.Contains("flags"))
            {
                questionText.text = "Sorry! The correct answer is " + currentCountry.GetCountryName();
            }
            else if (SceneManager.GetActiveScene().name.Contains("currency"))
            {
                questionText.text = "Sorry! The correct answer is " + currentCountry.GetCountryCurrency();
            }
            else if (SceneManager.GetActiveScene().name.Contains("Continents"))
            {
                questionText.text = "Sorry! The correct answer is " + currentCountry.GetCountryContinent();
            }
            else
            {
                questionText.text = "Sorry! The correct answer is " + currentCountry.GetCountryCapital();
            }

            spriteButton = answerButtons[correctAnswerIndex].GetComponent<Image>();
            spriteButton.sprite = correctSprite;
            progress.value++;
            if (countries.Contains(currentCountry)) countries.Remove(currentCountry);

        }
    }

    void SetButtonInteractability(bool isInteractable)
    {
        Button tmp;
        foreach (GameObject i in answerButtons)
        {
            tmp = i.GetComponent<Button>();
            tmp.interactable = isInteractable;
        }
    }

    void SetDefaultSpriteForButtons()
    {
        Image spriteButton;
        foreach (GameObject i in answerButtons)
        {
            spriteButton = i.GetComponent<Image>();
            spriteButton.sprite = defaultSprite;
        }
    }

    public void PauseOrResumeTimer()
    {
        timer.SetIsPause();
        if (timer.GetIsPaused())
        {
            buttonToPause.image.sprite = resumeSprite;
        }
        else
        {
            buttonToPause.image.sprite = pauseSprite;
        }
    }

    public void UseHint()
    {
        Button tmp;
        int rand1;
        for (int i = 0; i < ((int)answerButtons.Count / 2); i++)
        {
            rand1 = Random.Range(0, answerButtons.Count);
            while (rand1 == correctAnswerIndex || answerButtons[rand1].GetComponent<Button>().interactable == false)
            {
                rand1 = Random.Range(0, answerButtons.Count);
            }
            tmp = answerButtons[rand1].GetComponent<Button>();
            tmp.interactable = false;
            tmp.image.sprite = inactiveSprite;
        }
        SetHintButtonInteractability(false);
    }

    void SetHintButtonInteractability(bool isInteractable)
    {
        hint.interactable = isInteractable;
    }

    int NumberOfCountriesInList()
    {
        int tmp=0;
        for (int i = 0; i < listOfLists.Count; i++)
        {
            for (int j = 0; j < listOfLists[i].countries.Count; j++)
                tmp++;
        }
        return tmp;
    }
}
