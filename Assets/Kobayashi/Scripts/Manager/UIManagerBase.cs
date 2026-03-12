using UnityEngine;

/// <summary>
/// Šî–{UIManagerƒNƒ‰ƒX
/// </summary>
public abstract class UIManagerBase : MonoBehaviour
{
    protected virtual void Start()
    {
        GameManager.Instance.RegisterUIManager(this);
    }
    /// <summary>
    /// ‰Šú‰»
    /// </summary>
    public abstract void InitUI();
}
