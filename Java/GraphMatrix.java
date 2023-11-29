/*******************************************
 /** Author:  Javier Lopez
 /*  Created: August 2022
 *******************************************/
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.util.Scanner;

public class GraphMatrix {
    private final int  MAX_NODES = 100;

	int[][] edges;
    String[] nodes;
    int[] nodesColor;	// 0 means without color
    
    public GraphMatrix(String[] nodes) {
        int size, i, j;
        this.nodes = nodes;
        size = nodes.length;
        edges = new int[size][size];
        for (i=0; i < size; i++) 
            for (j=size-1; j >= 0; j--)
                edges[i][j] = 0;
    }
    
    public GraphMatrix(String filename) throws FileNotFoundException {
        Scanner dataFile = null;
        String line;
        int numNodes;
        String[] nodesSplitted;
        int node1, node2;

        try {
        	dataFile = new Scanner(new FileReader(filename));

        	// Load number of nodes
        	line = dataFile.nextLine();
        	numNodes = Integer.valueOf(line);
        	
        	// Reserve memory for nodes
        	nodes = new String[numNodes];
        	nodesColor = new int[numNodes];
        	
            // Load nodes
        	for (int i = 0; i < numNodes; i++) {
        		nodes[i] = dataFile.nextLine();
        	}
        	
            // Load vertex
        	edges = new int[MAX_NODES][MAX_NODES];
        	while (dataFile.hasNext()) {
        		line = dataFile.nextLine();
        		nodesSplitted = line.split(" ");
        		// Create vertex
        		node1 = findNode(nodesSplitted[0]);
        		node2 = findNode(nodesSplitted[1]);
        		edges[node1][node2] = 1;
        	}
        } finally {
        	if (dataFile != null) {
        		dataFile.close();
        	}
        }
    }
    
    
    public int findNode(String node) {
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
            
        
    // mutators
    public void changeEdge(int newValue, String source, String dest, boolean digraph) {
        int s, d;
        
        s = findNode(source);
        d = findNode(dest);
        if (digraph) addEdge(newValue,s,d);
        else {
            addEdge(newValue,s,d);
            addEdge(newValue,d,s);
        }
    }
    
    private void addEdge(int newValue, int s, int d) {
        edges[s][d] = newValue;
    }
    /*
    public String breadthSearch(String first) {
        boolean visited[];
        Queue myQueue;
        String result;
        String cur;
        int vertex;
       
        visited = new boolean(nodes.length);
        for (int i=0; i < nodes.length; i++)
            visited(i) = false;
        myQueue = new Queue();
        result = "";
        
        myQueue.enqueue(first); 
        while (!myQueue.isEmpty()) {
            cur = myQueue.dequeue();
            vertex = lookupVertex(cur);
            if (!visited[vertex]) {
                result += cur+" ";
                visited[vertex] = true;
                for (int i=0; i < nodes.length; i++) {
                    if (edges[vertex][i] == 1)
                        myQueue.enqueue(nodes[i]);
                }
            }
        }
    }

    public String depthSearch(String first) {
        boolean visited[];
        Stack myStack;
        String result;
        String cur;
        int vertex;
       
        visited = new boolean(nodes.length);
        for (int i=0; i < nodes.length; i++)
            visited(i) = false;
        myStack = new Stack();
        result = "";
        
        myStack.push(first); 
        while (!myStack.isEmpty()) {
            cur = myStack.pop();
            vertex = lookupVertex(cur);
            if (!visited[vertex]) {
                result += cur+" ";
                visited[vertex] = true;
                for (int i=0; i < nodes.length; i++) {
                    if (edges[vertex][i] != 0)
                        myStack.push(nodes[i]);
                }
            }
        }
    }
*/
}
