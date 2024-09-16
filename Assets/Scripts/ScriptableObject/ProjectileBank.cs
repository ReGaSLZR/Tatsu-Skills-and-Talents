using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Bank", menuName = StringConstants.SO_MENU + "Projectile Bank")]
public class ProjectileBank : AbstractBank<GameObject>
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