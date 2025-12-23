using UnityEngine;

[CreateAssetMenu(menuName = "Effect/EffectDebug")]
public class EffectDebug : EffectBase
{
    public override void OnExcute(AttackMagic magic)
    {
        Debug.Log("”­“®");
    }
}
