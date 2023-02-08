using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TittleView : MonoBehaviour
{

    [SerializeField]
    Button NewGameButton;
    [SerializeField]
    Button LoadGameButton;
    [SerializeField]
    Button SettingButton;

    // Start is called before the first frame update
    void Start()
    {
        NewGameButton.onClick.AddListener(() => LoadGame());
    }

    // TODO : 現状はテスト用の為、後々に実装を書き換える
    void LoadGame()
    {
        SceneManager.LoadScene("InGame");
    }
}
