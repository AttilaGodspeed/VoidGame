using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class manages the following:
// Player status
// Music
// Pausing the game
// inventory
// UI elements not related to the pause screen

public class GameManager : MonoBehaviour {
    // other managers
    [SerializeField] private PauseUIManager pauseUIManager;
    [SerializeField] private SpecialManager specialManager;

    // persistant UI elements
    [SerializeField] private Text healthText, orbText, coolDownText;

    // music
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource pauseMusic;

    // internal variables
    private bool paused;
    private int playerHealth, soulOrbs;

	// Use this for initialization
	void Start () {
        playerHealth = 10;
        soulOrbs = 0;
        healthText.text = "Health: " + playerHealth;
        orbText.text = "SoulOrbs: " + soulOrbs;
        coolDownText.text = "To Special: 0";
        paused = false;
        gameMusic.Play();
	}

    void Update() {
        // check for pause hit
        if(Input.GetKeyDown(KeyCode.Return)) {
            togglePause();
        }

        // update special cooldown if nto paused
        if (!paused)
            coolDownText.text = "To Special: " + specialManager.getCooldown();//.toString("##");
    } 

    public void togglePause () {
        if (!paused) {
            Time.timeScale = 0;
            paused = true;
            gameMusic.Pause();
            pauseMusic.Play();
        }
        else {
            Time.timeScale = 1;
            paused = false;
            gameMusic.Play();
            pauseMusic.Stop();
        }
        pauseUIManager.openClose(paused);
    }

    public void damagePlayer(int damage) {
        playerHealth -= damage;
        healthText.text = "Health: " + playerHealth;
        if (playerHealth < 1)
            print("You are dead...");
    }

    public void healPlayer(int healing) {
        playerHealth += healing;
        healthText.text = "Health: " + playerHealth;
    }

    public void addOrb() {
        soulOrbs++;
        orbText.text = "SoulOrbs: " + soulOrbs;
    }

    public void dropOrb() {
        if (soulOrbs > 0) {
            soulOrbs--;
            orbText.text = "SoulOrbs: " + soulOrbs;
        }
    }

    public void shatterOrb() {
        if (soulOrbs > 0) {
            soulOrbs--;
            orbText.text = "SoulOrbs: " + soulOrbs;
        }
        healPlayer(5);
        specialManager.shatter();
    }

    public bool hasOrb() {
        return soulOrbs > 0;
    }
}
