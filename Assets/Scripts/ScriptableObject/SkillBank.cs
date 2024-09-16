using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Bank", menuName = StringConstants.SO_MENU + "Skill Bank")]
public class SkillBank : AbstractBank<Skill>
{
    public override string GetItemId(Skill item)
    {
        if (item == null || item.basicInfo == null)
        {
            return string.Empty;
        }

        return item.basicInfo.id;
    }

}