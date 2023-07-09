using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class StealthAndDetection : MonoBehaviour
{
    private Coroutine _detectionCountdownRoutine;
    [SerializeField] private float detectionPercentage;
    [SerializeField] private float fadeInSpeed;//0.012f
    [SerializeField] private float fadeOutSpeed;//0.0025f
    [SerializeField] private GameController gameController;
    [SerializeField] private bool alreadyDetected;
    [SerializeField] private Renderer renderer;

    [SerializeField] private Slider _slider;

    public void StartDetection()
    {
        if (renderer.sharedMaterial.GetFloat("_Fade") >= 0.5f || alreadyDetected) return;
        alreadyDetected = true;
        _detectionCountdownRoutine = StartCoroutine(DetectionCountdownCoroutine());
    }

    private void Awake()
    {
        renderer.sharedMaterial.EnableKeyword("_Fade");
    }

    private void Update()
    {
        var currentFade = renderer.sharedMaterial.GetFloat("_Fade");
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            currentFade += fadeInSpeed;
            if (currentFade > 1) currentFade = 1f;
            renderer.sharedMaterial.SetFloat("_Fade", currentFade);
        }
        else
        {
            currentFade -= fadeOutSpeed;
            if (currentFade < 0) currentFade = 0f;
            renderer.sharedMaterial.SetFloat("_Fade", currentFade);
        }
    }

    private IEnumerator DetectionCountdownCoroutine()
    {
        while (true)
        {
            if (renderer.sharedMaterial.GetFloat("_Fade") == 0f)
            {
                StopCoroutine(_detectionCountdownRoutine);
                detectionPercentage = 0;
                _slider.value = detectionPercentage;
                alreadyDetected = false;
                break;
            }
            detectionPercentage += 5f;
            _slider.value = detectionPercentage;
            if (detectionPercentage >= 100)
            {
                gameController.LoseGame();
            }
            yield return null;
        }
    }
}