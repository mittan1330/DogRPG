using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
using UnityEngine.AI;
using DG.Tweening;

public abstract class EnemyBasePresenter : CharactorBasePresenter, IDamagable
{
    /// <summary>
    /// Enemyのモードを設定する
    /// </summary>
    public enum EnemyMode
    {
        NOLMAL,
        STAND,
    };
    /// <summary>
    /// Enemyのモードを設定
    /// </summary>
    public EnemyMode enemyMode;
    /// <summary>
    /// 巡回場所を定義する親となるゲームオブジェクトを指定
    /// </summary>
    [Header("巡回先を定義するゲームオブジェクト")]
    [SerializeField]
    GameObject targetsControllObject;
    /// <summary>
    /// 巡回場所のTransformを保存するList
    /// </summary>
    [NonSerialized]
    public List<Transform> targets = new List<Transform>();
    /// <summary>
    /// navMeshを設定する変数
    /// </summary>
    public NavMeshAgent navMesh;
    // NavMeshで向かう方向を指定する
    [NonSerialized]
    public Transform directionTarget;
    [SerializeField]
    GameObject damageUiPrefab;
    /// <summary>
    /// Player参照用の変数
    /// </summary>
    private GameObject player;
    public GameObject Player
    {
        get { return player; }
        set { player = value; }
    }
    /// <summary>
    /// ダメージエフェクトのPrefab
    /// </summary>
    [SerializeField]
    GameObject damageEffect;

    // EnemyのHP表示に関する実装
    // TODO Modelに実装を移したい
    [SerializeField]
    int hp = 100;

    /// <summary>
    /// Enemyに関係する変数の値をセットするメソッド
    /// </summary>
    public virtual void SetValues()
    {
        navMesh.speed = walkMoveSpeed;
        hpSlider.SetMaxValue(hp);
        charactorState = CharactorState.Alive;
    }

    /// <summary>
    /// 巡回する場所を全て取得する
    /// </summary>
    public void GetTargets()
    {
        foreach(Transform target in
            targetsControllObject.GetComponentsInChildren<Transform>())
        {
            // 自分自身の場合は処理をスキップする
            if (target.gameObject == targetsControllObject) continue;
            targets.Add(target);
        }
        //if(targets.Count != 0) directionTarget = targets[0];
    }

    /// <summary>
    /// 移動する方向のターゲット座標をランダムで設定する
    /// </summary>
    public void SetPatrolDirectionTarget()
    {
        if (targets.Count == 0) return ;
        var beforeTarget = directionTarget;
        do
        {
            directionTarget = targets[Random.Range(0, targets.Count)];
        }
        while (beforeTarget == directionTarget);
        //TODO SetDestinationをNullにしてターゲット座標への移動しない状態を作ってもいいかも？
        navMesh.SetDestination(directionTarget.position);
    }

    /// <summary>
    ///  移動する方向までの距離を取得する
    /// </summary>
    public float GetDirectionDistance(Transform target)
    {
        return Vector3.Distance(transform.position, target.position);
    }

    /// <summary>
    /// ダメージを受けた際の処理
    /// </summary>
    //　TODO　コライダーのダメージ実装をどうする？？
    // TODO そもそもこの定義はもう一個上の階層では？？
    public void GetDamage(int damage, Collider hit)
    {
        if (hp <= 0) return;
        var damageUi = Instantiate(
                damageUiPrefab,
                charactorCanvas.position,
                charactorCanvas.rotation,
                charactorCanvas);
        damageUi.GetComponent<DamageDialogPresenter>().OnDamaged(damage);
        hit.enabled = false;

        // TODO ダメージ処理全てをDoTweenで管理したい
        //var damageSequence = DOTween.Sequence();

        // TODO ノックバック実装をする

        // TODO HPの減少処理はUniRxで実装したい
        // TODO メソッド分割したい
        hp -= damage;
        // TODO アクション処理にしてModel->HpSliderで接続したい
        hpSlider.SetSliderValue(hp);
        if (hp <= 0)
        {
            OnCharactorDead();
        }
        else
        {
            charactorAnimator.SetTrigger("Damage");
        }
    }

    /// <summary>
    /// キャラクターが死亡した際の処理
    /// </summary>
    public void OnCharactorDead()
    {
        var sequence = DOTween.Sequence();
        sequence.
            Append(charactorCanvas.DOScale(0, 0.5f).SetEase(Ease.Linear)).
            Append(this.transform.DOScale(0, 0.5f).SetEase(Ease.Linear));
        charactorAnimator.SetTrigger("Dead");
        var effect = Instantiate(damageEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2);
        charactorState = CharactorState.Dead;
    }
}
