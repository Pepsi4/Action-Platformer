using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //private const float MaxHeroY = 0.95f;
    private const float MaxCamY = 0.35f;
    private const float MinCamY = -0.25f;

    
    
    private void Start()
    {
    }

    void Update()
    {
        Transform mainHeroTransform = GameObject.Find("MainHero").GetComponent<Transform>();

        //If the go is not too down or too high
        if (mainHeroTransform.position.y < MaxCamY && mainHeroTransform.position.y > MinCamY)
        {
            transform.position = new Vector3(mainHeroTransform.position.x, mainHeroTransform.position.y,
                transform.position.z);
            return;
        }
        //else if (mainHeroTransform.position.y > MaxHeroY) //If the go is too high
        //{
        //    GameObject.Find("MainHero").GetComponent<PlayerScript>().GetDamage();
        //} 
        
            transform.position = new Vector3(mainHeroTransform.position.x, transform.position.y,
                transform.position.z);
        

    }
}
