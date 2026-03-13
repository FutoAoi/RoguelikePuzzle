using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Audio/AudioData")]
public class AudioData : ScriptableObject
{
    public string ID => _id;
    public float Volume => _volume;

    [Header("基本設定")]
    [SerializeField, Tooltip("取得する際のID")] private string _id;
    [SerializeField, Tooltip("クリップ(複数の場合ランダム)")] private AudioClip[] _clips;
    [SerializeField, Tooltip("音量"),Range(0f, 1f)] private float _volume = 1f;

    [Header("ミキサー")] private AudioMixerGroup _mixerGroup;

    /// <summary>
    /// クリップの取得
    /// </summary>
    /// <returns></returns>
    public AudioClip GetClip()
        => _clips == null || _clips.Length == 0 ? null
         : _clips[Random.Range(0, _clips.Length)];
}

[CreateAssetMenu(menuName = "Audio/AudioDatabase")]
public class AudioDatabase : ScriptableObject
{
    public List<AudioData> Entries => _entries;
    [SerializeField] private List<AudioData> _entries;

    private Dictionary<string, AudioData> _cache;

    private void OnEnable()
        => _cache = _entries.ToDictionary(e => e.ID);

    /// <summary>
    /// オーディオデータの取得
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public AudioData Get(string id)
    {
        if (_cache.TryGetValue(id, out var data)) return data;
        Debug.LogWarning($"AudioData not found: {id}");
        return null;
    }
}