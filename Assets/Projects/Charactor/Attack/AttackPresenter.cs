using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackPresenter : MonoBehaviour, IAttackable
{
    [SerializeField]
    private Collider _weaponCollider;

    /// <summary>
    /// Animator Controllerにおいて「Attack」ステートの開始に
    /// 一時的に武器のコライダーを有効にするためにのコールバックメソッド
    /// </summary>
    public void EnableWeaponCollider()
    {
        var sequence = DOTween.Sequence();
        sequence.
            OnComplete(() => ChangeColliderEnable(true)).
            AppendInterval(0.5f);
    }

    /// <summary>
    /// Animator Controllerにおいて「Attack」ステートの開始に
    /// 一時的に武器のコライダーを有効にするためにのコールバックメソッド
    /// </summary>
    public void DisableWeaponCollider()
    {
        ChangeColliderEnable(false);
    }

    void ChangeColliderEnable(bool state)
    {
        if (_weaponCollider == null) return;
        _weaponCollider.enabled = state;
    }
}
