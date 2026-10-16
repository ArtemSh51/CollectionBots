using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode _enableFlag;
    [SerializeField] private KeyCode _disableFlag;

    public event Action DisableFlagButtonPressed;
    public event Action EnableFlagButtonPressed;

    private void Update()
    {
        if (Input.GetKeyDown(_enableFlag))
        {
            EnableFlagButtonPressed?.Invoke();
        }
        else if (Input.GetKeyDown(_disableFlag))
        {
            DisableFlagButtonPressed?.Invoke();
        }
    }
}
