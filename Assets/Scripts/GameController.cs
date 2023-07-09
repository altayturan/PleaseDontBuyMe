using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameData _gameData;

    [SerializeField] private GameObject failScreen;
    [SerializeField] private GameObject winScreen;
    [FormerlySerializedAs("startPosition")] [SerializeField] private Transform startTransform;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private GameObject pauseScreen;
    


    private void Start()
    {
        RestartGame();
    }


    private void Update()
    {
        if (_gameData.loseGame)
        {
            LoseGame();
        }

        if (_gameData.winGame)
        {
            WinGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }


    public IEnumerator UpdateTimerData()
    {
        while (_gameData.gameTime > 0)
        {
            _gameData.gameTime--;
            UpdateTimerText();
            yield return new WaitForSeconds(1);
        }

        _gameData.loseGame = true;
    }


    private void UpdateTimerText()
    {
        var minute = (_gameData.gameTime / 60).ToString("00");
        var seconds = (_gameData.gameTime % 60).ToString("00");
        
        timerText.text = $"{minute}:{seconds}";
    }


    public void RestartGame()
    {
        Time.timeScale = 1;
        _gameData.winGame = false;
        _gameData.loseGame = false;
        _gameData.gameTime = 500;
        StopAllCoroutines();
        StartCoroutine(UpdateTimerData());
        player.transform.position = startTransform.position;
        player.transform.rotation = startTransform.rotation;
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
        failScreen.SetActive(false);
        winScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    public void LoseGame()
    {
        if(!failScreen.activeSelf) failScreen.SetActive(true);
    }

    public void WinGame()
    {
        if(!winScreen.activeSelf) winScreen.SetActive(true);
    }

    public void PauseGame()
    {
        if(!pauseScreen.activeSelf) pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}