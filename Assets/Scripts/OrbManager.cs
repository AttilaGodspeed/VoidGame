using UnityEngine;

public class OrbManager : MonoBehaviour {

    [SerializeField] OrbStack parentStack;
    [SerializeField] InventoryManager inventoryManager;

    void Awake() {
        reStack();	
    }

    void OnCollisionEnter(Collision col) {
        //print("Orb is colliding with " + col.gameObject.tag);
        if (col.gameObject.tag == "Player") {
            // add orb to inventory
            inventoryManager.addOrb(gameObject.name);
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
