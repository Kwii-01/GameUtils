using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Pool;

namespace GameUtils {
    [DefaultExecutionOrder(-5)]
    public class SoundManager : MonoBehaviour {
        [SerializeField] private Audio _audioPF;
        private ObjectPool<Audio> _audioPool;
        private Audio _audioMusic;
        private Dictionary<string, Audio> _audioSourceEffects = new Dictionary<string, Audio>();

        private void Awake() {
            this._audioPool = new ObjectPool<Audio>(this.CreateAudio, this.OnGetAudio, this.OnReleaseAudio, this.OnDestroyAudio, false, 5, 20);
        }

        private Audio CreateAudio() {
            Audio audio = Instantiate(this._audioPF, this.transform);
            audio.Pool = this._audioPool;
            audio.gameObject.SetActive(false);
            return audio;
        }

        private void OnGetAudio(Audio audio) {
            audio.gameObject.SetActive(true);
        }

        private void OnReleaseAudio(Audio audio) {
            audio.Source.Stop();
            var effect = this._audioSourceEffects.FirstOrDefault(v => v.Value == audio);
            if (effect.Key != null) {
                this._audioSourceEffects.Remove(effect.Key);
            }
            audio.gameObject.SetActive(false);
        }

        private void OnDestroyAudio(Audio audio) {
            this.OnReleaseAudio(audio);
            Destroy(audio.gameObject);
        }

        /// <summary>
        /// Play an audioclip as an Effect, will spawn an audiosource if the key is not present
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        /// <param name="key"></param>
        public void PlayEffect(AudioClip clip, float volume = 1f, float pitch = 1f, string key = "") {
            Audio audio;
            if (this._audioSourceEffects.TryGetValue(key, out audio) == false || audio == null) {
                audio = this._audioPool.Get();
                this._audioSourceEffects[key] = audio;
            }
            audio.LifeTime = clip.length;
            audio.Source.volume = volume;
            audio.Source.pitch = pitch;
            audio.Source.PlayOneShot(clip);
        }

        /// <summary>
        /// Play an audioclip as a music, will loop it
        /// </summary>
        /// <param name="audioClip"></param>
        /// <param name="volume"></param>
        public void PlayMusic(AudioClip audioClip, float volume = 0.5f) {
            if (this._audioMusic == null) {
                this._audioMusic = Instantiate(this._audioPF, this.transform);
                this._audioMusic.Timed = false;
                this._audioMusic.name += "MUSIC";
                this._audioMusic.Source.loop = true;
            }
            this._audioMusic.Source.clip = audioClip;
            this._audioMusic.Source.volume = volume;
            this._audioMusic.Source.Play();

        }

        /// <summary>
        /// Pause the music
        /// </summary>
        public void PauseMusic() {
            if (this._audioMusic != null) {
                this._audioMusic.Source.Pause();
            }
        }

        /// <summary>
        /// Resume the music
        /// </summary>
        public void ResumeMusic() {
            if (this._audioMusic != null) {
                this._audioMusic.Source.UnPause();
            }
        }

        /// <summary>
        /// Stop the music from playing
        /// </summary>
        public void StopMusic() {
            if (this._audioMusic != null) {
                this._audioMusic.Source.clip = null;
            }
        }

    }
}