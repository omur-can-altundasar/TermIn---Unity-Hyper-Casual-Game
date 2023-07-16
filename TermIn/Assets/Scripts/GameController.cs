using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel, moveJoystick, rotateJoystick;
    [SerializeField] TextMeshProUGUI time, killScore, highscoreText;
    private int currentTime, newScore, highScore;

    void Start()
    {
        Time.timeScale = 1.0f; //Game over sonrasý durdurulan zaman tekrar aktifleþtirilir.
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    void Update()
    {
        TheTime();
    }

    private void TheTime()
    {   /*timeSinceLevelLoad, sahne baþladðýnda zaman 0'dan saymaya baþlar. 40'dan geriye saymak için 40'tan timeSinceLevelLoad çýkarýlýr.*/

        currentTime = 60 - (int)Time.timeSinceLevelLoad;
        time.text = "Time: " + currentTime.ToString();
        /*Süre bittiðinde Game Over metotu çaðýrýlýr. */
        if (currentTime < 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {   /*Oyun bittiðinde ekranda gözükmesi veya gözükmemesi gereken ayarlarý barýndýran metot.*/
        gameOverPanel.SetActive(true);
        moveJoystick.SetActive(false);
        rotateJoystick.SetActive(false);
        time.enabled = false;
        killScore.enabled = false;
        Time.timeScale = 0;
        ScoreCompare();
    }
    public void Restart()
    {   /*Oyun tekrar baþlar*/
        SceneManager.LoadScene(0); 
    }
    public void ScoreAdd()
    {   /*Player skor artýrýr*/
        newScore++;
        killScore.text = newScore.ToString();
    }
    private void ScoreCompare()
    {   /*Þu anki en yüksek skor ile bir önceki en yüksek skor karþýlaþtýrýlarak en yüksek skor deðeri kaydedilir.*/
        if (newScore > PlayerPrefs.GetInt("HighScore"))
        {
            highScore = newScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            highscoreText.text = "Highscore: " + newScore.ToString();
        }
        else
        {
            highscoreText.text = "Highscore: " + highScore.ToString();
        }
    }

}
