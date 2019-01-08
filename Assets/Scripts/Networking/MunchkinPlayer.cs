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

    public override void OnStartLocalPlayer()
    {
        Debug.Log("On Start Local Player: " + playerID);
        instance = this;
        base.OnStartLocalPlayer();
    }

}
