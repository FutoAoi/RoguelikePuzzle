using UnityEngine;

[CreateAssetMenu(menuName = "Effect/AddPower")]
public class EffectAddPawer : EffectBase
{
    [Header("UŒ‚—Íã¸’l")]
    [SerializeField] private int _addPower = 1;
    public override void OnExcute(AttackMagic magic)
    {
        magic.AttackPower += _addPower;
    }
}
