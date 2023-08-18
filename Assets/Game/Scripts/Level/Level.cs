using UnityEngine;

public class Level : MonoBehaviour
{
    public const float MOVE_X_VELOCITY = 4.0f;

    [SerializeField] private LevelMovement _levelMovement;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private Bee _bee;
    [SerializeField] private Transform _woodsTransform;
    [SerializeField] private Transform _charactersTransform;

    private GameController _gameController;

    private void OnDestroy()
    {
        _gameController.Unsubscribe(_bee);
    }

    public void Initialize(GameController gameController)
    {
        _gameController = gameController;

        _levelSpawner.Initialize(_gameController, _bee, _woodsTransform, _charactersTransform);
        _bee.Initialize();

        _gameController.Subscribe(_bee);
    }

    public void SetGameState(GameState state)
    {
        _levelMovement.SetGameState(state);
        _levelSpawner.SetGameState(state);
        _bee.SetGameState(state);
    }
}