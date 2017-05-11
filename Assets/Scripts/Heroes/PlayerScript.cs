#define FirstVersion
//#define SecondVersion

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;


public class PlayerScript : NetworkBehaviour
{
    public GameStatus GameStatusPrefab;

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
    private const float InvulAnimationTime = 0.2f; //how often main hero is bllinking
    //Main hero's rigibody
    //using to move it.
    private Rigidbody2D rb;

    void Update()
    {
        Debug.Log("Local " + isLocalPlayer + "isServer" + isServer);
        if (isLocalPlayer || isServer)
        {
            MoveInput();
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("PlayerScript start");
        rb = GetComponent<Rigidbody2D>();
        CmdLoadScene();
    }

    

    /// <summary>
    /// The count of current connections.
    /// </summary>
    [SyncVar]
    public int Connections;

    [Command]
    void CmdLoadScene()
    {
        if (Connections == 1)
        {
            NetworkManager.singleton.ServerChangeScene("Level (0)");
            return;
        }

        Connections++;
        Debug.Log(Connections + " : Connections ");
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            Debug.Log("Level 2");
            CreateLocalObjects();
        }
    }

    public void CreateLocalObjects()
    {
        //--- MainHero ---
        //NetworkServer.Spawn(MainHero);


        //--- Camera ---
        GameObject camera = (GameObject)Resources.Load("GameObjects/MainCamera");
        camera.GetComponent<CameraScript>().MainHero = this.gameObject;
        Instantiate(camera);

        //--- GameStatus ---
        GameObject gameStatus = (GameObject)Resources.Load("GameObjects/GameStatus");
        Instantiate(gameStatus);

        //--- GameScore ---
        GameObject gameScore = (GameObject)Resources.Load("GameObjects/GameScore");
        gameScore.GetComponent<GameScore>().GameStatusPrefab = gameStatus.GetComponent<GameStatus>();
        Instantiate(gameScore);

        //--- Canvas ---
        GameObject canvas = (GameObject)Resources.Load("GameObjects/Canvas");
        canvas.GetComponentInChildren<ScorePanelScript>().GameScorePrefab = gameScore.GetComponent<GameScore>();
        canvas.GetComponentInChildren<ScorePanelScript>().GameStatusPrefab = gameStatus.GetComponent<GameStatus>();
        canvas.GetComponentInChildren<UiScript>().GameStatusPrefab = gameStatus.GetComponent<GameStatus>();
        var canvasPrefab = Instantiate(canvas);
        canvasPrefab.name = "Canvas";

        //--- Event System ---
        //Instantiate((GameObject)Resources.Load("GameObjects/EventSystem"));
    }

    //Smells like a local player spirit
    //public override void OnStartServer()
    //{
    //    Debug.Log("OnStartServer");
    //    //CreateLocalObjects();
    //}

    ////What we should to initialize for the local player. Camera, balloon spawner, etc.
    //public void OnConnectedToServer()
    //{
    //    Debug.Log("OnStartLocalPlayer");
    //    CreateLocalObjects();
    //}

    //What we should to initialize for the local player. Camera, balloon spawner, etc.
    //public override void OnStartLocalPlayer()
    //{
    //    Debug.Log("OnStartLocalPlayer");
    //    CreateLocalObjects();
    //}



    private void OnTriggerStay2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.GetComponent<ObjectInfo>().IsDealDamage == true && isCanTakeDamage)
            {
                if (!IsHealthLow()) //if we have more than 0 hp
                {
                    GetTheInvul(); // Our hero is invuled for InvulTime now
                    GetDamage(); // Lose an HP.
                }

                if (IsHealthLow()) //If our hp is <= 0 now.
                {
                    EndTheGame();
                    return;
                }
            }
        }
        catch (NullReferenceException) // if the triggered game object has`t objInfo component
        {
            Debug.Log("Gameoject has`t ObjectInfo component");
        }
    }

    private void EndTheGame()
    {
        Debug.Log("Low HP");
        GameStatusPrefab.StopTheGame(true);
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

    private void GetDamage()
    {
        StartCoroutine(DestroyTheLife(lifes));
        lifes--;
        isCanTakeDamage = false;
    }

    private IEnumerator DestroyTheLife(int lifeNumber)
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
            StartCoroutine(DestroyTheLife(lifeNumber));
            //exit from the corountine
            yield break;
        }
        if (fillAmount <= 0) //if the fillAmount have the min value
        {
            //lifes--;
        }
    }
}
