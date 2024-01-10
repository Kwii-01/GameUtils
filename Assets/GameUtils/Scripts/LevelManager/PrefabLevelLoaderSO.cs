using System;

using UnityEngine;

namespace GameUtils {
    [CreateAssetMenu(menuName = "PrefabLevelLoader")]
    public class PrefabLevelLoaderSO : LevelLoaderSO {
#if UNITY_EDITOR
        [SerializeField] private Level _forceLevel;
#endif
        [SerializeField] private Level[] _levels;

        public override void LoadLevel(int level, Action<Level> onLevelLoaded) {
#if UNITY_EDITOR
            if (this._forceLevel) {
                onLevelLoaded.Invoke(Instantiate(this._forceLevel));
                return;
            }
#endif
            Level lvl = Instantiate(this._levels[level % this._levels.Length]);
            onLevelLoaded.Invoke(lvl);
        }

        public override void UnloadLevel(Level level) {
            Destroy(level.gameObject);
        }
    }
}