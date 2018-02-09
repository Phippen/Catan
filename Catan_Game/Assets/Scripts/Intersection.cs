using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour {

    // Every intersecion is given a numberical intersection
    private int id;
    // This list will represent the other intersections that this intersection is linked to
    // The neighbors will read left to right as in 0 place will be the bottom or upper left 
    // node, 1 will be upper or lower, and 2 will be upper rigth or lower right
    private int[] neighbors;
    // Doubley indexed array of 6 ints that represents yield for a node
    // First column represents the dice roll token that is connected to that node
    // Second column represents the yield that the dice roll is tied to
    private int[][] rewards;

    public Intersection(int[] init_neighbors, int init_id)
    {
        id = init_id;
        neighbors = init_neighbors;
    }

    // Getters and setters

    public int Get_Id() { return id; }
    public void Set_Id(int new_id) { id = new_id; }

    public int[] Get_Neighbors() { return neighbors; }
    public void Set_Neightbors(int new_neighbors) { id = new_neighbors; }
}
