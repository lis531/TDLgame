[System.Serializable]
public class PlayerData
{
    public float health;
    public float[] position;
    public float yRotation;
    public bool latarkaOn;
    public int medkitCount;
    public bool hasNVGoggles;
    public bool hasKeycard;
    public bool nvGogglesEnabled;

    public float[] enemyPosition;
    public float enemyYRotation;

    public PlayerData(float health, float stamina, float[] position, float yRotation, bool latarkaOn, int medkitCount, bool hasNVGoggles, bool hasKeycard, float[] enemyPosition, float enemyYRotation, bool nvGogglesEnabled)
    {
        this.health       = health;
        this.position     = position;
        this.yRotation    = yRotation;
        this.latarkaOn    = latarkaOn;
        this.medkitCount  = medkitCount;
        this.hasNVGoggles = hasNVGoggles;
        this.hasKeycard   = hasKeycard;

        this.enemyPosition  = enemyPosition;
        this.enemyYRotation = enemyYRotation;

        this.nvGogglesEnabled = nvGogglesEnabled;
    }
}
