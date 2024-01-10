using System;

using UnityEngine;

namespace GameUtils {
    public abstract class LevelLoaderSO : ScriptableObject {
        public virtual void Initialize() {

        }

        public abstract void LoadLevel(int level, Action<Level> onLevelLoaded);

        public abstract void UnloadLevel(Level level);
    }
}