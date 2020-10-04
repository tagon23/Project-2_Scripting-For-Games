using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] ParticleSystem _gunFire = null;
    [SerializeField] AudioClip audioClip = null;

    //testing purposes
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    public void Fire()
    {
        //play graphics
        _gunFire.Play();
        //TODO Audio
        AudioHelper.PlayClip2D(audioClip,1);
    }
}
