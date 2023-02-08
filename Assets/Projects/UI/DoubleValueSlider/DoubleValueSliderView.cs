using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoubleValueSliderView : MonoBehaviour
{
    [SerializeField]
    Slider frontSlider;
    [SerializeField]
    Slider backSlider;
    private float sliderValue;

    /// <summary>
    /// SliderのmaxValue初期値を設定する
    /// </summary>
    public void SetMaxValue(int value)
    {
        sliderValue = value;
        frontSlider.maxValue = sliderValue;
        backSlider.maxValue = sliderValue;
        frontSlider.value = sliderValue;
        backSlider.value = sliderValue;
    }

    /// <summary>
    /// Sliderの値を変化させる処理
    /// </summary>
    public void SetSliderValue(int value)
    {
        var sequence = DOTween.Sequence();
        sequence.
            OnStart(() => frontSlider.value = sliderValue).
            AppendInterval(0.3f).
            OnComplete(() => DOTween.To(() => backSlider.value,
            val => backSlider.value = val,
            (float)value, 1.0f));
        sliderValue = value;
    }
}
