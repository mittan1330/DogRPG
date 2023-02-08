public class InGameModel
{
    /// <summary>
    /// InGameゲームで獲得したスコアを代入する変数
    /// </summary>
    public int score { get; set; }

    /// <summary>
    /// InGameゲームで獲得したコインを代入する変数
    /// </summary>
    public int coin { get; set; }

    /// <summary>
    /// InGameゲームでコインを獲得したら変数へ加算するメソッド
    /// </summary>
    public void InGameAddCoin()
    {
        coin += 1;
    }

    /// <summary>
    /// InGameゲームでスコアを獲得したら変数へ加算するメソッド
    /// </summary>
    public void InGameAddScore(int addScore)
    {
        score += addScore;
    }
}
