using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using SavingSystem;
namespace GameUtils {
    public class DataManager : ADatamanager {
        private static readonly string LEVEL = "LEVEL_INDEX";


        public int GetLevel() {
            return this.saver.LoadInt(LEVEL, 0);
        }

        public void SetLevel(int level) {
            this.saver.SaveInt(LEVEL, level);
        }

    }
}