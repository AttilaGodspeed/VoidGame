using UnityEngine;
using UnityEngine.UI;

// this class is for mananging the commands and controls within the pause screen, and its UI elements

public class PauseUIManager : MonoBehaviour {

	// Other Managers
	[SerializeField] private GameManager gameManager;
	[SerializeField] private InventoryManager inventoryManager;
	[SerializeField] private PlayerManager player;

	// the overall pause screen UI element group
	[SerializeField] private GameObject pauseUIElements;

	// specific UI compontents
	[SerializeField] private Button unPauseButton;
	[SerializeField] private Button leftShatterButton;
	[SerializeField] private Button rightShatterButton;
	[SerializeField] private Button leftCycleButton;
	[SerializeField] private Button rightCycleButton;

    
    void Start () {
    	// when using buttons, assign functions to them
		unPauseButton.onClick.AddListener(unPause);
		leftCycleButton.onClick.AddListener(cycleLeft);
		rightCycleButton.onClick.AddListener(cycleRight);
		leftShatterButton.onClick.AddListener(shatterLeftOrb);
		rightShatterButton.onClick.AddListener(shatterRightOrb);

    	// have all elements closed at start
    	openClose(false);
    }

	// opens/closes all pause screen elements
	public void openClose (bool open) {
		//leftCycleButton.gameObject.SetActive(open);
		//rightCycleButton.gameObject.SetActive(open);
		pauseUIElements.SetActive(open);
	}

    private void shatterLeftOrb() {
		if (inventoryManager.shatterLeftOrb(player.transform.position)) {
			gameManager.healPlayer(5);
        	gameManager.togglePause();

			// update tracker
        	ScoreCounter.self.numShatter ++;
			ScoreCounter.self.healthGained += 5;
		}
    }
	private void shatterRightOrb() {
		if (inventoryManager.shatterRightOrb(player.transform.position)) {
			gameManager.healPlayer(5);
        	gameManager.togglePause();

			// update tracker
        	ScoreCounter.self.numShatter ++;
			ScoreCounter.self.healthGained += 5;
		}
    }


	private void cycleLeft() {
		inventoryManager.cycleLeftHand();
		print("cycle left button triggered");	
	}
	private void cycleRight() {
		inventoryManager.cycleRightHand();
	}

    private void unPause() {
        gameManager.togglePause();
    }
}