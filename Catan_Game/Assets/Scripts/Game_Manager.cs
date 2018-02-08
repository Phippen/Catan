using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // player who's turn it is currently
    int current_player;
    Player p;


	// Use this for initialization
	void Start ()
    {
        current_player = 1;
        //TODO 
        // 4 needs to be eventually replaced by the number of players that the user selects
        number_of_players = 4;
        for(int i = 1; i < number_of_players; i++)
        {

        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
