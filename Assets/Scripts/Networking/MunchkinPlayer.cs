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
    public void CmdDrawCard(DeckController.CardType cardType)
    {
        Card newCard = DeckController.instance.DrawCard(cardType, playerID);
        RpcDrawCard(newCard.gameObject, playerID);
    }

    [ClientRpc]
    public void RpcDrawCard(GameObject newCard, int ID)
    {
        newCard.GetComponent<Card>().playerOwner = ID;

        // Continue only if this is the local player
        if (!isLocalPlayer)
        {
            return;
        }

        HandController.instance.AddCardToHand(newCard.GetComponent<Card>());
    }

    // Called by a client that wants authority over the dice
    [Command]
    public void CmdRequestDiceControl()
    {
        var diceNetID = GameController.instance.dice.GetComponent<NetworkIdentity>();
        NetworkConnection playerConnection = MunchkinLobbyManager.instance.connectionsDict[playerID];

        // Remove old authority if it has one
        if(diceNetID.clientAuthorityOwner != null)
        {
            diceNetID.RemoveClientAuthority(diceNetID.clientAuthorityOwner);
        }

        diceNetID.AssignClientAuthority(playerConnection);
    }

    // Set up the static singleton
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
            MunchkinLobbyManager.instance.connectionsDict[playerID] = GetComponent<NetworkIdentity>().connectionToClient;
        }

        base.OnStartServer();
    }

}
