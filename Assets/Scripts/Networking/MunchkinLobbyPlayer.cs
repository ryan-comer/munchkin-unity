using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MunchkinLobbyPlayer : NetworkLobbyPlayer
{

    public int playerID;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClientEnterLobby()
    {
        base.OnClientEnterLobby();

        playerID = slot;
    }
}
