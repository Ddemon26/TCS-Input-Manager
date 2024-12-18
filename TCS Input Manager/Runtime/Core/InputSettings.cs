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
        [Space(20)]
        [Header("Mouse Sensitivity")]
        [SerializeField] float m_mouseRotationSpeedX = 1.0f;
        [SerializeField] float m_mouseRotationSpeedY = 1.0f;
        [Space(10)]
        [SerializeField] float m_gamepadRotationSpeedX = 1.0f;
        [SerializeField] float m_gamepadRotationSpeedY = 1.0f;
        
        public float MouseRotationSpeedX {
            get => m_mouseRotationSpeedX;
            set {
                value = Mathf.Clamp(value, 0.01f, 999);
                SetField(ref m_mouseRotationSpeedX, value);
            }
        }
        public float MouseRotationSpeedY {
            get => m_mouseRotationSpeedY;
            set {
                value = Mathf.Clamp(value, 0.01f, 999);
                SetField(ref m_mouseRotationSpeedY, value);
            }
        }
        public float GamepadRotationSpeedX {
            get => m_gamepadRotationSpeedX;
            set {
                value = Mathf.Clamp(value, 0.01f, 999);
                SetField(ref m_gamepadRotationSpeedX, value);
            }
        }
        public float GamepadRotationSpeedY {
            get => m_gamepadRotationSpeedY;
            set {
                value = Mathf.Clamp(value, 0.01f, 999);
                SetField(ref m_gamepadRotationSpeedY, value);
            }
        }
        public bool LockCursor {
            get => m_lockCursor;
            set => SetField(ref m_lockCursor, value);
        }
        public bool InvertY {
            get => m_invertY;
            set => SetField(ref m_invertY, value);
        }
        public bool InvertX {
            get => m_invertX;
            set => SetField(ref m_invertX, value);
        }
        
        public Action OnValuesChanged;
        
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            OnValuesChanged?.Invoke();
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
            return true;
        }
    }
}