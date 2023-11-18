using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LinkHandler : MonoBehaviour
{
    [SerializeField] string link;
    [SerializeField] TMP_Text textBox;
    [SerializeField] string Text;

    private void Start()
    {
        textBox.text = Text;
    }

    public void GoToLink()
    {
        Application.OpenURL(link);
    }
}
