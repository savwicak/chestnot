using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PemancarEvent : MonoBehaviour
{
    public static event Action TekanTombol;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("pemancar spasi di terima");
            TekanTombol?.Invoke();
        }
    }
}
