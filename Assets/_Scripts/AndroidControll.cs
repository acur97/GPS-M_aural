using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidControll : MonoBehaviour
{
    private void Awake()
    {
        ApplicationChrome.statusBarState = ApplicationChrome.States.VisibleOverContent;
        ApplicationChrome.statusBarColor = ApplicationChrome.navigationBarColor = 0x00000000;
        ApplicationChrome.navigationBarState = ApplicationChrome.States.VisibleOverContent;
    }
}