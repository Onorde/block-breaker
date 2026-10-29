using UnityEngine;

public class EnemyTile : MonoBehaviour
{
    [Min(1)] public int enemyHitPoints;
    [SerializeField] private AudioClip damageSoundClip;
    [SerializeField] private AudioClip deathSoundClip;
    [SerializeField] private ParticleSystem deathVFX;

    public void TakeDamage(int damageAmount)
    {
        int updatedHitPoints = enemyHitPoints - damageAmount;
        

        if (updatedHitPoints <= 0)
        {
            ParticleSystem vfx = Instantiate(deathVFX, transform.position, transform.rotation);
            SoundFXManager.Instance.PlaySoundFX(deathSoundClip, transform, 0.8f);
            Destroy(vfx);
            Destroy(gameObject);
        }
        SoundFXManager.Instance.PlaySoundFX(damageSoundClip,transform, 0.8f);
        enemyHitPoints = updatedHitPoints;
    }
}