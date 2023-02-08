using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// Playerのアニメーション再生Event処理を司るクラス
/// </summary>
/// TODO Playerだけでなく汎用化したい
public class PlayerAttackState : StateMachineBehaviour
{
    private PlayerPresenter playerPresenter;
    private float savedSpeed;

    // OnStateEnterは遷移が始まり、ステートマシンがこの状態を
    // 評価し始めると呼び出されます。
    override public void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
    {
        // PlayerConttollerへの参照を取得
        playerPresenter = animator.GetComponentInParent<PlayerPresenter>();
        // 現在のスピードを保存
        savedSpeed = playerPresenter.runMoveSpeed;
        // スピードを0に設定
        playerPresenter.runMoveSpeed = 0;

        // 武器のコライダーを有効化
        ExecuteEvents.Execute<IAttackable>(
            target: playerPresenter.FunctionArea,
            eventData: null,
            functor: (reciever, eventData) => reciever.EnableWeaponCollider()
        );
    }

    // OnStateUpdateは、OnStateEnterとOnStateExitの
    // コールバック間の各Updateフレームで呼び出されます。
    override public void OnStateUpdate(
            Animator animator,AnimatorStateInfo stateInfo,
            int layerIndex)
    {

    }

    // OnStateExitは、遷移が終了し、ステートマシンが
    // この状態の評価を終了したときに呼び出されます。
    override public void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
    {
        // スピードを元に戻す
        playerPresenter.runMoveSpeed = savedSpeed;

        // 武器のコライダーを無効化
        ExecuteEvents.Execute<IAttackable>(
            target: playerPresenter.FunctionArea,
            eventData: null,
            functor: (reciever, eventData) => reciever.DisableWeaponCollider()
        );
    }
}
