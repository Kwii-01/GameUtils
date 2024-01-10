using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Cam : MonoBehaviour {
    [SerializeField] private Camera _camera;
    public Camera Camera => this._camera;


    //TODO: CAM SHAKES using either dotween with simple cam or noises with Cinemachine cam
    public void Shake(float duration, float amplitude, float frequence = 10f) {

    }

    public void MicroShake() => this.Shake(.2f, .4f);
    public void LightShake() => this.Shake(.2f, .4f);
    public void MediumShake() => this.Shake(.2f, .4f);
    public void BigShake() => this.Shake(.2f, .4f);
}
