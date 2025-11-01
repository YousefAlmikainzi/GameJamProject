using UnityEngine;

public class EnemyDetectionRunAway : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float radius = 2.0f;
    [SerializeField] float movementSpeed = 2.0f;

    [SerializeField] Material insideRadiusMaterial;
    [SerializeField] Material originalMaterial;

    [SerializeField] float fleeDuration = 5f;

    Renderer rend;
    float fleeTimer = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend && originalMaterial) rend.material = originalMaterial;
    }

    void Update()
    {
        if (!player) return;

        Vector3 playerPos = player.transform.position;
        Vector3 ownPos = transform.position;

        Vector3 difference = playerPos - ownPos;
        Vector2 d2 = new Vector2(difference.x, difference.z);

        float dist2 = Vector2.Dot(d2, d2);
        float r2 = radius * radius;

        if (dist2 < r2) fleeTimer = fleeDuration;

        if (fleeTimer > 0f)
        {
            fleeTimer -= Time.deltaTime;

            if (rend && insideRadiusMaterial) rend.material = insideRadiusMaterial;

            float mag = Mathf.Sqrt(dist2);
            if (mag > Mathf.Epsilon)
            {
                Vector2 away2 = -d2 / mag;
                Vector3 away = new Vector3(away2.x, 0f, away2.y);
                transform.position += away * movementSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (rend && originalMaterial) rend.material = originalMaterial;
        }
    }
}
