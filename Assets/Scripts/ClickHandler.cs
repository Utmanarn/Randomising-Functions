using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    private GameLogic _gameLogic;

    public void Awake()
    {
        _gameLogic = FindFirstObjectByType<GameLogic>(); // The game is only supposed to have one of these loaded. If there are more something has gone very wrong.
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(name + " Game Object Clicked!!!");
    }
}
