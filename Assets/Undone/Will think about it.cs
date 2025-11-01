using UnityEngine;

public class UIWeaponBobPop : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] RectTransform weaponUI;   // assign your image RectTransform

    [Header("Bob")]
    [SerializeField] float ampX = 6f;          // horizontal sway pixels
    [SerializeField] float ampY = 4f;          // vertical bob pixels
    [SerializeField] float hz = 6f;          // steps per second at full speed
    [SerializeField] float smooth = 12f;       // lerp back smoothness

    [Header("Pop")]
    [SerializeField] float punchScale = 0.04f; // +4% scale on step
    [SerializeField] float punchTime = 0.10f; // how fast it decays

    Vector2 basePos;
    float phase;
    float punch;       // current punch amount
    float prevSin;

    void Awake()
    {
        if (!weaponUI) weaponUI = GetComponent<RectTransform>();
        basePos = weaponUI.anchoredPosition;
    }

    void Update()
    {
        // 1) Movement magnitude from WASD
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float speed01 = Mathf.Clamp01(new Vector2(h, v).magnitude);

        // 2) Advance phase based on movement
        if (speed01 > 0f)
            phase += (hz * speed01) * Mathf.PI * 2f * Time.deltaTime;
        else
            phase = Mathf.MoveTowards(phase, 0f, 8f * Time.deltaTime); // settle

        // 3) Bob offset (scaled by speed)
        float s = Mathf.Sin(phase);
        float c = Mathf.Cos(phase);
        Vector2 targetOffset = new Vector2(c * ampX, Mathf.Abs(s) * ampY) * speed01;

        // 4) Trigger "pop" each time a new step starts (sin crosses upward)
        if (prevSin <= 0f && s > 0f && speed01 > 0.2f)
            punch = punchScale;
        prevSin = s;

        // 5) Decay pop
        punch = Mathf.MoveTowards(punch, 0f, (punchScale / Mathf.Max(0.0001f, punchTime)) * Time.deltaTime);

        // 6) Apply
        Vector2 cur = weaponUI.anchoredPosition;
        Vector2 target = basePos + targetOffset;
        weaponUI.anchoredPosition = Vector2.Lerp(cur, target, 1f - Mathf.Exp(-smooth * Time.unscaledDeltaTime));

        float scale = 1f + punch;
        weaponUI.localScale = new Vector3(scale, scale, 1f);
    }
}
