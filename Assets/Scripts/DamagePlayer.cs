using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] public int damageAmount;

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<CharacterDeath>().GiveDamage(damageAmount);
        }
    }
}
