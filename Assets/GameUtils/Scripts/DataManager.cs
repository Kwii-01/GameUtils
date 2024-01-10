using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameUtils {
    [DefaultExecutionOrder(-10)]
    public class DataManager : MonoBehaviour {
        private static readonly string LEVEL = "LEVEL_INDEX";


        public int GetLevel() {
            return PlayerPrefs.GetInt(LEVEL, 0);
        }

        public void SetLevel(int level) {
            PlayerPrefs.SetInt(LEVEL, level);
        }

    }
}