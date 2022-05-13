using UnityEngine;
//using UnityEngine.InputSystem;

public enum MovementActions { Move, Jump, NotJump}

public abstract class MovementInputManager : MonoBehaviour
{
    /*
    public PlayerInput playerInput;

    protected virtual void Awake()
    {
        //playerInput.SwitchCurrentActionMap("TutorialInput");

        playerInput.actions[MovementActions.Move.ToString()].performed += Move;
        playerInput.actions[MovementActions.Jump.ToString()].performed += Jump;
        playerInput.actions[MovementActions.NotJump.ToString()].performed += NotJump;
    }

    private void OnDisable()
    {
        playerInput?.SwitchCurrentActionMap(playerInput.defaultActionMap);
    }

    protected abstract void Move(InputAction.CallbackContext value);
    protected abstract void Jump(InputAction.CallbackContext value);
    protected abstract void NotJump(InputAction.CallbackContext value);
    */
}
