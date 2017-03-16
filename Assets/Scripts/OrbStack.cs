using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbStack : MonoBehaviour {
    OrbManager temp;
    Stack<OrbManager> stack = new Stack<OrbManager>();

    // Use this for initialization
    void Awake() {

    }

    public void pop(Vector3 location) {
        temp = stack.Pop();
        if (temp != null) {
            temp.deploy(location);
        }
    }

    public void push(OrbManager orb) {
        stack.Push(orb);
    }
}
