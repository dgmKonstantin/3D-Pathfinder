using System;
using System.Collections.Generic;

namespace Pathfinder
{
    public struct NodeWeight
    {
        public Node Node;
        public int Weight;

        public static NodeWeight Create(Node n, int weight)
        {
            return new NodeWeight()
            {
                Node = n,
                Weight = weight
            };
        }
    }

    public class PrioQueue
    {
        private List<NodeWeight> elements = new List<NodeWeight>();


        public void AddQueue(PrioQueue Queue)
        {
            elements.AddRange(Queue.elements);
        }

        public void Add(Node node, int weight)
        {
            elements.Add(NodeWeight.Create(node, weight));
        }


        public int Count
        {
            get { return elements.Count; }
        }

        public void Enqueue(Node node, int weight)
        {
            elements.Add(NodeWeight.Create(node, weight));
        }

        public Node Dequeue()
        {
            int index = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Weight < elements[index].Weight)
                {
                    index = i;
                }
            }

            Node node = elements[index].Node;
            elements.RemoveAt(index);
            return node;
        }

        public List<Node> GetNodes()
        {
            var result = new List<Node>();

            for (int c = 0; c < elements.Count; c++)
            {
                result.Add(elements[c].Node);
            }

            return result;
        }
    }
}