using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.DropdownExtension;

public class DropdownInstantiater : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    private List<TMP_Dropdown.OptionData> _dropdownItems;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _dropdownItems = new List<TMP_Dropdown.OptionData>();
    }

    private void Start()
    {
        _dropdown.ClearOptions();
        _dropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Option A", Color.green));
        _dropdownItems.Add(new DropdownOptionsExtender.ColorOptionData("Option B", Color.red));
        _dropdown.AddOptions(_dropdownItems);
    }
}
