using UnityEngine;
using System.Collections;

public class SimpleWaveTimeline : MonoBehaviour
{
    [Header("Player + Range")]
    [SerializeField] Transform player;
    [SerializeField] float triggerRadius = 12f;

    [Header("Spawn (edge opposite player)")]
    [SerializeField] float spawnRadius = 6f;
    [SerializeField] float spawnOffsetX = 0f;
    [SerializeField] float spawnOffsetZ = 0f;

    [Header("Prefabs")]
    [SerializeField] GameObject soldierPrefab;
    [SerializeField] GameObject heroPrefab;

    [Header("Counts / Timing (seconds)")]
    [SerializeField] int soldiersCount = 20;
    [SerializeField] float soldierInterval = 1f;
    [SerializeField] float soldiersStartAt = 30f;
    [SerializeField] float heroAt = 60f;

    [Header("Endless Enemy")]
    [SerializeField] GameObject enemyPrefab;      
    [SerializeField] float enemyInterval = 1f;    

    [Header("Audio (switch once on first entry)")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip enterClip;
    bool audioSwitched;

    void Start()
    {
        StartCoroutine(BeginWhenPlayerInside());
    }

    IEnumerator BeginWhenPlayerInside()
    {
        while (!PlayerInside()) yield return null;

        if (!audioSwitched && audioSource && enterClip)
        {
            audioSource.clip = enterClip;
            audioSource.Play();
            audioSwitched = true;
        }

        StartCoroutine(SpawnSoldiersPhase());
        StartCoroutine(SpawnHeroPhase());
        StartCoroutine(SpawnEnemyForeverPhase()); 
    }

    IEnumerator SpawnSoldiersPhase()
    {
        yield return new WaitForSeconds(soldiersStartAt);
        for (int i = 0; i < soldiersCount; i++)
        {
            SpawnAtEdge(soldierPrefab);
            yield return new WaitForSeconds(soldierInterval);
        }
    }

    IEnumerator SpawnHeroPhase()
    {
        yield return new WaitForSeconds(heroAt);
        SpawnAtEdge(heroPrefab);
    }

    IEnumerator SpawnEnemyForeverPhase()
    {
        while (true)
        {
            SpawnAtEdge(enemyPrefab);
            yield return new WaitForSeconds(enemyInterval);
        }
    }

    bool PlayerInside()
    {
        if (!player) return false;
        Vector3 center = transform.position;
        Vector2 d = new Vector2(player.position.x - center.x, player.position.z - center.z);
        return Vector2.Dot(d, d) <= triggerRadius * triggerRadius;
    }

    void SpawnAtEdge(GameObject prefab)
    {
        if (!prefab) return;

        Vector3 spawnCenter = GetSpawnCenter();
        Vector3 pos;

        if (!player || spawnRadius <= 0f)
        {
            pos = spawnCenter;
        }
        else
        {
            Vector3 toPlayer = player.position - spawnCenter;
            toPlayer.y = 0f;

            if (toPlayer.sqrMagnitude < 0.0001f)
            {
                float ang = Random.value * Mathf.PI * 2f;
                Vector3 dir = new Vector3(Mathf.Cos(ang), 0f, Mathf.Sin(ang));
                pos = spawnCenter + dir * spawnRadius;
            }
            else
            {
                Vector3 dirAway = (-toPlayer).normalized;
                pos = spawnCenter + dirAway * spawnRadius;
            }
        }

        Instantiate(prefab, pos, Quaternion.identity);
    }

    Vector3 GetSpawnCenter()
    {
        return transform.TransformPoint(new Vector3(spawnOffsetX, 0f, spawnOffsetZ));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        DrawCircleXZ(transform.position, triggerRadius);

        Vector3 spawnCenter = GetSpawnCenter();
        Gizmos.color = Color.cyan;
        DrawCircleXZ(spawnCenter, spawnRadius);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position + Vector3.up * 0.1f, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(spawnCenter + Vector3.up * 0.1f, 0.1f);

        if (player && spawnRadius > 0f)
        {
            Vector3 toPlayer = player.position - spawnCenter; toPlayer.y = 0f;
            if (toPlayer.sqrMagnitude >= 0.0001f)
            {
                Vector3 dirAway = (-toPlayer).normalized;
                Vector3 edge = spawnCenter + dirAway * spawnRadius;
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(edge + Vector3.up * 0.1f, 0.12f);
                Gizmos.DrawLine(spawnCenter + Vector3.up * 0.1f, edge + Vector3.up * 0.1f);
            }
        }
    }

    void DrawCircleXZ(Vector3 c, float r, int segs = 64)
    {
        if (r <= 0f) return;
        Vector3 prev = c + new Vector3(r, 0f, 0f);
        for (int i = 1; i <= segs; i++)
        {
            float t = (i / (float)segs) * Mathf.PI * 2f;
            Vector3 p = c + new Vector3(Mathf.Cos(t) * r, 0f, Mathf.Sin(t) * r);
            Gizmos.DrawLine(prev, p);
            prev = p;
        }
    }
}
