using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float missileSpeed = 4f;
    public float turn;
    [SerializeField] Transform target;
    [SerializeField] Rigidbody rocketRigidBody;
    Vector3 originalPos;
    [SerializeField] ParticleSystem boomBoy;
    [SerializeField] Transform missile;
    public int contactCount;
    private ContactPoint[] collision;
   

    private void Awake()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z); ;
    }

    private void FixedUpdate()
    {
        missileFire();
    }

    private void missileFire()
    {
        rocketRigidBody.velocity = transform.forward * missileSpeed;

        var targetrotation = Quaternion.LookRotation(target.position - transform.position);

        rocketRigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetrotation, turn));
    }

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Respawn(6f,10f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Object"))
        {
                boomBoy.Play();
            StartCoroutine(Respawn(2f, 3f));
        }
    }
 

    IEnumerator Respawn(float timeToDespawn, float timeToRespawn)
    {
        yield return new WaitForSeconds(timeToDespawn);
        missileSpawner.Respawn(timeToRespawn);
       
        missile.transform.position = originalPos;
        Destroy(gameObject);
    }
}
