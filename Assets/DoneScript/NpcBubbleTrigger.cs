using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NpcBubbleTrigger : MonoBehaviour
{
    [Header("Who to watch")]
    [SerializeField] Transform player;

    [Header("Trigger (X/Z)")]
    [SerializeField] float radius = 5f;         
    [SerializeField] float rearmBuffer = 0.25f;

    [Header("UI Sequence (children on this NPC's Canvas)")]
    [SerializeField] Image firstImage;           
    [SerializeField] float firstDuration = 1.0f;
    [SerializeField] Image secondImage;      
    [SerializeField] float secondDuration = 1.0f;

    bool running = false;
    bool armed = true;

    void Awake()
    {
        if (firstImage) firstImage.gameObject.SetActive(false);
        if (secondImage) secondImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!player) return;

        Vector2 p = new Vector2(player.position.x, player.position.z);
        Vector2 me = new Vector2(transform.position.x, transform.position.z);
        float dist = Vector2.Distance(p, me);

        if (armed && !running && dist <= radius)
        {
            StartCoroutine(ShowSequence());
        }
        else if (!armed && dist > radius + rearmBuffer)
        {
            armed = true;
        }
    }

    IEnumerator ShowSequence()
    {
        running = true;
        armed = false;

        if (firstImage)
        {
            firstImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(firstDuration);
            firstImage.gameObject.SetActive(false);
        }

        if (secondImage)
        {
            secondImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(secondDuration);
            secondImage.gameObject.SetActive(false);
        }

        running = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
