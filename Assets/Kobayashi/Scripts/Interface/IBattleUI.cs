using System.Collections;
using UnityEngine;

public interface IBattleUI
{
    /// <summary>
    /// ƒJ[ƒh‚ğˆø‚­
    /// </summary>
    IEnumerator DrawCard();
    /// <summary>
    /// èD‚ğ•À‚×‚é
    /// </summary>
    void HandOrganize();
    void DisplayReward();
}
