using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongNetworkManager : NetworkManager
{

    [SerializeField] Transform leftPaddleSpawn, rightPaddleSpawn;
    [SerializeField] GameObject ballPrefab;
    GameObject ball;


    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform start = numPlayers == 0 ? leftPaddleSpawn : rightPaddleSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == 2)
        {

            ball = Instantiate(ballPrefab);
            NetworkServer.Spawn(ball);

        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        if (ball) NetworkServer.Destroy(ball);
    }

}
