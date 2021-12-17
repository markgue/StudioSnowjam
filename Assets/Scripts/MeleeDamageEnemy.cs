using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeDamageEnemy : MonoBehaviour
{
    [SerializeField] public int damageAmount;
    [SerializeField] public int attackRadius;
    InputAction meleeAttackAction;
    private bool meleeAttackActionOccured;
    public Vector3 collision = Vector3.zero;
    public GameObject lastHit;

    void Awake() {
        meleeAttackAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions.FindActionMap("Player").FindAction("Melee");
        meleeAttackAction.performed += ctx => meleeAttackActionOccured = true;
    }

    void Update() {
        if (meleeAttackActionOccured) {
            var ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, attackRadius)) {
                lastHit = hit.transform.gameObject;
                collision = hit.point;
                if (lastHit.gameObject.CompareTag("Enemy")) {
                    lastHit.gameObject.GetComponent<EnemyDeath>().GiveDamage(damageAmount);
                }
            }
        }
        meleeAttackActionOccured = false;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}
