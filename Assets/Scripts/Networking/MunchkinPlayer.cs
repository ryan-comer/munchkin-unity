using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MunchkinPlayer : NetworkBehaviour
{
    [SyncVar]
    public int playerID;

    public static MunchkinPlayer instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Send a command to the server to draw a card
    [Command]
    public void CmdDrawCard(DeckController.CardType cardType, int playerID)
    {
        Card newCard = DeckController.instance.DrawCard(cardType, playerID);
        RpcDrawCard(newCard.gameObject);
    }

    [ClientRpc]
    public void RpcDrawCard(GameObject newCard)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Debug.Log(newCard);
        HandController.instance.AddCardToHand(newCard.GetComponent<Card>());
    }

    public override void OnStartLocalPlayer()
    {
        instance = this;

        base.OnStartLocalPlayer();
    }

    public override void OnStartServer()
    {
        // Add connection to the manager
        if (isServer)
        {
            Debug.Log("Adding connection for: " + playerID);
            MunchkinLobbyManager.instance.connectionsDict[playerID] = GetComponent<NetworkIdentity>().connectionToClient;
        }

        base.OnStartServer();
    }

}
