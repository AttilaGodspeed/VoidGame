using UnityEngine;

public class ScoreCounter : MonoBehaviour {

	public static ScoreCounter self;

	// kill stats
	public int enemyKills, portalKills;

	// attack stats
	public int numBasic, num1h, num2h, numShatter;

	// health stats
	public int healthLost, healthGained;

	void Awake () {
		if (self == null) {
			self = this;
		} else {
			Destroy (gameObject);
		}

		enemyKills = 0;
		portalKills = 0;
		numBasic = 0;
		num1h = 0;
		num2h = 0;
		numShatter = 0;
		healthLost = 0;
		healthGained = 0;
	}
}