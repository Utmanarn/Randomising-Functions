using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.DropdownExtension;

public class DropdownInstantiater : MonoBehaviour
{
    private TMP_Dropdown _dropdown;
    
    public List<TMP_Dropdown.OptionData> _dropdownItems { get; private set; }

    private readonly Color defaultColor = new Color(245, 245, 245);
    private readonly Color clickedColor = Color.red; // Temp

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _dropdownItems = new List<TMP_Dropdown.OptionData>();
    }

    private void Start()
    {
        _dropdownItems.Clear();
        _dropdown.ClearOptions();
        _dropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Option A", defaultColor));
        _dropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Option B", defaultColor));
        _dropdown.AddOptions(_dropdownItems);
    }

    // TODO: Send the new dropdownItems from a third party method that changes the colour when the buttons are clicked. This will probably be the "ClickHandler" class.
    // This is probably a question of encapsulation and maintainability of the code vs memory usage. We can either leave the _dropdownItems list publicly accessibly and have it be changed in other classes
    // or have it stay in this class but require a new list to be created based on this list and have the other classes call this function to update the list with the updated list the other class created. This will cause more garbage for the GC.
    private void UpdateDropdownMenu(List<TMP_Dropdown.OptionData> updatedList)
    {
        _dropdownItems.Clear();
        _dropdown.ClearOptions();
        _dropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Option A", defaultColor));
        _dropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Option B", defaultColor));
        _dropdown.AddOptions(_dropdownItems);
    }
}
