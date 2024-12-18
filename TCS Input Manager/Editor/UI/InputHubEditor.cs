/*using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace TCS.InputSystem.Editor {
    [CustomEditor(typeof(InputHub))]
    public class InputHubEditor : UnityEditor.Editor {
        [SerializeField] VisualTreeAsset m_visualTreeAsset;
        VisualElement m_root;
        VisualElement m_visualTree;
        InputHub m_hub;

        ToggleButtonGroup m_toggleButtonGroup;
        ToggleButtonGroupState m_activeButton;
        const string INPUT_SETTINGS_CONTAINER = "input-settings__container";
        VisualElement m_inputSettingsContainer;
        const string INPUT_VALUES_CONTAINER = "input-values__container";
        VisualElement m_inputValuesContainer;
        const string INPUT_FLAGS_CONTAINER = "input-flags__container";
        VisualElement m_inputFlagsContainer;

        Button m_createSettingsButton;
        const string CREATE_SETTINGS_BUTTON = "create-settings__button";

        Toggle m_lockCursorToggle;
        const string LOCK_CURSOR_TOGGLE = "lock-cursor__toggle";

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();
            m_visualTreeAsset.CloneTree(root);
            m_hub = (InputHub)target;
            m_root = root;

            Initialize();

            return m_root;
        }

        void Initialize() {
            RegisterContainers();
            RegisterToggleGroup();
            RegisterCursorLock();
            RegisterCreateSettingsButton();
            HandleSettingsPanel();
            //Keep this comment for reference
            //DefaultFoldout();
        }

        void RegisterContainers() {
            m_inputSettingsContainer = m_root.Q<VisualElement>(INPUT_SETTINGS_CONTAINER);
            m_inputValuesContainer = m_root.Q<VisualElement>(INPUT_VALUES_CONTAINER);
            m_inputFlagsContainer = m_root.Q<VisualElement>(INPUT_FLAGS_CONTAINER);
        }

        void RegisterCursorLock() {
            m_lockCursorToggle = m_root.Q<Toggle>(LOCK_CURSOR_TOGGLE);
            m_lockCursorToggle.RegisterValueChangedCallback
            (
                evt => {
                    m_hub.LockCursor(evt.newValue);
                }
            );
        }

        void RegisterToggleGroup() {
            m_toggleButtonGroup = m_root.Q<ToggleButtonGroup>();
            m_toggleButtonGroup.RegisterValueChangedCallback
            (
                evt => {
                    m_activeButton = evt.newValue;
                    SwitchContainer(m_activeButton);
                }
            );
            SetEnabled(m_inputSettingsContainer, m_toggleButtonGroup.value[0]);
            SetEnabled(m_inputValuesContainer, m_toggleButtonGroup.value[1]);
            SetEnabled(m_inputFlagsContainer, m_toggleButtonGroup.value[2]);
        }

        void RegisterCreateSettingsButton() {
            m_createSettingsButton = m_root.Q<Button>(CREATE_SETTINGS_BUTTON);
            m_createSettingsButton.clicked += () => {
                var settings = CreateInputSettings.Create();
                m_hub.SetInputSettings(settings);
                HandleSettingsPanel();
            };
        }

        void HandleSettingsPanel() {
            if (!m_hub.m_inputSettings) {
                SetEnabled(m_inputSettingsContainer, true);
                SetToggleElement(m_toggleButtonGroup, 0, true);
            }
            else {
                SetEnabled(m_inputSettingsContainer, false);
                SetToggleElement(m_toggleButtonGroup, 0, false);
            }
        }

        void DefaultFoldout() {
            var foldout = new Foldout { viewDataKey = "FullInspectorKey", text = "InputHub" };
            InspectorElement.FillDefaultInspector(foldout, serializedObject, this);
            m_root.Add(foldout);
        }

        void SwitchContainer(ToggleButtonGroupState activeButton) {
            SetEnabled(m_inputSettingsContainer, activeButton[0]);
            SetEnabled(m_inputValuesContainer, activeButton[1]);
            SetEnabled(m_inputFlagsContainer, activeButton[2]);
        }

        static void SetEnabled(VisualElement element, bool enabled) {
            element.SetEnabled(enabled);
            element.style.display = enabled ? DisplayStyle.Flex : DisplayStyle.None;
        }

        static void SetToggleElement(ToggleButtonGroup element, int index, bool display) {
            element[index].style.display = display ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}*/