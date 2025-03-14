using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sokoban : MonoBehaviour
{
    public List<GameObject> rocks;
    public float moveDistance = 0.16f;
    private bool isMovingRock = false; // Flag to prevent multiple rock movements at once
    
    void OnCollisionStay2D(Collision2D collision)
    {
        
    }

}