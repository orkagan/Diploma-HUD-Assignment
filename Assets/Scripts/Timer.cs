using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text timerText;
    public float time;
    private void Start()
    {
        time = 0f;
        timerText = GetComponent<Text>();
    }
    private void Update()
    {
        if (GameManager.Instance.gameState==GameState.Playing)
        {
            time += Time.deltaTime; //time where 1 = 1 second

            //display time
            int minutes = (int)time / 60;
            int seconds = (int)time - 60 * minutes;
            int milliseconds = (int)(time * 100 - minutes * 6000 - seconds * 100); //milliseconds to two digits (factor of 100, not 1000)
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
    }
}