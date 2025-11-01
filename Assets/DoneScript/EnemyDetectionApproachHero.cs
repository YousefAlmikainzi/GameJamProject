using UnityEngine;

public class EnemyDetectionApproachHero : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float radius = 5f;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip inRangeClip;
    [SerializeField] AudioClip originalClip;

    bool switched;

    void Awake()
    {
        if (!originalClip && audioSource) originalClip = audioSource.clip;
    }

    void Update()
    {
        if (!player || !audioSource) return;

        Vector3 c = transform.position;
        Vector2 d = new Vector2(player.transform.position.x - c.x, player.transform.position.z - c.z);
        bool inRange = Vector2.Dot(d, d) <= radius * radius;

        if (inRange && !switched && inRangeClip)
        {
            audioSource.clip = inRangeClip;
            audioSource.Play();
            switched = true;
        }
    }

    void OnDestroy()
    {
        if (audioSource && originalClip)
        {
            audioSource.clip = originalClip;
            audioSource.Play();
        }
    }
}
