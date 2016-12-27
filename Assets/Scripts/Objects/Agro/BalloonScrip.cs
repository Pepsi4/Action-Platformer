using UnityEngine;

public class BalloonScrip : MonoBehaviour
{
    void Start()
    {
        SetSettings();
    }

    void SetSettings()  //Adding info
    {
        ObjectInfo info = GetComponent<ObjectInfo>(); 

        info.IsDealDamage = true;
    }
}
