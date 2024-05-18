using System;
using System.Collections.Generic;
using UnityEngine;

    public class PathFindingComponent : CoreComponent
    {
        private List<Node> path = new List<Node>();
        private List<Node> checkedNeighbours = new List<Node>();
        

        // TODO: need to make this search algorithm faster by using Binary search
        public Node FindClosestNode(Vector2 position)
        {
            Node closestNode = new Node(Vector2.positiveInfinity);
            float closestDistance = float.MaxValue;

            foreach (var nodeList in NodeGraph.Instance.Nodes)
            {
                foreach (var node in nodeList)
                {
                    float distance = Vector2.Distance(node.WorldPosition, position);
                    if (closestDistance > distance)
                    {
                        closestDistance = distance;
                        closestNode = node;
                    }
                }
            }
            return closestNode;
        }
        public Node FindClosestNode(GameObject targetObject)
        {
            return FindClosestNode(gameObject.transform.position);
        }
        public List<Node> FindPath(Node startingNode, Node targetNode)
        {
            List<Node> empty = new List<Node>();
            empty.Add(startingNode);
            Queue<Node> nodesToCheck = new Queue<Node>();

            HashSet<Node> visitedNodes = new HashSet<Node>();

            Dictionary<Node, Node> parentChildNodes = new Dictionary<Node, Node>();
            
            nodesToCheck.Enqueue(startingNode);
            visitedNodes.Add(startingNode);
            while (nodesToCheck.Count > 0)
            {
                Node currentNode = nodesToCheck.Dequeue();

                if (currentNode.Equals(targetNode))
                {
                    return ReconstructPath(parentChildNodes, targetNode);
                }

                foreach (Node neighbour in currentNode.Neighbours)
                {
                    if (!visitedNodes.Contains(neighbour))
                    {
                        visitedNodes.Add(neighbour);
                        nodesToCheck.Enqueue(neighbour);
                        parentChildNodes.Add(neighbour,currentNode);
                    }
                }
            }
            return empty;
        }
        public List<Node> FindPath(GameObject startingGameObject, GameObject targetGameObject)
        {
            Node startingNode = FindClosestNode(startingGameObject);
            Node targetNode = FindClosestNode(targetGameObject);
            return FindPath(startingNode, targetNode);
        }
        public List<Node> FindPath(GameObject startingGameObject, Node targetNode)
        {
            Node startingNode = FindClosestNode(startingGameObject);
            return FindPath(startingNode, targetNode);
        }
        public List<Node> FindPath(Node startingNode, GameObject targetGameObject)
        {
            Node targetNode = FindClosestNode(targetGameObject);
            return FindPath(startingNode, targetNode);
        }
        private List<Node> ReconstructPath(Dictionary<Node, Node> parents, Node goal)
        {
            Node current = goal;
            List<Node> pathToGoal = new List<Node>();
            while (parents.ContainsKey(current))
            {
                pathToGoal.Insert(0,current);
                current = parents[current];
            }
            return pathToGoal;
        }
        private List<Node> CheckNeighbours(List<Node> neighbours, Node targetNode)
        {
            if (path[^1].Equals(targetNode)) return path;

            foreach (var neighbour in neighbours)
            {
                if (neighbour.WorldPosition == targetNode.WorldPosition)
                {
                    path.Add(neighbour);
                    return path;
                }
                else if ((neighbour.TileType == TileType.aboveGround || neighbour.TileType == TileType.InAir) && !checkedNeighbours.Contains(neighbour))
                {
                    path.Add(neighbour);
                    checkedNeighbours.Add(neighbour);

                    CheckNeighbours(neighbour.Neighbours, targetNode);

                    if (path[^1].Equals(targetNode)) return path;
                }
                else
                {
                    checkedNeighbours.Add(neighbour);
                }


            }

            path.Remove(path[^1]);
            return path;
        }
    }
