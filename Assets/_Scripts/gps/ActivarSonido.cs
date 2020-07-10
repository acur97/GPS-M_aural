using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarSonido : MonoBehaviour
{
    private AudioSource soure;
    private Animator anim;
    private Collider otherActual;
    private Collider otherSalio;

    private readonly string _sonido = "Sonido";
    private readonly string _in = "in";
    private readonly string _out = "out";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_sonido))
        {
            soure = other.GetComponent<AudioSource>();
            anim = other.GetComponent<Animator>();
            anim.SetTrigger(_in);
            soure.Play();
            otherActual = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_sonido))
        {
            otherSalio = other;
            if (otherSalio != otherActual)
            {
                soure = otherSalio.GetComponent<AudioSource>();
                anim = otherSalio.GetComponent<Animator>();
                anim.ResetTrigger(_in);
                anim.ResetTrigger(_out);
                anim.SetTrigger(_out);
                anim = null;
                soure.Pause();
                soure = null;
            }
            else
            {
                anim.ResetTrigger(_in);
                anim.ResetTrigger(_out);
                anim.SetTrigger(_out);
                anim = null;
                soure.Pause();
                soure = null;
            }
        }
    }
}