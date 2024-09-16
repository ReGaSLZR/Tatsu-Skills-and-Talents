using UnityEngine;

[CreateAssetMenu(fileName = "New Animation States Bank", menuName = StringConstants.SO_MENU + "Animation States Bank")]
public class AnimationStateBank : AbstractBank<string>
{

    public override string GetItemId(string item) => item;

}