using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeValueListener : MonoBehaviour
{
    public TMP_Text text;
    public Slider slider;
    public AudioSource audioSource;

    public void UpdateValue()
    {
        // eliminar los decimales del slider
        int volumeValue = Mathf.RoundToInt(slider.value * 100);
        text.text = volumeValue.ToString();

        // actualiza el volumen del audio
        audioSource.volume = slider.value;
    }
}