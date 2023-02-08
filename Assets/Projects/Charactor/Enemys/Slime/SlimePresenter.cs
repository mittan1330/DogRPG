using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePresenter : EnemyBasePresenter
{
    [SerializeField]
    float changeDirectionDistance = 1f;
    /// <summary>
    /// 敵キャラクターの種類を指定
    /// </summary>
    public EnemyState enemyState;

    public override void Initalize()
    {
        SetValues();
        GetTargets();
        if (enemyMode == EnemyMode.STAND) return;
        SetPatrolDirectionTarget();
        // TODO 強制的にアニメーションを再生している状態を修正する
        PlayBoolAnimation("Walk", true);
        // 最初の状態
        enemyState = new Idle(this.gameObject, navMesh, Player.transform);
    }

    public override void OverrideFixedUpdate()
    {
        
    }

    public override void OverrideUpdate()
    {
        if (enemyMode == EnemyMode.STAND)
        {
            var direction = Player.transform.position - transform.position;
            direction.y = 0;

            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
            return;
        }

        // 現在の状態を実行。戻り値は次の状態
        enemyState = enemyState.Process();

    }

    public override void SetBinds()
    {
        
    }

    public override void SetEvents()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO Tag処理からEvent処理に変更する必要がありそう
        if (other.gameObject.tag == "PlayerAttack")
        {
            // TODO ダメージの付与数をなんとかしたい
            GetDamage(Random.Range(20,50),other);
        }
    }
}
