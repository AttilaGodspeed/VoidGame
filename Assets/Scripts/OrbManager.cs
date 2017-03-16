using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour {

    [SerializeField] OrbStack parentStack;
    [SerializeField] GameManager gameManager;

    void Awake() {
        reStack();	
    }

    void OnCollisionEnter(Collision col) {
        //print("Orb is colliding with " + col.gameObject.tag);
        if (col.gameObject.tag == "Player") {
            // add orb to inventory
            gameManager.addOrb();
            // restack self
            reStack();
        }
    }

    public void deploy(Vector3 location) {
        gameObject.SetActive(true);
        gameObject.transform.position = location;
    }

    public void reStack() {
        gameObject.SetActive(false);
        parentStack.push(this);
    }
}
