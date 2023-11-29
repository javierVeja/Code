/*******************************************
 /** Author: Javier Lopez
 /*  Course:  CSC 122, Fall 2022
 /*  Lab:     Lab 4: Sorting
 /*  Created: August 2022
 /*  Class:   DataGroup:  Can hold a limted but large number of items.
 /*  Related classes:  ???
 /*  Comments: Must count the number of swaps, compares, reads, and writes
 *******************************************/
public class sortPlayer {
    public static void main(String args[]) {
        int arr[] = {12, 11, 13, 5, 6, 7};

        System.out.println("Given Array");
        printArray(arr);

        MergeSort ob = new MergeSort();
        ob.sort(arr, 0, arr.length-1);

        System.out.println("\nSorted array");
        printArray(arr);
    }
}
