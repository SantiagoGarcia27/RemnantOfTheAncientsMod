using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SangarUtilities.Common;
using SangarUtilities.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
    [JITWhenModsEnabled("CalamityMod")]
    public class DevourerOfGodsArmor : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
        }
        public override string Texture => GetTexturePath();
        public override void SetStaticDefaults()
        {
        } 
        public override void SetDefaults()
        {
            NPC.width = 104;
            NPC.height = 104;
            NPC.lifeMax = 10000;
            NPC.defDefense = 90;
            NPC.damage = 1;  
            NPC.friendly = false;
            NPC.boss = false;
            NPC.scale = 1f;
            NPC.value = 0;
            NPC.aiStyle = -1;
        }
        public override void AI()
        {
            NPC NpcOwner = Main.npc[(int)NPC.localAI[2]];
            if (NpcOwner != null)
            {
                NPC.friendly = NpcOwner.friendly;
                NPC.Center = NpcOwner.Center;
                NPC.rotation = NpcOwner.rotation;
                NPC.spriteDirection = NpcOwner.spriteDirection;
                NPC.scale = NpcOwner.scale + 0.2f;
                NPC.Size = NpcOwner.Size + new Vector2(10,10);
            }
   
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
          
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(GetTexturePath());
            Vector2 drawPosition = NPC.Center - screenPos;
            Vector2 halfSizeTexture = new Vector2(texture.Width() / 2, texture.Height() / 2);
            drawPosition -= new Vector2(texture.Width(), texture.Height()) * NPC.scale / 2f;



            drawPosition += halfSizeTexture * NPC.scale + new Vector2(0f, NPC.gfxOffY);
            spriteBatch.Draw((Texture2D)texture, drawPosition, null, NPC.GetAlpha(drawColor), NPC.rotation, halfSizeTexture, NPC.scale, NPC.spriteDirection == 1? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(GetTexturePath()+"_Glow");
            Vector2 drawPosition = NPC.Center - screenPos;
            Vector2 halfSizeTexture = new Vector2(texture.Width() / 2, texture.Height() / 2);
            drawPosition -= new Vector2(texture.Width(), texture.Height()) * NPC.scale / 2f;

            drawPosition += halfSizeTexture * NPC.scale + new Vector2(0f, NPC.gfxOffY);

            Color color = Utils.MultiplyRGBA(new Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0), Color.SkyBlue);
            SpriteEffects effect = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw((Texture2D)texture, drawPosition, null, color, NPC.rotation, halfSizeTexture, NPC.scale, effect, 0f);

            base.PostDraw(spriteBatch, screenPos, drawColor);
        }
        public override bool CheckActive()
        {
            return false;
        }

        public string GetTexturePath()
        {
            NPC NpcOwner = Main.npc[(int)NPC.localAI[2]];
            if (!Main.dedServ)
            {
                if (NpcOwner.type == CallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DevourerofGodsHead"))
                {
                    return "RemnantOfTheAncientsMod/Content/NPCs/Bosses/Calamity/DOGShield_Phase1_Head";
                }
                if (NpcOwner.type == CallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DevourerofGodsBody"))
                { 
                    return "RemnantOfTheAncientsMod/Content/NPCs/Bosses/Calamity/DOGShield_Phase1_Body";
                }
                if (NpcOwner.type == CallUtils.GetNpcFromMod(RemnantOfTheAncientsMod.CalamityMod, "DevourerofGodsTail"))
                {         
                    return "RemnantOfTheAncientsMod/Content/NPCs/Bosses/Calamity/DOGShield_Phase1_Tail";
                }
            }
            return "RemnantOfTheAncientsMod/Content/NPCs/Bosses/Calamity/DOGShield_Phase1_Head"; ;
        }
    }
}
