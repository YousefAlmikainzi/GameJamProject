using UnityEngine;
using System.Collections;

public class SimpleWaveTimeline : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject villagerPrefab;
    [SerializeField] GameObject soldierPrefab;
    [SerializeField] GameObject heroPrefab;

    [Header("Counts")]
    [SerializeField] int villagersCount = 10;
    [SerializeField] int soldiersCount = 20;

    [Header("Timing (seconds)")]
    [SerializeField] float villagerInterval = 1f;   // 1s between villagers
    [SerializeField] float soldierInterval = 1f;   // 1s between soldiers
    [SerializeField] float soldiersStartAt = 30f;  // start soldiers at t=30s
    [SerializeField] float heroAt = 60f;  // spawn hero at t=60s

    [Header("Spawn")]
    [SerializeField] float spawnRadius = 0f;        // 0 = exact spawner position

    void Start()
    {
        StartCoroutine(SpawnVillagersPhase());
        StartCoroutine(SpawnSoldiersPhase());
        StartCoroutine(SpawnHeroPhase());
    }

    IEnumerator SpawnVillagersPhase()
    {
        for (int i = 0; i < villagersCount; i++)
        {
            Spawn(villagerPrefab);
            yield return new WaitForSeconds(villagerInterval);
        }
    }

    IEnumerator SpawnSoldiersPhase()
    {
        yield return new WaitForSeconds(soldiersStartAt);
        for (int i = 0; i < soldiersCount; i++)
        {
            Spawn(soldierPrefab);
            yield return new WaitForSeconds(soldierInterval);
        }
    }

    IEnumerator SpawnHeroPhase()
    {
        yield return new WaitForSeconds(heroAt);
        Spawn(heroPrefab);
    }

    void Spawn(GameObject prefab)
    {
        if (!prefab) return;
        Vector3 basePos = transform.position;
        if (spawnRadius > 0f)
        {
            Vector2 off = Random.insideUnitCircle * spawnRadius;
            basePos += new Vector3(off.x, 0f, off.y);
        }
        Instantiate(prefab, basePos, Quaternion.identity);
    }
}
