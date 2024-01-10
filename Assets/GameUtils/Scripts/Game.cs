using System.Collections;
using System.Collections.Generic;

using Events;

using UnityEngine;

[DefaultExecutionOrder(-5)]
public class Game : MonoBehaviour {
    public enum State {
        None,
        Home,
        Playing,
        Win,
        Lose
    }

    public State state { get; private set; } = default;

    private void Awake() {
        EventManager.AddListener<State>(this.OnStateChanged);
    }

    private void Start() {
        EventManager.Raise(State.Home);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<State>(this.OnStateChanged);
    }

    private void OnStateChanged(State state) {
        this.state = state;
    }

    public void Win() {
        if (this.state == State.Playing) {
            EventManager.Raise(State.Win);
        }
    }

    public void Lose() {
        if (this.state == State.Playing) {
            EventManager.Raise(State.Lose);
        }
    }
}
