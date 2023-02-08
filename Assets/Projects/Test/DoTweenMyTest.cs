using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DoTweenMyTest : MonoBehaviour
{
    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        //Debug.Log("On");
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(
                transform.DOScale(1.1f, 0.5f).SetEase(Ease.OutElastic))
            .Append(
                transform.DOScale(1f, 0.1f));
        //GetComponentInChildren<Text>().text = "押された";
    }
}
