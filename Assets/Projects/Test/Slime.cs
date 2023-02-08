using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Slime : MonoBehaviour
{
    //DOShakeやDOColor
    RectTransform imageRect;
    Image image;
    [SerializeField]
    Color damageColor;
    [SerializeField]
    Button attackButton;
    [SerializeField]
    RectTransform buttonRect;


    private void Start()
    {
        imageRect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void OnDamege()
    {
        // ボタンのダメージアニメーション
        var buttonSequence = DOTween.Sequence();
        buttonSequence
            .Append(
                buttonRect.DOScale(0, 0))
            .Append(
                buttonRect.DOScale(1.2f, 0.5f)
                    .SetEase(Ease.OutElastic));


        // スライムのダメージアニメーション
        var sequence = DOTween.Sequence();
        sequence
            .Append(
                imageRect.DOShakeAnchorPos(1, 10))
            .Join(
                image.DOColor(damageColor, 0.2f)
                    .SetLoops(2, LoopType.Yoyo));
            //.OnStart(() => attackButton.interactable = false);

    }
}