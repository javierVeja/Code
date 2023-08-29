/*******************************************
 /** Author:  Javier Lopez
 /*  Created: August 2022
 *******************************************/
import java.io.FileNotFoundException;

public class GraphPlayer {

	public static void main(String[] args) {
		GraphMatrix miGraph;

		try {
			// Simple test
			miGraph = new GraphMatrix("smallgraph");
			
			miGraph.color();
			
			System.out.println("Graph colored is: " );
			System.out.println(miGraph);
			
			// Test with 7 nodes and at 25 edge
			miGraph = new GraphMatrix("8x26graph");
			miGraph.color();
			System.out.println("Graph colored is: " );
			System.out.println(miGraph);
		} catch (FileNotFoundException e) {
			System.err.println("File not found!");
		}
	}
}
