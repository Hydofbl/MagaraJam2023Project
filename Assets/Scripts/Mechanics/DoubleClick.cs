using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]float _timer = 0.25f;
    float currentTimer;
    [SerializeField] GameObject _tab;
    [SerializeField] GameObject _errorTab;
    bool _clickTime = false;
    [SerializeField] bool readMeOpened = false;

    void Start()
    {
        currentTimer = _timer;

        // subscribe to events
        GameEventsManager.instance.onReadMeOpened += OnReadMeOpened;
    }

    void Update()
    {
        if(_clickTime)
        {
            if(_timer > 0)
            {
                _timer -= Time.deltaTime;
            }

            else
            {
                _clickTime = false;
                _timer = currentTimer;
            }
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (_timer == currentTimer)
        { _clickTime = true; }

        else if (_clickTime && !readMeOpened) 
        {
            _errorTab.SetActive(true);
        }

        else if (gameObject.name == "ReadMe.txt")
        {
            _tab.SetActive(true);
            GameEventsManager.instance.ReadMeOpened();
        }

        else if (gameObject.name == "CurseOfTheCursor")
        {
            _tab.SetActive(true);
            ScenesManager.Instance.LoadNewGame();
        }

        else _tab.SetActive(true);
        
        return;
    }

    void OnReadMeOpened()
    {
        readMeOpened = true;
    }
}
