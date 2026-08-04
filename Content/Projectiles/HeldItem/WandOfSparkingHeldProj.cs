using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class WandOfSparkingHeldProj : HeldCyrcleModel
	{
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;
      
        public override void DrawCirclee(ref List<MagicCircle> internalCircle, ref List<MagicCircle> externalCircle)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_2");
            Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_2");

            int BaseWeapon = (int)Projectile.ai[2];
            Color color1 = BaseWeapon == ItemID.WandofSparking ? Color.DarkRed : Color.DarkCyan;
            Color color2 = BaseWeapon == ItemID.WandofSparking ? Color.Red : Color.Cyan;

            MagicCircle circle1 = new(texture, color1, 1.2f);
            MagicCircle circle2 = new(texture2, color2, 1.2f);

            internalCircle.Add(circle1);
            externalCircle.Add(circle2);
            base.DrawCirclee(ref internalCircle, ref externalCircle);
        }
        public override void AI()
        {
            if(Main.LocalPlayer.whoAmI != Projectile.owner)
            {
                base.AI();
                return;
            }

            float maxCharge = GetMaxCharge();
            if (Charge < maxCharge)
            {
                Player player = Main.player[Projectile.owner];
                int spawnDistanceX = 10 * player.direction;
                int spawnDistanceY = 0;
                       
                Vector2 start = player.Center + new Vector2(spawnDistanceX, spawnDistanceY).ToCoordenatePosition();

                if (DistanceUtils.TryFindGround(start, 100, out Point groundTile))
                {
                    Vector2 impactPos = groundTile.ToWorldCoordinates(8, 0);
                    int dustID = (int)Projectile.ai[2] == ItemID.WandofSparking ? DustID.Torch : DustID.IceTorch;
                    List<Dust> dusts =
                    [
                        Dust.NewDustPerfect(impactPos, dustID, new Vector2(0, -19), 0, Color.White, 1.5f),
                        Dust.NewDustPerfect(impactPos - new Vector2(1,0), dustID, new Vector2(0, -10), 0, Color.Orange, 1.5f),
                        Dust.NewDustPerfect(impactPos + new Vector2(1, 0), dustID, new Vector2(0, -10), 0, Color.Orange, 1.5f),
                        Dust.NewDustPerfect(impactPos - new Vector2(2,0), dustID, new Vector2(0, -5), 0, Color.Red, 1.5f),
                        Dust.NewDustPerfect(impactPos + new Vector2(2, 0), dustID, new Vector2(0, -5), 0, Color.Red, 1.5f),
                    ];

                    foreach (Dust dust in dusts)
                    {
                        dust.noGravity = true;
                    }
                }
            }
            base.AI();
        }
        public override void ShootEffect(ref bool killAfterEnd)
        {
            Player player = Main.player[Projectile.owner];
            int itemBase = (int)Projectile.ai[2];
            int spawnDistanceX = -10 * player.direction;
            int spawnDistanceY = 100;
            Vector2 spawnDistance = new Vector2(spawnDistanceX, spawnDistanceY);
            Vector2 position = player.position - DistanceUtils.ToCoordenatePosition(spawnDistance);

            Vector2 velocity = new Vector2(0, 8);
            int projectileType = ModContent.ProjectileType<SparkBomb>();
            int damage = (int)(player.HeldItem.damage * 3);
            float knockBack = player.HeldItem.knockBack;
            float scale = 8;

            int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), position, velocity, projectileType, damage, knockBack, player.whoAmI,ai1: scale, ai2: itemBase);
            Main.projectile[p].GetAlpha(Color.Red);

            base.ShootEffect(ref killAfterEnd);
        }

      

    }
}
