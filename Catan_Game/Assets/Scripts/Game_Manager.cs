using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    // Constants for assets
    private readonly int wheat = 1;
    private readonly int log   = 2;
    private readonly int brick = 3;
    private readonly int stone = 4;
    private readonly int wool  = 5;

    // player who's turn it is currently
    int current_player;
    Intersection[] intersections = Create_Intersections(); 

    // for future reference, order of the tokens
    // 5 2 6 3 8 10 9 12 11 4 8 10 9 4 5 6 3 11

    // Use this for initialization
    void Start()
    {
        current_player = 1;
        //TODO 
        // 4 needs to be eventually replaced by the number of players that the user selects
        int number_of_players = 4;
        for (int i = 1; i < number_of_players; i++)
        {

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    static private Intersection[] Create_Intersections()
    {
        Intersection[] new_intersections = new Intersection[59];

        // Creating a list of the order that neighbors will be arranged in. If you can
        // think of better way to make this less hideous then please do. But I thought
        // This was better than 59 lines of calling new Intersection

        // 0 will represent neighbors that do not exist for coastal cities
        int[] neighbors = { 4, 0, 5, 5, 0, 6, 6, 0, 7, 0, 8, 1, 1, 9, 2, 2, 10, 3, 7, 11, 0, 12, 4, 13, 13, 5, 14, 14, 6, 15, 15, 7, 16, 22, 12, 23, 23, 13, 24, 24, 14, 25, 15, 15, 16, 16, 16, 27, 0, 28, 17, 17, 29, 18, 18, 30, 19, 19, 31, 20, 20, 32, 21, 21, 33, 0, 0, 22, 34, 34, 23, 35, 35, 24, 36, 36, 25, 37, 37, 26, 38, 38, 27, 0, 0, 34, 44, 44, 35, 45, 45, 36, 46, 46, 37, 47, 47, 38, 0, 0, 44, 52, 52, 45, 53, 53, 46, 54, 54, 47, 0, 48, 0, 47, 49, 0, 50, 50, 0, 51 };

        int neighbor_count = 0;
        for( int intersection = 1; intersection < 55; intersection++)
        {
            int[] neighbors_to_add =
            {
                neighbors[neighbor_count],
                neighbors[neighbor_count + 1],
                neighbors[neighbor_count + 2]
            };

            new_intersections[intersection] = new Intersection(neighbors, intersection);
            neighbor_count += 3;
        }
        
        return new_intersections;
    }

}
