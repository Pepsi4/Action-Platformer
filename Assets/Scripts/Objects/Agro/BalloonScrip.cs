using UnityEngine;

public class BalloonScrip : MonoBehaviour
{
    private const float DestroyY = -5; 

    private void Update()
    {
        //If the balloon is too down we destroy it.
        if (this.gameObject.GetComponent<Transform>().position.y <= DestroyY)
        {
            DestroyBalloon();
        }
    }

    void Start()
    {
        SetSettings();
    }

    void SetSettings()  //Adding info
    {
        ObjectInfo info = GetComponent<ObjectInfo>(); 

        info.IsDealDamage = true;
    }

    void DestroyBalloon()
    {
        DestroyImmediate(this.gameObject);
    }
}
