using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<int> _deckMain = new List<int>();

    List<int> _deck;
    int _randomIndex;
    int _temp;

    /// <summary>
    /// ��V�ȂǂŃ��C���̃f�b�L�ɓ���邽�߂Ɏg�p�\��
    /// </summary>
    /// <param name="id"></param>
    public void AddDeck(int id)
    {
        _deckMain.Add(id);
    }

    /// <summary>
    /// �f�b�L�̃V���b�t�����\�b�g
    /// </summary>
    public void ShuffleDeck()
    {
        _deck = new List<int>(_deckMain);
        for(int i = 0; i < _deck.Count; i++)
        {
            _randomIndex = Random.Range(i, _deck.Count);

            _temp = _deck[i];
            _deck[i] = _deck[_randomIndex];
            _deck[_randomIndex] = _temp;
        }
        Debug.Log("�f�b�L���V���b�t�����܂���");
    }

    /// <summary>
    /// �f�b�L�h���[���b�\�g
    /// </summary>
    /// <returns></returns>
    public int DrawCard()
    {
        if (_deck.Count == 0)
        {
            ShuffleDeck();
        }

        int _topCard = _deck[0];
        _deck.Remove(0);
        return _topCard;
    }
}
