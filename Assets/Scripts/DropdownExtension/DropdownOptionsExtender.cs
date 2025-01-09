using TMPro;
using UnityEngine;

namespace Assets.Scripts.DropdownExtension
{
    class DropdownOptionsExtender
    {
        public class ColorOptionData : TMP_Dropdown.OptionData
        {
            public ColorOptionData(string text, Color color) : base(text)
            {
                Color = color;
            }

            public Color Color { get; set; }
        }
    }
}
