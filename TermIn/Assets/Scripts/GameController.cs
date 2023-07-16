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
        Time.timeScale = 1.0f; //Game over sonras� durdurulan zaman tekrar aktifle�tirilir.
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    void Update()
    {
        TheTime();
    }

    private void TheTime()
    {   /*timeSinceLevelLoad, sahne ba�lad��nda zaman 0'dan saymaya ba�lar. 40'dan geriye saymak i�in 40'tan timeSinceLevelLoad ��kar�l�r.*/

        currentTime = 60 - (int)Time.timeSinceLevelLoad;
        time.text = "Time: " + currentTime.ToString();
        /*S�re bitti�inde Game Over metotu �a��r�l�r. */
        if (currentTime < 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {   /*Oyun bitti�inde ekranda g�z�kmesi veya g�z�kmemesi gereken ayarlar� bar�nd�ran metot.*/
        gameOverPanel.SetActive(true);
        moveJoystick.SetActive(false);
        rotateJoystick.SetActive(false);
        time.enabled = false;
        killScore.enabled = false;
        Time.timeScale = 0;
        ScoreCompare();
    }
    public void Restart()
    {   /*Oyun tekrar ba�lar*/
        SceneManager.LoadScene(0); 
    }
    public void ScoreAdd()
    {   /*Player skor art�r�r*/
        newScore++;
        killScore.text = newScore.ToString();
    }
    private void ScoreCompare()
    {   /*�u anki en y�ksek skor ile bir �nceki en y�ksek skor kar��la�t�r�larak en y�ksek skor de�eri kaydedilir.*/
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
