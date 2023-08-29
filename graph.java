/*******************************************
 /** Author:  Javier Lopez
 /*  Created: August 2022
 *******************************************/
public class graph {
    String[] nodes;
    int[] nodesColor;
    int[][] edges;

    public void color() {
        for (int i = 0; i < nodes.length; i++) {
            nodesColor[i] = assignColor(i);
        }
    }

    private int assignColor(int currNodeIdx) {
        int result = 0;
        boolean colored;
        boolean flag;
        colored = false;
        nodesColor[currNodeIdx] = nodesColor[currNodeIdx] + 1;

        while (!colored && nodesColor[currNodeIdx] <=  nodes.length) {
            flag = true;
            for (int i = 0; i < nodes.length; i++) {
                if (edges[currNodeIdx][i] == 1 && nodesColor[i] == nodesColor[currNodeIdx]) {
                    flag = false;
                }
            }
            if (flag) {
                colored = true;
                result = nodesColor[currNodeIdx];
            } else {
                nodesColor[currNodeIdx] = nodesColor[currNodeIdx] + 1;
            }
        }
        return result;
    }

    public String toString() {
        String result;
        result = "";

        for (int i = 0; i < nodes.length; i++) {
            result= result + nodes[i] + " (color: " +nodesColor[i] + ")\n";
        }
        return result;
    }

    public int getNode(String node) {
        int i;
        boolean found;
        i = 0;
        found = false;
        while (!found && i < nodes.length)
            if (node.equals(nodes[i])) found = true;
            else i++;
        if (found) return i;
        return -1;
    }

    public Vertex getVertex(String label) {
        return vertexByLabel.get(label);
    }


    public Vertex getVertex(String label) {
        Vertex result = null;
        for (Object next: vertices) {
            Vertex vertex = (Vertex) next;
            if (vertex.label.equals(label)) {
                result = vertex;
                break;
            }
        }
        return result;
    }

    private class Vertex {
    }
}
