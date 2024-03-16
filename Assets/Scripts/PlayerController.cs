using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public float rotationSpeed = 0.3f;

    

    public static PlayerController Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    

    public float GetSpeed()
    {
        return speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.levelStarted)
        {
            return;
        }
        if (PlayerManager.gameOver)
        {
            return;
        }
        transform.Translate(0, 0, speed * Time.deltaTime);

        if(Touchscreen.current != null)
        {
            Vector2 delta = Touchscreen.current.primaryTouch.delta.ReadValue();
            transform.Rotate(0, 0, delta.x * rotationSpeed);
            
        }     
    }
    
}
