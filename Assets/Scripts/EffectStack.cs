using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStack : MonoBehaviour {

    EffectManager temp;
    Stack<EffectManager> stack = new Stack<EffectManager>();

	// Use this for initialization
	void Awake () {
		
	}
	
    public void pop (Vector3 location) {
        temp = stack.Pop();
        if (temp != null) {
            temp.playAt(location);
        }
    }

    public void push(EffectManager effect) {
        stack.Push(effect);
    }
}
