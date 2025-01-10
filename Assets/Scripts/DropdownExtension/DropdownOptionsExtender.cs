using TMPro;
using UnityEngine;

namespace Assets.Scripts.DropdownExtension
{
    class DropdownOptionsExtender
    {
        public class ColorOptionData : TMP_Dropdown.OptionData
        {
            public bool Interactable { get; set; }

            public Color Color { get; set; }

            public ColorOptionData(string text, Color color, bool interactable = true) : base(text)
            {
                Color = color;
                Interactable = interactable;
            }
        }
    }
}
