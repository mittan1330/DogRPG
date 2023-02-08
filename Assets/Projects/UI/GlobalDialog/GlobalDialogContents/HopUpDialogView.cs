using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HopUpDialogView : GlobalDialogContentBaseView
{
    /// <summary>
    /// Tittleのテキスト
    /// </summary>
    [SerializeField]
    TextMeshProUGUI tittleText;
    public TextMeshProUGUI TittleText => tittleText;
    /// <summary>
    /// Contentのテキスト
    /// </summary>
    [SerializeField]
    TextMeshProUGUI contentText;
    public TextMeshProUGUI ContentText => contentText;
}
