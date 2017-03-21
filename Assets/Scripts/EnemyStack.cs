using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStack : MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;

    EnemyManager temp;
    Stack<EnemyManager> stack = new Stack<EnemyManager>();

    // Use this for initialization
    void Awake() {

    }

    public void pop(Vector3 location) {
        // make more effects, if you need them
        if (stack.Count < 3) {
            Instantiate(enemyPrefab);
            print("Instantiating Enemy....");
        }
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
