using System.Collections;
using System.Collections.Generic;

using GameUtils;

using UnityEngine;

[DefaultExecutionOrder(-20)]
public class Managers : MonoBehaviour {
    private static Managers Instance = default;

    public static DataManager dataManager { get; private set; } = default;
    public static LevelManager levelManager { get; private set; } = default;
    public static SoundManager soundManager { get; private set; } = default;
    public static Game game { get; private set; } = default;
    public static Cam cam { get; private set; } = default;

    [SerializeField] private DataManager _dataManager;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private Game _game;
    [SerializeField] private Cam _cam;

    private void Awake() {
        if (Instance != default) {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        dataManager = this._dataManager;
        levelManager = this._levelManager;
        soundManager = this._soundManager;
        game = this._game;
        cam = this._cam;
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = default;
        }
    }
}
