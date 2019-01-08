using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MunchkinLobbyManager : NetworkLobbyManager
{

    public static MunchkinLobbyManager instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        // Set the player ID appropriately
        MunchkinLobbyPlayer munchkinLobbyPlayer = lobbyPlayer.GetComponent<MunchkinLobbyPlayer>();
        MunchkinPlayer munchkinPlayer = gamePlayer.GetComponent<MunchkinPlayer>();

        munchkinPlayer.playerID = munchkinLobbyPlayer.playerID;
        return true;
    }

}
