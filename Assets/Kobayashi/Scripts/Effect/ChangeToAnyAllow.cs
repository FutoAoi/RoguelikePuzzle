using UnityEngine;
[CreateAssetMenu(menuName = "Effect/Vector")]
public class ChangeToAnyAllow : EffectBase
{
    [SerializeField] private MagicVector _vector;
    
    protected override void OnExcute()
    {
        MagicObjectPool.Instance.ActiveMagics[0].ChangeVector(_vector);
    }
}
