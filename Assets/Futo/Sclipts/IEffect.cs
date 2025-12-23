using UnityEngine;

public interface IEffect 
{
    public void OnExcute(AttackMagic magic);
}

public class EffectDebug : IEffect
{
    public void OnExcute(AttackMagic magic)
    {
        Debug.Log("î≠ìÆ");
    }
}

public class EffectChangeToAnyAllow : IEffect
{
    [SerializeField] private MagicVector _vector;

    public void OnExcute(AttackMagic magic)
    {
        MagicObjectPool.Instance.ActiveMagics[0].ChangeVector(_vector);
    }
}

public class EffectAddPawer : IEffect
{
    [Header("çUåÇóÕè„è∏íl")]
    [SerializeField] private int _addPower = 1;
    public void OnExcute(AttackMagic magic)
    {
        magic.AttackPower += _addPower;
    }
}