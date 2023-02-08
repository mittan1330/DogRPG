using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelPresenter : MonoBehaviour
{
    bool isOpenMenu = false;
    [SerializeField]
    MenuPanelView menuPanelView;

    private void Start()
    {
        menuPanelView.onOpenButton.onClick.AddListener(() => OnClickButton());
    }

    public void OnClickButton()
    {
        if (isOpenMenu)
        {
            menuPanelView.OnCloseWindow();
            isOpenMenu = false;
        }
        else
        {
            menuPanelView.OnOpenWindow();
            isOpenMenu = true;
        }
    }
}
