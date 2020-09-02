using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Color32 colorFade;

    public void CambiarA(string nombreScene)
    {
        Initiate.Fade(nombreScene, colorFade, 0.5f);
    }
}