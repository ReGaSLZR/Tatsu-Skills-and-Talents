[System.Serializable]
public class StatChangeSettings
{

    public TargetSettings targetSettings;
    public ProjectileSettings projectileSettings;

    public StatChangeSettings(TargetSettings targetSettings, 
        ProjectileSettings projectileSettings)
    { 
        this.targetSettings = targetSettings; 
        this.projectileSettings = projectileSettings;
    }

}