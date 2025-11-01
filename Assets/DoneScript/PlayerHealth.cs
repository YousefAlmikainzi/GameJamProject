using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HpBar hpBar;
    [SerializeField] int maxHealth = 10;
    [SerializeField] float restartDelay = 0f;

    int current;
    bool dead;

    void Start()
    {
        current = maxHealth;
        if (hpBar) hpBar.SetHealth(current);
    }

    public void TakeDamage(int dmg)
    {
        if (dead) return;
        current = Mathf.Max(0, current - dmg);
        if (hpBar) hpBar.SetHealth(current);
        if (current <= 0) Die();
    }

    void Die()
    {
        dead = true;
        if (restartDelay <= 0f)
            SceneManager.LoadScene(GetPreviousIndex());
        else
            StartCoroutine(RestartAfterDelay());
    }

    System.Collections.IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(GetPreviousIndex());
    }

    int GetPreviousIndex()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        return Mathf.Max(0, i - 1);
    }
}
