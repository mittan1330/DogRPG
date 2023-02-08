using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InGamePresenter : MonoBehaviour
{
    /// <summary>
    /// InGameのModel
    /// </summary>
    private InGameModel _inGameModel;
    public InGameModel inGameModel => _inGameModel;
    /// <summary>
    /// InGameのView
    /// </summary>
    [SerializeField]
    private InGameView _inGameView;
    /// <summary>
    /// enemyを保存するList
    /// </summary>
    //[NonSerialized]
    public List<EnemyBasePresenter> enemys = new List<EnemyBasePresenter>();
    /// <summary>
    /// EnemyGameObjectの親に当たるGameObject
    /// </summary>
    [SerializeField]
    private GameObject enemyBaseTransform;
    /// <summary>
    /// PlayerのGameObject
    /// </summary>
    public PlayerPresenter player;
    /// <summary>
    /// CameraのGameObject
    /// </summary>
    public GameObject mainCamera;
    /// <summary>
    /// Menuパネルを制御するPresenter
    /// </summary>
    [SerializeField]
    MenuPanelPresenter menuPresenter;
    /// <summary>
    /// ダイアログを制御するPresenter
    /// </summary>
    [SerializeField]
    DialogPresenter dialogPresenter;

    bool isPlay;
    // TODO テスト用
    float time;


    /// <summary>
    /// Enemyを全て取得する
    /// </summary>
    public void GetEnemys()
    {
        foreach (EnemyBasePresenter enemy in
            enemyBaseTransform.GetComponentsInChildren<EnemyBasePresenter>())
        {
            // 自分自身の場合は処理をスキップする
            if (enemy.gameObject == enemyBaseTransform) continue;
            enemys.Add(enemy);
        }
    }

    private void Awake()
    {
        // フレームレートを60fpsに設定
        Application.targetFrameRate = 60;
        // カーソルを非表示にする
        Cursor.lockState = CursorLockMode.Locked;
        isPlay = true;

        // modelを生成させる
        _inGameModel = new InGameModel();

        // TODO テスト用
        time = 60;

        GetEnemys();
        // Enemy内の初期アタッチを実行する
        foreach (EnemyBasePresenter enemy in enemys)
        {
            enemy.Player = player.gameObject;
            enemy.MainCamera = mainCamera;
        }
        player.MainCamera = mainCamera;
    }

    private void FixedUpdate()
    {
        // Enemy内のUpdateを実行する
        foreach (EnemyBasePresenter enemy in enemys)
        {
            enemy.OverrideFixedUpdate();
        }
    }

    private void Update()
    {
        // TODO
        if (isPlay)
        {
            time -= Time.deltaTime;
            _inGameView.OnSetTimerText(time);

        }

        // Enemy内のUpdateを実行する
        var tmpEnemys = new List<EnemyBasePresenter>(enemys);
        foreach (EnemyBasePresenter enemy in enemys)
        {
            if (enemy.charactorState == CharactorBasePresenter.CharactorState.Dead)
            {
                tmpEnemys.Remove(enemy);
            }
            enemy.OverrideUpdate();
        }
        enemys = tmpEnemys;

        if(enemys.Count == 0 && isPlay)
        {
            isPlay = false;
            dialogPresenter.OnOpenDialog(DialogPresenter.DialogTypes.HopUpDialog);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPresenter.OnClickButton();
        }
    }
}
