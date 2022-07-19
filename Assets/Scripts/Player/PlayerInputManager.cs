using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance => instance;

    //inputs
    public Vector2 move;
    public bool interact;
    public bool pause;
    public bool run;

    //mouse lockers
    public bool curserlocked = true;
    public bool curserInputLock = true;

    static PlayerInputManager instance;

    void Awake()
    {
        //create an instance
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #region Functions We Call On Button Press

    public void MoveInputs(Vector2 a_moveDirection)
    {
        move = a_moveDirection;
    }

    public void RunInput(bool a_runState)
    {
        run = a_runState;
    }

    public void PauseInput(bool a_pauseState)
    {
        pause = a_pauseState;
    }

    public void InteractInput(bool a_interactState)
    {
        interact = a_interactState;
    }
    #endregion

    #region Functions Created For Connecting to Unity Input System
    public void OnMove(InputValue a_value)
    {
        MoveInputs(a_value.Get<Vector2>());
    }

    public void OnSprint(InputValue a_value)
    {
        RunInput(a_value.isPressed);
        Debug.Log("is running");
    }

    public void OnInteract(InputValue a_value)
    {
        InteractInput(a_value.isPressed);
    }

    public void OnPause(InputValue a_value)
    {
        PauseInput(a_value.isPressed);
    }

    void OnApplicationFocus(bool focus)
    {
        SetCurserState(curserlocked);
    }

    void SetCurserState(bool a_state)
    {
        Cursor.lockState = a_state ? CursorLockMode.Locked : CursorLockMode.None;
    }
    
    #endregion
}
