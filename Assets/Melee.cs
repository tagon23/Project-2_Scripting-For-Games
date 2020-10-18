using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] Transform knuckles;
    Vector3 originalPosition;
    public float countDown = 1f;
    public float deltaTime = 5f;
    [SerializeField] ParticleSystem punchyboi;
    Vector3 forward = new Vector3(-5f, 0, 0);
    Vector3 back = new Vector3(5f, 0, 0);
    [SerializeField] AudioClip Panch = null;


    public void Awake()
    {
        originalPosition = knuckles.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            basicAnimation();
            
        }
    }

    public void basicAnimation()
    {
        if (countDown >= 1.0f)
        {
            knuckles.transform.position += Vector3.forward * Time.deltaTime;
            countDown -= 5;
            punchyboi.Play();
            AudioHelper.PlayClip2D(Panch, 1);
        }

        else if(countDown <= 0f)
        {
            knuckles.transform.position += Vector3.back * Time.deltaTime;
            countDown += 5;
        }
        if(countDown == -6)
        {
            countDown = 1f;
        }
    }
}
