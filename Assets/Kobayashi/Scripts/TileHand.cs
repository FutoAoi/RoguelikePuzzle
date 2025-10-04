using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
/// <summary>
/// ��D
/// </summary>
public class TileHand : MonoBehaviour
{
    [Header("��D")]
    public List<GameObject> HandTile = new List<GameObject>();
    [Header("�ꏊ"), SerializeField] private Transform _playerHandTransfrom;
    [Header("�Ԋu"), SerializeField] private float _distance = 1;
    private Vector3 _newPos;
    private float _startX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// ��D����ׂ�
    /// </summary>
    public void HandOrganize()
    {
        _startX = -(HandTile.Count - 1) * _distance / 2f;
        for (int i = 0; i < HandTile.Count; i++)
        {
            HandTile[i].transform.SetParent(_playerHandTransfrom);
            _newPos = new Vector3(_startX + i * _distance, 0, 0);
            HandTile[i].transform.localPosition = _newPos;
            HandTile[i].transform.localRotation = Quaternion.identity;
        }
    }
}
