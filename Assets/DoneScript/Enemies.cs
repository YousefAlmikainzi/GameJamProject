using UnityEngine;

public interface IDamageable { void TakeDamage(int amount); }

public class Enemies : MonoBehaviour, IDamageable
{
    [SerializeField] EnemyType type;
    [SerializeField] AudioClip deathClip;
    [SerializeField, Range(0f, 1f)] float deathVolume = 1f;
    [SerializeField] AudioSource audioSource;

    int hp;

    void Awake()
    {
        hp = type.maxHealth;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            if (deathClip)
            {
                if (audioSource) audioSource.PlayOneShot(deathClip, deathVolume);
                else AudioSource.PlayClipAtPoint(deathClip, transform.position, deathVolume);
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.GetComponentInParent<FireAttack>() != null)
            TakeDamage(1);
    }
}
