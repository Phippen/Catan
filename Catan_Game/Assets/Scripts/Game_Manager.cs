using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Manager : MonoBehaviour
{
    // Constants for assets
    static public readonly int wheat = 1;
    static public readonly int log   = 2;
    static public readonly int brick = 3;
    static public readonly int stone = 4;
    static public readonly int wool  = 5;
    static public readonly int desert  = 6;

    // player who's turn it is currently
    int current_player;
    Intersection[] intersections = Create_Intersections();
    int[,] tiles = Create_Tile_List();

    // dictionary that holds what intersection gets what yeild for a roll
    // the key is the roll value while the arraylist will hold a touple that contains
    // the intersection id and the yield
    private Dictionary<int, ArrayList> roll_yeilds = new Dictionary<int, ArrayList>();
    

    // Use this for initialization
    void Start()
    {
        Debug.Log("Game Starting");
        current_player = 1;
        //TODO 
        // 4 needs to be eventually replaced by the number of players that the user selects
        int number_of_players = 4;
        for (int i = 1; i < number_of_players; i++)
        {

        }

        for(int i = 0; i < 19; i++)
        {
            Debug.Log(tiles[i, 0] + ", " + tiles[i, 1]);
        }

        for (int i = 2; i < 13; i++)
        {
            roll_yeilds.Add(i, new ArrayList());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    static private int[,] Create_Tile_List()
    {
        // List of of dice roll tokens in order
        int[] tokens = { 5, 2, 6, 3, 8, 1, 0, 9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3, 11 };
        // List of possible yields
        int[] yields = { wheat, wheat, wheat, wheat, log, log, log, log, brick, brick, brick, stone, stone, stone, wool, wool, wool, wool, desert};
        // Make it random so we can insert it into the board
        yields = Randomize(yields);

        // 19 tiles, first column is the dice roll token it has, second is its yield
        int[,] tiles = new int[19, 2];

        int j = 0;
        for(int i = 0; i < 19; i++)
        {
            // place a yield in each tile
            tiles[i, 1] = yields[i];
            // if the yield isn't desert then add a token to it
            if(yields[i] != 6)
            {
                tiles[i, 0] = tokens[j];
                j++;
            }
        }

        return tiles;
    }

    static private Intersection[] Create_Intersections()
    {
        Intersection[] new_intersections = new Intersection[54];

        // Creating a list of the order that neighbors will be arranged in. If you can
        // think of better way to make this less hideous then please do. But I thought
        // This was better than 59 lines of calling new Intersection

        // 0 will represent neighbors that do not exist for coastal cities
        int[,] neighbors = { { 4, 0, 5 },    //1
                             { 5, 0, 6 },    //2
                             { 6, 0, 7 },    //3
                             { 0, 8, 1 },    //4
                             { 1, 9, 2 },    //5
                             { 2, 10, 3 },   //6
                             { 7, 11, 0 },   //7
                             { 12, 4, 13 },  //8
                             { 13, 5, 14 },  //9
                             { 14, 6, 15 },  //10
                             { 15, 7, 16 },  //11
                             { 22, 12, 23 }, //12
                             { 23, 13, 24 }, //13
                             { 24, 14, 25 }, //14
                             { 10, 20, 11 }, //15
                             { 11, 21, 0 },  //16
                             { 22, 12, 23 }, //17
                             { 23, 13, 24 }, //18
                             { 24, 14, 25 }, //19
                             { 25, 15, 26 }, //20
                             { 26, 16, 27 }, //21
                             { 0, 28, 27 },  //22
                             { 17, 29, 18 }, //23
                             { 18, 30, 19 }, //24
                             { 19, 31, 20 }, //25
                             { 20, 32, 21 }, //26
                             { 21, 33, 0 },  //27
                             { 0, 22, 34 },  //28
                             { 34, 23, 35 }, //29
                             { 35, 24, 36 }, //30
                             { 36, 25, 37 }, //31
                             { 37, 26, 38 }, //32
                             { 38, 27, 0 },  //33
                             { 28, 39, 29 }, //34
                             { 29, 40, 0 },  //35
                             { 30, 41, 31 }, //36
                             { 31, 42, 32 }, //37
                             { 32, 43, 33 }, //38
                             { 0, 34, 44 },  //39
                             { 44, 35, 45 }, //40
                             { 45, 36, 46 }, //41
                             { 46, 37, 47 }, //42
                             { 47, 38, 0 },  //43
                             { 39, 48, 40 }, //44
                             { 40, 49, 41 }, //45
                             { 41, 50, 42 }, //46
                             { 42, 51, 43 }, //47
                             { 0, 44, 52 },  //48
                             { 52, 45, 53 }, //49
                             { 53, 46, 54 }, //50
                             { 54, 47, 0 },  //51
                             { 48, 0, 47 },  //52
                             { 49, 0, 50 },  //53
                             { 50, 0, 51 } };//54
        
        for(int intersection = 0; intersection < 54; intersection++)
        {
            int[] neighbors_to_add =
            {
                neighbors[intersection, 0],
                neighbors[intersection, 1],
                neighbors[intersection, 2]
            };

            new_intersections[intersection] = new Intersection(neighbors_to_add, intersection);
        }
        
        return new_intersections;
    }

    // Goes through each tile and intersection and inserts the yield for each intersection
    private void Add_Intersection_Yeilds()
    {

        // for every dice roll
        for(int roll = 2; roll < 13; roll++)
        {
            // for every tile
            for (int tile = 0; tile < tiles.Length; tile++)
            {
                int token = tiles[tile, 0];
                int yield = tiles[tile, 1];
                // if the tile's token is the roll
                if (token == roll)
                {
                    Add_Yeild_To_Dictionary(roll, tile, yield);
                }
            }

        }
    }

    private void Add_Yeild_To_Dictionary(int roll, int tile, int yield)
    {
        // list of the tiles that a given intersection is connected to
        // first index is the intersection, the second will be a list of 3 ints for the 3
        // tiles it is connected to. 0 represents a coast
        int[,] tiles_connected_to_intersection =
        {
            { -1, 0, -1 },
            { -1, 1, -1 },
            { -1, 2, -1 },
            { -1, -1, 0 },
            { 0, -1, 1 },
            { 1, -1, 2 },
            { 2, -1, -1 },
            { -1, 11, 0 },
            { 0, 12, 1 },
            { 1, 13, 2 },
            { 2, 3, -1 },
            { -1, -1, 11 },
            { 11, 0, 12 },
            { 12, 1, 13 },
            { 13, 2, 3 },
            { 3, -1, -1 },
            { -1, 10, 11 },
            { 11, 17, 12 },
            { 12, 18, 13 },
            { 13, 14, 3 },
            { 3, 4, -1 },
            { -1, -1, 10 },
            { 10, 11, 17 },
            { 17, 12, 18 },
            { 18, 13, 14 },
            { 14, 3, 4 },
            { 4, -1, -1 },
            { -1, -1, 10 },
            { 10, 9, 17  },
            { 17, 16, 18 },
            { 18, 15, 14 },
            { 14, 5, 4 },
            { -1, -1, 4 },
            { -1, 10, 9 },
            { 9, 17, 16 },
            { 16, 18, 15 },
            { 15, 14, 5 },
            { 5, 4, -1 },
            { -1, -1, 9 },
            { 9, 8, 16 },
            { 16, 7, 15 },
            { 15, 6, 5 },
            { -1, -1, 5 },
            { -1, 9, 8 },
            { 8, 16, 7 },
            { 7, 15, 6 },
            { 6, 5, -1 },
            { -1, -1, 8 },
            { 8, -1, 7 },
            { 7, -1, 6 },
            { 6, -1, -1 },
            { -1, -1, 5 },
            { -1, -1, 7 },
            { -1, -1, 6 }
        };

        for(int i = 0; i < tiles_connected_to_intersection.Length; i ++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(tile == tiles_connected_to_intersection[i, j])
                {
                    int[] roll_yeild = { i, yield };
                    roll_yeilds[roll].Add(roll_yeild);
                }
            }
        }

    }

    static private int[] Randomize(int[] given_array)
    {
        System.Random rng = new System.Random();
        int n = given_array.Length - 1;
        for(n = given_array.Length-1; n > -1; n--)
        {
            int k = rng.Next(n);
            int value = given_array[k];
            given_array[k] = given_array[n];
            given_array[n] = value;
        }
        return given_array;
     }
    

}
