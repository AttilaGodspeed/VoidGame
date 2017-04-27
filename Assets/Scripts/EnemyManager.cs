using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    // game manager
    [SerializeField] private GameManager gameManager;

    // stack for this enemy type
    [SerializeField] private EnemyStack parentStack;

    // other stats
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private EffectStack deathStack;

    [SerializeField] private Transform player; //the enemy's target

    [SerializeField] private float moveSpeed = 3f; //move speed
    [SerializeField] private float rotationSpeed = 3; //speed of turning

    // stats for the attack
    [SerializeField] private float attackRange = 1f; // distance enemy will start attacking from
    [SerializeField] private int attackDamage = 1; // the damage the attack does (integer)
    [SerializeField] private float attackCooldown = 0.5f; // time in seconds until it can attack again

    [SerializeField] private EffectStack attackEffect; // attack SFx

    [SerializeField] private AudioSource attackSound; // attack sound 

    private Transform targetPosition;
    private bool los;
    private RaycastHit impact;
    private float distance, cooldown;
    private int health, damage;
    private string temp;

    void Awake() {
        // add self to the stack of its type
        restack();
    }

    // spawn behaviours
    public void deploy (Vector3 location) {
        gameObject.SetActive(true);
        gameObject.transform.position = location;
        los = false;
        cooldown = attackCooldown;
        health = maxHealth;
    }

    public void restack() {
        gameObject.SetActive(false);
        parentStack.push(this);
    }

    private void OnTriggerEnter(Collider other) {
        temp = other.tag;
        print("Enemy collided with trigger: " + other.name + " which has a tag of: " + temp);
        if (temp.Contains("Attack")) {
            damage = int.Parse(temp.Substring(0,1));
            health -= damage;
            if (health < 1) {
                deathStack.pop(gameObject.transform.position);

                // update counter
                ScoreCounter.self.enemyKills ++;

                restack();
            }
        }
    }

    private void FixedUpdate() {
        // check LOS
        if (Physics.Linecast(gameObject.transform.position, player.position, out impact) && impact.collider.tag == "Player")
            los = true;
        else
            los = false;

        // update distance to player.
        distance = Vector3.Distance(gameObject.transform.position, player.position);

        // update cooldown (if needed)
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        }

        // attack if in range
        if (distance < attackRange) {
            // and if off cooldown
            if (!(cooldown > 0)) {
                attackEffect.pop(player.position);
                attackSound.Play();
                cooldown = attackCooldown;

                // do damage to player
                gameManager.damagePlayer(attackDamage);
            }
        }
    }

    void Update() {
        // only move/attack if player in LOS
        if (los) {

            // move if not in range
            if (true) {
                // update transform internally?
                targetPosition = player;

                //rotate to look at the player
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,
                Quaternion.LookRotation(targetPosition.position - gameObject.transform.position), rotationSpeed * Time.deltaTime);

                //move towards the player
                gameObject.transform.position += gameObject.transform.forward * moveSpeed * Time.deltaTime;

            }
        }
    }
}
