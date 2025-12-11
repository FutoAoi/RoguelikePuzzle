using System;
using UnityEngine;

public class AttackMagic : MonoBehaviour
{
    private Action _onDisable;
    public int Attack = 1;

    public void Initialize(Action onDisable)
    {
        _onDisable = onDisable;
    }
    /// <summary>
    /// –‚–@‚ð‰ó‚·
    /// </summary>
    public void DestroyMagic()
    {
        _onDisable?.Invoke();
        gameObject.SetActive(false);
    }
}
