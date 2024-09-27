using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace TCS.InputSystem.Editor {
    [CustomEditor(typeof(InputHub))]
    public class InputHubEditor : UnityEditor.Editor {
        [SerializeField] VisualTreeAsset m_visualTreeAsset;

        //InputHub m_hub;
        VisualElement m_root;
        VisualElement m_visualTree;
        StyleSheet m_styleSheet;

        ToggleButtonGroup m_toggleButtonGroup;
        ToggleButtonGroupState m_activeButton;
        
        const string HIDE_CONTAINER_CLASS = "container__hide";

        const string INPUT_SETTINGS_CONTAINER = "input-settings__container";
        VisualElement m_invertSettingsContainer;
        const string INPUT_VALUES_CONTAINER = "input-values__container";
        VisualElement m_inputValuesContainer;
        const string INPUT_FLAGS_CONTAINER = "input-flags__container";
        VisualElement m_inputFlagsContainer;

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();
            m_visualTreeAsset.CloneTree(root);
            //m_hub = (InputHub) target;

            m_root = root;
            m_styleSheet = Resources.Load<StyleSheet>("InputHubEditor");
            m_root.styleSheets.Add(m_styleSheet);

            m_invertSettingsContainer = m_root.Q<VisualElement>(INPUT_SETTINGS_CONTAINER);
            m_inputValuesContainer = m_root.Q<VisualElement>(INPUT_VALUES_CONTAINER);
            m_inputFlagsContainer = m_root.Q<VisualElement>(INPUT_FLAGS_CONTAINER);
            
            m_toggleButtonGroup = m_root.Q<ToggleButtonGroup>();
            m_toggleButtonGroup.RegisterValueChangedCallback
            (evt => {
                m_activeButton = evt.newValue;
                SwitchContainer(m_activeButton);
            });
            SetEnabled(m_invertSettingsContainer, m_toggleButtonGroup.value[0]);
            SetEnabled(m_inputValuesContainer, m_toggleButtonGroup.value[1]);
            SetEnabled(m_inputFlagsContainer, m_toggleButtonGroup.value[2]);

            // var foldout = new Foldout {viewDataKey = "FullInspectorKey", text = "InputHub"};
            // InspectorElement.FillDefaultInspector(foldout, serializedObject, this);
            // m_root.Add(foldout);
            
            return m_root;
        }

        //method to switch each container on and off using the toggle buttons we have the values for
        void SwitchContainer(ToggleButtonGroupState activeButton) {
            SetEnabled(m_invertSettingsContainer, activeButton[0]);
            SetEnabled(m_inputValuesContainer, activeButton[1]);
            SetEnabled(m_inputFlagsContainer, activeButton[2]);
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            // Begin a vertical layout group
            EditorGUILayout.BeginVertical();
            try {
                // Your GUI code here
                EditorGUILayout.PropertyField(serializedObject.FindProperty("someProperty"));
                // Use m_activeButton to change things based on the active button
            }
            finally {
                // Ensure EndVertical is always called
                EditorGUILayout.EndVertical();
            }

            serializedObject.ApplyModifiedProperties();
        }

        static void SetEnabled(VisualElement element, bool enabled) {
            element.SetEnabled(enabled);
            if (enabled) {
                element.RemoveFromClassList(HIDE_CONTAINER_CLASS);
            } else {
                element.AddToClassList(HIDE_CONTAINER_CLASS);
            }
        }
    }
}