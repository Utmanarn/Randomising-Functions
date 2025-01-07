using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private DiceHand _playerHand;
    [SerializeField] private Button _rollButton;

    private void Awake()
    {
        _playerHand = GetComponent<DiceHand>();
    }

    public void OnRoll()
    {
        _rollButton.interactable = false;
    }
}
