using UnityEngine;

[CreateAssetMenu(menuName = "CardEffect/EffectDebug")]
public class EffectDebug : EffectBase
{
    protected override void OnExcute()
    {
        Debug.Log("”­“®");
    }
}
