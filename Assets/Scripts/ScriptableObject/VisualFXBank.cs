namespace ReGaSLZR
{

    using UnityEngine;

    [CreateAssetMenu(fileName = "New VFX Bank",
            menuName = StringConstants.SO_MENU + "VFX Bank")]
    public class VisualFXBank : AbstractBank<GameObject>
    {

        public override string GetItemId(GameObject item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            return item.name;
        }

    }
}