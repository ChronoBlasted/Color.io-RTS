using DG.Tweening;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Slider sliderWhite;
    [SerializeField] TMP_Text sliderValue;

    public Tweener FillTween { get; private set; }
    public Tweener FillWhiteTween { get; private set; }

    public void Init(float value, float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = value;

        sliderWhite.value = value;
        sliderWhite.maxValue = maxValue;
    }

    public void Init(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        sliderWhite.maxValue = value;
        sliderWhite.value = value;
    }

    public void SetValue(float newValue)
    {
        slider.value = newValue;
        sliderWhite.value = newValue;
    }

    public void SetMaxValue(float newValue)
    {
        slider.maxValue = newValue;
        sliderWhite.maxValue = newValue;
    }

    public void SetValueSmooth(float newValue, float duration = 0.2f, Ease ease = Ease.OutCirc, TweenCallback callback = null)
    {
        if (FillTween != null)
        {
            FillTween.Kill(true);
            FillTween = null;
        }

        if (FillWhiteTween != null)
        {
            FillWhiteTween.Kill();
            FillWhiteTween = null;
        }

        FillTween = slider.DOValue(newValue, duration).SetEase(ease).OnComplete(callback);

        FillWhiteTween = sliderWhite.DOValue(newValue, duration).SetEase(Ease.Linear);
    }

    public float GetValue()
    {
        return slider.value;
    }


    #region TextUpdateOnValueChange
    public void UpdateTextWithSlash() => sliderValue.text = Mathf.RoundToInt(slider.value) + "/" + slider.maxValue; // Pour l'inspecteur onchange du slider
                                                                                                                    // public void UpdateTextValue() => sliderValue.text = UIManager.GetFormattedInt(Mathf.RoundToInt(slider.value)).ToString(); // Pour l'inspecteur onchange du slider
    public void UpdateTextValueWithSuffixe(string suffixe) => sliderValue.text = Mathf.RoundToInt(slider.value) + suffixe; // Pour l'inspecteur onchange du slider
    public void UpdateTextValueWithPrefix(string prefix) => sliderValue.text = prefix + Mathf.RoundToInt(slider.value); // Pour l'inspecteur onchange du slider
    public void UpdateText(string prefix = "", string suffixe = "", bool slash = false) => sliderValue.text = prefix + Mathf.RoundToInt(slider.value) + (slash ? "/" + slider.maxValue : "") + suffixe; // Cas precis
    public void UpdateTextPerCent() => sliderValue.text = Mathf.RoundToInt((slider.value / slider.maxValue) * 100) + "%";
    public void UpdateTextTimeInMinSec() => sliderValue.text = FloatToMMSS(Mathf.Round(slider.value)); // Cas precis

    public string FloatToMMSS(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        int remainingSeconds = Mathf.FloorToInt(seconds % 60f);

        return $"{minutes:00}:{remainingSeconds:00}";
    }

    #endregion
}