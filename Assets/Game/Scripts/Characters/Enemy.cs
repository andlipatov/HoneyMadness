using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject _enemyEffectPrefab;

    protected Transform _transform;

    protected Bee _bee;

    protected Vector2 _position;

    protected float _positionY;

    protected float _moveVelocity;

    public virtual void Initialize(Bee bee)
    {
        _bee = bee;
        _bee.DefeatEvent += HandleDefeat;

        _transform = transform;
    }

    public virtual void Initialize(Bee bee, Vector2 position)
    {
        Initialize(bee);

        _position = position;
        _transform.position = _position;
    }

    protected virtual void SetDestroyState()
    {
        _bee.DefeatEvent -= HandleDefeat;

        Instantiate(_enemyEffectPrefab, _transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    protected virtual void HandleDefeat()
    {
        SetDestroyState();
    }
}