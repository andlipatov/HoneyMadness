using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    private const string _HONEY_TEXT = "0";

    [SerializeField] private TMP_Text _honeyText;
    [SerializeField] private TMP_Text _inputText;

    private GameController _gameController;

    private GameState _gameState;

    private void Update()
    {
        if (_gameState == GameState.Menu)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
            {
                _gameController.SetGameState(GameState.Level);
            }
        }
    }

    public void Initialize(GameController gameController)
    {
        _gameController = gameController;
    }

    public void Subscribe(Honey honey)
    {
        honey.HoneyChangedEvent += HandleHoneyChanged;
    }

    public void Unsubscribe(Honey honey)
    {
        honey.HoneyChangedEvent -= HandleHoneyChanged;
    }

    public void SetGameState(GameState gameState)
    {
        _gameState = gameState;

        _honeyText.text = _HONEY_TEXT;
        _inputText.enabled = _gameState == GameState.Menu;
    }

    public void HandleHoneyChanged(int amount)
    {
        _honeyText.text = amount.ToString();
    }
}