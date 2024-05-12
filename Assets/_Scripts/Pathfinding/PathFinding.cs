using System;
using System.Collections.Generic;
using UnityEngine;


    public static class PathFinding
    {
        private static List<Node> path = new List<Node>();


        public static Node FindClosestNode(this List<List<Node>> nodes, Vector2 position)
        {
            Node closestNode = new Node(Vector2.positiveInfinity);
            float closestDistance = float.MaxValue;

            foreach (var nodeList in nodes)
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
        /*public static List<Node> FindPath(this List<List<Node>> path, Vector2 myPosition,Vector2 targetPosition)
        {
            
            return new List<Node>();
        }

        private static List<Node> CheckNeighbours(List<Node> neighbours)
        {
            foreach (var neighbour in neighbours)
            {
                CheckNeighbours(neighbours);
            }
        }*/
    }
