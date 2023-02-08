using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopUpDialogPresenter : GlobalDialogContentBasePresenter
{
    [SerializeField]
    HopUpDialogView hopUpDialogView;
    // TODO この内容を設定できるようにしたいね〜
    [SerializeField]
    string tittle="";
    [SerializeField]
    string content="";

    public override void SetTexts()
    {
        if (tittle != "") hopUpDialogView.TittleText.SetText(tittle);
        if (content != "") hopUpDialogView.ContentText.SetText(content);
    }
}
