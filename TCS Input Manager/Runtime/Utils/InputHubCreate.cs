using UnityEditor;
using UnityEngine;
namespace TCS.InputSystem {
    public static class InputHubCreate {
#if UNITY_EDITOR
        [MenuItem("GameObject/Tent City Studio/Global Managers/Add InputHub", false, 1000)]
        public static void CreateInputHub() {
            if (Object.FindAnyObjectByType<InputHub>(FindObjectsInactive.Include)) {
                Logger.LogWarning("An InputHub already exists in the scene.");
                return;
            }

            var inputHub = new GameObject("[InputHub]");
            inputHub.AddComponent<InputHub>();
            Logger.Log("InputHub spawned successfully.");
        }
#endif
    }
}