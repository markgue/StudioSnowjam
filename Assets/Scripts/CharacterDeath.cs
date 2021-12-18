using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDeath : MonoBehaviour
{
    [SerializeField] private int hitpoints;
    private int maxHP;
    private bool playerDead = false;

    public float regenCountdown = 10;
    public bool allowRegen = false;

    public float damageCountdown = 2;
    public bool invinsible = false;

    void Start() {
        maxHP = hitpoints;
    }

    public int getHealth() {
        return hitpoints;
    }

    void Update() {
        if (playerDead)
        {
            Destroy(gameObject); // game over transition
            SceneManager.LoadScene("GameOver");
        }
        if (allowRegen) {
            if (regenCountdown > 0) {
                regenCountdown -= Time.deltaTime;
            } else {
                hitpoints+=10;
                if (hitpoints == maxHP) {
                    allowRegen = false;
                }
                regenCountdown = 10; // potential public var
            }
        }
        if (invinsible) {
            if (damageCountdown > 0) {
                damageCountdown -= Time.deltaTime;
            } else {
                invinsible = false;
                damageCountdown = 2;
            }
        }
    }

    public void GiveDamage(int damage) {
        if (hitpoints >= 1 && !invinsible) {
            hitpoints -= damage;
            invinsible = true;
            if (!allowRegen) {
                regenCountdown = 10;
                allowRegen = true;
            }
            if (hitpoints < 1)
                playerDead = true;
        } else if (!invinsible) {
            playerDead = true;
        }
    }
}
