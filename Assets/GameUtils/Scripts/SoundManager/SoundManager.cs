using System.Collections.Generic;

using UnityEngine;

namespace GameUtils {
    [DefaultExecutionOrder(-5)]
    public class SoundManager : MonoBehaviour {
        private static readonly float MIN_TIME_BEFORE_KILL = 10f;

        [SerializeField] private Audio _audioPF;
        private Audio _audioMusic;
        private Dictionary<string, Audio> _audioSourceEffects = new Dictionary<string, Audio>();

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
                audio = Instantiate(this._audioPF, this.transform);
                audio.name += key;
                this._audioSourceEffects[key] = audio;
            }
            audio.LifeTime = clip.length + MIN_TIME_BEFORE_KILL;
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
                this._audioMusic.LifeTime = float.MaxValue;
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