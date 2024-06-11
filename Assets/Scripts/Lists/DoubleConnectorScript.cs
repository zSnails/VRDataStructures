using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleConnectorScript : MonoBehaviour
{
    public float detectionRadius = 1.0f; // Adjust this value to set the range for detecting connectors

    void Update()
    {
        // Find all objects with the tag "Connectable"
        GameObject[] connectables = GameObject.FindGameObjectsWithTag("Connectable");
        GameObject[] rightConnectors = GameObject.FindGameObjectsWithTag("RightConnector");
        GameObject[] leftConnectors = GameObject.FindGameObjectsWithTag("LeftConnector");

        // Reset all next and prev references before performing the check
        ResetAllNodes(rightConnectors);
        ResetAllNodes(leftConnectors);

        // Handle right connections
        HandleConnections(connectables, rightConnectors, isRightConnection: true);

        // Handle left connections
        HandleConnections(connectables, leftConnectors, isRightConnection: false);
    }

    private void ResetAllNodes(GameObject[] connectors)
    {
        foreach (GameObject connector in connectors)
        {
            DoubleNode connectorNode = connector.GetComponent<DoubleNode>();
            if (connectorNode != null)
            {
                connectorNode.next = null;
                connectorNode.prev = null;
            }
        }
    }

    private void HandleConnections(GameObject[] connectables, GameObject[] connectors, bool isRightConnection)
    {
        foreach (GameObject connectable in connectables)
        {
            foreach (GameObject connector in connectors)
            {
                float distance = Vector3.Distance(connectable.transform.position, connector.transform.position);
                if (distance <= detectionRadius)
                {
                    // Reposition the connectable object to share its center with the connector object
                    connectable.transform.position = connector.transform.position;

                    // Update the next or prev reference
                    DoubleNode connectorNode = connector.GetComponent<DoubleNode>();
                    DoubleNode connectableNode = connectable.GetComponent<DoubleNode>();

                    if (connectorNode != null && connectableNode != null)
                    {
                        if (isRightConnection)
                        {
                            connectorNode.next = connectableNode.node;
                            connectableNode.prev = connectorNode.node;
                        }
                        else
                        {
                            connectorNode.prev = connectableNode.node;
                            connectableNode.next = connectorNode.node;
                        }
                    }
                }
            }
        }
    }
}
