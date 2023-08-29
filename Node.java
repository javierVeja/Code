/*******************************************
 /** Author:  Javier Lopez
 /*  Course:  CSC 122, Fall 2022
 /*  Lab:     Lab 6: Huffman binary tree
 /*  Created: October 2022
 *******************************************/
import java.util.Scanner;

 class Node {
    Integer data;
    Node left;
    Node right;
    Node() {
        data = null;
        left = null;
        right = null;
    }
}

class huffmanTree {
    Node head;
    Scanner input = new Scanner(System.in);
    huffmanTree() {

        head = null;
    }

    public void createNode(Node temp, Node newNode) {

        if(head==null) {
            System.out.println("No value exist in tree, the value just entered is set to Root");
            return;
        }
        if(temp==null)
            temp = head;

        System.out.println("where you want to insert this value, l for left of ("+temp.data+") ,r for right of" +
                " ("+temp.data+")");
        char inputValue=input.next().charAt(0);
        if(inputValue=='l'){
            if(temp.left==null) {
                temp.left=newNode;
                System.out.println("value got successfully added to left of ("+temp.data+")");
                return;
            }else  {
                System.out.println("value left to ("+temp.data+") is occupied 1by ("+temp.left.data+")");
                createNode(temp.left,newNode);
            }
        }
        else if(inputValue=='r') {
            if(temp.right==null) {
                temp.right=newNode;
                System.out.println("value got successfully added to right of ("+temp.data+")");
                return;

            }else  {
                System.out.println("value right to ("+temp.data+") is occupied by ("+temp.right.data+")");
                createNode(temp.right,newNode);
            }

        }else{
            System.out.println("incorrect input plz try again , correctly");
            return;
        }

    }
    public Node generateTree(){
        int [] a = new int[10];
        int index;
        index = 0;
        while(index) a[index] = getData();
        index++;

        if(a.length==0 ){
    return null;
    }
    Node newNode= new Node();
        return generateTreeWithArray(newNode,a,0);

    }
    public Node generateTreeWithArray(Node head,int [] a,int index){

        if(index >= a.length)
            return null;
        System.out.println("at index "+index+" value is "+a[index]);
        if(head==null)
            head= new Node();
        head.data = a[index];
        head.left=generateTreeWithArray(head.left,a,index*2+1);
        head.right=generateTreeWithArray(head.right,a,index*2+2);
        return head;
    }

    public Integer getData() {
        System.out.println("Enter the value to insert:");
        return input.nextInt();
    }

    public void print() {
        inorder(head);
    }

    public void inorder(Node tempHead) {
        if(tempHead!=null) {
            inorder(tempHead.left);
            System.out.println(tempHead.data);
            inorder(tempHead.right);
        }
        else
            return;
    }
}

