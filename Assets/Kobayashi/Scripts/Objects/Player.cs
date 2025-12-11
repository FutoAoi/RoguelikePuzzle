using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxPlyerHP = 10;
    public int CurrentHP = 10;
    public int MaxCost = 8;
    public bool IsDead {  get; private set; }
    
    private void Start()
    {
        IsDead = false;
    }
    /// <summary>
    /// プレイヤーが攻撃を受ける
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public IEnumerator Damaged(int damage,float duration)
    {
        CurrentHP -= damage;
        yield return new WaitForSeconds(duration);
    }
    /// <summary>
    /// プレイヤーがやられた
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayerDead(float duration)
    {
        Debug.Log($"{name}が倒れてしまった…");
        yield return new WaitForSeconds(duration);
    }
}
