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

    private bool isCanTakeDamage = true;
    private const float InvulTime = 2f;

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
            GetTheInvul();
            GetDamage(collision);
        }
    }

    private void GetTheInvul()
    {
        StartCoroutine(InvulThePlayer());
    }

    IEnumerator InvulThePlayer()
    {
        yield return new WaitForSeconds(InvulTime);
        isCanTakeDamage = true;
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
        Debug.Log(lifeNumber);
        float fillAmount = GameObject.Find("Canvas/Health (" + lifeNumber + ")").GetComponent<Image>().fillAmount;

        if (fillAmount > 0)
        {
            GameObject.Find("Canvas/Health (" + lifeNumber + ")").GetComponent<Image>().fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(DestroyTheLife(lifes, coll));
            yield break;
        }
        if (fillAmount <= 0)
        {
            lifes--;
        }

    }



}
