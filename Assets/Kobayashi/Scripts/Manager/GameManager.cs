using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("データベース")]
    [SerializeField, Tooltip("カード")] public CardDataBase CardData;
    [SerializeField, Tooltip("ステージ")] public StageDataBase StageData;

    [Header("ID")]
    [SerializeField, Tooltip("ステージID")] public int StageID = 1;

    public BattlePhase CurrentPhase;
    public bool Reset = false;
    [NonSerialized] public UIManagerBase CurrentUIManager;
    [NonSerialized] public AttackManager AttackManager;

    private AttackManager _attackManager;
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
                    _attackManager = FindAnyObjectByType<AttackManager>();
                    StartCoroutine(_attackManager.AttackStart());

                    _isAction = true;
                }
                if (Reset)
                {
                    Reset = false;
                    _isDraw = false;
                    _isOrganize = false;
                    _isAction = false;

                    CurrentPhase = BattlePhase.Draw;
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
