using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TCS.InputSystem {
    public static class CreateInputSettings {
        public static InputSettings Create() {
            var inputSettings = AssetDatabase.FindAssets("t:InputSettings")
                .Select
                (
                    guid => AssetDatabase.LoadAssetAtPath<InputSettings>
                    (
                        AssetDatabase.GUIDToAssetPath(guid)
                    )
                ).FirstOrDefault();

            if (inputSettings) {
                EditorGUIUtility.PingObject(inputSettings);
                return inputSettings;
            }

            inputSettings = ScriptableObject.CreateInstance<InputSettings>();
            AssetDatabase.CreateAsset(inputSettings, "Assets/InputSettings.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return inputSettings;
        }
    }
}