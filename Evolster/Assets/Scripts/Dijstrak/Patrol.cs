using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed = 5f;
    public List<Transform> patrolPoints;
    private Graph graph;
    private Transform currentPatrolPoint;
    private List<Transform> currentPath;

    void Start()
    {
        graph = new Graph(patrolPoints);

        // Configura las conexiones específicas
        graph.AddEdge(patrolPoints[0], patrolPoints[1]);
        graph.AddEdge(patrolPoints[1], patrolPoints[2]);
        graph.AddEdge(patrolPoints[2], patrolPoints[3]);
        graph.AddEdge(patrolPoints[3], patrolPoints[0]);


        currentPatrolPoint = patrolPoints[0];
        MoveToNextPatrolPoint();
    }

    void Update()
    {
        if (currentPath != null && currentPath.Count > 1)
        {
            Transform targetNode = currentPath[1];
            MoveTowards(targetNode.position);

            if (Vector3.Distance(transform.position, targetNode.position) < 0.1f)
            {
                currentPath.RemoveAt(0);

                if (currentPath.Count == 1)
                {
                    MoveToNextPatrolPoint();
                }
            }
        }
    }

    void MoveTowards(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void MoveToNextPatrolPoint()
    {
        UpdatePathToNextPatrolPoint();
    }

    void UpdatePathToNextPatrolPoint()
    {
        Transform nextPatrolPoint = GetRandomPatrolPoint();

        while (nextPatrolPoint == currentPatrolPoint)
        {
            nextPatrolPoint = GetRandomPatrolPoint();
        }

        currentPath = graph.Dijkstra(currentPatrolPoint, nextPatrolPoint);

        currentPatrolPoint = nextPatrolPoint;
    }

    Transform GetRandomPatrolPoint()
    {
        return patrolPoints[Random.Range(0, patrolPoints.Count)];
    }
}

public class Graph
{
    private Dictionary<Transform, List<Transform>> adjacencyList;

    public Graph(List<Transform> nodes)
    {
        adjacencyList = new Dictionary<Transform, List<Transform>>();

        foreach (Transform node in nodes)
        {
            adjacencyList[node] = new List<Transform>();
        }
    }
    public void AddEdge(Transform node1, Transform node2)
    {
        adjacencyList[node1].Add(node2);
    }
    public List<Transform> Dijkstra(Transform start, Transform end)
    {
        Dictionary<Transform, float> distance = new Dictionary<Transform, float>();
        Dictionary<Transform, Transform> previous = new Dictionary<Transform, Transform>();
        List<Transform> unvisitedNodes = new List<Transform>();

        foreach (Transform node in adjacencyList.Keys)
        {
            distance[node] = float.MaxValue;
            previous[node] = null;
            unvisitedNodes.Add(node);
        }

        distance[start] = 0;

        while (unvisitedNodes.Count > 0)
        {
            Transform currentNode = GetMinimumDistanceNode(unvisitedNodes, distance);
            unvisitedNodes.Remove(currentNode);

            foreach (Transform neighbor in adjacencyList[currentNode])
            {
                float alt = distance[currentNode] + Vector3.Distance(currentNode.position, neighbor.position);
                if (alt < distance[neighbor])
                {
                    distance[neighbor] = alt;
                    previous[neighbor] = currentNode;
                }
            }
        }

        return GetShortestPath(end, previous);
    }

    private Transform GetMinimumDistanceNode(List<Transform> nodes, Dictionary<Transform, float> distance)
    {
        Transform minNode = null;
        foreach (Transform node in nodes)
        {
            if (minNode == null || distance[node] < distance[minNode])
            {
                minNode = node;
            }
        }
        return minNode;
    }

    private List<Transform> GetShortestPath(Transform end, Dictionary<Transform, Transform> previous)
    {
        List<Transform> path = new List<Transform>();
        Transform currentNode = end;

        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = previous[currentNode];
        }

        path.Reverse();
        return path;
    }
}
