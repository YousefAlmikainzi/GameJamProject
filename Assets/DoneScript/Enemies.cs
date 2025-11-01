using UnityEngine;

public interface IDamageable { void TakeDamage(int amount); }

public class Enemies : MonoBehaviour, IDamageable
{
    [SerializeField] EnemyType type;
    int hp;

    void Awake()
    {
        hp = type.maxHealth;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.GetComponentInParent<FireAttack>() != null)
            TakeDamage(1);
    }
}
