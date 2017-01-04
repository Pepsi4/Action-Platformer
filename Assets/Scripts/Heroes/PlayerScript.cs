#define FirstVersion
//#define SecondVersion

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private static int lifes = 3;

    private const float MoveSpeed = 0.01f;
    private const float JumpPower = 0.017f;

    private static bool isCanTakeDamage = true;
    public static bool IsCanTakeDamage
    {
        get { return isCanTakeDamage; }
        set { isCanTakeDamage = value; }
    }
               
    private const float InvulTime = 1f;            //how long main hero is can't be toched
    private const float InvulAnimationTime = 0.2f; //how often main hero is blblinking
    //Main hero's rigibody
    //using for moving him
    private Rigidbody2D rb;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveInput();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If main hero is triggering with the objects 
        //with IsDealDamage = true fields 
        //and it is not invuled
        try
        {
            if (collision.gameObject.GetComponent<ObjectInfo>().IsDealDamage == true && isCanTakeDamage)
            {
                GetTheInvul(); // Our hero is invuled for InvulTime now
                GetDamage(collision); //Lose an HP
            }
        }
        catch (System.NullReferenceException ex)
        {
            //ex code here
        }
    }

    private void GetTheInvul()
    {
        StartCoroutine(InvulThePlayerAnimation());
        StartCoroutine(InvulThePlayer()); 
    }

    IEnumerator InvulThePlayer()
    {
        yield return new WaitForSeconds(InvulTime);
        isCanTakeDamage = true;
    }

    IEnumerator InvulThePlayerAnimation()
    {
        for (int x = 0; x < 5; x++)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(InvulAnimationTime);
        }
        GetComponent<SpriteRenderer>().enabled = true;
    }
    
    private void MoveInput()
    {
#if (FirstVersion)
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(MoveSpeed, 0f));
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-MoveSpeed, 0f));
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, MoveSpeed));
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector2(0f, -MoveSpeed));
        }
    
#endif

#if (SecondVersion)

        //User forces
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(MoveSpeed, 0f));
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-MoveSpeed, 0f));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, JumpPower));
        }
#endif
    }

    private void GetDamage(Collider2D coll)
    {
        StartCoroutine(DestroyTheLife(lifes, coll));
        isCanTakeDamage = false;
    }

    private IEnumerator DestroyTheLife(int lifeNumber, Collider2D coll)
    {
        float fillAmount = GameObject.Find("Canvas/Health (" + lifeNumber + ")").GetComponent<Image>().fillAmount;

        if (fillAmount > 0)
        {
            GameObject.Find("Canvas/Health (" + lifeNumber + ")").GetComponent<Image>().fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(DestroyTheLife(lifes, coll));
            yield break;
        }
        if (fillAmount <= 0)
        {
            lifes--;
        }
    }


}
