using UnityEngine;

public class RippleButtons : MonoBehaviour
{
    private Transform _transform;
    private float _pulseSpeed = 0.2f;
    private float _minPulse = 1f;
    private float _maxPulse = 1.3f;
    private float _pulse;

    private void Start() => _transform = gameObject.transform;

    private void LateUpdate() => StartRipple();

    private void StartRipple()
    {
        _pulse = Mathf.PingPong(Time.time * _pulseSpeed, _maxPulse - _minPulse) + _minPulse;
        _transform.localScale = new Vector3(_pulse, _pulse, 1);
    }
}
