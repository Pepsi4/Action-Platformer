using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : NetworkBehaviour
{
    public GameObject MainHero;

    private void Start()
    {
        while (true)
        {
            if (ClientScene.ready)
            {
                Debug.Log("Spawn1");
                NetworkServer.AddPlayerForConnection(connectionToServer, MainHero, playerControllerId);
                Instantiate(MainHero);
                return;
            }
        }
    }
}
