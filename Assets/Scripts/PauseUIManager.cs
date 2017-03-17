using UnityEngine;
using UnityEngine.UI;

// this class is for manaaging the commands and controls within the pause screen, and its UI elements

public class PauseUIManager : MonoBehaviour {

	// Other Managers
	[SerializeField] private GameManager gameManager;
	[SerializeField] private SpecialManager specialManager;

	// UI compontens
	[SerializeField] private GameObject colorFilter;
	[SerializeField] private Button unPauseButton;
    [SerializeField] private Button shatterOrbButton;
    [SerializeField] private Button specialAttackButton;

    
    void Start () {
    	// when using buttons, assign functions to them
		unPauseButton.onClick.AddListener(unPause);
        shatterOrbButton.onClick.AddListener(shatterOrb);
        specialAttackButton.onClick.AddListener(specialAttack);
    	// have all elements closed at start
    	openClose(false);
    }

	// opens/closes all pause screen elements
	public void openClose (bool open) {
		colorFilter.SetActive(open);
		unPauseButton.gameObject.SetActive(open);
		shatterOrbButton.gameObject.SetActive(open);
		specialAttackButton.gameObject.SetActive(open);
	}

	private void specialAttack() {
		if (gameManager.hasOrb())
			specialManager.specialAttack(-1);
		else
			print("No orbs for special attack!");
        gameManager.togglePause();
    }

    private void shatterOrb() {
        gameManager.shatterOrb();
        gameManager.togglePause();
    }

    private void unPause() {
        gameManager.togglePause();
    }
}