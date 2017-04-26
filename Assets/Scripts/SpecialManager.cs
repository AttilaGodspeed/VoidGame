using UnityEngine;

// this class manages the colliders and particle systems of special attacks

public class SpecialManager : MonoBehaviour {

    // The SFx component of the special attack
    [SerializeField] private ParticleSystem effect;
    //[SerializeField] private Collider areaCollider; // shouldn't need this assuming the collider is disabled along with the game object

    void Awake () {
        gameObject.SetActive(false);
    }

    // remove self when the SFx animation completes
    void FixedUpdate () {
        if (!effect.IsAlive())
            gameObject.SetActive(false);
    }

    // place the special attack, face the player, enable it, and play its animation
    public void deploy (Vector3 deployLoc, Vector3 playerLoc) {
        transform.position = deployLoc;
        transform.LookAt(playerLoc);
        gameObject.SetActive(true);
        effect.Play();
    }
}

/* This is the old version
public class SpecialManager : MonoBehaviour {

    //[SerializeField] private GameManager gameManager;

    [SerializeField] private ParticleSystem columnEffect; 
    [SerializeField] private Collider columnCollider;

    //[SerializeField] private ParticleSystem sphereEffect;
    //[SerializeField] private Collider sphereCollider;

    [SerializeField] private ParticleSystem shatterEffect;
    [SerializeField] private Collider shatterCollider;

    [SerializeField] private float specialCooldown = 7f; 
    private float specialTimer;

    void Awake () {
        specialTimer = 0;
        // disable all colliders
        columnCollider.enabled = false;
        shatterCollider.enabled = false;
    }

    void FixedUpdate() {
        // update timer
        if (specialTimer > 0)
            specialTimer -= Time.deltaTime;
    }

    public float getCooldown () {
        return specialTimer;
    }

    // for specials with persistant effects, make a call to a script that controls that effect
    public void specialAttack (int i) {
        // use params to determine which attack to use
        // for now, we just have the 'kamehameha' column attack
        
        // only initiate if timer isn't running/on cooldown 
        if (!(specialTimer > 0)) {
            // use pointers or the like to not have to repeat this for each attack?
            columnEffect.Play();
            StartCoroutine(pulseCollider(columnCollider));
            specialTimer = specialCooldown;
        }
    }

    // shatter works a little differently from specialAttack, and (currently) gets its own method
    // in future bundle into special method?
    public void shatter () {
        shatterEffect.Play();
        StartCoroutine(pulseCollider(shatterCollider));
    }

    // turns the passed collider on, waits, then turns it off again
    private IEnumerator pulseCollider (Collider col) {
        col.enabled = true;
        yield return new WaitForSeconds(1f);
        col.enabled = false;
    }
}
 */