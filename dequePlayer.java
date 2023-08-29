/*******************************************
 /** Author:  Javier Lopez
 /*  Created: August 2022
 *******************************************/
import java.util.ArrayDeque;
import java.util.Deque;
import java.util.Scanner;

public class dequePlayer {
    static Deque<Integer> deque = new ArrayDeque<Integer>(10);

    public static void add() {

        deque.add(2);
        deque.add(6);
        deque.add(10);
        deque.add(12);
        System.out.println(deque);

    }

    public static void addLast() {
        Scanner in = new Scanner(System.in);

        System.out.println("Since you're the receptionist, what number has come?");
        String number = in.nextLine();
        deque.add(Integer.valueOf(number));
        System.out.println(deque);

    }

    public static void getLast() {
        int j;
        j = deque.getLast();
        if (j % 2 != 0) {
            deque.removeLast();
            deque.addFirst(j);
            System.out.println(deque);

        }
    }


    public static void addAgain() {
        Scanner in = new Scanner(System.in);

        System.out.println("Do you want to add a new patient?");
        String number = in.nextLine();
        deque.add(Integer.valueOf(number));
        System.out.println(deque);

        int j;
        j = deque.getLast();
        if (j % 2 != 0) {
            deque.removeLast();
            deque.addFirst(j);
            System.out.println(deque);

        }
        addAgain();
    }
}