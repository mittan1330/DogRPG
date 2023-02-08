using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public abstract class GlobalDialogContentBasePresenter : MonoBehaviour, IShowable
{
    /// <summary>
    /// dialogのView
    /// </summary>
    [SerializeField]
    GlobalDialogContentBaseView dialogView;

    // Actions
    /// <summary>
    /// OnOpenDialogが実行されることでDialogを表示させる
    /// </summary>
    //public Action OnCloseDialog;

    private void Start()
    {
        SetActions();
        SetTexts();
    }

    /// <summary>
    /// ActionをSetする
    /// </summary>
    public virtual void SetActions()
    {
        //OnCloseDialog += dialogView.OnCloseDialog;
    }

    /// <summary>
    /// テキスト等のSetを行うメソッド
    /// </summary>
    public abstract void SetTexts();

    /// <summary>
    /// Dialogを表示させる関数
    /// </summary>
    /// <param name="isEnableBackground">BackGroundをグレー化させるか</param>
    public async UniTask ShowDialog()
    {
        dialogView.OnOpenDialog();
        //await UniTask.WaitUntil(() => dialogView.closeButton.onClick.AddListener());
        await dialogView.closeButton.OnClickAsync();
        dialogView.OnCloseDialog();

        //OnCloseDialog?.Invoke();
    }
}
