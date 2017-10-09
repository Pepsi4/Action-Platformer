using UnityEngine;

public class CastleScript : MonoBehaviour, ISettings
{
    void Start()
    {
        SetSettings();
    }

    public void SetSettings()
    {
        ObjectInfo info = GetComponent<ObjectInfo>();

        info.IsDealDamage = true;
    }
}
