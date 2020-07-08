using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PuntosSonorosControl))]
public class PuntosSonorosEditor : Editor
{
    private int contadorPuntosCreados;

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        PuntosSonorosControl PScontrol = (PuntosSonorosControl)target;

        PScontrol.Padre = (Transform)EditorGUILayout.ObjectField("Padre", PScontrol.Padre, typeof(Transform), true);
        PScontrol.Prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", PScontrol.Prefab, typeof(GameObject), true);

        GUILayout.Space(5);
        GUILayout.Label("Cantidad actual: " + PScontrol.listadoDePuntos.Count);

        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Crear Punto"))
            {
                contadorPuntosCreados += 1;
                PScontrol.CrearPunto(contadorPuntosCreados);
            }
            if (GUILayout.Button("Limpiar"))
            {
                PScontrol.LimpiarPuntos();
                contadorPuntosCreados = 0;
            }
        }
        GUILayout.EndHorizontal();

        ///
        GUILayout.Space(10);

        for (int i = 0; i < PScontrol.listadoDePuntos.Count; i++)
        {
            PuntosSonorosControl.PuntoSonoro punto = PScontrol.listadoDePuntos[i];
            punto.fold = EditorGUILayout.Foldout(punto.fold, punto.nombre);

            if (punto.fold)
            {
                //GUI.color = Color.black;
                //GUI.contentColor = Color.red;

                if (GUILayout.Button("Seleccionar"))
                {
                    EditorGUIUtility.PingObject(PScontrol.listadoGameobjects[i] as Object);
                }

                punto.nombre = EditorGUILayout.TextField("Nombre:", punto.nombre);
                Object obj = PScontrol.listadoGameobjects[i] as Object;
                obj.name = punto.nombre;

                //punto.posicionReal = EditorGUILayout.Vector2Field("Posicion real:", punto.posicionReal);

                GUILayout.BeginHorizontal();
                {
                    float originalV = EditorGUIUtility.labelWidth;
                    EditorGUIUtility.labelWidth = 15;
                    punto.posicionReal.x = EditorGUILayout.FloatField("X", punto.posicionReal.x);
                    punto.posicionReal.z = EditorGUILayout.FloatField("Z", punto.posicionReal.z);
                    EditorGUIUtility.labelWidth = originalV;
                }
                GUILayout.EndHorizontal();

                GUILayout.Label("Posicion x10:");
                GUILayout.BeginHorizontal();
                {
                    if (GUI.changed)
                    {
                        PScontrol.ActualizarPosicion(i);
                    }

                    GUILayout.Label("X: " + punto.posicionX10.x);
                    GUILayout.Label("Z: " + punto.posicionX10.z);
                }
                GUILayout.EndHorizontal();

                punto.audio = (AudioClip)EditorGUILayout.ObjectField("Audio:", punto.audio, typeof(AudioClip), true);

                if (punto.audio && GUI.changed)
                {
                    PScontrol.ActualizarAudio(i);
                }

                punto.imagen = (Sprite)EditorGUILayout.ObjectField("Imagen:", punto.imagen, typeof(Sprite), true);

                if (punto.imagen && GUI.changed)
                {
                    PScontrol.ActualizarImagen(i);
                }

                punto.borrar = EditorGUILayout.Foldout(punto.borrar, "Borrar");

                if (punto.borrar)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(25);
                        if (GUILayout.Button("Si", EditorStyles.miniButton))
                        {
                            punto.borrar = false;
                            PScontrol.EliminarPunto(i);
                        }

                        GUILayout.Space(25);

                        if (GUILayout.Button("No", EditorStyles.miniButton))
                        {
                            punto.borrar = false;
                        }
                        GUILayout.Space(25);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);
                }
                else
                {
                    GUILayout.Space(10);
                }
            }
            else
            {
                punto.borrar = false;
            }
        }
    }
}