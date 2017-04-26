using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenuManager : MonoBehaviour {

	[SerializeField] private GameObject menuUIElements;

	// Modifiable UI text elements
	[SerializeField] private Text endgameMessage;
	[SerializeField] private Text enemyKillText; 
	[SerializeField] private Text portalKillText;
	[SerializeField] private Text basicText;
	[SerializeField] private Text n1hText; 
	[SerializeField] private Text n2hText; 
	[SerializeField] private Text shatterText; 
	[SerializeField] private Text healthDownText;
	[SerializeField] private Text healthUpText;

	void Start() {
		menuUIElements.gameObject.SetActive(false);
	}

	public void showEndMenu(bool isWin) {
		menuUIElements.gameObject.SetActive(true);

		if (isWin) 
			endgameMessage.text = "All portals destroyed, congratulations!";
		else
			endgameMessage.text = "You have died . . . ";

		enemyKillText.text = ScoreCounter.self.enemyKills.ToString();
		portalKillText.text = ScoreCounter.self.portalKills.ToString();
		basicText.text = ScoreCounter.self.numBasic.ToString();
		n1hText.text = ScoreCounter.self.num1h.ToString();
		n2hText.text = ScoreCounter.self.num2h.ToString();
		shatterText.text = ScoreCounter.self.numShatter.ToString();
		healthDownText.text = ScoreCounter.self.healthLost.ToString();
		healthUpText.text = ScoreCounter.self.healthGained.ToString();
	}

	// returns player to the main menu
	public void returnToMenu() {
		print("Retuning to main menu...");
		SceneManager.LoadScene(0);
	}
}
