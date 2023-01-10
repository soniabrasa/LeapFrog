using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform shore;
    public GameObject frog;

    GameObject[] rockPrefabs;
    Vector3 initFrogPosition;

    int nRocks, totalRocks;
    int nTest, nJumps, totalJumps;
    float separation;
    float media;

    bool testOver, gameOver;

    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        // if ( testOver ) {
        //     return;
        // }

        if ( nRocks < totalRocks )
        {
            if ( Input.GetKeyDown(KeyCode.Space) ) {
                Jump();
            }
        }

        else {
            if ( Input.GetKeyDown(KeyCode.Space) ) {
                TeleTransport();
                RestartTest();
            }
        }
    }

    void InitGame()
    {
        gameOver = false;
        nTest = 0;
        totalJumps = 0;
        totalRocks = 10;
        rockPrefabs = new GameObject[totalRocks];
        separation = 1.8f;

        SpawnRocks();
        InitTest();
    }

    void InitTest()
    {
        testOver = false;
        nTest++;
        nJumps = 0;
        nRocks = 0;
        initFrogPosition = FrogCs.instance.GetPosition();
    }

    Vector3 ToPoint()
    {
        int n = Random.Range( nRocks, rockPrefabs.Length );

        Vector3 frogPosition = FrogCs.instance.GetPosition();
        Vector3 rockPosition = rockPrefabs[n].transform.position;

        // print("Position rana antes: " + frogPosition );
        // print("Num. roca destino: " + n );
        // print("Position roca destino: " + rockPosition );

        rockPosition.y = frogPosition.y;

        nRocks = n+1;

        return rockPosition;
    }

    void Jump()
    {
        Vector3 toPoint = ToPoint();
        // FrogCs.instance.SetPosition( toPoint );
        FrogCs.instance.DiagonalDirection ( toPoint );
        FrogCs.instance.SetMovement( true );
        nJumps++;
    }

    void TeleTransport()
    {
        FrogCs.instance.SetPosition( initFrogPosition );
    }

    void RestartTest()
    {
        print($" Saltos test {nTest}: {nJumps}");
        totalJumps += nJumps;
        media = (float)totalJumps / (float)nTest;
        print($"Media: {media}");

        InitTest();
    }

    void SpawnRocks()
    {
        Vector3 rockPosition = shore.transform.position;

        for ( int i=0; i < rockPrefabs.Length; i++ ) {
            rockPosition.x += separation;

            // Instantiate( prefab, posicion, rotacion );
            rockPrefabs[i] = Instantiate (
                rockPrefab,
                rockPosition,
                Quaternion.identity
            );
        }
    }

    void OnGUI()
    {
        GUI.Label( new Rect(10, 10, 400, 20), "Test nº. " + nTest );
        GUI.Label( new Rect(110, 10, 400, 20), "Saltos: " + nJumps );

        GUI.Label( new Rect(800, 10, 400, 20), "Total Ensayos: " + (nTest -1) );
        GUI.Label( new Rect(910, 10, 400, 20), "Total Saltos: " + totalJumps );
        GUI.Label( new Rect(1010, 10, 400, 20), "Media: " + media );

    }
}
