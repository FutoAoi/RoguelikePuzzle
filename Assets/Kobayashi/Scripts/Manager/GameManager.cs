using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BattlePhase CurrentPhase;
    [NonSerialized] public UIManagerBase CurrentUIManager;
    private bool _isOrganize = false;

    private void Awake()
    {
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
                if (!_isOrganize)
                {
                    (CurrentUIManager as IBattleUI)?.HandOrganize();
                    _isOrganize = true;
                }
                break;
            case BattlePhase.Set:

                break;
            case BattlePhase.Action:

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
