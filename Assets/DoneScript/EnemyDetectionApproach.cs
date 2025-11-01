using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionApproach : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float radius = 5.0f;
    [SerializeField] float movementSpeed = 2.0f;
    [SerializeField] float playerRadius = 1.0f;

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 ownPos = transform.position;

        Vector3 difference = playerPos - ownPos;
        Vector3 d2 = new Vector2(difference.x, difference.z);

        float dotDistance = Vector2.Dot(d2, d2);
        float r2 = radius * radius;

        if (dotDistance < r2)
        {
            float dist = Mathf.Sqrt(dotDistance);
            if (dist > playerRadius + 1e-4f)
            {
                Vector2 dir2 = d2 / dist;
                float maxStep = movementSpeed * Time.deltaTime;
                float step = Mathf.Min(maxStep, dist - playerRadius);
                Vector3 delta = new Vector3(dir2.x, 0f, dir2.y) * step;
                transform.position += delta;
            }
        }
    }
}
