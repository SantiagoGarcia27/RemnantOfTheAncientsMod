using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.VanillaChanges;

namespace RemnantOfTheAncientsMod.Items.Mele
{
    public class TheSpiker : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Spiker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le Spiker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La punzante");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;	
		}
		public static int counter = 0;
		public static int counter2 = 0;
		public override void SetDefaults()
		{
			Item.damage = 90;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.knockBack = 1;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.value = Item.sellPrice(gold: 30);
			Item.scale = 2.0f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.GetGlobalItem<GlobalItem1>().customRarity = CustomRarity.Legendary;
			Item.GetGlobalItem<GlobalItem1>().LegendaryDrop = true;
		}
		public override bool AltFunctionUse(Player player) => true;
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse != 2)
			{
				if (counter <= 3)
				{
					Item.damage = 90;
					Item.useStyle = ItemUseStyleID.Thrust;
					Item.shoot = ModContent.ProjectileType<InfernalSpike_f>();
					Item.shootSpeed = 0f;
					counter++;
				}
				else
				{
					Item.damage = 190;
					Item.useStyle = ItemUseStyleID.Thrust;
					Item.shoot = ModContent.ProjectileType<InfernalSpikeF_f>();
					Item.shootSpeed = 0f;
					counter = 0;
				}
			}
			else
			{
				if (counter2 <= 3)
				{
					counter++;
					Item.damage = 70;
					Item.useStyle = ItemUseStyleID.Swing;
					Item.shoot = ModContent.ProjectileType<InfernalBall_f>();
					Item.shootSpeed = 20f;
					Item.autoReuse = true;
				}
				else
				{
					Item.damage = 100;
					Item.useStyle = ItemUseStyleID.Swing;
					Item.shoot = ModContent.ProjectileType<InfernalBallF_f>();
					Item.shootSpeed = 20f;
					Item.autoReuse = true;
					counter = 0;
				}
			}
			
			return base.CanUseItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) => target.defense = target.defense / 2;
    }
}
