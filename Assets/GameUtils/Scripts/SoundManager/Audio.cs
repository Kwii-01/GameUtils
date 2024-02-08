
using UnityEngine;

using UnityEngine.Pool;

namespace GameUtils {
    [RequireComponent(typeof(AudioSource))]
    public class Audio : MonoBehaviour {
        [field: SerializeField] public AudioSource Source { get; private set; } = default;
        public ObjectPool<Audio> Pool { get; set; } = default;
        public float LifeTime { get; set; } = 0;
        public bool Timed { get; set; } = true;

        private void Reset() {
            this.Source = this.GetComponent<AudioSource>();
        }

        private void Update() {
            if (this.Timed == false) {
                return;
            }
            this.LifeTime -= Time.deltaTime;
            if (this.LifeTime <= 0) {
                this.Pool.Release(this);
            }
        }
    }
}