using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class İntroController : MonoBehaviour
{
    [SerializeField] float activationTimer = 3f;
    [SerializeField] float introTimer = 7f;
    [SerializeField] float fadeOutTimer = 3f;
    [SerializeField] CanvasGroup introGroup;
    [SerializeField] GameObject introObj;
    [SerializeField] AudioClip _clip;

    IEnumerator FadeInIntro(float activationTimer)
    {
        yield return new WaitForSeconds(activationTimer);
        while (introGroup.alpha < 1)
        {
            introGroup.alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(introTimer);
        introGroup.alpha = 0;
        yield return new WaitForSeconds(fadeOutTimer);
        AudioManager.instance.PlaySound(_clip);
        introObj.SetActive(false);
        

    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey("isIntroPlayed"))
        {
            PlayerPrefs.SetInt("isIntroPlayed", 1);
            StartCoroutine(FadeInIntro(activationTimer));
        }

        else
        {
            introObj.SetActive(false);
        }
    }
}
