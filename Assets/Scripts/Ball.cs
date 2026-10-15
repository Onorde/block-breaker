using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;


public class Ball : MonoBehaviour
{
    
    [SerializeField, Min(0f)] private float speed = 5f;
    private Rigidbody2D _rBody;
    private bool _ballOnTheMove;

    
    
    private void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        Keyboard playerKeyboard = Keyboard.current;

        if (playerKeyboard.spaceKey.isPressed && !_ballOnTheMove)
        {
            _rBody.linearVelocity = GetRandomDirection() * speed;
            _ballOnTheMove = true;
        }
    }
    private Vector2 GetRandomDirection()
    {
        float randomAngle = Random.Range(10f, 170.0f);
        float randomInRadian = randomAngle * Mathf.Deg2Rad;

        float x = Mathf.Cos(randomInRadian);
        float y = Mathf.Sin(randomInRadian);
            
        Vector2 direction = new Vector2(x,y);

        return direction;
    }
}
