using UnityEngine;

[CreateAssetMenu(menuName = "Effect/EffectDebug")]
public class EffectDebug : EffectBase
{
    protected override void OnExcute()
    {
        Debug.Log("”­“®");
    }
}
