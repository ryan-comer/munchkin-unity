using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MunchkinLobbyManager : NetworkLobbyManager
{

    public static MunchkinLobbyManager instance;

    public Dictionary<int, NetworkConnection> connectionsDict = new Dictionary<int, NetworkConnection>();

    // Start is called before the first frame update
    void Start()
    {
        StartMatchMaker();
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

    // Client connected to the server
    public override void OnClientConnect(NetworkConnection conn)
    {
        // Add a UI element for the player in the lobby
        MainMenuController.instance.AddPlayerToLobby(conn);
    }

    // Client disconnected from the server
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        // Remove the player UI
        MainMenuController.instance.RemovePlayerFromLobby(conn);
    }

}
