using UnityEngine;

public class EnemyTile : MonoBehaviour
{
    [Min(1)] public int enemyHitPoints;
    [SerializeField] private AudioClip damageSoundClip;
    [SerializeField] private AudioClip deathSoundClip;

    public void TakeDamage(int damageAmount)
    {
        int updatedHitPoints = enemyHitPoints - damageAmount;
        

        if (updatedHitPoints <= 0)
        {
            SoundFXManager.Instance.PlaySoundFX(deathSoundClip, transform, 0.8f);
            Destroy(gameObject);
        }
        SoundFXManager.Instance.PlaySoundFX(damageSoundClip,transform, 0.8f);
        enemyHitPoints = updatedHitPoints;
    }
}