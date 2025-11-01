using UnityEngine;

public class YourAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float fireCooldown = 0.1f;

    float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireCooldown;

            GameObject go = Instantiate(projectilePrefab, transform.position, transform.rotation);

            FireAttack proj = go.GetComponent<FireAttack>();
            if (proj != null)
                proj.Launch(projectileSpeed, gameObject);
        }
    }
}
