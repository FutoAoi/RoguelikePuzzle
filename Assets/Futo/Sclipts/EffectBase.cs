using UnityEngine;

public abstract class EffectBase : ScriptableObject
{
    public void Excute()
    {
        OnExcute();
    }
    protected abstract void OnExcute();
}
