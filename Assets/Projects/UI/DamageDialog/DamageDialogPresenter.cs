using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class DamageDialogPresenter : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI damageValueText;
    [SerializeField]
    RectTransform damegaeImageRect;
    [SerializeField]
    Image damegaeImage;
    [SerializeField]
    Color damageColor;
    [SerializeField]
    float moveYValue = 1;

    public void OnDamaged(int damageValue)
    {
        // ダメージを表示する
        damageValueText.SetText(damageValue.ToString());
        // ダメージのUIアニメーション
        var uiSequence = DOTween.Sequence();
        uiSequence
            .Append(
                damegaeImage.DOFade(1, 0))
            .Join(
                damageValueText.DOFade(1, 0))
            .Join(
                damegaeImageRect.DOScale(0, 0))
            .Append(
                damegaeImageRect.DOScale(0.01f, 0.5f)
                    .SetEase(Ease.OutElastic))
            .Join(
                damegaeImageRect.DOLocalMoveX(
                    damegaeImageRect.anchoredPosition.x + Random.Range(-1.0f, 1.0f), 0.3f)
                        .SetEase(Ease.InOutQuad))
            .Join(
                damegaeImageRect.DOLocalMoveY(
                    damegaeImageRect.anchoredPosition.y + moveYValue, 0.3f))
            .Join(
                damegaeImage.DOColor(damageColor, 0.2f)
                    .SetLoops(2, LoopType.Yoyo))
            .AppendInterval(0.2f)         // 0.2秒待機
            .Append(
                damegaeImage.DOFade(0, 0.5f))
            .Join(
                damageValueText.DOFade(0, 0.5f))
            .OnComplete(() => Destroy(this.gameObject));
            //.OnComplete(() => damegaeImageRect.localPosition = new Vector3(0, 0.3f, 0));
            //.OnComplete(() => attackButton.interactable = true);
    }
}
