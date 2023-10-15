using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 _movementVector;
    [SerializeField, Range (0,1)] private float _movementFactor;
    [SerializeField] private float _period=0.5f;
    
    private Vector3 _startingPosition;
    
    void Start()
    {
        _startingPosition = transform.position;
    }

    void Update()
    {
        Oscillate();
    }

    private void Oscillate()
    {
        if (_period <= Mathf.Epsilon)
            return;

        var cycle = Time.time / _period;
        var sinWave = Mathf.Sin(cycle);
        _movementFactor = (sinWave + 1f) / 2f;

        var offset = _movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}
