using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using opswordsII.Items.Summon.Saplings;
using opswordsII.Buffs.acorns;

namespace opswordsII.Items.Summon.Saplings
{
	public class SaplingMinion : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Animated Sapling Minion");
			
				Main.projFrames[Projectile.type] = 6;
				Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			//ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
		}

		public override void SetDefaults() {
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			//inertia = 100f;//20
			Projectile.scale = 2f;
			//shoot = ProjectileType<Stick_f_P>();
			//shootSpeed = 9f; //12
			//shootCool =100f;	 
		}

		private bool CheckActive(Player owner)
		{
			if (owner.dead || !owner.active)
			{
				owner.ClearBuff(ModContent.BuffType<SaplingMinionBuff>());

				return false;
			}

			if (owner.HasBuff(ModContent.BuffType<SaplingMinionBuff>()))
			{
				Projectile.timeLeft = 2;
			}

			return true;
		}
		
	}

public class SaplingSilverMinion : ModProjectile
{
	public override void SetStaticDefaults() {
		DisplayName.SetDefault("Animated Silver Sapling Minion");

		Main.projFrames[Projectile.type] = 6;
		Main.projPet[Projectile.type] = true;
		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		//ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
	}

	public override void SetDefaults() {
		Projectile.netImportant = true;
		Projectile.width = 24;
		Projectile.height = 32;
		Projectile.friendly = true;
		Projectile.minion = true;
		Projectile.minionSlots = 1;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 10000;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		//inertia = 100f;//20
		Projectile.scale = 2f;
		//shoot = ProjectileType<Stick_f_P>();
		//shootSpeed = 9f; //12
		//shootCool =80f;	 
	}

	private bool CheckActive(Player owner)
	{
		if (owner.dead || !owner.active)
		{
			owner.ClearBuff(ModContent.BuffType<SilverAcornBuff>());

			return false;
		}

		if (owner.HasBuff(ModContent.BuffType<SilverAcornBuff>()))
		{
			Projectile.timeLeft = 2;
		}

		return true;
	}
} 
	

public class SaplingTungstenMinion : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Animated Tungsten Sapling Minion");
			
				Main.projFrames[Projectile.type] = 6;
				Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			//ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
		}

		public override void SetDefaults() {
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			//inertia = 100f;//20
			Projectile.scale = 2f;
			//shoot = ProjectileType<Stick_f_P>();
			//shootSpeed = 9f; //12
			//shootCool =80f;	 
		}

	private bool CheckActive(Player owner)
	{
		if (owner.dead || !owner.active)
		{
			owner.ClearBuff(ModContent.BuffType<TungstenAcornBuff>());

			return false;
		}

		if (owner.HasBuff(ModContent.BuffType<TungstenAcornBuff>()))
		{
			Projectile.timeLeft = 2;
		}

		return true;
	}
}
public class SaplingPlatinumMinion : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Animated Platinum Sapling Minion");
			
				Main.projFrames[Projectile.type] = 6;
				Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			//ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
		}

		public override void SetDefaults() {
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			//inertia = 100f;//20
			Projectile.scale = 2f;
			//shoot = ProjectileType<Stick_f_P>();
			//shootSpeed = 9f; //12
			//shootCool =80f;	 
		}

	private bool CheckActive(Player owner)
	{
		if (owner.dead || !owner.active)
		{
			owner.ClearBuff(ModContent.BuffType<PlatinumAcornBuff>());

			return false;
		}

		if (owner.HasBuff(ModContent.BuffType<PlatinumAcornBuff>()))
		{
			Projectile.timeLeft = 2;
		}

		return true;
	}
}
public class SaplingGoldMinion : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Animated Golden Sapling Minion");
			
				Main.projFrames[Projectile.type] = 6;
				Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			//ProjectileID.Sets.CountsAsHoming[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
		}

		public override void SetDefaults() {
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			//inertia = 100f;//20
			Projectile.scale = 2f;
			//shoot = ProjectileType<Stick_f_P>();
			//shootSpeed = 9f; //12
			//shootCool =80f;	 
		}

	private bool CheckActive(Player owner)
	{
		if (owner.dead || !owner.active)
		{
			owner.ClearBuff(ModContent.BuffType<GoldenAcornBuff>());

			return false;
		}

		if (owner.HasBuff(ModContent.BuffType<GoldenAcornBuff>()))
		{
			Projectile.timeLeft = 2;
		}

		return true;
	}
	}
}