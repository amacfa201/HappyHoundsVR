using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathFindingScript : MonoBehaviour
{

    PathRequestManager requestManager;
    CreateGrid grid;
    WanderScript wanderScript;
    TraversePath path;

    public Vector3 starty;
    public Vector3 Endeo;

    void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<CreateGrid>();
        wanderScript = GameObject.FindGameObjectWithTag("Corgi").GetComponent<WanderScript>();
        path = GameObject.FindGameObjectWithTag("Corgi").GetComponent<TraversePath>();
    }


    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        starty = startPos;
        Endeo = targetPos;
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);


        if (startNode.traversable && targetNode.traversable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.traversable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            wanderScript.foundPath = true;
            waypoints = RetracePath(startNode, targetNode);
        }
        else
        {
            Array.Resize(ref path.path, 0);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);

    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;

    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector3 directionOld = Vector3.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector3 directionNew = new Vector3(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY, path[i - 1].gridZ - path[i].gridZ);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].nodePos);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        int dstZ = Mathf.Abs(nodeA.gridZ - nodeB.gridZ);

        return dstX * dstX + dstY * dstY + dstZ * dstZ;
        //if (dstX > dstY && dstX > dstZ)
        //{
        //    if (dstY > dstZ)
        //    {
        //        return 17 * dstZ + 14 * (dstY - dstZ) + 10 * (dstX - dstY - dstZ);
        //    }
        //    else
        //    {
        //        return 17 * dstY + 14 * (dstZ - dstY) + 10 * (dstX - dstZ - dstY);
        //    }

        //}
        //else if (dstY > dstX && dstY > dstZ)
        //{
        //    if (dstX > dstZ)
        //    {
        //        return 17 * dstZ + 14 * (dstX - dstZ) + 10 * (dstY - dstX - dstZ);
        //    }
        //    else
        //    {
        //        return 17 * dstX + 14 * (dstZ - dstX) + 10 * (dstY - dstZ - dstX);
        //    }

        //}
        //else
        //{
        //    if (dstX > dstY)
        //    {
        //        return 17 * dstY + 14 * (dstX - dstY) + 10 * (dstZ - dstX - dstY);
        //    }
        //    else
        //    {
        //        return 17 * dstX + 14 * (dstY - dstX) + 10 * (dstZ - dstY - dstX);
        //    }
        //}

    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(starty, new Vector3(0.5f, 0.5f, 0.5f));

        Gizmos.color = Color.red;
        Gizmos.DrawCube(Endeo, new Vector3(0.5f, 0.5f, 0.5f));
    }

}