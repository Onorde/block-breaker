using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;


public class Ball : MonoBehaviour
{
    public int damage = 1;
    [SerializeField] [Min(0f)] private float speed = 5f;
    [SerializeField] private GameObject playerPlatform;
    private readonly Vector3 _ballOffset = new(0, 1, 0);

    private bool _ballOnTheMove;
    private Rigidbody2D _rBody;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var playerKeyboard = Keyboard.current;

        if (!_ballOnTheMove)
        {
            transform.localPosition = playerPlatform.transform.localPosition + _ballOffset;

            if (playerKeyboard.spaceKey.isPressed)
            {
                _rBody.linearVelocity = GetRandomDirection() * speed;
                _ballOnTheMove = true;
            }
        }
        else
        {
            _rBody.linearVelocity = _rBody.linearVelocity.normalized * speed;
        }
    }

    private Vector2 GetRandomDirection()
    {
        var randomAngle = Random.Range(10f, 170.0f);
        var randomInRadian = randomAngle * Mathf.Deg2Rad;

        var x = Mathf.Cos(randomInRadian);
        var y = Mathf.Sin(randomInRadian);

        var direction = new Vector2(x, y);

        return direction;
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyTile enemy = other.gameObject.GetComponent<EnemyTile>();
            enemy.TakeDamage(damage);
        }
    }
}