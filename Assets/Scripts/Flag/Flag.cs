using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public event Action<Flag> Appeared;

    public bool HasFlagTaken {  get; private set; }

    public void SetHasFlagTaken(bool state)
    {
        HasFlagTaken = state;
    }

    private void OnEnable()
    {
        Appeared?.Invoke(this);
    }

    public void Deactivate()
    {
        transform.gameObject.SetActive(false);
    }
}
