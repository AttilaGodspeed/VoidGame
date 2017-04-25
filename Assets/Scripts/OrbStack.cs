using System.Collections.Generic;
using UnityEngine;

// Holds a stack of orbs of one type
// Has no function to re-supply its contents,
// make sure therre are enough orbs in the stack for all the portals

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
