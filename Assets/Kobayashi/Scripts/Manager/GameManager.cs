using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("データベース")]
    [SerializeField, Tooltip("カード")] private CardDataBase _cardDataBase;
    [SerializeField, Tooltip("ステージ")] private StageDataBase _stageDataBase;
    [SerializeField, Tooltip("エネミー")] private EnemyDataBase _enemyDataBase;
    public CardDataBase CardDataBase => _cardDataBase;
    public StageDataBase StageDataBase => _stageDataBase;
    public EnemyDataBase EnemyDataBase => _enemyDataBase;

    [Header("ID")]
    [SerializeField, Tooltip("ステージID")] public int StageID = 1;

    public BattlePhase CurrentPhase;
    public bool Reset = false, IsEnemyAction = false;
    public Player Player;

    [NonSerialized] public UIManagerBase CurrentUIManager;
    [NonSerialized] public AttackManager AttackManager;
    [NonSerialized] public StageManager StageManager;
    [NonSerialized] public GenerateMapData GenerateMapData;

    private AttackManager _attackManager;
    private bool _isOrganize = false,_isDraw = false,_isAction = false,_isReward = false;

    [SerializeField]private SceneType _currentScene;
    private void Awake()
    {
        Application.targetFrameRate = 60;
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
        switch (_currentScene)
        {
            case SceneType.TitleScene:
                break;
            case SceneType.InGameScene:
                switch (CurrentPhase)
                {
                    case BattlePhase.BuildStage:
                        if (StageManager != null)
                        {
                            StageManager.CreateStage(StageID);
                            CurrentPhase = BattlePhase.Draw;
                        }
                        break;
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
                        if (_isDraw && _isOrganize) CurrentPhase = BattlePhase.Set;
                        break;
                    case BattlePhase.Set:

                        break;
                    case BattlePhase.Action:
                        if (!_isAction)
                        {
                            _attackManager = FindAnyObjectByType<AttackManager>();
                            _attackManager.AttackTurn(true);
                            _isAction = true;
                        }
                        if (IsEnemyAction)
                        {
                            StartCoroutine(_attackManager.EnemyTurn());
                            IsEnemyAction = false;
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
                    case BattlePhase.Reward:
                        if (!_isReward)
                        {
                            (CurrentUIManager as IBattleUI)?.DisplayReward();
                            _isReward = true;
                        }
                        break;
                }
                break;
            case SceneType.StageSerectScene:
                break;
        }
    }

    public void RegisterUIManager(UIManagerBase ui)
    {
        CurrentUIManager = ui;
        CurrentUIManager.InitUI();
    }

    public void SceneChange(SceneType sceneType)
    {
        SceneManager.LoadScene($"{sceneType}");
        _currentScene = sceneType;
    }
}
