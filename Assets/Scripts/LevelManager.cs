using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    protected LevelManager() { }
    //Music
    public AudioClip music;
    //Seed management
    public int seed = 00000;
    //PlayerList
    public GameObject[] players;

    void Start()
    {

        DontDestroyOnLoad(gameObject);

        PlayerScan();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerScan()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}
