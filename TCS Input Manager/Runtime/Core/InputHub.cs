using System;
using UnityEngine;
namespace TCS.InputSystem {
    public enum InputAction {
        Jump, // Space Key
        Run, // Left Shift Key
        Reload, // R Key
        Attack, // Left Mouse Button
        Crouch, // Left Control Key
        Block, // Right Mouse Button
        Interact, // E Key
        Escape, // Escape Key
        OpenUI, // Tab Key
        Emote, // G Key
        Command, // ~ Key
        NumOne // 1 Key
    }

    public enum ControlScheme {
        KeyboardMouse,
        Gamepad,
        Mobile
    }

    [DefaultExecutionOrder(-5000)]
    public class InputHub : InputHubSingleton<InputHub> {
        public bool m_isMouseConnected = true;
        public bool m_isGamepadConnected;
        public ControlScheme m_currentControlScheme;

        public bool m_invertY = true;
        public bool m_invertX;

        public Vector2 m_moveInput = Vector2.zero;
        public Vector2 m_rotateInput = Vector2.zero;

        public Vector2 m_rotateControllerInput = Vector2.zero;
        public Vector2 m_scrollWheelInput;

        InputReader m_inputReader;

        void OnEnable() {
            try {
                m_inputReader = ScriptableObject.CreateInstance<InputReader>();

                CheckConnectedDevices();
            }
            catch (Exception ex) {
                Debug.LogError($"An error occurred: {ex.Message}");
            }

            EnablePlayerActions();
        }

        void OnDisable() => DisablePlayerActions();

        #region Enable/Disable Player Actions
        void EnablePlayerActions() {
            m_inputReader.Move += OnMove;
            m_inputReader.Rotate += OnRotate;
            m_inputReader.RotateController += OnRotateController;
            m_inputReader.ScrollWheel += OnScrollWheel;


            m_inputReader.Jump += OnJumpFlag;
            m_inputReader.Jump += JumpActionEvent;

            m_inputReader.Run += OnRunFlag;
            m_inputReader.Run += RunActionEvent;

            m_inputReader.Reload += OnReloadFlag;
            m_inputReader.Reload += ReloadActionEvent;

            m_inputReader.Attack += OnAttackFlag;
            m_inputReader.Attack += AttackActionEvent;

            m_inputReader.Crouch += OnCrouchFlag;
            m_inputReader.Crouch += CrouchActionEvent;

            m_inputReader.Block += OnBlockFlag;
            m_inputReader.Block += BlockActionEvent;

            m_inputReader.Interact += OnInteractFlag;
            m_inputReader.Interact += InteractActionEvent;

            m_inputReader.Escape += OnEscapeFlag;
            m_inputReader.Escape += EscapeActionEvent;

            m_inputReader.OpenUI += OnOpenUIFlag;
            m_inputReader.OpenUI += OpenUIActionEvent;

            m_inputReader.Emote += OnEmoteFlag;
            m_inputReader.Emote += EmoteActionEvent;

            m_inputReader.CommandKey += OnCommandFlag;
            m_inputReader.CommandKey += CommandKeyActionEvent;

            m_inputReader.NumOne += OnNumOneFlag;
            m_inputReader.NumOne += NumOneActionEvent;


            m_inputReader.EnablePlayerActions();
        }
        void DisablePlayerActions() {
            m_inputReader.Move -= OnMove;
            m_inputReader.Rotate -= OnRotate;
            m_inputReader.RotateController -= OnRotateController;
            m_inputReader.ScrollWheel -= OnScrollWheel;

            m_inputReader.Jump -= OnJumpFlag;
            m_inputReader.Jump -= JumpActionEvent;

            m_inputReader.Run -= OnRunFlag;
            m_inputReader.Run -= RunActionEvent;

            m_inputReader.Reload -= OnReloadFlag;
            m_inputReader.Reload -= ReloadActionEvent;

            m_inputReader.Attack -= OnAttackFlag;
            m_inputReader.Attack -= AttackActionEvent;

            m_inputReader.Crouch -= OnCrouchFlag;
            m_inputReader.Crouch -= CrouchActionEvent;

            m_inputReader.Block -= OnBlockFlag;
            m_inputReader.Block -= BlockActionEvent;

            m_inputReader.Interact -= OnInteractFlag;
            m_inputReader.Interact -= InteractActionEvent;

            m_inputReader.Escape -= OnEscapeFlag;
            m_inputReader.Escape -= EscapeActionEvent;

            m_inputReader.OpenUI -= OnOpenUIFlag;
            m_inputReader.OpenUI -= OpenUIActionEvent;

            m_inputReader.Emote -= OnEmoteFlag;
            m_inputReader.Emote -= EmoteActionEvent;

            m_inputReader.CommandKey -= OnCommandFlag;
            m_inputReader.CommandKey -= CommandKeyActionEvent;

            m_inputReader.NumOne -= OnNumOneFlag;
            m_inputReader.NumOne -= NumOneActionEvent;

            m_inputReader.DisablePlayerActions();
        }
        #endregion

