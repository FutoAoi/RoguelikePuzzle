using UnityEngine;
/// <summary>
/// ステータスのデータ
/// </summary>
public class PlayerStatus
{
    public int MaxHP => _maxHP;
    public int CurrentHP => _currentHP;
    public int MaxCost => _maxCost;
    public int Money => _money;
    private int _maxHP = 10;
    private int _currentHP = 10;
    private int _maxCost = 8;
    private int _money = 0;
    /// <summary>
    /// HP上限値の上昇
    /// </summary>
    /// <param name="plus"></param>
    public void IncreaseMaxHP(int plus)
    {
        _maxHP += plus;
    }
    /// <summary>
    /// コスト上限値の上昇
    /// </summary>
    /// <param name="plus"></param>
    public void IncreaseMaxCost(int plus)
    {
        _maxCost += plus;
    }
    /// <summary>
    /// PlayerHPの回復
    /// </summary>
    /// <param name="plus"></param>
    public void IncreaseCurrentHP(int plus)
    {
        _currentHP = Mathf.Clamp(_currentHP + plus, 0, _maxHP);
    }
    /// <summary>
    /// PlayerHPの更新
    /// </summary>
    /// <param name="hp"></param>
    public void UpdateHP(int hp)
    {
        _currentHP = hp;
    }
    /// <summary>
    /// お金を支払う
    /// </summary>
    /// <param name="pay"></param>
    public void PayMoney(int pay)
    {

        if(_money - pay <= 0)
        {
            Debug.Log("お金が足りないよ！！！");
            return;
        }
        _money -= pay;
    }
    /// <summary>
    /// お金を手に入れる
    /// </summary>
    /// <param name="plus"></param>
    public void GetMoney(int plus)
    {
        _money += plus;
    }
}
