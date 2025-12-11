using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private CardDataBase _cardData;

    private void Start()
    {
        _cardData = GameManager.Instance.CardDataBase;
    }
}
