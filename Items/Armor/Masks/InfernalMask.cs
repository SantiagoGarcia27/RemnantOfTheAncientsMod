using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Armor.Masks
{
	[AutoloadEquip(EquipType.Head)]
	public class InfernalMask : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			DisplayName.SetDefault("Infernal Tyrant Mask");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Maska Piekielny Tyran");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Masque D'Tyran infernal");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Máscara de Tirano infernal");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.rare = ItemRarityID.Blue;
			Item.vanity = true;
		}

		public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor) {
			color = drawPlayer.GetImmuneAlphaPure(Color.White, shadow);
		}
	}
}