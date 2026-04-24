using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IBuffable
{
    #region 変数宣言
    private BuffStacks _buffs = new BuffStacks((int)BuffType.End);
    #endregion
    #region 基本処理
    /// <summary>
    /// ダメージを与える
    /// </summary>
    /// <param name="damage">ダメージ数</param>
    public abstract void Damaged(int damage);
    #endregion

    #region バフ
    public void AddBuff(BuffType type, int time)
    {
        _buffs[type] = (byte)Mathf.Clamp(_buffs[type] + time,0,255);
    }

    public void DecreaseAll(byte amount = 1)
    {
        _buffs.DecreaseAll(amount);
    }
    public bool HasBuff(BuffType type) => _buffs.Has(type);

    #endregion
}
