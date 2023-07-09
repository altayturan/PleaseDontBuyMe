using Pathfinding;
using System.Collections;
using UnityEngine;

public class EnemyCatcher : MonoBehaviour
{
    public float coneAngle = 45f;
    public float coneDistance = 10f;

    void Update()
    {
        Vector3 coneDirection = transform.forward;

        Quaternion coneRotation = Quaternion.Euler(-coneAngle / 2f, 0f, 0f);

        for (float i = 0f; i <= coneAngle; i += 0.5f)
        {
            Vector3 direction = coneRotation * coneDirection;

            Ray ray = new(transform.position, direction);
            Debug.DrawRay(transform.position, direction * coneDistance, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit, coneDistance))
            {
                if (hit.collider.gameObject.TryGetComponent<StealthAndDetection>(out var stealthAndDetection))
                {
                    AIDestinationSetter.Instance.target = hit.collider.gameObject.transform;
                    stealthAndDetection.StartDetection();
                    StartCoroutine(TimerToGoToNextRandomTarget());
                }
            }
        }
    }

    private IEnumerator TimerToGoToNextRandomTarget()
    {
        yield return new WaitForSeconds(5);
        AIDestinationSetter.Instance.SetTargetToNextRandom();
    }
}
