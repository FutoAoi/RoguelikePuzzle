/// <summary>
/// 戦闘時のフェイズ
/// </summary>
public enum BattlePhase:int
{
    Draw,
    Set,
    Action,
    Direction
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
