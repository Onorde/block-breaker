using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Platform : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float speed = 5f;
    private float _horizontalInput;
    private Rigidbody2D _rBody;


    private void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var playerKeyboard = Keyboard.current;

        if (playerKeyboard == null)
        {
            _horizontalInput = 0f;
            return;
        }
        
        var leftPressed = playerKeyboard.aKey.isPressed || playerKeyboard.leftArrowKey.isPressed;
        var rightPressed = playerKeyboard.dKey.isPressed || playerKeyboard.rightArrowKey.isPressed;

        // Opposing Movement keys pressed
        if (leftPressed == rightPressed)
            _horizontalInput = 0f;
        else if (leftPressed)
            _horizontalInput = -1f;
        else
            _horizontalInput = 1f;
    }

    private void FixedUpdate()
    {
        _rBody.linearVelocityX = _horizontalInput * speed;
    }
}