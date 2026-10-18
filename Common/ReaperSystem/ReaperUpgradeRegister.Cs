using Terraria;
using Terraria.ModLoader;

public class ReaperUpgradeRegister
{
    public int npcType { get; set; }
    public int itemType { get; set; }
    public float Progression { get; set; }
    public SoulCategory Category { get; set; }
    public float MaxValue = 1f;
    public float Increment = 1f;

    public Mod mod
    {
        get
        {
            Mod _mod = npc?.ModNPC?.Mod ?? RemnantOfTheAncientsMod.RemnantOfTheAncientsMod.Terraria;
            return _mod;
        }
    }

    public NPC npc
    {
        get
        {
            NPC npc = new();
            npc.SetDefaults(npcType);
            return npc;
        }
    }

    public enum SoulCategory
    {
        Stats,
        Summons,
        Immunity,
        Mixed
    }
    public ReaperUpgradeRegister() { }
}