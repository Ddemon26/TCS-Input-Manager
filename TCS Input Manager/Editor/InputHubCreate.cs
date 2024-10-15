using UnityEditor;
using UnityEngine;
namespace TCS.InputSystem.Editor {
    public static class InputHubCreate {
        const string INPUT_HUB = "<color=#00FF00ff>[InputHub]</color>";

#if UNITY_EDITOR
        [MenuItem("GameObject/Tent City Studio/Global Managers/Add InputHub", false, 1000)]
        public static void CreateInputHub() {
            if (Object.FindAnyObjectByType<InputHub>(FindObjectsInactive.Include)) {
                Debug.LogWarning($"{INPUT_HUB} An InputHub already exists in the scene.");
                return;
            }

            var inputHub = new GameObject("[InputHub]");
            inputHub.AddComponent<InputHub>();
            Debug.Log($"{INPUT_HUB} InputHub spawned successfully.");
        }
#endif
    }
}