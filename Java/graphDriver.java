/*******************************************
 /** Author:  Javier Lopez
 /*  Created: August 2022
 *******************************************/
import java.util.*;

public class graphDriver{
    public static void main (String[] args) {
        List<LinkedList> edges = Arrays.asList(new LinkedList(0, 1, 2), new LinkedList(0, 2, 4),
                new LinkedList(1, 2, 4), new LinkedList(2, 0, 5), new LinkedList(2, 1, 4),
                new LinkedList(3, 2, 3), new LinkedList(4, 5, 1), new LinkedList(5, 4, 3));

        LinkedList graph = new LinkedList(edges);

        LinkedList.printGraph(graph);

    }
}
