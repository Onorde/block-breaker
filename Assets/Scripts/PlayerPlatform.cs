using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Platform : MonoBehaviour
{
    [SerializeField, Min(0f)] private float speed = 5f;
    private float _horizontalInput;
    private Rigidbody2D _rBody;
    
    
    private void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Keyboard playerKeyboard = Keyboard.current;
        
        if (playerKeyboard != null)
        {
            // Opposing Movement keys pressed

            bool leftPressed = playerKeyboard.aKey.isPressed || playerKeyboard.leftArrowKey.isPressed;
            bool rightPressed = playerKeyboard.dKey.isPressed || playerKeyboard.rightArrowKey.isPressed;

            if (leftPressed == rightPressed)
            {
                _horizontalInput = 0f;
            }
            else if (leftPressed)
            {
                _horizontalInput = -1f;
            }
            else
            {
                _horizontalInput = 1f;
            }

        }
        else
        {
            _horizontalInput = 0f;
        }
    }
    private void FixedUpdate()
    {
        _rBody.linearVelocityX = _horizontalInput * speed;
    }
}