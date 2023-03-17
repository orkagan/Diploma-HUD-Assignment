using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Division : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            DivisionTest();
        }

    }


    private void DivisionTest()
    {
        int a = 3;
        int b = 4;

        int result = a / b;

        Debug.Log(result);
    }

}
