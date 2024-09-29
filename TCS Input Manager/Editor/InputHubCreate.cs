using UnityEditor;
using UnityEngine;
namespace TCS.InputSystem.Editor {
    public static class InputHubCreate {
        const string INPUT_HUB = "<color=#00FF00ff>[InputHub]</color>";

        [MenuItem("Tools/TCS/Create InputHub")]
        static void CreateInputHub() {
            // Check if an InputHub already exists in the scene
            if (!Object.FindFirstObjectByType<InputHub>(FindObjectsInactive.Include)) {
                // Create a new GameObject named InputHub
                var inputHub = new GameObject("InputHub");
                // Add the InputHub component to the GameObject
                inputHub.AddComponent<InputHub>();
            }
            else {
                Debug.LogWarning($"{INPUT_HUB} An InputHub already exists in the scene.");
            }
        }
    }
}