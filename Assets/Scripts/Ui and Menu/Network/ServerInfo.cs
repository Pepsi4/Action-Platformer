using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class ServerInfo : NetworkBehaviour {
    
    public static int ReadyCount = 0;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
