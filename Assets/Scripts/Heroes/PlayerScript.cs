#define FirstVersion
//#define SecondVersion

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private static int lifesMax = 3;
    /// <summary>
    /// Max value of hero's HP
    /// </summary>
    public static int LifesMax
    {
        get { return lifesMax; }
        set { lifesMax = value; }
    }

    private static int lifes = 3;
    /// <summary>
    /// Current count of hero's HP
    /// </summary>
    public static int Lifes
    {
        get { return lifes; }
        set { lifes = value; }
    }

    private const float MoveSpeed = 0.01f;
    private const float JumpPower = 0.017f;

    private static bool isCanTakeDamage = true;
    /// <summary>
    /// If it's sets true, the hero can't take any damage.
    /// </summary>
    public static bool IsCanTakeDamage
    {
        get { return isCanTakeDamage; }
        set { isCanTakeDamage = value; }
    }

    private const float InvulTime = 1f;            //how long main hero is can't be touched
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
        if (collision.gameObject.GetComponent<ObjectInfo>().IsDealDamage == true && isCanTakeDamage)
        {
            if (!IsHealthLow()) //if we have more than 0 hp
            {
                GetTheInvul();                      // Our hero is invuled for InvulTime now
                GetDamage(collision);               // Lose an HP.
            }

            if (IsHealthLow()) //If our hp is <= 0 now.
            {
                EndTheGame();
                return;
            }
        }
    }

    private void EndTheGame()
    {
        Debug.Log("Low HP");
        GameStatus.StopTheGame(true);
    }



    private bool IsHealthLow()
    {
        if (lifes <= 0)
        {
            return true;
        }
        return false;
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
        lifes--;
        isCanTakeDamage = false;
    }

    private IEnumerator DestroyTheLife(int lifeNumber, Collider2D coll)
    {
        //getting the fillAmount from the lifeNumber obj
        float fillAmount = GameObject.Find("Canvas/Health (" + lifeNumber + ")").GetComponent<Image>().fillAmount;

        if (fillAmount > 0) // if the fillAmount haven't min value (min value: 0; max: 1)
        {
            // -.01 fillAmount to the life obj
            GameObject.Find("Canvas/Health (" + lifeNumber + ")").GetComponent<Image>().fillAmount -= 0.01f;
            //waiting some time
            yield return new WaitForSeconds(0.01f);
            //recursion
            StartCoroutine(DestroyTheLife(lifeNumber, coll));
            //exit from the corountine
            yield break;
        }
        if (fillAmount <= 0) //if the fillAmount have the min value
        {
            //lifes--;
        }
    }


}
