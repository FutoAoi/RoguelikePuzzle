using UnityEngine;
using System;

public interface IEffect 
{
    public void OnExcute(AttackMagic magic);
}

[Serializable]
public class EffectDebug : IEffect
{
    public void OnExcute(AttackMagic magic)
    {
        Debug.Log("î≠ìÆ");
    }
}

[Serializable]
public class EffectChangeToAnyAllow : IEffect
{
    [SerializeField] private MagicVector _vector;

    public void OnExcute(AttackMagic magic)
    {
        magic.ChangeVector(_vector);
    }
}

[Serializable]
public class EffectAddPawer : IEffect
{
    [Header("çUåÇóÕè„è∏íl")]
    [SerializeField] private int _addPower = 1;
    public void OnExcute(AttackMagic magic)
    {
        magic.AttackPower += _addPower;
    }
}