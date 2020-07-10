using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObtenerGPS : MonoBehaviour
{
    public Text DebugText;
    public Text updateText;
    public Transform cubeTest;
    public RectTransform cubeTestUI;
    public Transform cameraT;
    public Vector3 offset = new Vector3(0, 0, 0);

    private string testo = "";
    private float nowPosX = 0;
    private float nowPosZ = 0;
    private bool modoDebug = true;
    private int maxWait = 10;
    private float compass = 0;

    private readonly string _enter = "\n";

    IEnumerator Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //probar si esta activado el servicio
        //if (!Input.location.isEnabledByUser)
        //{
        //    if (modoDebug)
        //    {
        //        DebugText.text += "gps no activado";
        //    }
        //    //yield break;
        //}

        //iniciar servicio
        Input.location.Start(1f, 0.1f);
        Input.compass.enabled = true;

        //espera a que inicie
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        //si no inicia en 10 segundos
        if (maxWait < 1)
        {
            if (modoDebug)
            {
                DebugText.text += _enter + "tiempo acabado";
            }
            yield break;
        }

        //conexion fallida
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            if (modoDebug)
            {
                DebugText.text += _enter + "imposible determinar ubicacion por gps";
            }
            yield break;
        }
        else
        {
            //acceso aceptado
            if (modoDebug)
            {
                DebugText.text += _enter + "GPS aceptado";
                DebugText.text += _enter + "gps: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            }
        }
    }

    private void Update()
    {
        var Ldat = Input.location.lastData;
        nowPosX = Ldat.latitude;
        nowPosZ = Ldat.longitude;
        if (modoDebug)
        {
            testo = _enter + "Latitud: " + nowPosX;
            testo += _enter + "Longitud: " + nowPosZ;
            testo += _enter + "Altitud: " + Ldat.altitude;
            testo += _enter + "Precision horizontal: " + Ldat.horizontalAccuracy;
            testo += _enter + "Precision vertical: " + Ldat.verticalAccuracy;
            //testo += "\n" + "Update´s: " + Time.time;
            testo += _enter + _enter + "valor x(x10): " + (nowPosX * 10);
            testo += _enter + "valor z(x10): " + (nowPosZ * 10);
            testo += _enter + _enter + "compass trueHeading: " + Input.compass.trueHeading;
        }

        compass = Input.compass.trueHeading;

#if !UNITY_EDITOR && UNITY_ANDROID
        cubeTest.localPosition = new Vector3((nowPosX * 10), 0, (nowPosZ * 10));

        ///Si el sonido esta al rever, agregarle +180 rotacion al objeto, primero revisar si con euler angles si funciona///
        cubeTest.localEulerAngles = new Vector3(0, compass, 0);
        cubeTestUI.localEulerAngles = new Vector3(0, 0, -compass);
#endif
        cameraT.position = new Vector3(cubeTest.position.x + offset.x, 1 + offset.y, cubeTest.position.z + offset.z);

        updateText.text = testo;
    }

    public void DebugMode(bool on)
    {
        modoDebug = on;
    }
}