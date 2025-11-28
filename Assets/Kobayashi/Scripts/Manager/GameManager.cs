using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BattlePhase CurrentPhase;
    [NonSerialized] public UIManagerBase CurrentUIManager;

    [Header("データベース")]
    [SerializeField, Tooltip("カード")] public CardDataBase CardData;

    private bool _isOrganize = false,_isDraw = false,_isAction = false;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentPhase)
        {
            case BattlePhase.Draw:
                if (!_isDraw)
                {
                    DeckManager.Instance.ShuffleDeck();
                    StartCoroutine((CurrentUIManager as IBattleUI)?.DrawCard());
                    _isDraw = true;
                }
                if (!_isOrganize)
                {
                    (CurrentUIManager as IBattleUI)?.HandOrganize();
                    _isOrganize = true;
                }
                if(_isDraw && _isOrganize)CurrentPhase = BattlePhase.Set;
                break;
            case BattlePhase.Set:

                break;
            case BattlePhase.Action:
                if (!_isAction)
                {



                    _isAction = true;
                }
                break;
            case BattlePhase.Direction:

                break;
        }
    }

    public void RegisterUIManager(UIManagerBase ui)
    {
        CurrentUIManager = ui;
        CurrentUIManager.InitUI();
    }
}
