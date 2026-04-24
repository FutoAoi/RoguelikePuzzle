using UnityEngine;

public struct BuffStacks
{
    private byte[] _counts;

    public BuffStacks(int size) => _counts = new byte[size];

    public byte this[BuffType type]
    {
        get => _counts[(int)type];
        set => _counts[(int)type] = value;
    }
    /// <summary>
    /// 指定のバフを持っているかどうか
    /// </summary>
    /// <param name="type">バフの指定</param>
    /// <returns></returns>
    public bool Has(BuffType type) => _counts[(int)type] > 0;
    /// <summary>
    /// 全てのバフターンを減らす
    /// </summary>
    /// <param name="amount">減る量</param>
    public void DecreaseAll(byte amount = 1)
    {
        for(byte i = 0; i < _counts.Length; i++)
        {
            if (_counts[i] == 0) continue;

            _counts[i] -= amount;
            if (_counts[i] < 0)
                _counts[i] = 0;
        }
    }
}
