using UnityEngine;

public class DeathVFX : MonoBehaviour
{
    private readonly float _delay = 1.5f;
    void Start()
    {
        Destroy(gameObject, _delay);
    }
    
}
