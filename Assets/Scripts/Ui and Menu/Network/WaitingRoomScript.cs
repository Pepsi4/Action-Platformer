using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class WaitingRoomScript : NetworkBehaviour
{
    private bool isReady;
    public int ReadyCount = 0;
    public NetworkLogic NetworkLogic;

    private void Awake()
    {
    }

    void Start()
    {
        Debug.Log("The Waiting room script started.");

        NetworkLogic = GameObject.Find("GameStatus").GetComponent<GameStatus>().MainHero.GetComponent<NetworkLogic>();

        GameObject.Find("Canvas/UI/Ready").GetComponent<Button>().onClick.AddListener(delegate
        {
            if (isReady == false)
            {
                NetworkServer.SendToAll(Msg.StartGame, new EmptyMessage());
               // isReady = true;

                if (ReadyCount == 2) // if 2 players are ready.
                {
                    Debug.Log("IT'S TIME TO CHANGE LEVEL");
                    NetworkLogic.CmdLoadScene("Level (0)");
                }
            }
        });
    }
}
