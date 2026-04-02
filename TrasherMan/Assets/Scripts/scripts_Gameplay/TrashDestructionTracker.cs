using UnityEngine;
using System;

public class TrashDestructionTracker : MonoBehaviour
{
    public event System.Action onDestroyed;

    private void OnDestroy()
    {
        onDestroyed?.Invoke();
    }
}