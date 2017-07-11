using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private const float MaxCamY = 0.35f;
    private const float MinCamY = -0.25f;

    public GameObject MainHero;

    void Update()
    {
        try
        {
            Transform mainHeroTransform = MainHero.GetComponent<Transform>();

            // If MainHero is not too down or too high.
            if (mainHeroTransform.position.y < MaxCamY && mainHeroTransform.position.y > MinCamY)
            {
                // Changes X and Y on camera's transform.
                transform.position = new Vector3(mainHeroTransform.position.x, mainHeroTransform.position.y,
                    transform.position.z);
                return;
            }
            // Changes only X on camera's transform.
            transform.position = new Vector3(mainHeroTransform.position.x, transform.position.y,
                transform.position.z);
        }
        catch (UnassignedReferenceException)
        {
            Debug.Log("MainHero can not be found!");
        }
    }
}
