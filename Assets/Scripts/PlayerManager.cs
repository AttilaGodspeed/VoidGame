using UnityEngine;
using UnityEngine.UI;

// controls player movement, orientation, and the basic attack

public class PlayerManager : MonoBehaviour {
    [SerializeField] GameManager gameManager;
    [SerializeField] InventoryManager inventoryManager;

    // attack/movement modifiers
    // speed at which the player moves
    [SerializeField] private float runSpeed = 5f;
    // time between basic attacks (defaults to 0.5 seconds)
    [SerializeField] private float basicCoolDownTime = 0.5f;
    // time between orb special attacks (defaults to 7 seconds)
    [SerializeField] private float orbCoolDownTime = 7f;
    // the height off the ground (world-space) the player looks at while follwing the mouse
    private float lookHeight = 1f;

    // link fields for the basic attack
    [SerializeField] ParticleSystem basicAttackEffect;
    [SerializeField] SphereCollider basicAttackCollider;

    // Orb attack UI elements
    [SerializeField] Text leftCoolDownText, rightCoolDownText;
    [SerializeField] Image grayOutLeft, grayOutRight;

    private float xAxis, zAxis, basicCoolDown, leftOrbCoolDown, rightOrbCoolDown, deltaTime;
    private Vector3 nextPosition;
    private Ray mouseRay;
    private RaycastHit rayHit;
    private int tempInt;

	// Use this for initialization
	void Start () {
        basicCoolDown = leftOrbCoolDown = rightOrbCoolDown = 0;
        basicAttackCollider.enabled = false;
        toggleLeft(false);
        toggleRight(false);
	}

    // toggles left-hand cooldown UI elements on and off
    private void toggleLeft(bool isOn) {
        leftCoolDownText.gameObject.SetActive(isOn);
        grayOutLeft.gameObject.SetActive(isOn);
    }

    // toggles left-hand cooldown UI elements on and off
    private void toggleRight(bool isOn) {
        rightCoolDownText.gameObject.SetActive(isOn);
        grayOutRight.gameObject.SetActive(isOn);
    } 

    // player inputs are controlled from here
    void Update() {
        // disable basic collider if it's up
        if(basicAttackCollider.enabled == true)
            basicAttackCollider.enabled = false;

        // do nothing if game in pause state
        if (!gameManager.isPaused()) {

            deltaTime = Time.deltaTime;

            // Update cooldowns
            if (basicCoolDown > 0)
                basicCoolDown -= deltaTime;
            if (leftOrbCoolDown > 0) {
                leftOrbCoolDown -= deltaTime;
                tempInt = Mathf.CeilToInt(leftOrbCoolDown);
                if (tempInt > 0)
                    leftCoolDownText.text = tempInt.ToString();
                else
                    toggleLeft(false);
            } 
            if (rightOrbCoolDown > 0) {
                rightOrbCoolDown -= deltaTime;
                tempInt = Mathf.CeilToInt(rightOrbCoolDown);
                if (tempInt > 0)
                    rightCoolDownText.text = tempInt.ToString();
                else
                    toggleRight(false);
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

            // Special Attacks ovveride basic attacks
            // Combo attacks ovveride single specials

            // control special attacks
            // 1: Left Hand Orb Attack
            // 2: Right Hand Orb Attack
            // 3: Combination Attack

            // both hands (both must be off cooldown, not empty, and can't be the same type)
            if (Input.GetKeyDown(KeyCode.Alpha3) && !inventoryManager.isDouble() && checkLeft() && checkRight()) {
                leftOrbCoolDown = orbCoolDownTime;
                toggleLeft(true);
                rightOrbCoolDown = orbCoolDownTime;
                toggleRight(true);
                //nextPosition = new Vector3 (rayHit.point.x, 0, rayHit.point.y);
                inventoryManager.deploySpecial(3, rayHit.point, rayHit.point); //gameObject.transform.position);
            } 
            // left hand
            else if (Input.GetKeyDown(KeyCode.Alpha1) && checkLeft()) {
                leftOrbCoolDown = orbCoolDownTime;
                toggleLeft(true);
                inventoryManager.deploySpecial(1, rayHit.point, gameObject.transform.position);
            } 
            // right hand
            else if (Input.GetKeyDown(KeyCode.Alpha2) && checkRight()) {
                rightOrbCoolDown = orbCoolDownTime;
                toggleRight(true);
                inventoryManager.deploySpecial(2, rayHit.point, gameObject.transform.position);
            }
            // Control Basic Attack, activates on space or left click
            else if (Input.GetMouseButtonDown(0) && basicCoolDown <= 0) {
                // play effect
                basicAttackEffect.Play();
                basicCoolDown = basicCoolDownTime;

                // enable collider
                basicAttackCollider.enabled = true;
            }
        }
    }

    private bool checkLeft() {
        return leftOrbCoolDown <= 0 && !inventoryManager.isEmpty(true);
    }
    private bool checkRight() {
        return rightOrbCoolDown <= 0 && !inventoryManager.isEmpty(false);
    }
}
