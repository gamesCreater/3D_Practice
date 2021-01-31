using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            gameObject.transform.parent = other.gameObject.transform;
            var AIC = gameObject.transform.parent.GetComponent<AIController>();
            
            AIC.Dead();

            Destroy(gameObject);

        }

        if (other.gameObject.CompareTag("Player"))
        {

            gameObject.transform.parent = other.gameObject.transform;
            var CharControl = gameObject.transform.parent.GetComponent<CharController>();

            CharControl.Dead();

            Destroy(gameObject);

        }
    }
}
