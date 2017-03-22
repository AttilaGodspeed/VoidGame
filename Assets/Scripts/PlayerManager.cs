using UnityEngine;

// controls player movement, orientation, and the basic attack

public class PlayerManager : MonoBehaviour {
    [SerializeField] GameManager gameManager;

    // attack/movement modifiers
    // speed at which the player moves
    [SerializeField] private float runSpeed = 5f;
    // time between basic attacks
    [SerializeField] private float coolDownTime = 0.5f;
    // the height off the ground (world-space) the player looks at while follwing the mouse
    private float lookHeight = 1f;

    // link fields for the basic attack
    [SerializeField] ParticleSystem basicAttackEffect;
    [SerializeField] SphereCollider basicAttackCollider;

    private float xAxis, zAxis, coolDown, deltaTime;
    private Vector3 nextPosition;
    private Ray mouseRay;
    private RaycastHit rayHit;

	// Use this for initialization
	void Start () {
        coolDown = 0;
        basicAttackCollider.enabled = false;
	}

    // player inputs are controlled from here
    void Update() {
        // disable collider if it's up
        if(basicAttackCollider.enabled == true)
            basicAttackCollider.enabled = false;

        // do nothing if game in pause state
        if (!gameManager.isPaused()) {
            deltaTime = Time.deltaTime;
            // Update basic attack cooldown
            if (coolDown > 0) {
                coolDown -= deltaTime;
            }

            // Control Player Rotation
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(mouseRay, out rayHit);
            nextPosition.Set(rayHit.point.x, lookHeight, rayHit.point.z);
            transform.LookAt(nextPosition);

            // Control Player Movement
            xAxis = Input.GetAxisRaw("Horizontal");
            zAxis = Input.GetAxisRaw("Vertical");
            nextPosition.Set(xAxis, 0.0f, zAxis);

            transform.Translate(nextPosition * runSpeed * deltaTime, Space.World);

            // Control Basic Attack, activates on space or left click
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && (coolDown <= 0)) {
                // play effect
                basicAttackEffect.Play();
                coolDown = coolDownTime;

                // enable collider
                basicAttackCollider.enabled = true;
            }
        }
    }
}
