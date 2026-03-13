using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI PlayerScore;
    [SerializeField] private TextMeshProUGUI MasterScore;
    LeaderboardManager leaderboardManager;
    private int playerScore;
    private int masterScore;
    private int starsCount;
    

    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    //[SerializeField] private GameObject draw;

    void Start()
    {
        

        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        masterScore = PlayerPrefs.GetInt("MasterScore", 0);
        PlayerScore.text = playerScore.ToString();        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (playerScore > masterScore)
        {
            win.SetActive(true);
        }
        if (playerScore < masterScore)
        {
            lose.SetActive(true);
        }
        /*if (playerScore == masterScore)
        {
            draw.SetActive(true);
        }*/
    }

    public void WinGame()
    {
        win.SetActive(true);
       
    }
    public void LoseGame()
    {
        lose.SetActive(true);
       
    }
    /*public void DrawGame()
    {
        draw.SetActive(true); draw şu an oyunda yok, eklenebilir.
    }*/
}
