using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarSonido : MonoBehaviour
{
    private AudioSource soure;
    private Animator anim;
    private Collider otherActual;
    private Collider otherSalio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sonido"))
        {
            soure = other.GetComponent<AudioSource>();
            anim = other.GetComponent<Animator>();
            anim.SetTrigger("in");
            soure.Play();
            otherActual = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sonido"))
        {
            otherSalio = other;
            if (otherSalio != otherActual)
            {
                soure = otherSalio.GetComponent<AudioSource>();
                anim = otherSalio.GetComponent<Animator>();
                anim.ResetTrigger("in");
                anim.ResetTrigger("out");
                anim.SetTrigger("out");
                anim = null;
                soure.Pause();
                soure = null;
            }
            else
            {
                anim.ResetTrigger("in");
                anim.ResetTrigger("out");
                anim.SetTrigger("out");
                anim = null;
                soure.Pause();
                soure = null;
            }
        }
    }
}