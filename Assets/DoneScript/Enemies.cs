using UnityEngine;

public interface IDamageable { void TakeDamage(int amount); }

public class Enemies : MonoBehaviour, IDamageable
{
    [SerializeField] EnemyType type;
    [SerializeField] AudioClip deathClip;
    [SerializeField, Range(0f, 1f)] float deathVolume = 1f;
    [SerializeField] AudioSource audioSource;

    [Header("Hit Flash")]
    [SerializeField] Renderer targetRenderer;
    [SerializeField] Color hitColor = new Color(1f, 0f, 0f, 1f);
    [SerializeField] float flashDuration = 0.08f;

    int hp;
    MaterialPropertyBlock mpb;
    string colorProp = "_BaseColor";
    Color originalColor;
    bool flashing;

    void Awake()
    {
        hp = type.maxHealth;

        if (!targetRenderer) targetRenderer = GetComponentInChildren<Renderer>();
        if (!targetRenderer) return;

        var mat = targetRenderer.sharedMaterial;
        colorProp = mat && mat.HasProperty("_BaseColor") ? "_BaseColor" : "_Color";
        originalColor = mat ? mat.GetColor(colorProp) : Color.white;

        mpb = new MaterialPropertyBlock();
        targetRenderer.GetPropertyBlock(mpb);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (targetRenderer && !flashing) StartCoroutine(Flash());
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

    System.Collections.IEnumerator Flash()
    {
        flashing = true;
        targetRenderer.GetPropertyBlock(mpb);
        mpb.SetColor(colorProp, hitColor);
        targetRenderer.SetPropertyBlock(mpb);

        yield return new WaitForSeconds(flashDuration);

        targetRenderer.GetPropertyBlock(mpb);
        mpb.SetColor(colorProp, originalColor);
        targetRenderer.SetPropertyBlock(mpb);
        flashing = false;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.GetComponentInParent<FireAttack>() != null)
            TakeDamage(1);
    }
}
