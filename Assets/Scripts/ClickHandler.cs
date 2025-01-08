using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    private GameLogic _gameLogic;
    private Toggle _toggle;

    public void Awake()
    {
        _gameLogic = FindFirstObjectByType<GameLogic>(); // The game is only supposed to have one of these loaded. If there are more something has gone very wrong.
        _toggle = GetComponent<Toggle>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _toggle.interactable = false;
        ColorBlock modifiedColorBlock = _toggle.colors;

        modifiedColorBlock.normalColor = Color.red;

        _toggle.colors = modifiedColorBlock;

        _gameLogic.SelectScore(name);
    }
}
