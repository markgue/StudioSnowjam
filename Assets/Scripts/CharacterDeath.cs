using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private int hitpoints;

    private bool playerDead = false;

    void Update() {
        if (playerDead) Destroy(gameObject);
    }

    public void GiveDamage(int damage) {
        if (hitpoints >= 1) {
            hitpoints -= damage;
            if (hitpoints < 1) playerDead = true;
        }
    }
}
