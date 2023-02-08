using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public abstract class GlobalDialogContentBaseView : MonoBehaviour
{

    [SerializeField]
    RectTransform dialogRect;

    public Button closeButton;

    /// <summary>
    /// Dialogを非表示にする
    /// </summary>
    public void OnCloseDialog()
    {
        //Debug.Log("cloae");
        Cursor.lockState = CursorLockMode.Locked;
        var sequence = DOTween.Sequence();
        sequence
            .Append(
                dialogRect.DOScale(0, 1)
                    .SetEase(Ease.InSine));
    }

    /// <summary>
    /// Dialogを表示にする
    /// </summary>
    public void OnOpenDialog()
    {
        //Debug.Log("open");
        Cursor.lockState = CursorLockMode.Confined;
        dialogRect.transform.localScale = Vector3.zero;
        var sequence = DOTween.Sequence();
        sequence
            .Append(
                dialogRect.DOScale(1, 1)
                    .SetEase(Ease.OutBack));
    }
}
