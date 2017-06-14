#define FirstVersion
//#define SecondVersion

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;


public class PlayerScript : NetworkBehaviour
{
    /// <summary>
    /// The GameStatus prefab. Needs for the server side.
    /// </summary>
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

    [SyncVar]
    private int lifes = 3;

    /// <summary>
    /// Current count of hero's HP
    /// </summary>
    public int Lifes
    {
        get { return lifes; }
        set { lifes = value; }
    }

    private const float MoveSpeed = 0.01f;
    private const float JumpPower = 0.017f;

    private bool isCanTakeDamage = false;
    /// <summary>
    /// If it's sets true, the hero can't take any damage.
    /// </summary>
    public bool IsCanTakeDamage
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
        Debug.Log("PlayerScript started");
        rb = GetComponent<Rigidbody2D>();

        NetworkLogic = GetComponent<NetworkLogic>();
        //Loads scene on the server.
        NetworkLogic.CmdLoadScene();
    }

    #region Server Contact Place

    NetworkLogic NetworkLogic;

    /// <summary>
    /// The health UI. Uses as on the server side and on the client side.
    /// </summary>
    private GameObject[] _healthUi = new GameObject[4];

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("Current Level : " + level);
        if (level == 2 && isLocalPlayer)
        {
            CreateLocalObjects();
            isCanTakeDamage = true;
        }
    }

    [Client]
    //Smells like a local player spirit
    public void CreateLocalObjects()
    {
        //--- BalloonSpawner ---

        //
        //this.gameObject.AddComponent<BalloonSpawner>();

        //--- Camera ---
        if (GameObject.Find("Camera") == false)
        {
            GameObject camera = (GameObject)Resources.Load("GameObjects/MainCamera");
            camera.GetComponent<CameraScript>().MainHero = this.gameObject;
            camera = Instantiate(camera);
            camera.name = "Camera";
        }

        //--- GameStatus ---
        GameObject gameStatusPrefab = null;

        if (GameObject.Find("GameStatus") == false)
        {

            gameStatusPrefab = Instantiate((GameObject)Resources.Load("GameObjects/GameStatus"));
            gameStatusPrefab.GetComponent<GameStatus>().MainHero = this.gameObject;
            gameStatusPrefab.name = "GameStatus";
        }

        //--- GameScore ---
        GameObject gameScorePrefab = null;

        if (GameObject.Find("GameScore") == false)
        {
            gameScorePrefab = (GameObject)Resources.Load("GameObjects/GameScore");
            gameScorePrefab.GetComponent<GameScore>().GameStatusPrefab = gameStatusPrefab.GetComponent<GameStatus>();
            gameScorePrefab = Instantiate(gameScorePrefab);
            gameScorePrefab.name = "GameScore";
        }
        //--- Canvas ---
        if (GameObject.Find("Canvas") == false)
        {
            SetCanvas(gameScorePrefab, gameStatusPrefab);
        }

    }

    private void SetCanvas(GameObject gameScore, GameObject gameStatusPrefab)
    {
        GameObject canvas = (GameObject)Resources.Load("GameObjects/Canvas");

        var canvasPrefab = Instantiate(canvas);
        canvasPrefab.name = "Canvas";

        //Load form the resources.
        _healthUi[1] = (GameObject)Resources.Load("GameObjects/Health (1)");
        _healthUi[2] = (GameObject)Resources.Load("GameObjects/Health (2)");
        _healthUi[3] = (GameObject)Resources.Load("GameObjects/Health (3)");
        GameObject timePanel = (GameObject)Resources.Load("GameObjects/TimePanel");

        //Create exemplairs.
        _healthUi[1] = Instantiate(_healthUi[1]);
        _healthUi[2] = Instantiate(_healthUi[2]);
        _healthUi[3] = Instantiate(_healthUi[3]);
        timePanel = Instantiate(timePanel);

        //Change the position.
        _healthUi[1].GetComponent<Transform>().position = new Vector2(_healthUi[1].GetComponent<Transform>().position.x, Screen.height - 50);
        _healthUi[2].GetComponent<Transform>().position = new Vector2(_healthUi[2].GetComponent<Transform>().position.x, Screen.height - 50);
        _healthUi[3].GetComponent<Transform>().position = new Vector2(_healthUi[3].GetComponent<Transform>().position.x, Screen.height - 50);
        timePanel.GetComponent<Transform>().position = new Vector2(timePanel.GetComponent<Transform>().position.x, Screen.height - 100);

        //Set the parent the Canvas.
        _healthUi[1].transform.SetParent(canvasPrefab.transform);
        _healthUi[2].transform.SetParent(canvasPrefab.transform);
        _healthUi[3].transform.SetParent(canvasPrefab.transform);
        timePanel.transform.SetParent(canvasPrefab.transform);

        //Set needed GameStatus or GameScore to the UI`s scripts.
        canvasPrefab.GetComponentInChildren<ScorePanelScript>().GameScorePrefab = gameScore.GetComponent<GameScore>();
        canvasPrefab.GetComponentInChildren<ScorePanelScript>().GameStatusPrefab = gameStatusPrefab.GetComponent<GameStatus>();
        canvasPrefab.GetComponent<UiScript>().GameStatusPrefab = gameStatusPrefab;
        canvasPrefab.GetComponent<UiScript>().TimePanel = timePanel;
    }
    #endregion

    private void OnTriggerStay2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.GetComponent<ObjectInfo>().IsDealDamage == true && isCanTakeDamage)
            {
                if (!IsHealthLow()) //if we have more than 0 hp
                {
                    Debug.Log("Dmg recived from : " + collision.gameObject.name);
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
        if (IsHealthLow())
        {
            ShowResult(connectionToServer.connectionId);
        }
        isCanTakeDamage = false;
    }

    public void ShowResult(int connectionId)
    {
        NetworkLogic.CmdShowResult(this.gameObject, new Color(0, 1, 0), connectionId);
    }

    private IEnumerator DestroyTheLife(int lifeNumber)
    {
        NetworkLogic.CmdDebugLog("Destroy the Life : " + lifeNumber + " On the :" + connectionToServer.connectionId);
        //getting the fillAmount from the lifeNumber obj
        float fillAmount = _healthUi[lifeNumber].GetComponent<Image>().fillAmount;//GameObject.Find("Canvas/Health (" + lifeNumber + ")").GetComponent<Image>().fillAmount;

        if (fillAmount > 0) // if the fillAmount haven't min value (min value: 0; max: 1)
        {
            // -.01 fillAmount to the life obj
            _healthUi[lifeNumber].GetComponent<Image>().fillAmount -= 0.01f;
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