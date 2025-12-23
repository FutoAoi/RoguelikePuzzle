using UnityEngine;
[CreateAssetMenu(menuName = "Effect/Vector")]
public class EffectChangeToAnyAllow : EffectBase
{
    [SerializeField] private MagicVector _vector;
    
    public override void OnExcute(AttackMagic magic)
    {
        MagicObjectPool.Instance.ActiveMagics[0].ChangeVector(_vector);
    }
}
