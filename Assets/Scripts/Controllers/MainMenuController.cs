using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MainMenuController : MonoBehaviour
{

    public GameObject lobbyUI;  // Container for all the lobby UI components
    public GameObject activeGameListUI; // Container for the active games
    public GameObject createGameUI; // Container for creating a game

    public GameObject lobbyPlayerHorizontalLayoutOne;   // Horizontal layout for first 3 people
    public GameObject lobbyPlayerHorizontalLayoutTwo;   // Horizontal layout for last 3 people

    // Create Game Menu
    public InputField gameNameInput; // The name of the game to create

    public GameObject lobbyPlayer_p;    // Prefab for the lobby player UI

    private GameObject activeMenu;  // The currently active menu

    Dictionary<NetworkConnection, GameObject> lobbyPlayerElements;  // UI elements showing the players in the lobby

    public static MainMenuController instance;

    private void Awake()
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

    // Show the lobby screen with all the connected players
    public void ShowLobbyUI()
    {
        showWindow(lobbyUI);
    }

    // User wants to join an active game
    public void ShowJoinGameUI()
    {
        showWindow(activeGameListUI);
    }

    // Create a new game
    public void ShowCreateGameUI()
    {
        showWindow(createGameUI);
    }

    // Create a new game with a name
    public void CreateGame()
    {
        // Check for a name
        var name = gameNameInput.text;
        if(name.Length == 0)
        {
            // No name, don't create
            return;
        }

        // Create the match
        MunchkinLobbyManager.singleton.matchMaker.CreateMatch(name, 6, true, "", "", "", 0, 0, OnInternetMatchCreate);
    }

    // Ready in the lobby
    public void LobbyReady()
    {

    }

    // Add a UI element for the new player in the lobby
    public void AddPlayerToLobby(NetworkConnection networkConnection)
    {
        var lobbyPlayer = Instantiate(lobbyPlayer_p);

        // Determine which horizontal layout to use
        if(lobbyPlayerHorizontalLayoutOne.transform.childCount < 3)
        {
            lobbyPlayer.transform.SetParent(lobbyPlayerHorizontalLayoutOne.transform);
        }
        else
        {
            lobbyPlayer.transform.SetParent(lobbyPlayerHorizontalLayoutTwo.transform);
        }

        lobbyPlayerElements.Add(networkConnection, lobbyPlayer);    // Add to the dictionary
    }

    // Remove the UI element for the player that left
    public void RemovePlayerFromLobby(NetworkConnection networkConnection)
    {
        var obj = lobbyPlayerElements[networkConnection];
        Destroy(obj.gameObject);

        // Remove from the dictionary
        lobbyPlayerElements.Remove(networkConnection);
    }

    // Helper function for showing a window
    private void showWindow(GameObject window)
    {
        if (activeMenu != null)
        {
            activeMenu.SetActive(false);
        }

        window.SetActive(true);
        activeMenu = window;
    }

    // Callback for when the internet match is created
    private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        // Show the lobby screen
        showWindow(lobbyUI);
    }

}
