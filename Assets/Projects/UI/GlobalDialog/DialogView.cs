using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DialogView : MonoBehaviour
{
    [SerializeField]
    Image dialogBackgroundImage;

    public void CloseBackgroundView()
    {
        var sequence = DOTween.Sequence();
            sequence.Append(
                 dialogBackgroundImage.DOFade(0, 0.5f)).
                 OnComplete(() => SetBackgroundActive(false));
    }

    public void ShowBackgroundView()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(
             dialogBackgroundImage.DOFade(0.64f, 0.5f)).
             OnStart(() => SetBackgroundActive(true));
    }

    void SetBackgroundActive(bool status)
    {
        dialogBackgroundImage.raycastTarget = status;
        dialogBackgroundImage.gameObject.SetActive(status);
    }
}
