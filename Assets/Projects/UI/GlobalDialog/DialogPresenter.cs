using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPresenter : MonoBehaviour
{

    [SerializeField]
    DialogView dialogView;
    // TODO Enumsへ移動させた方が良さげ
    public enum DialogTypes
    {
        HopUpDialog,
    }
    [SerializeField]
    GameObject[] dialogs;

    public async void OnOpenDialog(DialogTypes type = DialogTypes.HopUpDialog)
    {
        var targetDialog = dialogs[(int)type];
        dialogView.ShowBackgroundView();
        var dialog = Instantiate(targetDialog,
            transform.position,
            transform.rotation,
            transform).
            GetComponent<IShowable>(); ;
        await dialog.ShowDialog();
        dialogView.CloseBackgroundView();
    }
}
