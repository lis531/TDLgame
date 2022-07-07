[System.Serializable]
public class PlayerData
{
    public float health;
    public float[] position;
    public float yRotation;
    public bool latarkaOn;
    public bool hasNVGoggles;
    public bool hasKeycard;
    public bool nvGogglesEnabled;
    public bool hasGasMask;
    public bool isCrouching;

    public int medkitCount;
    public int batteryCount;
    public int bezpiecznikCount;

    public float[] enemyPosition;
    public float enemyYRotation;

    public PlayerData(float health, float stamina, float[] position, float yRotation, bool isCrouching, bool latarkaOn, bool hasNVGoggles, bool hasKeycard, bool hasGasMask, int medkitCount, int bezpiecznikCount, int batteryCount, float[] enemyPosition, float enemyYRotation, bool nvGogglesEnabled)
    {
        this.health       = health;
        this.position     = position;
        this.yRotation    = yRotation;
        this.latarkaOn    = latarkaOn;
        this.hasNVGoggles = hasNVGoggles;
        this.hasKeycard   = hasKeycard;
        this.hasGasMask   = hasGasMask;
        this.batteryCount = batteryCount;
        this.bezpiecznikCount = bezpiecznikCount;
        this.medkitCount  = medkitCount;

        this.isCrouching = isCrouching;

        this.enemyPosition  = enemyPosition;
        this.enemyYRotation = enemyYRotation;

        this.nvGogglesEnabled = nvGogglesEnabled;
    }
}
public class SettingsData
{
    public bool fullScreen;
    public int isVsync;
    public int qualityLevel;
    public float[] m_MainVolume;
    public SettingsData(bool fullScreen, int isVsync, int qualityLevel, float[] m_MainVolume)
    {
        this.fullScreen = fullScreen;
        this.isVsync = isVsync;
        this.qualityLevel = qualityLevel;
        this.m_MainVolume = m_MainVolume;
    }
}