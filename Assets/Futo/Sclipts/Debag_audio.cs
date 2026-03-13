using UnityEngine;

public class Debag_audio : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayBGM("BGM_Test");
    }

    public void test()
    {
        AudioManager.Instance.PlaySE("SE_Test");
    }
}
