using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI textSeconds;
    [SerializeField] private TextMeshProUGUI textMinutes;
    [SerializeField] private TextMeshProUGUI textscore;
    [SerializeField] private TextMeshProUGUI textLifes;
    [SerializeField] private TextMeshProUGUI textCountdown;

    private float seconds;
    private float timer = 3;

    private int minutes;
    private readonly int limitSeconds = 59;

    private void Initialization()
    {
        gameManager = GameManager.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI();
    }

    private void ShowUI()
    {
        seconds += Time.deltaTime;
        textSeconds.text = seconds.ToString("00");
        textMinutes.text = minutes.ToString();
        textscore.text = gameManager.score.ToString();
        textLifes.text = gameManager.life.ToString();

        if(seconds >= limitSeconds)
        {
            minutes++;
            gameManager.difficulty += 0.3f;
            seconds = 0 + 1;
        }

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            textCountdown.text = Mathf.RoundToInt(timer).ToString();
        }
        else
        {
            textCountdown.enabled = false;
        }
    }

    public void PauseGame()
    {
        if (!gameManager.isPaused)
        {
            gameManager.isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (gameManager.isPaused)
        {
            gameManager.isPaused = false;
        }
    }
}
