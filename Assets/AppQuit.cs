using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AppQuit : MonoBehaviour

{
    IEnumerator ApplicationQuit()
    {
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

    public void QuitCoroutine()
    {
        StartCoroutine(ApplicationQuit());
    }
}
