﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class manages the following:
// Player status
// Music
// Pausing the game
// UI elements not related to the pause screen

public class GameManager : MonoBehaviour {

    // the total number of portals in the level
    [SerializeField] private int numPortals;

    // other managers
    [SerializeField] private PauseUIManager pauseUIManager;
    [SerializeField] private EndMenuManager endMenuManager;

    // persistant UI elements
    [SerializeField] private Text healthText;//, orbText;

    // music
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource pauseMusic;

    // internal variables
    private bool paused;
    private int playerHealth, soulOrbs, areaOrbs, rangeOrbs, forceOrbs;

	// Use this for initialization
	void Start () {
        playerHealth = 10;
        soulOrbs = areaOrbs = rangeOrbs = forceOrbs = 0;
        healthText.text = "Health: " + playerHealth;
        //orbText.text = "SoulOrbs: " + soulOrbs;
        //coolDownText.text = "To Special: 0";
        paused = false;
        gameMusic.Play();
	}

    void Update() {
        // check for pause hit
        if (!paused) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                togglePause();
            }
        }

        // update special cooldown if not paused
        //if (!paused)
        //    coolDownText.text = "To Special: " + specialManager.getCooldown();//.toString("##");
    } 

    public void portalKilled() {
        // update stats
        ScoreCounter.self.portalKills ++;

        // check if all portals are dead
        if (ScoreCounter.self.portalKills >= numPortals) {
            Time.timeScale = 0;
            paused = true;
            gameMusic.Pause();
            pauseMusic.Play();

            endMenuManager.showEndMenu(true);
        }
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
        
        // update tracker
		ScoreCounter.self.healthLost += damage;

        if (playerHealth < 1) {
            print("You are dead...");
            Time.timeScale = 0;
            paused = true;
            gameMusic.Pause();
            pauseMusic.Play();

            endMenuManager.showEndMenu(false);
        }
    }

    public void healPlayer(int healing) {
        playerHealth += healing;
        healthText.text = "Health: " + playerHealth;
    }

    // adds an orb to inventory
    public void addOrb(string type) {
        soulOrbs++;
        //orbText.text = "SoulOrbs: " + soulOrbs;

        if(type.StartsWith("area"))
            areaOrbs ++;
        else if(type.StartsWith("range"))
            rangeOrbs ++;
        else   
            forceOrbs ++;
    }

    public void dropOrb() {
        if (soulOrbs > 0) {
            soulOrbs--;
            //orbText.text = "SoulOrbs: " + soulOrbs;
        }
    }

/*
    public void shatterOrb() {
        // do the check for orbs in the UI
        if (soulOrbs > 0) {
            soulOrbs--;
            orbText.text = "SoulOrbs: " + soulOrbs;
        }
        healPlayer(5);
        //specialManager.shatter();
    }
*/

    public bool hasOrb() {
        return soulOrbs > 0;
    }

    public bool isPaused() {
        return paused;
    }
}
