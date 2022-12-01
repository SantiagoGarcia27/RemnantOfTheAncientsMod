using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Items.Mele.saber
{
    public class Fire_saber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fiery Saber");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Sabre de Feu");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sable Ardiente");
			Tooltip.SetDefault("Inflict fire on enemies");
           	Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger du feu aux ennemis");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije fuego a los enemigos");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 32;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = Item.sellPrice(silver: 54);
			Item.rare = ItemRarityID.Orange;
			Item.scale = 1.28f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 80);
		}	
		public override void MeleeEffects(Player player, Rectangle hitbox) 
		{
			if (Main.rand.NextBool(1)) Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height,DustID.Pixie);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.HellstoneBar,20)
			.AddTile(TileID.Anvils)   
			.Register();
		}
	}
}
