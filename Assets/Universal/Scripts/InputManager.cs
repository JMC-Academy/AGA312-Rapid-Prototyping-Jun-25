using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : GameBehaviour
{
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private InputActionMap playerActionMap;

    //Event setup
    public static Action<Vector2> OnMove = null;
    public static Action OnJump = null;
    public static Action OnAttack = null;

    private bool holdingJump = false;

    private void Awake()
    {
        playerActionMap = inputActions.FindActionMap("Player");

        moveAction = playerActionMap.FindAction("Move");

        playerActionMap.FindAction("Jump").performed += context =>
        {
            if (context.action.WasPressedThisFrame())
            {
                holdingJump = true;
                OnJump?.Invoke();
            }
                
            if (context.action.WasCompletedThisFrame())
            {
                holdingJump = false;
            }
                
        };
        playerActionMap.FindAction("Attack").performed += context =>
        {
            if (context.action.WasPressedThisFrame())
                OnAttack?.Invoke();
        };
    }

    private void Update()
    {
        //OnMove?.Invoke(moveAction.ReadValue<Vector2>());
        //if(holdingJump)
        //    print("JUMPING HOLD");

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        OnMove.Invoke(new Vector2(h, v));
    }
}