        public void ChangeControlScheme(ControlScheme controlScheme) {
            m_currentControlScheme = controlScheme;
            m_inputReader.EnablePlayerActions();
        }

        void CheckConnectedDevices() {
            m_isMouseConnected = UnityEngine.InputSystem.Mouse.current != null;
            m_isGamepadConnected = Input.GetJoystickNames().Length > 0;
        }

        public void InvertYAxis(bool value) => m_invertY = value;
        public void InvertXAxis(bool value) => m_invertX = value;

        static void HandleBooleanInput(bool input, Action<bool> setInputAction) => setInputAction(input);

        void OnMove(Vector2 input) => m_moveInput = input;

        [SerializeField] Vector2 m_mouseRotationSpeed = new(1.0f, 1.0f);
        [SerializeField] Vector2 m_gamepadRotationSpeed = new(1.0f, 1.0f);

        void OnRotate(Vector2 input, bool isMouseDevice) {
            if (!isMouseDevice) return;
            input.x *= m_mouseRotationSpeed.x;
            input.y *= m_mouseRotationSpeed.y;
            ProcessRotationInput(ref input);
            m_rotateInput = input;
        }

        void OnRotateController(Vector2 input, bool isGamepadDevice) {
            if (!isGamepadDevice) return;
            input.x *= m_gamepadRotationSpeed.x;
            input.y *= m_gamepadRotationSpeed.y;
            ProcessRotationInput(ref input);
            m_rotateControllerInput = input;
        }

        void OnScrollWheel(Vector2 value, bool isMouseDevice) => m_scrollWheelInput = value;

        void ProcessRotationInput(ref Vector2 input) {
            if (m_invertY) {
                input.y = -input.y;
            }

            if (m_invertX) {
                input.x = -input.x;
            }

            m_rotateInput = new Vector2(input.x, input.y);
        }

        #region Input Actions Flag type
        public bool m_jumpFlag, m_runFlag, m_reloadFlag,
            m_attackFlag, m_crouchFlag, m_blockFlag, m_interactFlag,
            m_escapeFlag, m_openUIFlag, m_emoteFlag, m_commandFlag, m_numOneFlag;
        void OnJumpFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_jumpFlag = value);
        void OnRunFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_runFlag = value);
        void OnReloadFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_reloadFlag = value);
        void OnAttackFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_attackFlag = value);
        void OnCrouchFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_crouchFlag = value);
        void OnBlockFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_blockFlag = value);
        void OnInteractFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_interactFlag = value);
        void OnEscapeFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_escapeFlag = value);
        void OnOpenUIFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_openUIFlag = value);
        void OnEmoteFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_emoteFlag = value);
        void OnCommandFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_commandFlag = value);
        void OnNumOneFlag(bool isPressed) => HandleBooleanInput(isPressed, value => m_numOneFlag = value);
        #endregion

        #region Input Actions Event type
        public Action<bool> OnAttackAction, OnJumpAction, OnRunAction,
            OnReloadAction, OnCrouchAction, OnBlockAction, OnInteractAction,
            OnEscapeAction, OnOpenUIAction, OnEmoteAction, OnCommandAction, OnNumOneAction;

        void HandleEventAction(bool isPressed, Action<bool> action) => action?.Invoke(isPressed);

        void AttackActionEvent(bool isPressed) => HandleEventAction(isPressed, OnAttackAction);
        void JumpActionEvent(bool isPressed) => HandleEventAction(isPressed, OnJumpAction);
        void RunActionEvent(bool isPressed) => HandleEventAction(isPressed, OnRunAction);
        void ReloadActionEvent(bool isPressed) => HandleEventAction(isPressed, OnReloadAction);
        void CrouchActionEvent(bool isPressed) => HandleEventAction(isPressed, OnCrouchAction);
        void BlockActionEvent(bool isPressed) => HandleEventAction(isPressed, OnBlockAction);
        void InteractActionEvent(bool isPressed) => HandleEventAction(isPressed, OnInteractAction);
        void EscapeActionEvent(bool isPressed) => HandleEventAction(isPressed, OnEscapeAction);
        void OpenUIActionEvent(bool isPressed) => HandleEventAction(isPressed, OnOpenUIAction);
        void EmoteActionEvent(bool isPressed) => HandleEventAction(isPressed, OnEmoteAction);
        void CommandKeyActionEvent(bool isPressed) => HandleEventAction(isPressed, OnCommandAction);
        void NumOneActionEvent(bool isPressed) => HandleEventAction(isPressed, OnNumOneAction);
        #endregion
    }
}