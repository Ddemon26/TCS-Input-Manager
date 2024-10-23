using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
namespace TCS.InputSystem {
    [CreateAssetMenu(menuName = "TCS/Input Settings", fileName = "InputSettings", order = 0)]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class InputSettings : ScriptableObject, INotifyPropertyChanged {
        [Header("Mouse Settings")]
        [SerializeField]  bool m_lockCursor;
        [SerializeField]  bool m_invertY = true;
        [SerializeField]  bool m_invertX;
        [Header("Mouse Sensitivity")]
        [SerializeField] float m_mouseRotationSpeedX = 1.0f;
        [SerializeField] float m_mouseRotationSpeedY = 1.0f;
        [SerializeField] float m_gamepadRotationSpeedX = 1.0f;
        [SerializeField] float m_gamepadRotationSpeedY = 1.0f;
        
        public float MouseRotationSpeedX {
            get => m_mouseRotationSpeedX;
            set {
                value = Mathf.Clamp(value, 0.01f, 999);
                if (!SetField(ref m_mouseRotationSpeedX, value)) return;
                OnValuesChanged?.Invoke();
                Debug.Log("MouseRotationSpeedX");
            }
        }
        public float MouseRotationSpeedY {
            get => m_mouseRotationSpeedY;
            set {
                value = Mathf.Clamp(value, 0.01f, 999);
                if (!SetField(ref m_mouseRotationSpeedY, value)) return;
                OnValuesChanged?.Invoke();
                Debug.Log("MouseRotationSpeedY");
            }
        }
        public float GamepadRotationSpeedX {
            get => m_gamepadRotationSpeedX;
            set {
                value = Mathf.Clamp(value, 0.01f, 999);
                if (!SetField(ref m_gamepadRotationSpeedX, value)) return;
                OnValuesChanged?.Invoke();
                Debug.Log("GamepadRotationSpeedX");
            }
        }
        public float GamepadRotationSpeedY {
            get => m_gamepadRotationSpeedY;
            set {
                value = Mathf.Clamp(value, 0.01f, 999);
                if (!SetField(ref m_gamepadRotationSpeedY, value)) return;
                OnValuesChanged?.Invoke();
                Debug.Log("GamepadRotationSpeedY");
            }
        }
        public bool LockCursor {
            get => m_lockCursor;
            set {
                if (!SetField(ref m_lockCursor, value)) return;
                OnValuesChanged?.Invoke();
                Debug.Log($"LockCursor {m_lockCursor}");
            }
        }
        public bool InvertY {
            get => m_invertY;
            set {
                if (!SetField(ref m_invertY, value)) return;
                OnValuesChanged?.Invoke();
                Debug.Log($"InvertY {m_invertY}");
            }
        }
        public bool InvertX {
            get => m_invertX;
            set {
                if (!SetField(ref m_invertX, value)) return;
                OnValuesChanged?.Invoke();
                Debug.Log($"InvertX {m_invertX}");
            }
        }
        
        public Action OnValuesChanged;
        void Awake() {
            Debug.Log($"Awake {m_mouseRotationSpeedX}");
        }
        void OnEnable() {
            Debug.Log($"OnEnable {m_mouseRotationSpeedX}");
        }
        public void OnValidate() {
            Debug.Log($"OnValidate {name}");
            OnValuesChanged?.Invoke();
            EditorUtility.SetDirty(this);
        }
        
        void OnDisable() {
            Debug.Log($"OnDisable {m_mouseRotationSpeedX}");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}