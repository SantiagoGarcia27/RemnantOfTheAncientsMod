public class ReaperUpgradeRegister
{
    public int npcType { get; set; }
    public int itemType { get; set; }
    public float Progression { get; set; }
    public SoulCategory Category { get; set; }
    public float MaxValue = 1f;
    public float Increment = 1f;

    public enum SoulCategory
    {
        Stats,
        Summons,
        Immunity,
        Mixed
    }
    public ReaperUpgradeRegister() { }
}