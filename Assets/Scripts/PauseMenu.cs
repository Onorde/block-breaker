using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    
    private bool _gamePaused = false;


    void Update()
    {
        var playerKeyboard = Keyboard.current;

        if (playerKeyboard == null)
        {
            return;
        }
        
        var escapePressed = playerKeyboard.escapeKey.isPressed;

        if (escapePressed && playerKeyboard.escapeKey.wasPressedThisFrame && _gamePaused == false)
        {
            _gamePaused = true;
            Time.timeScale = 0f;
            
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else if (escapePressed && playerKeyboard.escapeKey.wasPressedThisFrame && _gamePaused == true)
        {
            _gamePaused = false;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            
            Time.timeScale = 1f;
        }
    }
}
