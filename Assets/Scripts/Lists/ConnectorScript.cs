using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorScript : MonoBehaviour
{
    public float detectionRadius = 1.0f; // Adjust this value to set the range for detecting the connector

    void Update()
    {
        // Find all objects with the tag "Connectable"
        GameObject[] connectables = GameObject.FindGameObjectsWithTag("Connectable");
        GameObject[] connectors = GameObject.FindGameObjectsWithTag("Connector");

        // Reset all next references before performing the check
        foreach (GameObject connector in connectors)
        {
            Node connectorNode = connector.GetComponent<Node>();
            if (connectorNode != null)
            {
                connectorNode.next = null;
            }
        }

        foreach (GameObject connectable in connectables)
        {
            // Check if the connectable object is close to any connector object
            foreach (GameObject connector in connectors)
            {
                float distance = Vector3.Distance(connectable.transform.position, connector.transform.position);
                if (distance <= detectionRadius)
                {
                    // Reposition the connectable object to share its center with the connector object
                    connectable.transform.position = connector.transform.position;

                    // Update the next reference
                    Node connectorNode = connector.GetComponent<Node>();
                    Node connectableNode = connectable.GetComponent<Node>();

                    if (connectorNode != null && connectableNode != null)
                    {
                        connectorNode.next = connectableNode.node;
                    }
                }
            }
        }
    }
}
