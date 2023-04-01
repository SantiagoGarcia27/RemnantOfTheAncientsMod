using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Buffs.Debuff;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Items.Mele
{
    public class Marble_Broadsword : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Marble Broadsword");
Tooltip.SetDefault("Decrease enemy defense by 2.");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Epée large en marbe");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Diminue la défense ennemie de 2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de marmol");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Reduce la defensa del enemigo en 2");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
Item.damage = 14;
Item.DamageType = DamageClass.Melee;
Item.width = 40;
Item.height = 80;
Item.useTime = 30;
Item.useAnimation = 30;
Item.useStyle = ItemUseStyleID.Swing;
Item.knockBack = 8;
Item.value = Item.sellPrice(silver: 27);
Item.rare = ItemRarityID.White;
Item.UseSound = SoundID.Item1;
Item.autoReuse = true;
Item.scale = 1.25f;
//Item.buffType = ModContent.BuffType<Marble_erosion>();
		}
		public override bool AltFunctionUse(Player player) => true;
	
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
target.AddBuff(ModContent.BuffType<Marble_erosion>(), 180);
//Main.NewText(target.defense, new Color(175, 75, 255));
        }
		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.Marble, 20)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}

