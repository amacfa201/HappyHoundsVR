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

    }

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateTheGrid();
    }

    void CreateTheGrid()
    { }
}
