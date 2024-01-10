
using Events;

using UnityEngine;

namespace GameUtils {
    [DefaultExecutionOrder(-5)]
    public class LevelManager : MonoBehaviour {
        public static Level Level { get; private set; } = null;

        [SerializeField] private LevelLoaderSO _loaderSO;


        private int _currentLevel;

        private void Awake() {
            EventManager.AddListener<Game.State>(this.OnStateChanged);
            this._currentLevel = Managers.dataManager.GetLevel();
            this._loaderSO.Initialize();
        }

        private void OnDestroy() {
            EventManager.RemoveListener<Game.State>(this.OnStateChanged);
        }

        private void OnDisable() {
            this.SaveLevel();
        }

        private void OnStateChanged(Game.State state) {
            if (state == Game.State.Home) {
                this.ResetLevel();
                this.LoadLevel();
            } else if (state == Game.State.Win) {
                ++this._currentLevel;
                this.SaveLevel();
            } else if (state == Game.State.Lose) {

            }
        }

        private void ResetLevel() {
            if (Level != null) {
                this._loaderSO?.UnloadLevel(Level);
                Level = null;
            }
        }

        private void SaveLevel() {
            Managers.dataManager.SetLevel(this._currentLevel);
        }

        private void LoadLevel() {
            this._loaderSO?.LoadLevel(this._currentLevel, this.OnLevelLoaded);
        }

        private void OnLevelLoaded(Level level) {
            Level = level;
            EventManager.Raise(Level);
        }
    }
}