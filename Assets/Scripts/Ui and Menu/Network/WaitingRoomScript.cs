using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class WaitingRoomScript : NetworkBehaviour
{
    public bool IsReady;
    public int ReadyCount = 0;
    public NetworkLogic NetworkLogic;
    public PlayerScript PlayerScript;

    private void Awake()
    {
        GameObject.Find("Network").GetComponent<MyNetworkManager>().WaitingRoomScript =
            this.gameObject.GetComponent<WaitingRoomScript>();
    }

    void Start()
    {
        Debug.Log("The Waiting room script started.");

        NetworkLogic = GameObject.Find("GameStatus").GetComponent<GameStatus>().MainHero.GetComponent<NetworkLogic>();

        // Button's event.
        GameObject.Find("Canvas/UI/Ready").GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Button 'ready' pressed.");
            if (IsReady == false)
            {
                // Changes status.
                GameObject.Find("UI/CurrentStatus").GetComponent<Text>().text = "You are ready";

                SendReadyToBeginMessage();
                if (ReadyCount == 2) // if 2 players are ready.
                {
                    Debug.Log("IT'S TIME TO CHANGE LEVEL");
                    NetworkLogic.CmdLoadScene("Level (0)");
                }
            }
        });
    }

    private void ClearPlayerData()
    {
        PlayerScript.Lifes = PlayerScript.LifesMax;
    }

    public void SendReadyToBeginMessage()
    {
        Debug.Log("SendReadyToBeginMessage");
        IsReady = true;
        ReadyCount++;

        try
        {
            Debug.Log("client to server msg");
            GameObject.Find("Network").GetComponent<MyNetworkManager>().PlayerClient.Send(Msg.StartGame, new IntegerMessage(ReadyCount));
        }
        catch (NullReferenceException)
        {
            Debug.Log("server to clients msg");
            NetworkServer.SendToAll(Msg.StartGame, new IntegerMessage(ReadyCount));
        }
    }
}
