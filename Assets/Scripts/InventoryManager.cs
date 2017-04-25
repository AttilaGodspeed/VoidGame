using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// manages the player's inventory or soul orbs
// also manages which special is called

public class InventoryManager : MonoBehaviour {

	// the soul orbs in the player's possesion
	private int areaOrbs, rangeOrbs, forceOrbs;

	// the orbs the player has equipped
	private string leftHand, rightHand;

	// UI elements for inventory display
	[SerializeField] private Text areaOrbText, rangeOrbText, forceOrbText;
	[SerializeField] private Image leftHandImage, rightHandImage, bigLeftImage, bigRightImage;

	// links to 1-h specials
	[SerializeField] private SpecialManager areaSpecial, rangeSpecial, forceSpecial, shatter;
	// links to 2-h specials
	[SerializeField] private SpecialManager areaRangeSpecial, areaForceSpecial, rangeForceSpecial;

	// colours for the Orb visuals
	private Color areaColor, rangeColor, forceColor, emptyColor;

	// Use this for initialization
	void Start () {
		areaColor = new Color (140f / 255f, 80f / 255f, 200f / 255f); // purple
		rangeColor = new Color (60f / 255f, 160f / 255f, 70f / 255f); // green
		forceColor = new Color (200f / 255f, 150f / 255f, 70f / 255f); // orange
		emptyColor = new Color (0f, 0f, 0f); // black

		leftHand = "X";
		rightHand = "X";

		areaOrbs = 1;
		rangeOrbs = 1;
		forceOrbs = 1;
		
		leftHandImage.color = emptyColor;
		rightHandImage.color = emptyColor;
		bigLeftImage.color = emptyColor;
		bigRightImage.color = emptyColor;
		areaOrbText.text = "1";
		rangeOrbText.text = "1";
		forceOrbText.text = "1";
	}

	// returns true if the indicated hand is empty
	public bool isEmpty (bool isLeft) {
		if (isLeft)
			return leftHand == "X";
		else
			return rightHand == "X";
	}

	// returns true if the player has two of the same kind of orb
	public bool isDouble() {
		return leftHand == rightHand;
	}

	// adds orbs to the player's inventory
	public void addOrb(string orbType) {
		string tempOrb = orbType.Substring(0, 1);
		if (tempOrb == "A") {
			areaOrbs ++;
			areaOrbText.text = areaOrbs.ToString();
		} else if (tempOrb == "R") {
			rangeOrbs ++;
			rangeOrbText.text = rangeOrbs.ToString();
		} else if (tempOrb == "F") {
			forceOrbs ++;
			forceOrbText.text = forceOrbs.ToString();
		}
	}

	// attempts to shatter orb in the indicated hand, returns false if hand is empty
	public bool shatterLeftOrb(Vector3 playerPos) {
		if (leftHand == "X")
			return false;
		shatter.deploy(playerPos, playerPos);
		leftHand = "X";
		leftHandImage.color = emptyColor;
		bigLeftImage.color = emptyColor;
		return true;
	}
	public bool shatterRightOrb(Vector3 playerPos) {
		if (rightHand == "X")
			return false;
		shatter.deploy(playerPos, playerPos);
		rightHand = "X";
		rightHandImage.color = emptyColor;
		leftHandImage.color = emptyColor;
		return true;
	}

	// cycles through all available orbs for the left hand, order is a > r > f > empty
	// if an orb type is unvailable, it'll just give you empty
	public void cycleLeftHand() {
		if (leftHand != "X")
			addOrb(leftHand);
		leftHand = cycleOrb(leftHand);
		bigLeftImage.color = leftHandImage.color = parseColor(leftHand);
		print("Left Hand set to: " + leftHand);
	}

	// same as cycleLeftHand, but with the right hand slot
	public void cycleRightHand() {
		if (rightHand != "X")
			addOrb(rightHand);
		rightHand = cycleOrb(rightHand);
		bigRightImage.color = rightHandImage.color = parseColor(rightHand);
		print("Right Hand set to: " + rightHand);
	}

	// recursive function at the heart of the two above
	private string cycleOrb(string currentOrb) {
		if (currentOrb == "X") {
			if (areaOrbs > 0) {
				areaOrbs --;
				areaOrbText.text = areaOrbs.ToString();
				return "A";
			} else
				return cycleOrb("A");
		}
		else if (currentOrb == "A") {
			if (rangeOrbs > 0) {
				rangeOrbs --;
				rangeOrbText.text = rangeOrbs.ToString();
				return "R";
			} else
				return cycleOrb("R");
		}
		else if (currentOrb == "R") {
			if (forceOrbs > 0) {
				forceOrbs --;
				forceOrbText.text = forceOrbs.ToString();
				return "F";
			} else
				return "X";
		}
		else 
			return "X";
	}

	// converts orb type to its color
	private Color parseColor(string orb) {
		if (orb == "A") 
			return areaColor;
		else if (orb == "R") 
			return rangeColor;
		else if (orb == "F")
			return forceColor;
		else
			return emptyColor;
	}

	// drops a specialattack into the play area, 1 = left hand, 2 = right hand, 3 = both
	// should not be called if there is no corresponding orb(s), or if isDouble() [check these in playerManager]
	public void deploySpecial(int hand, Vector3 cursorPos, Vector3 playerPos) {
		string tempSpecial = "";
		// determine which special to deploy
		if (hand == 1)
			tempSpecial = leftHand;
		else if (hand == 2)
			tempSpecial = rightHand;
		else if (hand == 3)
			tempSpecial = leftHand + rightHand;
		else
			tempSpecial = "X";

		// deploy the selected special
		if (!(tempSpecial == "X")) {
			if (tempSpecial.Length < 2) {
				if (tempSpecial == "A")
					areaSpecial.deploy(playerPos, playerPos);
				else if (tempSpecial == "R")
					rangeSpecial.deploy(playerPos, cursorPos);
				else
					forceSpecial.deploy(playerPos, cursorPos);
				
				// update tracker
                ScoreCounter.self.num1h ++;
			}
			else {
				if (tempSpecial == "AR" || tempSpecial == "RA")
					areaRangeSpecial.deploy(cursorPos, playerPos);
				else if (tempSpecial == "AF" || tempSpecial == "FA")
					areaForceSpecial.deploy(cursorPos, playerPos);
				else
					rangeForceSpecial.deploy(cursorPos, playerPos);

				// update tracker
                ScoreCounter.self.num2h ++;
			}
		}
	}
}
