using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    
    [System.Serializable]

    public class DataBank
    {
        public string answerQuestion;
        public string[] FuorAnswer;
        public string qustions;
    }
    public DataBank[] dataBank;

    private int indexQuestions = 0;
    [SerializeField] private Color defaultButtonColor = Color.white;
    [SerializeField] private LeaderboardManager leaderboardManager;


    public Button[] AnswerButtons;

    public string answers;
    public TextMeshProUGUI showQuestions;
    public TextMeshProUGUI timeCount;


    public Image timerAmount;
    [SerializeField] private float timer = 10;


    [SerializeField] private Color green;
    [SerializeField] private Color red;


    [SerializeField] private TextMeshProUGUI PlayerScore;
    [SerializeField] private TextMeshProUGUI MasterScore;
    [SerializeField] private TextMeshProUGUI QuestionCount;

    private int playerScore;
    private int masterScore;
    private int questionCount;
    private int starCount;
    public LeaderboardManager leaderboard;


    public void Start()
    {
        starCount = PlayerPrefs.GetInt("StarCount");
        indexQuestions = 0;
        CheckQuestions();

    }

    private void Update()
    {
        PlayerScore.text = playerScore.ToString();
        MasterScore.text = masterScore.ToString();
        QuestionCount.text = questionCount.ToString();

        timer -= Time.deltaTime;
        timeCount.text= ((int)timer).ToString();
        timerAmount.fillAmount = timer / 10;

        if (timer <= 0)
        {
            Debug.Log("Time Ended!");
        }

        /*if (questionCount >= 5)
        {
            SceneManager.LoadScene("EndGame");
            Stars();
        }*/


    }


    private void CheckQuestions()
    {
        if (indexQuestions >= dataBank.Length)
        {
            // Tüm sorular bitti, oyun sonu
            SceneManager.LoadScene("EndGame");
            Stars();
            return;
        }
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            AnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = dataBank[indexQuestions].FuorAnswer[i];
            AnswerButtons[i].GetComponent<Image>().color = defaultButtonColor;
        }
        showQuestions.text = dataBank[indexQuestions].qustions;
        questionCount++;
    }


    private void CheckAnswers(int check)
    {
        if (answers == dataBank[indexQuestions].answerQuestion)
        {
            timer = 10;
            AnswerButtons[check].GetComponent<Image>().color = green;
            playerScore++;
        }
        else
        {
            AnswerButtons[check].GetComponent<Image>().color = red;
            masterScore++;
        }
        
        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.SetInt("MasterScore", masterScore);
        PlayerPrefs.Save();
        StartCoroutine(waitForNextQuestion(check));
        timer = 10;
    }

    IEnumerator waitForNextQuestion(int check)
    {
        yield return new WaitForSeconds(0.5f);
        indexQuestions++;
        CheckQuestions();

        // Renk sıfırlama CheckQuestions içinde yapılacak
    }

    public void ClickButtons(int check)
    {
        answers = dataBank[indexQuestions].FuorAnswer[check];
        CheckAnswers(check);
    }
    void Stars()
    {
        if (playerScore > masterScore)
        {
            starCount += 8;
        }
        else if (playerScore < masterScore)
        {
            starCount -= 4;
        }
        leaderboardManager.SubmitScore(starCount);
        PlayerPrefs.SetInt("StarCount", starCount);
        PlayerPrefs.Save();
        
        
    }
}
