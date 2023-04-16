using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{
	public class GodClaws : ModProjectile
	{
		public int FrameCount = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("God Slayer Slash"); //projectile name
			Main.projFrames[Projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			Projectile.width = 106;       //projectile width
			Projectile.height = 50;  //projectile height
			Projectile.friendly = true;      //make that the projectile will not damage you
			Projectile.DamageType = DamageClass.Melee;          // 
			Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
			Projectile.penetrate = 100;      //how many NPC will penetrate
			Projectile.timeLeft = 100000; //how many time this projectile has before disepire
			Projectile.light = 1.75f;    // projectile light
			Projectile.scale = 2f;
			Projectile.extraUpdates = 1;
			Projectile.ignoreWater = true;
			AIType = ProjectileID.TerraBeam;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown= 1;
		}
		public override void AI()
		{
			Projectile.direction = Main.player[Main.myPlayer].direction;
			Projectile.spriteDirection = Projectile.direction;
			AnimateTexture();

        }
        public void AnimateTexture()
        {
			switch (FrameCount)
			{
				case 0:
                    Projectile.frame = 0;
					FrameCount++;
					break;
				case 10:
                    Projectile.frame = 1;
					FrameCount++;

                    break;
				case 20:
                    Projectile.frame = 2;
					FrameCount++;
                    break;
				case 30:
                    Projectile.frame = 3;
					FrameCount++;
                    break;
				case 40:
					FrameCount = 0;
					Projectile.Kill();
                    break; 
				default:
					FrameCount++;
                    break;


            }

    //        if (++Projectile.frameCounter >= 4)
    //        {
    //            Projectile.frameCounter = 0;
				//if (++Projectile.frame >= Main.projFrames[Projectile.type])
				//{
				//	Projectile.frame = 0;
				//	Projectile.Kill();
				//}
    //        }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[Projectile.owner] = 0;

			if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
			{
				if (CalamityMod.TryFind("GodSlayerInferno", out ModProjectile GodSlayerInferno)) target.AddBuff(GodSlayerInferno.Type, 400);
				if (CalamityMod.TryFind("GodSlayerInferno", out ModProjectile Marked)) target.AddBuff(Marked.Type, 400);
			}
			target.AddBuff(BuffID.OnFire, 1080);
			target.AddBuff(BuffID.BrokenArmor, 1080);
			target.AddBuff(BuffID.Slow, 1080);
			target.AddBuff(BuffID.CursedInferno, 1080);
			target.AddBuff(BuffID.Ichor, 1080);
		}
	}
}
