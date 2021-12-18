using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageEnemy : MonoBehaviour
{
    [SerializeField] public int damageAmount;
    [SerializeField] public int attackRadius;
    [SerializeField] public AudioSource meleeAttackSound;
    public Vector3 collision = Vector3.zero;
    public GameObject lastHit;
    
    public Animator animator;

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.F)) {
            var ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, attackRadius)) {
                lastHit = hit.transform.gameObject;
                collision = hit.point;
                if (lastHit.gameObject.CompareTag("Enemy")) {
                    lastHit.gameObject.GetComponent<EnemyDeath>().GiveDamage(damageAmount);
                }
            }
            if (!meleeAttackSound.isPlaying) {
                meleeAttackSound.Play();
            }
            animator.SetTrigger("Swing");
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}
