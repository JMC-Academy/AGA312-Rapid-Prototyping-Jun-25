using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : GameBehaviour
{
    public InputActionAsset inputActions;
    private InputAction moveAction;

    //Event setup
    public static Action<Vector2> OnMove = null;
    public static Action OnJump = null;
    public static Action OnAttack = null;

    private void Awake()
    {
        moveAction = inputActions.FindActionMap("Player").FindAction("Move");

        inputActions.FindActionMap("Player").FindAction("Jump").performed += (context) =>
        {
            if (context.action.WasPressedThisFrame())
                OnJump?.Invoke();
        };
        inputActions.FindActionMap("Player").FindAction("Attack").performed += (context) =>
        {
            if (context.action.WasPressedThisFrame())
                OnAttack?.Invoke();
        };
    }

    private void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        OnMove?.Invoke(move);
    }
}
