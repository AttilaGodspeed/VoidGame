using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour {

    // stack of enemies to spawn, and spawn offset
    [SerializeField] private EnemyStack enemyStack;
    [SerializeField] private Vector3 offSet;
    //[SerializeField] private float xOffset = 0f;
    //[SerializeField] private float zOffset = 2f;
    [SerializeField] private float coolDownTime;

    // stack of soul orbs to use
    [SerializeField] private OrbStack soulOrbStack;

    // stack of sFX to play on death
    [SerializeField] private EffectStack deathStack;

    // health, etc
    [SerializeField] private int maxHealth = 5;

    private int health;
    private float coolDown;

    // Use this for initialization
    void Start () {
        health = maxHealth;
        coolDown = coolDownTime;
	}
	
	void FixedUpdate () {
        // count down to next spawn/spawn
        if (coolDown > 0)
            coolDown -= Time.deltaTime;
        else {
            coolDown = coolDownTime;
            enemyStack.pop(gameObject.transform.position + offSet);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Attack") {
            health -= 1;
            if (health < 1) {
                deathStack.pop(gameObject.transform.position);
                soulOrbStack.pop(gameObject.transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}
