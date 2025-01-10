using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.DropdownExtension.DropdownOptionsExtender;

namespace Assets.Scripts.DropdownExtension
{
    class ColorDropdown : TMP_Dropdown
    {
        private const int BackgroundItemIndex = 0;

        private int _dataIndex = 0;

        protected override GameObject CreateDropdownList(GameObject template)
        {
            _dataIndex = 0;
            return base.CreateDropdownList(template);
        }

        protected override DropdownItem CreateItem(DropdownItem itemTemplate)
        {
            var item = base.CreateItem(itemTemplate);
            var backgroundTemplate = item.transform.GetChild(BackgroundItemIndex);
            var image = backgroundTemplate.GetComponent<Image>();

            OptionData data = this.options[_dataIndex];

            if (data is ColorOptionData colorOptionData)
            {
                image.color = colorOptionData.Color;
                item.toggle.interactable = colorOptionData.Interactable;
            }
            else
            {
                image.color = Color.green;
            }

            _dataIndex++;
            return item;
        }
    }
}
