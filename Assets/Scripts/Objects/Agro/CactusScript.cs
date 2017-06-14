using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusScript : MonoBehaviour
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
