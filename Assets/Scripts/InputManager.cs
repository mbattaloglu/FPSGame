using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMovement motor; 
    private PlayerLook look;

    public PlayerInput.OnFootActions OnFoot { get => onFoot; set => onFoot = value; }

    private void Awake()
    {
        playerInput = new PlayerInput();
        OnFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();

        //Anytime the jump button is pressed, call the Jump() 
        //method with the callback from the Input System
        OnFoot.Jump.performed += ctx => motor.Jump();

        OnFoot.Crouch.performed += ctx => motor.Crouch();
        OnFoot.Sprint.performed += ctx => motor.Sprint();
        OnFoot.Sprint.canceled += ctx => motor.Sprint();
    }

    private void OnEnable()
    {
        OnFoot.Enable();
    }

    private void OnDisable()
    {
        OnFoot.Disable();
    }

    private void FixedUpdate()
    {
        motor.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(OnFoot.Look.ReadValue<Vector2>());
    }


}
