using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textSeconds;
    [SerializeField] private TextMeshProUGUI textMinutes;
    [SerializeField] private TextMeshProUGUI textscore;

    private GameManager gameManager;

    private float seconds;

    private int minutes;
    private int limitSeconds = 59;

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

        if(seconds >= limitSeconds)
        {
            minutes++;
            gameManager.difficulty += 0.7f;
            seconds = 0 + 1;
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
