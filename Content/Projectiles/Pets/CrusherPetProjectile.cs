using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Pets
{
	public class CrusherPetProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.CompanionCube);
			AIType = ProjectileID.CompanionCube;
			Projectile.scale = 1f;
			Projectile.width = 26;
			Projectile.height = 26;
			Projectile.tileCollide = true;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.companionCube = false;
			return true;
		}

		int stopTimmer;
		int stopTimmerMax = Utils1.FormatTimeToTick(Second: 0.5f);

        int stopTimmerDelay;
        int stopTimmerDelayMax = Utils1.FormatTimeToTick(Second: 1f);
        public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!player.dead && player.HasBuff(ModContent.BuffType<CrusherPetBuff>()))
			{
				Projectile.timeLeft = 2;
			}

			Vector2 pos = new(Projectile.Center.X, Projectile.Center.Y);
			pos = pos.ToTilePosition();

			if (stopTimmer == 0)
			{
				if (stopTimmerDelay == 0)
				{
					Tile tile = Main.tile[(int)pos.X, (int)pos.Y];

                    if (WorldGen.InWorld((int)pos.X, (int)pos.Y) && tile.HasTile &&  tile.TileType == TileID.Statues)
					{
						Projectile.velocity = new(0, 1);
                        stopTimmer = stopTimmerMax;
                        stopTimmerDelay = stopTimmerDelayMax;
                    }

					
                }
				else
				{
					stopTimmerDelay--;
                }
            }
			else
			{
				Projectile.tileCollide = true;
                Projectile.velocity = new(0,1);
                stopTimmer--;
				return;
            }
		}
	}
}