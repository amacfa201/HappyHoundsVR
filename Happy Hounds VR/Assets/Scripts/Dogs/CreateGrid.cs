using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {

    Node[,] nodeGrid;
    public Vector2 gridSize;
    public float nodeRadius;
    public LayerMask untraversableMask;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));
        if (nodeGrid != null)
        {
            foreach (Node n in nodeGrid)
            {
                Gizmos.color = (n.traversable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.nodePos, Vector3.one * (nodeDiameter-0.1f));
            }
        }
    }

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateTheGrid();
    }

    void CreateTheGrid()
    {
        nodeGrid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2  -Vector3.forward * gridSize.y / 2;
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter +nodeRadius);
                bool traversable = !(Physics.CheckSphere(worldPoint, nodeRadius, untraversableMask));
                nodeGrid[i, j] = new Node(traversable, worldPoint);
            }
        }
    }

    
}
