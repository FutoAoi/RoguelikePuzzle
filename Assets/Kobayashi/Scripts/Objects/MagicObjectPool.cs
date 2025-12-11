using UnityEngine;
using UnityEngine.Pool;

public class MagicObjectPool : MonoBehaviour
{
    private static MagicObjectPool _instance;
    public static MagicObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<MagicObjectPool>();
            }

            return _instance;
        }
    }
    [SerializeField,Tooltip("オブジェクトプール化するオブジェクト")] private AttackMagic _attackMagicPrefab;
    private ObjectPool<AttackMagic> _magicPool;
    void Start()
    {
        _magicPool = new ObjectPool<AttackMagic>(
            createFunc: () => OnCreateObject(),
            actionOnGet: (obj) => OnGetObject(obj),
            actionOnRelease: (obj) => OnReleaseObject(obj),
            actionOnDestroy: (obj) => OnDestroyObject(obj),
            collectionCheck: true,
            defaultCapacity: 3,
            maxSize: 10
        );
    }

    public AttackMagic GetAttackMagic()
    {
        return _magicPool.Get();
    }
    public void ClearMagic()
    {
        _magicPool.Clear();
    }
    private AttackMagic OnCreateObject()
    {
        return Instantiate(_attackMagicPrefab, transform);
    }
    private void OnGetObject(AttackMagic attackMagic)
    {
        attackMagic.Initialize(() => _magicPool.Release(attackMagic));
        attackMagic.gameObject.SetActive(true);
    }
    private void OnReleaseObject(AttackMagic attackMagic)
    {
        Debug.Log("Release");
    }
    private void OnDestroyObject(AttackMagic attackMagic)
    {
        Destroy(attackMagic.gameObject);
    }
}
