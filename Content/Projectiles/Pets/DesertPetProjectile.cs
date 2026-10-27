using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Pets
{
	public class DesertPetProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 12;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.KingSlimePet);
			Projectile.height = 30;
			Projectile.width = 30;
			AIType = ProjectileID.KingSlimePet;
			Projectile.scale = 1f;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.petFlagKingSlimePet = false;
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!player.dead && player.HasBuff(ModContent.BuffType<DesertPetBuff>()))
			{
				Projectile.timeLeft = 2;
			}



			if (Main.dedServ)
				return;

			foreach (Gore gore in Main.gore)
			{
				if (gore.active &&
					gore.type == GoreID.KingSlimePetCrown &&
					gore.timeLeft >= Gore.goreTime - 2 &&
					Vector2.DistanceSquared(
						gore.position,
						Projectile.Center
					) < 6400f)
				{
					gore.active = false;
				}
			}

		}
	}
}