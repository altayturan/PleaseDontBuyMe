using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameData _gameData;

    [SerializeField] private GameObject failScreen;
    [FormerlySerializedAs("startPosition")] [SerializeField] private Transform startTransform;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody playerRigidbody;


    private void Start()
    {
        RestartGame();
    }


    private void Update()
    {
        if (_gameData.gameTime <= 0)
        {
            LoseGame();
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
    }


    private void UpdateTimerText()
    {
        var minute = (_gameData.gameTime / 60).ToString("00");
        var seconds = (_gameData.gameTime % 60).ToString("00");
        
        timerText.text = $"{minute}:{seconds}";
    }


    public void RestartGame()
    {
        _gameData.gameTime = 10;
        StopAllCoroutines();
        StartCoroutine(UpdateTimerData());
        player.transform.position = startTransform.position;
        player.transform.rotation = startTransform.rotation;
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
        failScreen.SetActive(false);
    }

    public void LoseGame()
    {
        if(!failScreen.activeSelf) failScreen.SetActive(true);
    }
}