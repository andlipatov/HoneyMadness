using UnityEngine;
using UnityEngine.UIElements;

public class Fish : Enemy
{
    private const float _START_POSITION_Y = -6.0f;
    private const float _END_POSITION_Y = -4.2f;

    private const float _MOVE_Y_DISTANCE = _END_POSITION_Y - _START_POSITION_Y;

    private const float _MIN_MOVE_Y_VELOCITY = 0.4f;
    private const float _MAX_MOVE_Y_VELOCITY = 0.8f;

    private const float _MIN_JUMP_DIRECTION_X = 0.5f;
    private const float _MAX_JUMP_DIRECTION_X = 1.0f;

    private const float _MIN_FORCE = 400.0f;
    private const float _MAX_FORCE = 600.0f;

    [SerializeField] private Rigidbody2D _rigidbody;

    private float _time;

    private float _force;

    private State _state;

    private enum State
    {
        Move,
        Jump
    }

    private void Update()
    {
        if (_state == State.Move)
        {
            _time += Time.deltaTime;

            float distance = _time * _moveVelocity;
            float t = distance / _MOVE_Y_DISTANCE;
            _positionY = Mathf.Lerp(_START_POSITION_Y, _END_POSITION_Y, t);

            _position += Vector2.left * Level.MOVE_X_VELOCITY * Time.deltaTime;
            _position = new Vector2(_position.x, _positionY);

            _transform.position = _position;

            if (t >= 1.0f)
            {
                _state = State.Jump;
                
                float jumpDirectionX = Random.Range(_MIN_JUMP_DIRECTION_X, _MAX_JUMP_DIRECTION_X);
                Vector3 jumpDirection = new Vector2(-jumpDirectionX, 1.0f);

                _force = Random.Range(_MIN_FORCE, _MAX_FORCE);
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(jumpDirection * _force, ForceMode2D.Force);
            }
        }
        else
        {
            _position += Vector2.left * Level.MOVE_X_VELOCITY * Time.deltaTime;
        }
        
        if (_position.x <= -LevelSpawner.SPAWN_X)
        {
            SetDestroyState();
        }
    }

    public override void Initialize(Bee bee)
    {
        base.Initialize(bee);

        _position = new Vector2(LevelSpawner.SPAWN_X, _START_POSITION_Y);
        _transform.position = _position;

        _moveVelocity = Random.Range(_MIN_MOVE_Y_VELOCITY, _MAX_MOVE_Y_VELOCITY);

        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0.0f;

        _positionY = 0.0f;
        _time = 0.0f;
        _state = State.Move;
    }
}