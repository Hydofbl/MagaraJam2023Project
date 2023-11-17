using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]float _timer = 0.25f;
    float currentTimer;
    [SerializeField]GameObject _tab;
    bool _clickTime = false;

    void Start()
    {
        currentTimer = _timer;
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

        else if (_clickTime) 
        {
            _tab.SetActive(true);
        }
        return;
    }
}
