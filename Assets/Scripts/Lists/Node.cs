using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject node; // Reference to this node (itself)
    public GameObject next; // Reference to the next node (connected block)

    void Awake()
    {
        node = gameObject; // Set the node to the current game object
        next = null; // Initialize next as null
    }
}
