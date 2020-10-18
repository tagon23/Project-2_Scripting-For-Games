using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 center = Vector3.zero;

  void fireProjectile(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            //Debug.Log("I see you :)");
            missile();
        }
    }

    private void Update()
    {
        fireProjectile(center, .5f);
    }

    private void missile()
    {

    }
}
