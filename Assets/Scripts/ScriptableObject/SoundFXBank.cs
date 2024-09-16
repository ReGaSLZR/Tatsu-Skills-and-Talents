using UnityEngine;

[CreateAssetMenu(fileName = "New SFX Bank", menuName = StringConstants.SO_MENU + "SFX Bank")]
public class SoundFXBank : AbstractBank<AudioClip>
{

    public override string GetItemId(AudioClip item)
    {
        if (item == null)
        {
            return string.Empty;
        }
        return item.name;
    }

}