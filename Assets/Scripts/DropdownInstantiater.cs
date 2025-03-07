using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.DropdownExtension;

public class DropdownInstantiater : MonoBehaviour
{
    private TMP_Dropdown _dropdown;
    
    public List<TMP_Dropdown.OptionData> DropdownItems { get; private set; }

    private readonly Color defaultColor = new Color(245, 245, 245);
    private readonly Color clickedColor = Color.red; // Temp

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        DropdownItems = new List<TMP_Dropdown.OptionData>();
    }

    private void Start()
    {
        DropdownItems.Clear();
        _dropdown.ClearOptions();
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Ones", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Twos", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Threes", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Fours", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Fives", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Sixes", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("One Pair", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Two Pairs", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Three of a kind", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Four of a kind", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Small Straight", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Large Straight", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Full House", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Chance", defaultColor));
        DropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Yatzy", defaultColor));
        _dropdown.AddOptions(DropdownItems);
    }

    // TODO: Send the new dropdownItems from a third party method that changes the colour when the buttons are clicked. This will probably be the "ClickHandler" class.
    // This is probably a question of encapsulation and maintainability of the code vs memory usage. We can either leave the _dropdownItems list publicly accessibly and have it be changed
    // in other classes or have it stay in this class but require a new list to be created based on this list and have the other classes call this function to update the list with the
    //updated list the other class created. This will cause more garbage for the GC.
    public void UpdateDropdownMenu(int index)
    {
        DropdownItems[index] = new DropdownOptionsExtender.ColorOptionData(DropdownItems[index].text, clickedColor, false);

        _dropdown.ClearOptions();
        _dropdown.AddOptions(DropdownItems);
    }
}
