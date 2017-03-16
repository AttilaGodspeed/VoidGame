using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] GameManager gameManager;

    [SerializeField] Text cooldownTimer;

    [SerializeField] Button unPauseButton;
    [SerializeField] Button shatterOrbButton;
    [SerializeField] Button specialAttackButton;

    [SerializeField] PlayerManager player;

    //[SerializeField] ParticleSystem specialAttackEffect;
    //[SerializeField] Collider specialCollider;
    [SerializeField] ParticleSystem shatterOrbEffect;
    [SerializeField] SphereCollider shatterCollider;

    private float specialCooldown, shatterCooldown, maxCooldown, deltaTime;

    void Awake() {
        unPauseButton.onClick.AddListener(unPause);
        shatterOrbButton.onClick.AddListener(shatter);
        specialAttackButton.onClick.AddListener(special);

        specialCooldown = 0f;
        shatterCooldown = 0f;
        maxCooldown = 10f;

        //specialCollider.enabled = false;
        shatterCollider.enabled = false;
        //shatterCollider.SetActive(false);

        toggleEnable(false);
        cooldownTimer.text = "To Special: 0";
    }

    void Update() {
        //if (specialCollider.enabled == true)
        //    specialCollider.enabled = false;
        if (shatterCollider.enabled == true)
            shatterCollider.enabled = false;
        //shatterCollider.SetActive(false);


        deltaTime = Time.deltaTime;
        // shatter timer check
        //if(shatterCooldown > 0) {
            // turn off collider of active for > 1 sec
            //if (shatterCooldown < 1)
            //    shatterCollider.enabled = false;
        //    shatterCooldown -= deltaTime;
        //}

        // special timer check
        if(specialCooldown > 0) {
            // turn off special's collider
            //if (specialCooldown > (maxCooldown - 1))
            //    specialCollider.enabled = false;

            // update time
            specialCooldown -= deltaTime;
            cooldownTimer.text = "To Special: " + specialCooldown;// (specialCooldown % 60).ToString("F0");
        }
        

    }

    public void toggleEnable(bool enabled) {
        unPauseButton.gameObject.SetActive(enabled);
        shatterOrbButton.gameObject.SetActive(enabled);
        specialAttackButton.gameObject.SetActive(enabled);
    }

    private void unPause() {
        gameManager.togglePause();
    }

    private void shatter() {
        if (gameManager.hasOrb()) {
            // shatter
            gameManager.dropOrb();
            gameManager.healPlayer(5);
            gameManager.togglePause();
            //shatterCollider.SetActive(true);
            shatterOrbEffect.Play();
            shatterCooldown = 1f;

            shatterCollider.enabled = true;
        }
        else
            print("No orbs available!");
    }

    private void special() {
        if (gameManager.hasOrb() && specialCooldown <= 0) {
            // special
            gameManager.togglePause();
            
            //specialAttackEffect.Play();
            specialCooldown = maxCooldown;

            //specialCollider.enabled = true;
            player.specialAttack();
        }
        else
            print("No orbs available!");
    }
}
