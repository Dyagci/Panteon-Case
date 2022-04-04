using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public GameObject[] players;
    public PlayerController playerController;
    public Text text;
    public int placement;
    private GameObject player;
    void Start()
    {
        player = playerController.gameObject;
    }
    void Update()
    {
        if (!playerController.passedFinish)
        {
            players = players.OrderBy( x => x.transform.position.z ).ToArray();
            text.text = ""+(players.Length- Array.IndexOf(players,player))+"/"+players.Length+ " Place";
        }
    }

    public int FindPlacement(GameObject gameObject)
    {
        return (players.Length - Array.IndexOf(players, gameObject));
    }
}
