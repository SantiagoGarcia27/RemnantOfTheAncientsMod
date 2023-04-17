using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
	public class Corrupted_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Light´s Saber");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre Laser");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable de la Luz");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item Base = new Item(ItemID.LightsBane);
			Item.damage = Base.damage - 3;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 18;
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
                    DashPlayer.JumpDash(player, 1.1f, 0.77f);
                }
            }
            return true;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.DemoniteBar, 10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
