using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.VanillaChanges;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
	public class wooden_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wooden Saber");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre en Bois");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Madera");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item Base = new Item(ItemID.WoodenSword);
			Item.damage = Base.damage - 3;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Base.value;
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.GetGlobalItem<GlobalItem1>().Saber = true;
        }
        public override bool AltFunctionUse(Player player) => true;
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
                //Dust.QuickDust((int)(player.Center.X / 16),(int)((player.Center.Y + (2*16)) / 16), Color.Red);
                if (Main.tile[(int)(player.Center.X/16),(int)((player.Center.Y + (2 * 16)) /16)].HasTile == true)
				{
					DashPlayer.JumpDash(player, 1f, 0.75f);
                }
			}
			
			return true;
		}

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Wood, 10)
			.AddTile(TileID.WorkBenches)
			.Register();
		}
	}
}
