using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public string playerTag = "Player";
    public string wallTag = "Wall";
    public float moveSpeed = 5.0f;
    public float detectionDistance = 30.0f;
    public float avoidanceDistance = 2.0f;
    public float rotationSpeed = 5.0f;

    private Transform player;
    private bool avoiding = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    private void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= detectionDistance)
        {
            if (!avoiding)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                if (Physics.Raycast(transform.position, transform.forward, avoidanceDistance))
                {
                    // Wall is detected in front, start avoiding
                    avoiding = true;
                    StartCoroutine(AvoidObstacle());
                }
                else
                {
                    // Move forward
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            // Stop avoiding if player is not nearby
            avoiding = false;
        }
    }

    private IEnumerator AvoidObstacle()
    {
        Vector3 avoidanceDirection = Quaternion.Euler(0, 45, 0) * transform.forward; // Rotate by 45 degrees
        Vector3 targetPosition = transform.position + avoidanceDirection * avoidanceDistance;

        float startTime = Time.time;
        float duration = 1.0f; // Avoidance duration

        while (Time.time - startTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (Time.time - startTime) / duration);
            yield return null;
        }

        avoiding = false;
    }
}
