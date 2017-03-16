using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStack : MonoBehaviour {

    EnemyManager temp;
    Stack<EnemyManager> stack = new Stack<EnemyManager>();

    // Use this for initialization
    void Awake() {

    }

    public void pop(Vector3 location) {
        //print("popping bat");
        temp = stack.Pop();
        if (temp != null) {
            temp.deploy(location);
        }
    }

    public void push(EnemyManager entity) {
        //print("pushing bat");
        stack.Push(entity);
    }
}
