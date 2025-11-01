using UnityEngine;

[CreateAssetMenu(menuName = "Game/EnemyType")]
public class EnemyType : ScriptableObject
{
    public int maxHealth;
    public int damageToPlayer;
}
