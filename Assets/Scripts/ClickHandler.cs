using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private DropdownInstantiater _dropdownInstantiater;
    private GameLogic _gameLogic;
    private Toggle _toggle;
    private int _listIndex = -1; // Make sure we throw an error in case something goes wrong.

    private bool _hasBeenClicked = false;

    private readonly char[] _listOfNumbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

    public void Awake()
    {
        _gameLogic = FindFirstObjectByType<GameLogic>(); // The game is only supposed to have one of these loaded. If there are more something has gone very wrong.
        _toggle = GetComponent<Toggle>();
    }

    public void Start()
    {
        string value = name;

        if (value == null)
        { 
            Debug.LogError($"Failed to set index for {0}", gameObject);
            return;
        }

        int index = value.IndexOfAny(_listOfNumbers);

        if (index == -1)
        {
            Debug.LogError($"Failed to find item number index for {0}", gameObject);
            return;
        }

        int endIndex = value.IndexOf(':');

        if (endIndex == -1)
        {
            Debug.LogError($"Failed to find space index for {0}", gameObject);
            return;
        }

        int numberLength = endIndex - index;

        string listIndex = value.Substring(index, numberLength);

        int.TryParse(listIndex, out _listIndex);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_toggle.interactable) return;

        _dropdownInstantiater.UpdateDropdownMenu(_listIndex);

        _gameLogic.SelectScore(_listIndex);
    }
}
