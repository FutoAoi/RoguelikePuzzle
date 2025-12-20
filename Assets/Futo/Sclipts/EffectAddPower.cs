using UnityEngine;

[CreateAssetMenu(menuName = "Effect/AddPower")]
public class EffectAddPawer : EffectBase
{
    [SerializeField] private int _addPower = 1;
    protected override void OnExcute(AttackMagic magic)
    {
        magic.AttackPower += _addPower;
    }
}
