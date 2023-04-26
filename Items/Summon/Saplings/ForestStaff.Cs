using Terraria;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Projectiles.Summon.Minioms.Saplings;
using RemnantOfTheAncientsMod.Buffs.Minions.acorns;

namespace RemnantOfTheAncientsMod.Items.Summon.Saplings
{
	public class ForestStaff : ModelSummonerStaff
	{
		public override int ItemId => ItemID.Wood;
		public override int BuffId => BuffType<SaplingMinionBuff>();
		public override int MinionId => ProjectileType<SaplingMinion>();
		public override int Damage => 5;
		public override int UseTime => 36;
		public override int Useanimation => 36;
		public override int Mana => 5;
		public override int Price => Item.buyPrice(0, 0, 10, 0);


		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Forest Staff");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del bosque");
			//Tooltip.SetDefault("Summons a Sapling to fight for you");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño para que luche por ti.");
		}


		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Acorn, 5)
			.AddIngredient(ItemID.Wood, 10)
			.AddTile(TileID.WorkBenches)
			.Register();
		}
	}
	public class ForestStaff_S : ModelSummonerStaff
	{
		public override int ItemId => ItemID.SilverBar;
		public override int BuffId => BuffType<SilverAcornBuff>();
		public override int MinionId => ProjectileType<SaplingSilverMinion>();
		public override int Damage => 7;
		public override int UseTime => 36;
		public override int Useanimation => 36;
		public override int Mana => 5;
		public override int Price => Item.buyPrice(0, 0, 10, 0);

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Silver Forest Staff");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del Bosque Plateado");
			//Tooltip.SetDefault("Summons a Silver Sapling to fight for you");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño plateado para que luche por ti.");
		}
	}

	public class ForestStaff_T : ModelSummonerStaff
	{
		public override int ItemId => ItemID.TungstenBar;
		public override int BuffId => BuffType<TungstenAcornBuff>();
		public override int MinionId => ProjectileType<SaplingTungstenMinion>();
		public override int Damage => 8;
		public override int UseTime => 36;
		public override int Useanimation => 36;
		public override int Mana => 5;
		public override int Price => Item.buyPrice(0, 0, 10, 0);
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tungsten Forest Staff");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del bosque de tungsteno");
			//Tooltip.SetDefault("Summons a tungsten Sapling to fight for you");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño de tungsteno para que luche por ti");
		}
	}

	public class ForestStaff_G : ModelSummonerStaff
	{
		public override int ItemId => ItemID.GoldBar;
		public override int BuffId => BuffType<GoldenAcornBuff>();
		public override int MinionId => ProjectileType<SaplingGoldMinion>();
		public override int Damage => 9;
		public override int UseTime => 36;
		public override int Useanimation => 36;
		public override int Mana => 5;
		public override int Price => Item.buyPrice(0, 0, 10, 0);
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Golden Forest Staff");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del bosque Dorado");
			//Tooltip.SetDefault("Summons a Golden Sapling to fight for you");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño dorado para que luche por ti.");
		}
	}
	public class ForestStaff_P : ModelSummonerStaff
	{
		public override int ItemId => ItemID.PlatinumBar;
		public override int BuffId => BuffType<PlatinumAcornBuff>();
		public override int MinionId => ProjectileType<SaplingPlatinumMinion>();
		public override int Damage => 10;
		public override int UseTime => 36;
		public override int Useanimation => 36;
		public override int Mana => 5;
		public override int Price => Item.buyPrice(0, 0, 10, 0);
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Platinum Forest Staff");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bastón del bosque Platinado");
			//Tooltip.SetDefault("Summons a Platinum Sapling to fight for you");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca un retoño platinado para que luche por ti.");
		}
	}
}