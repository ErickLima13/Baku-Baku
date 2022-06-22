using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textSeconds;
    [SerializeField] private TextMeshProUGUI textMinutes;

    private float seconds;

    private int minutes;
    private int limitSeconds = 59;

    // Start is called before the first frame update
    void Start()
    {
        
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

        if(seconds >= limitSeconds)
        {
            minutes++;
            seconds = 0 + 1;
        }
    }
}
