using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bee : MonoBehaviour
{
    private const string _ANIMATOR_STATE = "IsIdle";

    private const float _MIN_FORCE = 0.0f;
    private const float _MAX_FORCE = 20.0f;
    private const float _DELTA_FORCE = 1.5f;

    private const float _ROTATION = 2.0f;

    private readonly Vector2 _startPosition = new Vector2(-4.5f, 0.0f);

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _enemyEffectPrefab;

    private Transform _transform;

    private float _force;

    private GameState _gameState;

    public Vector3 Position { get; private set; }

    public event Action PlayEvent;
    public event Action DefeatEvent;

    private void Update()
    {
        if (_gameState == GameState.Level)
        {
            if (Keyboard.current.spaceKey.isPressed || Mouse.current.leftButton.isPressed)
            {
                _force += _DELTA_FORCE;
                _force = Mathf.Clamp(_force, _MIN_FORCE, _MAX_FORCE);
            }
            else
            {
                _force = _MIN_FORCE;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_gameState == GameState.Level)
        {
            Position = _transform.position;

            _rigidbody.AddForce(Vector2.up * _force);
            _transform.rotation = Quaternion.Euler(0.0f, 0.0f, _rigidbody.velocity.y * _ROTATION);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;

        if (other.CompareTag(Tags.SURFACE))
        {
            SetDefeatState();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.ENEMY))
        {
            SetDefeatState();
        }
    }

    public void Initialize()
    {
        _transform = transform;
    }

    public void SetGameState(GameState state)
    {
        _gameState = state;

        if (_gameState == GameState.Menu)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0.0f;

            _animator.SetBool(_ANIMATOR_STATE, true);

            _particleSystem.Clear();
            _particleSystem.Stop();
        }
        else
        {
            _rigidbody.isKinematic = false;
            _animator.SetBool(_ANIMATOR_STATE, false);
            _particleSystem.Play();

            PlayEvent?.Invoke();
        }

        _rigidbody.MovePosition(_startPosition);
        _transform.rotation = Quaternion.identity;
    }

    private void SetDefeatState()
    {
        Instantiate(_enemyEffectPrefab, _transform.position, Quaternion.identity);
        DefeatEvent?.Invoke();
    }
}