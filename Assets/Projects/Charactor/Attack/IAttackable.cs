using UnityEngine.EventSystems;

public interface IAttackable : IEventSystemHandler
{
    /// <summary>
    /// Animator Controllerにおいて「Attack」ステートの開始に
    /// 一時的に武器のコライダーを有効にするためにのコールバックメソッド
    /// </summary>
    public void EnableWeaponCollider();
    /// <summary>
    /// Animator Controllerにおいて「Attack」ステートの開始に
    /// 一時的に武器のコライダーを有効にするためにのコールバックメソッド
    /// </summary>
    public void DisableWeaponCollider();
}
