using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerPresenter : CharactorBasePresenter
{
    // TODO Defendモードを追加する

    [SerializeField]
    GameObject playerCamera;
    [SerializeField]
    float rollSpeed = 360f;
    [SerializeField]
    float jumpPower = 3f;
    bool isJump = false;

    public override void Initalize()
    {
        
    }

    // Update is called once per frame
    // Test時のUpdateで実行
    // TODO メソッド削除
    void FixedUpdate()
    {
        OverrideFixedUpdate();
    }

    public override void OverrideFixedUpdate()
    {
        ControlMove();
        ControlJump();
        ControlAttack();
        //TODO 落下中のアニメーションを再生したいね〜・・・
    }

    public override void OverrideUpdate()
    {
        
    }

    public override void SetBinds()
    {
        
    }

    public override void SetEvents()
    {
        
    }

    /// <summary>
    /// カメラの向きに合わせて移動
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMoveVector()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        Vector3 moveVector = Vector3.zero;
        moveVector.z = vertical;
        moveVector.x = horizontal;

        // カメラの向きに合わせてキャラの移動方向を決定
        Quaternion cameraRotate;
        cameraRotate = Quaternion.Euler(
                    0f,
                    playerCamera.transform.eulerAngles.y,
                    0f);
        return cameraRotate * moveVector.normalized;
    }

    /// <summary>
    /// キャラクターの移動制御
    /// </summary>
    private void ControlMove()
    {
        var moveVector = GetMoveVector();
        var isMove = moveVector != Vector3.zero;

        if (charactorAnimator != null)
        {
            // TODO ハッシュ実装に変更する
            charactorAnimator.SetBool("Run", isMove);
        }

        if (isMove)
        {
            transform.localPosition +=
                moveVector * Time.fixedDeltaTime * runMoveSpeed;

            Quaternion lookRotation =
                Quaternion.LookRotation(
                    new Vector3(moveVector.x, 0f, moveVector.z));
            transform.rotation =
                Quaternion.RotateTowards(
                    transform.rotation,
                    lookRotation,
                    Time.deltaTime * rollSpeed);
        }
    }

    /// <summary>
    /// キャラクターの攻撃制御
    /// </summary>
    private void ControlAttack()
    {
        // 攻撃ボタンを押した
        if (charactorAnimator != null && Input.GetButtonDown("Fire1"))
        {
            charactorAnimator.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// キャラクターの攻撃制御
    /// </summary>
    private void ControlJump()
    {
        // 攻撃ボタンを押したら攻撃をする
        // TODO 攻撃しながらでもジャンプできてしまっているのを防ぐ
        if (charactorAnimator != null && Input.GetButtonDown("Jump") && !isJump)
        {
            charactorRigidbody.velocity = Vector3.up * jumpPower;
            charactorAnimator.SetTrigger("Jump");
            isJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // TODO シーケンス実装からEvent実装に変更したい
            var sequence = DOTween.Sequence();
            sequence
                .OnComplete(() => isJump = false)
                .AppendInterval(0.5f);
        }
    }
}
