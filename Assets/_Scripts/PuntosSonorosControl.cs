using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PuntosSonorosControl : MonoBehaviour
{
    [Serializable]
    public class Coordenada
    {
        public float x;
        public float z;
    }

    [Serializable]
    public class PuntoSonoro
    {
        public bool fold;
        public bool borrar;

        public string nombre = "Punto";
        public Coordenada posicionReal;
        public Coordenada posicionX10;
        public AudioClip audio;
        public Sprite imagen;
    }

    public List<PuntoSonoro> listadoDePuntos;
    public List<GameObject> listadoGameobjects;

    public Transform Padre;
    public GameObject Prefab;

#if UNITY_EDITOR

    public void CrearPunto(int numero)
    {
        PuntoSonoro puntoNuevo = new PuntoSonoro();
        puntoNuevo.nombre = "Punto " + numero;

        //listadoGameobjects.Add(Instantiate(Prefab, Vector3.zero, Quaternion.identity, Padre));
        listadoGameobjects.Add(PrefabUtility.InstantiatePrefab(Prefab as GameObject, Padre) as GameObject);
        listadoDePuntos.Add(puntoNuevo);
    }

    public void EliminarPunto(int posicion)
    {
        DestroyImmediate(listadoGameobjects[posicion]);
        listadoDePuntos.RemoveAt(posicion);
    }

    public void LimpiarPuntos()
    {
        listadoDePuntos.Clear();
        for (int i = 0; i < listadoGameobjects.Count; i++)
        {
            DestroyImmediate(listadoGameobjects[i]);
        }
        listadoGameobjects.Clear();
    }

    public void ActualizarPosicion(int index_objeto)
    {
        listadoDePuntos[index_objeto].posicionX10.x = listadoDePuntos[index_objeto].posicionReal.x * 10;
        listadoDePuntos[index_objeto].posicionX10.z = listadoDePuntos[index_objeto].posicionReal.z * 10;

        listadoGameobjects[index_objeto].transform.position = new Vector3(listadoDePuntos[index_objeto].posicionX10.x, 0, listadoDePuntos[index_objeto].posicionX10.z);
    }

    public void ActualizarAudio(int index_objeto)
    {
        listadoGameobjects[index_objeto].GetComponent<AudioSource>().clip = listadoDePuntos[index_objeto].audio;
    }

    public void ActualizarImagen(int index_objeto)
    {
        listadoGameobjects[index_objeto].GetComponentInChildren<SpriteRenderer>().sprite = listadoDePuntos[index_objeto].imagen;
    }

#endif
}