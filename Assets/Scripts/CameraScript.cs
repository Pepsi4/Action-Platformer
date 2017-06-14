using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //private const float MaxHeroY = 0.95f;
    private const float MaxCamY = 0.35f;
    private const float MinCamY = -0.25f;

    public GameObject MainHero;

    private void Start()
    {
    }

    void Update()
    {
        Transform mainHeroTransform = MainHero.GetComponent<Transform>();

        //If the go is not too down or too high
        if (mainHeroTransform.position.y < MaxCamY && mainHeroTransform.position.y > MinCamY)
        {
            transform.position = new Vector3(mainHeroTransform.position.x, mainHeroTransform.position.y,
                transform.position.z);
            return;
        }
        transform.position = new Vector3(mainHeroTransform.position.x, transform.position.y,
            transform.position.z);
    }
}
