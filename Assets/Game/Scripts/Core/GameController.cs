using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Level _level;

    private Honey _honey;

    private void Awake()
    {
        _honey = new Honey();

        _menu.Initialize(this);
        _level.Initialize(this);

        SetGameState(GameState.Menu);
    }

    public void SetGameState(GameState state)
    {
        _menu.SetGameState(state);
        _level.SetGameState(state);
    }

    public void Subscribe(Bee bee)
    {
        _menu.Subscribe(_honey);
        bee.DefeatEvent += HandleDefeat;
    }

    public void Unsubscribe(Bee bee)
    {
        _menu.Unsubscribe(_honey);
        bee.DefeatEvent -= HandleDefeat;
    }

    public void HoneyIncrement(int amount)
    {
        _honey.Increment(amount);
    }

    private void HandleDefeat()
    {
        _honey.Restore();

        SetGameState(GameState.Menu);
    }
}