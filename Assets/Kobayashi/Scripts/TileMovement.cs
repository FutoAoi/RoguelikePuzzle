using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMovement : MonoBehaviour
{
    [SerializeField] private Image _sampleCardPrefab;
    [SerializeField] private TileHand _tileHand;
    [SerializeField] private float _scaleUpFactor = 1.7f;
    [SerializeField] private float _scaleUpDuration = 0.1f;
    private Coroutine _currentCoroutine;
    private Vector3 _originalScale;
    private bool _max = false;

    private void Start()
    {
        _originalScale = _sampleCardPrefab.transform.localScale;
    }
    /// <summary>
    /// カード拡大
    /// </summary>
    /// <param name="number"></param>
    public void ExpansionCard(int number)
    {
        if(_currentCoroutine != null)StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(ScaleCard(_tileHand.HandTile[number].rectTransform, true));
    }
    /// <summary>
    /// カード縮小
    /// </summary>
    /// <param name="number"></param>
    public void ShrinkCard(int number)
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(ScaleCard(_tileHand.HandTile[number].rectTransform, false));
    }
    private IEnumerator ScaleCard(RectTransform card,bool expand)
    {
        yield return null;
    }
}
