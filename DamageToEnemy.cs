using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEnemy : MonoBehaviour
{
    public float damageToInflict;
    public GameObject damageAnim;
    public Transform hitPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHPManager>().DamageEnemy(damageToInflict);
            Instantiate(damageAnim, hitPoint.position, hitPoint.rotation); // or Quarternion.identity
        }
    }
}
