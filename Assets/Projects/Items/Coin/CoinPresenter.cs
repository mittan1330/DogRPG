using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinPresenter : ItemsBasePresenter
{

    /// <summary>
    /// コインに接触した時のAction
    /// </summary>
    public Action OnHitCoin;


    [SerializeField]
    private int _coinScore;

    public override void Initalize()
    {
        
    }

    public override void OnDisableSprite()
    {
        
    }

    public override void SetEvents()
    {
        OnHitCoin += inGamePresenter.inGameModel.InGameAddCoin;
        OnHitItemScore += inGamePresenter.inGameModel.InGameAddScore;
    }

    /// <summary>
    /// コインスコアの加算
    /// </summary>
    public void AddCoinScore()
    {
        OnHitCoin?.Invoke();
        OnHitItemScore?.Invoke(_coinScore);
        OnDestroy();
    }

    /// <summary>
    /// 2Dの当たり判定
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AddCoinScore();
        }
    }
}
