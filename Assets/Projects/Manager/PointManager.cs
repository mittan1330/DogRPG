using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// ポイント保存に関するクラス
    /// </summary>
    public class PointManager : MonoBehaviour
    {
        public static int LoadData(string dataId)
        {
            return PlayerPrefs.GetInt(dataId, 0);
        }

        private static void SaveData(string dataId, int value)
        {
            PlayerPrefs.SetInt(dataId, value);
        }
    }
}
