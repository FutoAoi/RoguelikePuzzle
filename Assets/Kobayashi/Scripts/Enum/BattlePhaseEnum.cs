/// <summary>
/// 戦闘時のフェイズ
/// </summary>
public enum BattlePhase:int
{
    Draw,
    Set,
    Action,
    Direction,
    Reward,
    BuildStage
}
/// <summary>
/// 攻撃の方向指定
/// </summary>
public enum MagicVector : int
{
    UP,
    Down,
    Right,
    Left,
}
/// <summary>
/// カードのレアリティ
/// </summary>
public enum CardRarity : int
{
    Uncommon,
    Common,
    Rare,
    Epic,
    Legendary
}
/// <summary>
/// エネミーの攻撃種類
/// </summary>
public enum EnemyAttackType : int
{
    Normal,
    Penetrating,
}
/// <summary>
/// シーンの名前
/// </summary>
public enum SceneType : int
{
    TitleScene,
    InGameScene,
    StageSerectScene
}