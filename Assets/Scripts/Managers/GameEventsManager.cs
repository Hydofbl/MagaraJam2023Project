using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    [SerializeField] float msnEventTimer;
    [SerializeField] GameObject msnObject;
    
    IEnumerator EventActivator()
    {
        yield return new WaitForSeconds(msnEventTimer);
        msnObject.SetActive(true);
    }

    public void Start()
    {
        StartCoroutine(EventActivator());
    }

    public void Quit()
    {
        Application.Quit();
    }

}
