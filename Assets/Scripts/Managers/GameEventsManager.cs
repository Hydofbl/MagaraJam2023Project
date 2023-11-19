using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    [SerializeField] float msnEventTimer;
    [SerializeField] GameObject msnObject;

    public static GameEventsManager instance { get; private set; }

    private void Awake()
    {
        if
            (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        Time.timeScale = 1.0f;
    }

    public event Action onReadMeOpened;

    public void ReadMeOpened()
    {
        if (onReadMeOpened != null)
        {
            onReadMeOpened();
        }

    }

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
