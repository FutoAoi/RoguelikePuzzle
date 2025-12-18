using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public RewardCard[] RewardCards => _rewardCard;
    public bool IsFinishTurnAnimation = false;

    private CardDataBase _cardData;
    [SerializeField] private RewardCard[] _rewardCard;
    [SerializeField] private CardRarity _rarity;
    [SerializeField] private int _serectReward = 0;
    [SerializeField] private Button _getButton;
    [SerializeField] private Button _skipButton;

    public void Reward()
    {
        _cardData = GameManager.Instance.CardDataBase;
        _getButton.onClick.AddListener(GetReward);
        _skipButton.onClick.AddListener(RewardSkip);
        foreach (var card in _rewardCard)
        {
            card.SetCard(_cardData.GetRandomCardIDByRarity(_rarity));
        }
    }

    public void GetReward()
    {
        DeckManager.Instance.AddDeck(_rewardCard[_serectReward].CardID);
        GameManager.Instance.SceneChange(SceneType.StageSerectScene);
    }

    public void RewardSkip()
    {
        GameManager.Instance.SceneChange(SceneType.StageSerectScene);
    }

    public void SetRewardNumber(int number)
    {
        _serectReward = number;
    }

    /// <summary>
    /// カードがめくれるアニメーション
    /// </summary>
    public IEnumerator RewardAnimation()
    {
        foreach(RewardCard rewardCard in _rewardCard)
        {
            rewardCard.TurnCardAnimation();
            yield return new WaitUntil(() => rewardCard.IsFinish);
        }
        IsFinishTurnAnimation = true;
    }
}
