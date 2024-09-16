using UnityEngine;

[CreateAssetMenu(fileName = "New Talent Bank", menuName = StringConstants.SO_MENU + "Talent Bank")]
public class TalentBank : AbstractBank<Talent>
{

    public override string GetItemId(Talent item)
    {
        if (item == null || item.basicInfo == null)
        {
            return string.Empty;
        }

        return item.basicInfo.id;
    }

}