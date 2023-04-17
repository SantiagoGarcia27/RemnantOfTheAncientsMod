using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
	public class Reinforced_Iron_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced Iron Saber");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de fer renforcé");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de Hierro Reforzado");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item Base = new Item(ModContent.ItemType<Reinforced_Iron_Broadsword>());
			Item.damage = Base.damage - 3; ;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Base.value;
			Item.rare = Base.rare;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile == true)
                {
                    DashPlayer.JumpDash(player, 0.75f, 0.5f);
                }
            }
            return true;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<Reinforced_ironBar>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

