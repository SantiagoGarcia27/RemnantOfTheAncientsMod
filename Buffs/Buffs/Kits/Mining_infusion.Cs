using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Buffs.Buffs.Kits
{
    public class Mining_infusion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining infusion");
            Description.SetDefault("");

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.findTreasure = true;
            player.nightVision = true;
            player.pickSpeed -= 0.25f;
            Lighting.AddLight((int)((double)player.position.X + player.width / 2) / 16, (int)(player.position.Y + (double)(player.height / 2)) / 16, 1f, 1f, 2f);//16,3f,3f,3f
            player.buffImmune[11] = true;
            player.buffImmune[12] = true;
            player.buffImmune[104] = true;
            player.buffImmune[9] = true;

        }
    }
}
  