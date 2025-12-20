using UnityEngine;

public abstract class EffectBase : ScriptableObject
{
    public void Excute(AttackMagic magic)
    {
        OnExcute(magic);
    }
    protected abstract void OnExcute(AttackMagic magic);
}
