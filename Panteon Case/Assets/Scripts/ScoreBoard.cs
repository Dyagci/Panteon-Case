using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public GameObject[] players;
    public GameObject player;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        players = players.OrderBy( x => x.transform.position.z ).ToArray();
        text.text = ""+(players.Length- ArrayUtility.IndexOf(players,player))+"/"+players.Length+ " Place";
    }
}
