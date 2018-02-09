using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private string username;

    private int order_id;

    private bool my_turn;

    private Dictionary<int, int> resources;


    public Player(string init_name, int init_order_id)
    {
        username = init_name;
        order_id = init_order_id;
        my_turn = false;
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(my_turn)
        {
            // Allow dice to be rolled
            // while (not rolled)
            //      wait
            // Alert player of results of dice or any actions needed to be taken(robber)
            // Allow actions to be taken..trading, purchasing items, using dev cards
        }
	}

    void Start_Turn()
    {
        my_turn = true;
        // Alert player it's their turn
    }

    void End_Turn()
    {
        my_turn = false;
    }

    /*
     * Give this player resources.
     * resources_taken : Key/Value pair mapping "resource given"/"amount given"
     */
    void Add_Resources(Dictionary<int, int> resources_added)
    {
        foreach(int key in resources_added.Keys)
        {
            resources[key] += resources_added[key];
        }
    }

    bool Check_Sufficient_Resources(Dictionary<int, int> resources_requested)
    {
        foreach (int key in resources_requested.Keys)
        {
            if (resources[key] < resources_requested[key])
                return false;
        }

        return true;
    }

    bool Remove_Resources(Dictionary<int, int> resources_removed)
    {
        if(Check_Sufficient_Resources(resources_removed) == false)
            return false;

        foreach (int key in resources_removed.Keys)
        {
            resources[key] -= resources_removed[key];
        }

        return true;
    }
}