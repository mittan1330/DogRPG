using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuPanelView : MonoBehaviour
{
    [SerializeField]
    GameObject rightToggleImage;
    [SerializeField]
    GameObject leftToggleImage;
    [SerializeField]
    RectTransform ImageRect;
    public Button onOpenButton;

    public void OnOpenWindow()
    {
        rightToggleImage.SetActive(true);
        leftToggleImage.SetActive(false);
        ImageRect.DOAnchorPos(new Vector2(-125, 0), 1f)
                    .SetEase(Ease.OutQuad);
                    //.OnComplete(() => Debug.Log("Opened"));
    }

    public void OnCloseWindow()
    {
        rightToggleImage.SetActive(false);
        leftToggleImage.SetActive(true);
        ImageRect.DOAnchorPos(new Vector2(125, 0), 1f)
                    .SetEase(Ease.OutQuad);
                    //.OnComplete(() => Debug.Log("Closeed"));
    }
}
