using UnityEngine;

public class EnemyDetectionApproach : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float radius = 5.0f;
    [SerializeField] float movementSpeed = 2.0f;
    [SerializeField] float playerRadius = 1.0f;
    [SerializeField] float contactBuffer = 0.15f;

    public bool PlayerInRange { get; private set; }
    public bool AtContact { get; private set; }
    public float CurrentDistance { get; private set; }
    public float ContactRadius => playerRadius;

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 ownPos = transform.position;
        Vector3 difference = playerPos - ownPos;
        Vector2 d2 = new Vector2(difference.x, difference.z);
        float dotDistance = Vector2.Dot(d2, d2);
        float r2 = radius * radius;

        CurrentDistance = Mathf.Sqrt(dotDistance);
        PlayerInRange = dotDistance < r2;
        AtContact = CurrentDistance <= playerRadius + contactBuffer;

        if (PlayerInRange && !AtContact)
        {
            Vector2 dir2 = d2 / CurrentDistance;
            float maxStep = movementSpeed * Time.deltaTime;
            float step = Mathf.Min(maxStep, CurrentDistance - playerRadius);
            Vector3 delta = new Vector3(dir2.x, 0f, dir2.y) * step;
            transform.position += delta;
        }
    }
}
