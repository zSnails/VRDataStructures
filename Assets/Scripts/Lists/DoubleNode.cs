using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleNode : MonoBehaviour
{
    public GameObject node; // Reference to this node (itself)
    public GameObject next; // Reference to the next node (connected block)
    public GameObject prev;

    void Awake()
    {
        node = gameObject; // Set the node to the current game object
        next = null; // Initialize next as null
        prev = null;
    }
}
