using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
namespace TCS.InputSystem {
    [CreateAssetMenu(fileName = "InputReader", menuName = "TCS/InputSystem/InputReader")]
    public class InputReader : ScriptableObject, PlayerInput.ICharacterControlsActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public Vector3 Direction => m_inputActions.CharacterControls.Move.ReadValue<Vector2>();
        public event UnityAction<Vector2, bool> Rotate = delegate { };
        public event UnityAction<Vector2, bool> RotateController = delegate { };
        public event UnityAction<Vector2, bool> ScrollWheel = delegate { };
        public event UnityAction<bool> Jump = delegate { };
        public event UnityAction<bool> Run = delegate { };
        public event UnityAction<bool> Reload = delegate { };
        public event UnityAction<bool> Attack = delegate { };
        public event UnityAction<bool> Crouch = delegate { };
        public event UnityAction<bool> Block = delegate { };
        public event UnityAction<bool> Interact = delegate { };
        public event UnityAction<bool> Escape = delegate { };
        public event UnityAction<bool> OpenUI = delegate { };
        public event UnityAction<bool> Emote = delegate { };
        public event UnityAction<bool> CommandKey = delegate { };
        public event UnityAction<bool> NumOne = delegate { };
        
        PlayerInput m_inputActions;

        void OnEnable()
        {
            if (m_inputActions != null) return;

            m_inputActions = new PlayerInput();
            m_inputActions.CharacterControls.SetCallbacks(this);
        }

        public void EnablePlayerActions() => m_inputActions.Enable();
        public void DisablePlayerActions() => m_inputActions.Disable();

        static bool IsMouseDevice(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
            context.control.device.name == "Mouse";
        static bool IsGamepadDevice(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
            context.control.device.name.Contains("Gamepad");
        public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context) => Move.Invoke(context.ReadValue<Vector2>());
        public void OnScrollWheel(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
            ScrollWheel.Invoke(context.ReadValue<Vector2>(), IsMouseDevice(context));
        public void OnRotatePlayer(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
            Rotate.Invoke(context.ReadValue<Vector2>(), IsMouseDevice(context));
        public void OnRotatePlayerController(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
            RotateController.Invoke(context.ReadValue<Vector2>(), IsGamepadDevice(context));
        static void HandleBinaryAction(UnityEngine.InputSystem.InputAction.CallbackContext context, UnityAction<bool> action)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                case InputActionPhase.Performed:
                    action.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                case InputActionPhase.Waiting:
                case InputActionPhase.Disabled:
                    action.Invoke(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void OnRun(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleBinaryAction(context, Run);
        public void OnReload(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleBinaryAction(context, Reload);
        public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleBinaryAction(context, Jump);
        public void OnCrouch(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleBinaryAction(context, Crouch);
        public void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleBinaryAction(context, Interact);
        public void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleBinaryAction(context, Escape);
        public void OnOpenUI(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleBinaryAction(context, OpenUI);
        public void OnEmote(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleBinaryAction(context, Emote);
        public void OnCommandKey(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleBinaryAction(context, CommandKey);
        public void OnNumOne(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleBinaryAction(context, NumOne);

        static void HandleHoldAction(UnityEngine.InputSystem.InputAction.CallbackContext context, UnityAction<bool> action)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                case InputActionPhase.Performed:
                case InputActionPhase.Waiting:
                    action.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                case InputActionPhase.Disabled:
                    action.Invoke(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleHoldAction(context, Attack);
        public void OnBlock(UnityEngine.InputSystem.InputAction.CallbackContext context) => HandleHoldAction(context, Block);
    }
}