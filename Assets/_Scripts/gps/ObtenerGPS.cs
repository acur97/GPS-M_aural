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
    public Vector3 offset;

    private string testo;
    //private bool primerPos;
    //private double firstPosX;
    //private double firstPosZ;
    //private double calcPosX;
    //private double calcPosZ;
    private float nowPosX;
    private float nowPosZ;
    private bool modoDebug = false;

    IEnumerator Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        cubeTestUI.sizeDelta = new Vector2(Screen.width / 10.8f, Screen.width / 10.8f);

        //probar si esta activado el servicio
        if (!Input.location.isEnabledByUser)
        {
            if (modoDebug)
            {
                DebugText.text += "gps no activado";
                Debug.Log("gps no activado");
            }
            //yield break;
        }

        //iniciar servicio
        Input.location.Start(1f, 0.1f);
        Input.compass.enabled = true;

        //espera a que inicie
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        //si no inicia en 20 segundos
        if (maxWait < 1)
        {
            if (modoDebug)
            {
                DebugText.text += "\n" + "tiempo acabado";
                Debug.Log("tiempo acabado");
            }
            yield break;
        }

        //conexion fallida
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            if (modoDebug)
            {
                DebugText.text += "\n" + "imposible determinar ubicacion por gps";
                Debug.Log("imposible determinar ubicacion por gps");
            }
            yield break;
        }
        else
        {
            //acceso aceptado
            if (modoDebug)
            {
                Debug.Log("GPS aceptado");
                DebugText.text += "\n" + "GPS aceptado";
                Debug.Log("gps: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
                DebugText.text += "\n" + "gps: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            }
            /*if (!primerPos)
            {
                firstPosX = Input.location.lastData.latitude;
                firstPosZ = Input.location.lastData.longitude;
            }*/
        }
    }

    private void Update()
    {
        var Ldat = Input.location.lastData;
        nowPosX = Ldat.latitude;
        nowPosZ = Ldat.longitude;
        if (modoDebug)
        {
            testo = "\n" + "Latitud: " + Ldat.latitude;
            testo += "\n" + "Longitud: " + Ldat.longitude;
            testo += "\n" + "Altitud: " + Ldat.altitude;
            testo += "\n" + "Precision horizontal: " + Ldat.horizontalAccuracy;
            testo += "\n" + "Precision vertical: " + Ldat.verticalAccuracy;
            testo += "\n" + "Update´s: " + Time.time;
            testo += "\n" + "\n" + "valor x: " + nowPosX;
            testo += "\n" + "valor z: " + nowPosZ;
            testo += "\n" + "\n" + "valor x(x10): " + (nowPosX * 10);
            testo += "\n" + "valor z(x10): " + (nowPosZ * 10);
            testo += "\n" + "\n" + "compass trueHeading: " + Input.compass.trueHeading;
        }
        //testo += "\n" + "Timestamp: " + Ldat.timestamp;
        //testo += "\n" + "compass trueHeading(offset): " + (Input.compass.trueHeading + 270);
        //testo += "\n" + "compass headingAccuracy: " + Input.compass.headingAccuracy;
        //testo += "\n" + "compass magneticHeading: " + Input.compass.magneticHeading;
        //testo += "\n" + "compass rawVector: " + Input.compass.rawVector;
        //testo += "\n" + "compass timestamp: " + Input.compass.timestamp;

#if !UNITY_EDITOR && UNITY_ANDROID
        cubeTest.localPosition = new Vector3((nowPosX * 10), 0, (nowPosZ * 10));

        ///Si el sonido esta al rever, agregarle +180 rotacion al objeto, primero revisar si con euler angles si funciona///
        cubeTest.localEulerAngles = new Vector3(0, (Input.compass.trueHeading), 0);
        cubeTestUI.localEulerAngles = new Vector3(0, 0, -(Input.compass.trueHeading));
#endif
        cameraT.position = new Vector3(cubeTest.position.x + offset.x, 1 + offset.y, cubeTest.position.z + offset.z);

        updateText.text = testo;
    }

    public void DebugMode(bool on)
    {
        modoDebug = on;
    }
}