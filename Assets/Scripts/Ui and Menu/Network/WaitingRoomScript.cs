using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class WaitingRoomScript : NetworkBehaviour
{
    public bool IsReady;
    public int ReadyCount = 0;
    public NetworkLogic NetworkLogic;

    private void Awake()
    {
        GameObject.Find("Network").GetComponent<MyNetworkManager>().WaitingRoomScript =
            this.gameObject.GetComponent<WaitingRoomScript>();
    }

    void Start()
    {
        Debug.Log("The Waiting room script started.");

        NetworkLogic = GameObject.Find("GameStatus").GetComponent<GameStatus>().MainHero.GetComponent<NetworkLogic>();

        GameObject.Find("Canvas/UI/Ready").GetComponent<Button>().onClick.AddListener(delegate
        {
            if (IsReady == false)
            {
                //NetworkServer.SendToAll(Msg.StartGame, new EmptyMessage());
                // isReady = true;
                SendReadyToBeginMessage();
                if (ReadyCount == 2) // if 2 players are ready.
                {
                    Debug.Log("IT'S TIME TO CHANGE LEVEL");
                    NetworkLogic.CmdLoadScene("Level (0)");
                }
            }
        });
    }

    public void SendReadyToBeginMessage()
    {
        IsReady = true;
        ReadyCount++;
        NetworkLogic.CmdDebugLog(ReadyCount + "READY COUNT CMD");
        NetworkServer.SendToAll(Msg.StartGame, new IntegerMessage(ReadyCount));
    }

}
