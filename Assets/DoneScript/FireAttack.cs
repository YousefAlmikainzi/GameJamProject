using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class FireAttack : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void Launch(float speed, GameObject owner)
    {
        var myCol = GetComponent<Collider>();
        if (myCol && owner)
        {
            var ownerCols = owner.GetComponentsInChildren<Collider>();
            foreach (var c in ownerCols) Physics.IgnoreCollision(myCol, c, true);
        }

        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision _)
    {
        Destroy(gameObject);
    }
}
