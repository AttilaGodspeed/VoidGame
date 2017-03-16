using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private bool paused;
    private int playerHealth, soulOrbs;
    [SerializeField] Text healthText, orbText;
    [SerializeField] GameObject colorFilter;
    [SerializeField] UIManager uiManager;

    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioSource pauseMusic;

	// Use this for initialization
	void Start () {
        playerHealth = 10;
        soulOrbs = 0;
        healthText.text = "Health: " + playerHealth;
        orbText.text = "SoulOrbs: " + soulOrbs;
        paused = false;
        colorFilter.SetActive(false);
        gameMusic.Play();
	}

    void Update() {
        if(Input.GetKeyUp(KeyCode.Return)) {
            togglePause();
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
        colorFilter.SetActive(paused);
        uiManager.toggleEnable(paused);
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

    public bool hasOrb() {
        return soulOrbs > 0;
    }
}
