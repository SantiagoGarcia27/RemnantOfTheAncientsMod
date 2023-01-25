using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Buffs;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using RemnantOfTheAncientsMod.Dusts;

namespace RemnantOfTheAncientsMod.Projectiles.Minioms
{
	public class DesertMinion : ModProjectile
	{
        public float dust;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Baby Desert Aniquilator Minion");
			Main.projFrames[Projectile.type] = 6;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			//ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
			//AIType = ProjectileID.BabySlime;
		}

		public sealed override void SetDefaults()
		{
			Projectile.width = 26;
			Projectile.height = 26;
			Projectile.netImportant = true;
			Projectile.friendly = true;
			Projectile.minionSlots = 0.5f;
			Projectile.alpha = 75;
			Projectile.aiStyle = 26;
			Projectile.timeLeft = 18000;
			Projectile.penetrate = -1;
			Projectile.timeLeft *= 5;
			Projectile.minion = true;
			AIType = 266;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 7;//23
		}


		public override bool? CanCutTiles()
		{
			return false;
		}

		public override bool MinionContactDamage()
		{
			return true;
		}

        /*	public override void AI()
            {
                Player player = Main.player[Projectile.owner];

                #region Active check
                // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
                if (player.dead || !player.active)
                {
                    player.ClearBuff(BuffType<DesertMinionBuff>());
                }
                if (player.HasBuff(BuffType<DesertMinionBuff>()))
                {
                    Projectile.timeLeft = 2;
                }
                #endregion
            }*/

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Player1 Modplayer = player.RemnantOfTheAncientsMod();
            if (dust == 0f)
            {
                //Projectile.spawnedPlayerMinionDamageValue = val.MinionDamage();
               // Projectile.spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
                int num = 16;
                for (int i = 0; i < num; i++)
                {
                    Vector2 vector = Utils.RotatedBy(Vector2.Normalize(Projectile.velocity) * new Vector2(Projectile.width / 2f, Projectile.height) * 0.75f, (double)((i - (num / 2 - 1)) * ((float)Math.PI * 2f) / num), default) + Projectile.Center;
                    Vector2 vector2 = vector - Projectile.Center;
                    int dust = Dust.NewDust(vector + vector2, 0, 0, DustType<QuemaduraA>() , vector2.X * 1f, vector2.Y * 1f, 100, default, 1.1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity = vector2;
                }
                dust += 1f;
            }
            /* if (player.MinionDamage() != Projectile.Calamity().spawnedPlayerMinionDamageValue)
             {
                 int damage = (int)((float)Projectile.Calamity().spawnedPlayerMinionProjectileDamageValue / Projectile.Calamity().spawnedPlayerMinionDamageValue * val.MinionDamage());
                 Projectile.damage = damage;
             }*/
            player.AddBuff(BuffType<DesertMinionBuff>(), 3600, true);
            bool Minionlive = Projectile.type == ProjectileType<DesertMinion>();
            
            if (!Modplayer.DesertMinion)
            {
                Projectile.active = false;
            }
            else if (Minionlive)
            {
                //if (player.dead || !player.active)
                //{
                //    player.ClearBuff(BuffType<DesertMinionBuff>());
                //}
                //if (player.HasBuff(BuffType<DesertMinionBuff>()))
                //{
                //    Projectile.timeLeft = 2;
                //}

                if (player.dead)
                {
                    Modplayer.DesertMinion = false;
                }
                if (Modplayer.DesertMinion)
                {
                    Projectile.timeLeft = 2;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextBool())
			{
				target.AddBuff(BuffType<Burning_Sand>(), 300);
			}
		}
	}
}