using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeValueListener : MonoBehaviour
{
    public TMP_Text text;
    public Slider slider;
    public void UpdateValue()
    {
        text.text = string.Format("{0:F1}", slider.value*100);
    }
}
