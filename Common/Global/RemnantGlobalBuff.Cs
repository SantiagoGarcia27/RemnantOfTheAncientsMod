using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class RemnantGlobalBuff : GlobalBuff
{
   public List<int> Pygmys = new List<int>()
   {
        ProjectileID.Pygmy,
        ProjectileID.Pygmy2,
        ProjectileID.Pygmy3,
        ProjectileID.Pygmy4,
        ModContent.ProjectileType<PygmyMelee>()
   };
    public override void Update(int type, Player player, ref int buffIndex)
    {
        if (type == BuffID.Pygmies)
        {

            if (DetectMultipleMinionVarian(player, Pygmys))
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
        else
        {
            base.Update(type, player, ref buffIndex);
        }
    }

    public bool DetectMultipleMinionVarian(Player player,List<int> list)
    {
        for(int i  = 0; i < list.Count; i++) 
        {
          if(player.ownedProjectileCounts[list[i]] > 0) return true;
        }
        return false;
    }
}