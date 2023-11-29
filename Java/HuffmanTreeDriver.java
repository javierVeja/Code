/*******************************************
 /** Author:  Javier Lopez
 /*  Course:  CSC 122, Fall 2022
 /*  Lab:     Lab 6: Huffman binary tree
 /*  Created: October 2022
 *******************************************/
import java.util.Scanner;

class HuffmanTreeDriver {
    public static Integer getData() {
        System.out.println("Enter the value to insert:");
        return input.nextInt();
    }
    public static huffmanTree createBinaryTree() {

        return new huffmanTree();
    }
    public static void main(String[] args) {

        displayMenu();
    }
    static huffmanTree treeObj = null;
    static Scanner input = new Scanner(System.in);
    public static void displayMenu() {
        int choice;
        do{
            System.out.print("\n Basic operations on a tree:");
            System.out.print("\n 1. Create tree  \n 2. Insert \n 3. Search value \n 4. print list\n 5. generate a " +
                    "tree \n Else. Exit \n Choice:");
            choice = input.nextInt();

            switch(choice)
            {
                case 1:
                    treeObj = createBinaryTree();
                    break;
                case 2:
                    Node newNode= new Node();
                    newNode.data = getData();
                    newNode.left=null;
                    newNode.right=null;
                    treeObj.createNode(treeObj.head,newNode);
                    break;
                case 3:
                    break;
                case 4:
                    System.out.println("inorder traversal of list gives follows");
                    treeObj.print();
                    break;
                case 5:
                    Node tempHead = treeObj.generateTree();
                    System.out.println("inorder traversal of list with head = ("+tempHead.data+")gives follows");
                    treeObj.inorder(tempHead);
                    break;
                default:
                    return;
            }
        }while(true);
    }

}
