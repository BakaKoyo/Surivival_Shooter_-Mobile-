using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singletons_1<GameManager>
{
    [SerializeField]
    private int ScoreRequired;
    
    [SerializeField]
    private Text ScoreTextRequired;

    [SerializeField]
    private int PlayerScore;

    [SerializeField]
    private Text PlayerScoreText;

    [SerializeField]
    private int PlayerLives;

    [SerializeField]
    private Text PlayerLiveCounter;

    [SerializeField]
    private GameObject WinScreen;
    [SerializeField]
    private GameObject LoseScreen;

    public Camera Cam { get; private set; }
    public GyroscopeInputs GM_Gyro { get; private set; }
    public MobileInputs Input { get; private set; }
    public PlayerController Player { get; private set; }
    public bool IsGameOver { get; private set; }

    EnemyManager enemySpawner;

    protected override void OnAwake()
    {
        base.OnAwake();

        /* Find object of type is expensive and bad */
        Cam = FindObjectOfType<Camera>();
        Player = FindObjectOfType<PlayerController>();
        enemySpawner = GetComponent<EnemyManager>();
    }


    private void Start()
    {
        SetKillsRequired();
        StartGame();
    }

    void SetKillsRequired()
    {
        ScoreTextRequired.text = "  Kills Required: " + ScoreRequired.ToString();
    }

    void StartGame()
    {
        if(enemySpawner != null)
        {
            enemySpawner.StartCoroutine(enemySpawner.StartEnemySpawning());
        }
    }

    public void ActivateWin()
    {
        WinScreen.SetActive(true);
        StopAllCoroutines();
    }

    public void PlayerChangeScore(int Score)
    {
        if (PlayerScore >= ScoreRequired)
        {
            ActivateWin();
        }
        PlayerScore += Score;
        PlayerScoreText.text = "  Kill Confirmed: " + PlayerScore.ToString();
    }

    public void PlayerChangeLives(int LifeChange)
    {
        PlayerLives -= LifeChange;
        PlayerLiveCounter.text = "  HP: " + PlayerLives.ToString();

        if (PlayerLives <= 0)
        {
            GameOver();
            IsGameOver = true;
        }
    }

    public void GameOver()
    {
        LoseScreen.SetActive(true);
        StopAllCoroutines();
    }
    
}
