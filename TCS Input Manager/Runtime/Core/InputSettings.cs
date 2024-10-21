using System;
using UnityEngine;
namespace TCS.InputSystem {
    [CreateAssetMenu(menuName = "TCS/Input Settings", fileName = "InputSettings", order = 0)]
    public class InputSettings : ScriptableObject {
        [Header("Mouse Settings")]
        public bool m_lockCursor;
        public bool m_invertY = true;
        public bool m_invertX;
        [Header("Mouse Sensitivity")]
        [Range(0.01f,999)] public float m_mouseRotationSpeedX = 1.0f;
        [Range(0.01f,999)] public float m_mouseRotationSpeedY = 1.0f;
        [Range(0.01f,999)] public float m_gamepadRotationSpeedX = 1.0f;
        [Range(0.01f,999)] public float m_gamepadRotationSpeedY = 1.0f;
        
        public Action OnValuesChanged;
        public void OnValidate() => OnValuesChanged?.Invoke();
    }
}