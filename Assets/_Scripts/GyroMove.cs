using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroMove : MonoBehaviour
{
    public Transform objetoT;
    public Transform objetoR;
    public float fuerzaVelocidad;
    [Space]
    public Vector3 aceleracion;
    public Quaternion giroscopio;
    public Vector3 eulerGiroscopio;
    public Vector3 Gaceleracion;

    private Rigidbody rb;
    private Vector3 BaseAcceleration;

    private void Awake()
    {
        rb = objetoT.GetComponent<Rigidbody>();
    }

    public GyroMove(Transform objetoT, Transform objetoR, float fuerzaVelocidad, Vector3 aceleracion, Quaternion giroscopio, Vector3 eulerGiroscopio, Vector3 gaceleracion, Rigidbody rb, Vector3 baseAcceleration)
    {
        this.objetoT = objetoT;
        this.objetoR = objetoR;
        this.fuerzaVelocidad = fuerzaVelocidad;
        this.aceleracion = aceleracion;
        this.giroscopio = giroscopio;
        this.eulerGiroscopio = eulerGiroscopio;
        Gaceleracion = gaceleracion;
        this.rb = rb;
        BaseAcceleration = baseAcceleration;
    }

    private void Start()
    {
        Input.gyro.enabled = true;
        BaseAcceleration = Gaceleracion;
    }

    void Update()
    {
        aceleracion = Input.acceleration;
        giroscopio = Input.gyro.attitude;
        eulerGiroscopio = Input.gyro.attitude.eulerAngles;
        Gaceleracion = Input.gyro.userAcceleration;

        //objeto.Translate(-Gaceleracion.x * fuerzaVelocidad * Time.deltaTime, 0, Gaceleracion.z * fuerzaVelocidad * Time.deltaTime);

        //objeto.rotation = giroscopio;
        //objeto.rotation.
        objetoR.rotation = giroscopio;
    }

    private void FixedUpdate()
    {
        rb.AddForce(-(Gaceleracion.x - BaseAcceleration.x) * fuerzaVelocidad, 0, (Gaceleracion.z - BaseAcceleration.z) * fuerzaVelocidad);
    }
}
