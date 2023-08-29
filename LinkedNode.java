/*******************************************
 /** Author:  Dr. Cathy Bareiss
 /*  Editor: Javier Lopez
 /*  Course:  CSC 122, Fall 2022
 /*  Lab:     Lab 4: Sorting
 /*  Created: August 2022
 /*  Class:   LinkedNode -> used for linked lists, etc.
 /*  Related classes:  LinkedQueue
 *******************************************/

public class LinkedNode {
    private Object item;
    private LinkedNode next;

    // constructor
    public LinkedNode(Object item) {
        this.item = item;
        next = null;
    }

    // accessors
    public Object getItem() {
        return item;
    }

    public LinkedNode getNext() {
        return next;
    }

    // mutators
    public void setItem(Object item) {
        this.item = item;
    }

    public void setNext(LinkedNode next) {
        this.next = next;
    }
}
