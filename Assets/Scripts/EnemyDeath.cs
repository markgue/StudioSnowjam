using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private int hitpoints;

    private bool enemyDead = false;

    void Update() {
        if (enemyDead) Destroy(gameObject);
    }

    public void GiveDamage(int damage) {
        if (hitpoints >= 1) {
            hitpoints -= damage;
            if (hitpoints < 1) enemyDead = true;
        }
    }
}
