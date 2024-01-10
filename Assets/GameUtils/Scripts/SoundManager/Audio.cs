
using UnityEngine;

namespace GameUtils {
    [RequireComponent(typeof(AudioSource))]
    public class Audio : MonoBehaviour {
        [field: SerializeField] public AudioSource Source { get; private set; } = null;
        public float LifeTime { get; set; } = 0;

        private void Reset() {
            this.Source = this.GetComponent<AudioSource>();
        }

        private void Update() {
            this.LifeTime -= Time.deltaTime;
            if (this.LifeTime <= 0) {
                Destroy(this.gameObject);
            }
        }
    }
}