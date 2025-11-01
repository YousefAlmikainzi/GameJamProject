using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int health;

    RectTransform greenBar;
    TextMeshProUGUI healthText;
    float bgWidth;
    float bgHeight;
    bool cached;

    void Awake() { CacheRefs(); }

    void Start()
    {
        SetHealth(health);
    }

    void CacheRefs()
    {
        if (cached) return;
        healthText = transform.Find("Canvas/HpText")?.GetComponent<TextMeshProUGUI>();
        greenBar = transform.Find("Canvas/HP")?.GetComponent<RectTransform>();
        RectTransform bg = transform.Find("Canvas/BG")?.GetComponent<RectTransform>();
        if (bg != null)
        {
            bgWidth = bg.sizeDelta.x;
            bgHeight = bg.sizeDelta.y;
        }
        cached = (healthText != null && greenBar != null && bg != null);
    }

    public void SetHealth(int newHealth)
    {
        if (!cached) CacheRefs();          
        if (!cached) return;               

        health = Mathf.Clamp(newHealth, 0, maxHealth);
        healthText.text = health + "/" + maxHealth;

        float ratio = (maxHealth > 0) ? (float)health / maxHealth : 0f;
        float barWidth = ratio * bgWidth;
        greenBar.sizeDelta = new Vector2(barWidth, bgHeight);
    }

    public void TakeDamage(int damage) { SetHealth(health - damage); }
    public void GainHealth(int increaseBy) { SetHealth(health + increaseBy); }
}
