using UnityEngine;
[CreateAssetMenu(menuName = "Effect/Vector")]
public class ChangeToAnyAllow : EffectBase
{
    [SerializeField] private MagicVector _vector;
    
    protected override void OnExcute()
    {
        GameManager.Instance.AttackManager.ChangeVector(_vector);
    }
}
