using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Fargos.Eternity
{

    public class FrostBarrier : ModProjectile
    {
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;
        public override void SetStaticDefaults() {}
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 999999;
            Projectile.light = 1.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = -1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CheckActive(player);

            Projectile.Center = player.Center;
            Point playerPosition = player.position.ToPoint();

            int width = 20;
            int height = 20;
            Rectangle area = new(playerPosition.X - width / 2.ToCoordinatePosition(), playerPosition.Y - height / 2.ToCoordinatePosition(), width.ToCoordinatePosition(), height.ToCoordinatePosition());

            foreach (Projectile projectile in Main.ActiveProjectiles)
            {
                if (projectile.type != this.Type)
                {
                    if (area.Contains(projectile.position.ToPoint()) && (projectile.hostile || !projectile.friendly))
                    {
                        projectile.velocity *= -1;
                        projectile.friendly = true;
                        projectile.hostile = !projectile.friendly;
                        projectile.GetAlpha(Color.Blue);
                    }
                }
            }

            RemnantFargosSoulsPlayer remnantFargosSoulsPlayer = player.GetModPlayer<RemnantFargosSoulsPlayer>();

            if (remnantFargosSoulsPlayer.FrostBarrierCounter > 0)
            {
                remnantFargosSoulsPlayer.FrostBarrierCounter--;
            }

        }
        public void CheckActive(Player player)
        {
            RemnantFargosSoulsPlayer remnantFargosSoulsPlayer = player.GetModPlayer<RemnantFargosSoulsPlayer>();
            if (player.HasBuff<FrostBarrierCouldown>() || remnantFargosSoulsPlayer.FrostBarrierCounter == 0)
            {
                Projectile.Kill();
            }

        }
        public override void OnKill(int timeLeft)
        {
            Main.player[Projectile.owner].opacityForAnimation = 1f;
            Main.player[Projectile.owner].AddBuff(BuffType<FrostBarrierCouldown>(), Utils1.FormatTimeToTick(0, 0, 1, 0) / 3);

            base.OnKill(timeLeft);
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];

            RemnantFargosSoulsPlayer remnantFargosSoulsPlayer = player.GetModPlayer<RemnantFargosSoulsPlayer>();

            player.opacityForAnimation = 0;
            remnantFargosSoulsPlayer.FrostBarrierCounter = Utils1.FormatTimeToTick(0, 0, 0, 10);
            base.OnSpawn(source);
        }
        public float fade = 2.6f;
        public override bool PreDraw(ref Color lightColor)
        {
            var texture = Request<Texture2D>("RemnantOfTheAncientsMod/Content/Projectiles/Fargos/Eternity/FrostBarrier");
            Vector2 origin = new(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
            Main.spriteBatch.Draw((Texture2D)texture, Projectile.Center - Main.screenPosition, null, Color.White, 0f, origin, 1, SpriteEffects.None, 1f);
            return false;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {        
            overPlayers.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);

        }
    }
}
