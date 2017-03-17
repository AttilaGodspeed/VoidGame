using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls player movement, orientation, and the basic attack

// needs to be cleaned up

public class PlayerManager : MonoBehaviour {
    
    [SerializeField] private float runSpeed = 2.5f;
    [SerializeField] private float coolDownTime = 3f;

    [SerializeField] ParticleSystem basicAttackEffect;
    [SerializeField] SphereCollider basicAttackCollider;

    //[SerializeField] ParticleSystem specialAttackEffect;
    //[SerializeField] Collider specialAttackCollider;

    private float xAxis, zAxis, coolDown, deltaTime;
    private Vector3 nextPosition;
    //private bool attackOff;

	// Use this for initialization
	void Start () {
        coolDown = 0;
        basicAttackCollider.enabled = false;
        //specialAttackCollider.enabled = false;
	}

    
    // Update is called once per frame
    void Update() {
        if(basicAttackCollider.enabled == true)
            basicAttackCollider.enabled = false;
        //if (specialAttackCollider.enabled == true)
        //    specialAttackCollider.enabled = false;

        // do nothing if game in pause state
        if (true) {
            deltaTime = Time.deltaTime;
            // Update basic attack cooldown
            if (coolDown > 0) {
                coolDown -= deltaTime;
                //if (coolDown < (coolDownTime - 1))
                //    basicAttackCollider.enabled = false;
            }

            // Control Player Movement
            xAxis = Input.GetAxisRaw("Horizontal");
            zAxis = Input.GetAxisRaw("Vertical");
            nextPosition.Set(xAxis, 0.0f, zAxis);

            if(xAxis != 0 && zAxis !=0)
                transform.rotation = Quaternion.LookRotation(nextPosition);

            transform.Translate(nextPosition * runSpeed * deltaTime, Space.World);

            // Control Basic Attack
            if (Input.GetKeyDown(KeyCode.Space) && (coolDown <= 0)) {
                // play effect
                basicAttackEffect.Play();
                coolDown = coolDownTime;

                // enable collider and check for collisions
                basicAttackCollider.enabled = true;
            }
        }
    }

    /*
    public void specialAttack() {
        specialAttackEffect.Play();
        specialAttackCollider.enabled = true;
    }
    */
}
