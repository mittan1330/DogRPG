using UnityEngine;

/// <summary>
/// ダメージ処理の中継をするインターフェース
/// </summary>
public interface IDamagable
{
    void GetDamage(int damage,Collider hit);
}