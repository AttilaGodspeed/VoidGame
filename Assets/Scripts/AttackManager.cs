using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour {

    [SerializeField] private int damage;

    public int getDamage() {
        return damage;
    }

    //private void OnTriggerEnter(Collider other) {
    //    other.GetComponent<EnemyManager>().takeDamage(1);
    //}
}
