using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

public class RemnantGlobalBossbar : GlobalBossBar
{
    public override bool PreDraw(SpriteBatch spriteBatch,NPC npc, ref BossBarDrawParams drawParams) => npc.GetGlobalNPC<RemnantGlobalNPC>().ShouldDrawBossBar;
}