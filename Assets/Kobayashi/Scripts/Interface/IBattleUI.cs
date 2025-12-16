using System.Collections;
using UnityEngine;

public interface IBattleUI
{
    /// <summary>
    /// カードを引く
    /// </summary>
    IEnumerator DrawCard();
    /// <summary>
    /// 手札を並べる
    /// </summary>
    void HandOrganize();
    /// <summary>
    /// リワード画面を表示
    /// </summary>
    void DisplayReward();
}
