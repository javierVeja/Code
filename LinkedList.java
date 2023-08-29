/*******************************************
 /** Author:  Javier Lopez
 /*  Created: August 2022
 *******************************************/
import java.util.ArrayList;
import java.util.List;

public class LinkedList {

    static class Node {
        int value, weight;
        Node(int value, int weight)  {
            this.value = value;
            this.weight = weight;
        }
    }

    List<List<Node>> adj_list = new ArrayList<>();

    public LinkedList(List<LinkedList> edges) {
        for (int i = 0; i < edges.size(); i++)
            adj_list.add(i, new ArrayList<>());

        for (LinkedList e : edges)
        {
            adj_list.get(e.src).add(new Node(e.dest, e.weight));
        }
    }
    public static void printGraph(LinkedList graph)  {
        int src_vertex = 0;
        int list_size = graph.adj_list.size();

        System.out.println("The contents of the graph:");
        while (src_vertex < list_size) {
            for (Node edge : graph.adj_list.get(src_vertex)) {
                System.out.print("Vertex:" + src_vertex + " ==> " + edge.value +
                        " (" + edge.weight + ")\t");
            }

            System.out.println();
            src_vertex++;
        }
    }

    public LinkedList(int i, int i1, int i2) {
    }
}
