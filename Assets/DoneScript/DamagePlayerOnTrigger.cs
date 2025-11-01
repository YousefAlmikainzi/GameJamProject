using UnityEngine;

public class DamagePlayerOnTrigger : MonoBehaviour
{
    [SerializeField] public int damage = 1;

    void OnTriggerEnter(Collider other)
    {
        var ph = other.GetComponentInParent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
