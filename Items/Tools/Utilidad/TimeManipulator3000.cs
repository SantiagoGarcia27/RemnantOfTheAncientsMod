using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Items.Tools.Utilidad
{
	public class TimeManipulator3000 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Time Manipulator 3000");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Manipulateur de temps 3000");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Manipulador temporal 3000");
			Tooltip.SetDefault("Summons the moon and de sun");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Invoque la lune et le soleil");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Convoca a la luna y al sol");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.maxStack = 1;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
		}

		
		public override bool? UseItem(Player player)
		{
            if (Main.netMode != 1)
            {
                Main.time = 0.0;
                Main.dayTime = !Main.dayTime;
                if (Main.dayTime && ++Main.moonPhase >= 8)
                {
                    Main.moonPhase = 0;
                }
                Netcode.SyncWorld();
            }
			return true;
        }
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<MoonManipulator>())
			.AddIngredient(ItemType<SunManipulator>())
			.AddIngredient(ItemID.SoulofMight, 2)
			.AddIngredient(ItemID.SoulofSight, 2)
			.AddIngredient(ItemID.SoulofFright, 2)
			.AddIngredient(ItemID.HallowedBar, 4)
			.AddIngredient(ItemID.Wire, 30)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}
