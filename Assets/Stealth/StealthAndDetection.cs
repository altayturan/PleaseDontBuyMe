using System.Collections;
using UnityEngine;

public class StealthAndDetection : MonoBehaviour
{
    private Coroutine _detectionCountdownRoutine;
    [SerializeField] private float detectionPercentage;
    public void StartDetection()
    {
        _detectionCountdownRoutine = StartCoroutine(DetectionCountdownCoroutine());
    }

    private IEnumerator DetectionCountdownCoroutine()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                StopCoroutine(_detectionCountdownRoutine);
                break;
            }

            detectionPercentage += 5f;
            if (detectionPercentage >= 100)
            {
                //lose
            }
            yield return null;
        }
    }
}