using UnityEngine;

public class YourAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float fireCooldown = 0.1f;
    [SerializeField] AudioClip fireSfx;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] float sfxVolume = 1f;

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

            PlayShotSfx();
        }
    }

    void PlayShotSfx()
    {
        if (!fireSfx) return;

        if (sfxSource)
            sfxSource.PlayOneShot(fireSfx, sfxVolume);
        else
            AudioSource.PlayClipAtPoint(fireSfx, transform.position, sfxVolume);
    }
}
