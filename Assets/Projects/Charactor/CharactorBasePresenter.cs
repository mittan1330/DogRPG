using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharactorBasePresenter : MonoBehaviour
{
    /// <summary>
    /// キャラクター現在の状態を示すEnum
    /// </summary>
    public enum CharactorState
    {
        Dead,
        Alive,
    }
    /// <summary>
    /// キャラクターの種類を示すEnum
    /// </summary>
    public enum CharactorType
    {
        Monster,
        Player,
    }

    /// <summary>
    /// キャラクターからのSEを再生するAudioSource
    /// </summary>
    [SerializeField]
    private AudioSource _audioSource;
    public AudioSource CharactorAudioSource => _audioSource;
    /// <summary>
    /// キャラクターのアニメーションを再生するAnimator
    /// </summary>
    public Animator charactorAnimator;
    /// <summary>
    /// このキャラクターの種類を保存する変数
    /// </summary>
    public CharactorType charactorType;
    /// <summary>
    /// このキャラクターの現在の状態を保存する変数
    /// </summary>
    public CharactorState charactorState;
    /// <summary>
    /// キャラクターの歩行移動速度
    /// </summary>
    public float walkMoveSpeed = 1;
    /// <summary>
    /// キャラクターのダッシュ移動速度
    /// </summary>
    public float runMoveSpeed = 3;
    /// <summary>
    /// キャラクターの目標地点までの距離
    /// </summary>
    //public float targetDilectionDistance;
    /// <summary>
    /// キャラクターのCanvas
    /// </summary>
    public Transform charactorCanvas;
    /// <summary>
    /// 攻撃や会話などを開始する当たり判定のエリアを指定
    /// </summary>
    [SerializeField]
    private GameObject functionArea;
    public GameObject FunctionArea => functionArea;
    /// <summary>
    /// Camera参照用の変数
    /// </summary>
    private GameObject mainCamera;
    public GameObject MainCamera
    {
        get { return mainCamera; }
        set { mainCamera = value; }
    }
    /// <summary>
    /// キャラクターのRigidbody
    /// </summary>
    public Rigidbody charactorRigidbody;
    /// <summary>
    /// Hp表示を行うSlider
    /// </summary>
    public DoubleValueSliderView hpSlider;

    /// <summary>
    /// このキャラクターを完全に削除する関数
    /// </summary>
    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initalize();
        SetEvents();
        SetBinds();
    }

    private void Update()
    {
        LookAtCanvas();
    }

    /// <summary>
    /// 継承先クラスで初期化を行うための関数
    /// </summary>
    public abstract void Initalize();
    /// <summary>
    /// 継承先クラスで実行するUpdate関数
    /// 基本的にはGameManagerで管理する
    /// </summary>
    public abstract void OverrideFixedUpdate();
    /// <summary>
    /// 継承先クラスで実行するUpdate関数
    /// 基本的にはGameManagerで管理する
    /// </summary>
    public abstract void OverrideUpdate();

    /// <summary>
    /// 継承先クラスでActionの繋ぎこみを行うための関数
    /// </summary>
    public abstract void SetEvents();

    /// <summary>
    /// 継承先クラスでUniRxの繋ぎこみを行うための関数
    /// </summary>
    public abstract void SetBinds();

    /// <summary>
    /// アニメーションの再生を行う
    /// </summary>
    public void PlayBoolAnimation(string animationName,bool isPlay)
    {
        // TODO アニメーションの呼び出しをハッシュ実装に変更する
        charactorAnimator.SetBool(animationName, isPlay);
    }

    /// <summary>
    /// CanvasをLookAtさせるメソッド
    /// </summary>
    void LookAtCanvas()
    {
        charactorCanvas.LookAt(mainCamera.transform);
    }
}
