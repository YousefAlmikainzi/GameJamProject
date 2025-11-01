using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform player;
    [SerializeField] EnemyType enemyType;

    [Header("Firing")]
    [SerializeField] float fireInterval = 1.5f;
    [SerializeField] float projectileSpeed = 3f;

    [Header("Spawn Offset (local XZ)")]
    [SerializeField] Vector2 offsetXZ = new Vector2(0f, 0.5f);

    [Header("Projectile Lifetime")]
    [SerializeField] float projectileLifetime = 2f;

    float nextFireTime;
    EnemyDetectionApproach detector;

    void Awake()
    {
        detector = GetComponent<EnemyDetectionApproach>();
    }

    void Update()
    {
        if (!projectilePrefab || !player) return;
        if (!detector || !detector.PlayerInRange) return;
        if (!detector.AtContact) return;

        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireInterval;

        Vector3 toPlayer = (player.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(toPlayer, Vector3.up);
        Vector3 spawnPos = transform.position + rot * new Vector3(offsetXZ.x, 0f, offsetXZ.y);

        GameObject go = Instantiate(projectilePrefab, spawnPos, rot);

        var dpt = go.GetComponent<DamagePlayerOnTrigger>();
        if (dpt) dpt.damage = enemyType ? enemyType.damageToPlayer : 1;

        var fire = go.GetComponent<FireAttack>();
        if (fire)
        {
            fire.Launch(projectileSpeed, gameObject);
            if (projectileLifetime > 0f) Destroy(go, projectileLifetime);
        }
    }
}
