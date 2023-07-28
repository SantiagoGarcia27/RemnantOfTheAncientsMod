using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
	public class Garritas : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Solar Claws");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "griffes solaires");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Garras Solares");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{

			Item.damage = 800;
			Item.DamageType = DamageClass.Melee;
			Item.width = 10;
			Item.height = 10;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 60;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

		}
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.Daybreak, 240);
			if (new RemnantOfTheAncientsMod().ParticleMeter(4) != 0)
			{
				Projectile.NewProjectile(Projectile.GetSource_None(), target.position, new Vector2(0f, 0f), ProjectileID.SolarWhipSwordExplosion, damageDone / 10, 0);
			}
		}


		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.ShroomiteDiggingClaw, 1)
			.AddIngredient(ItemID.FragmentSolar, 20)
			.AddIngredient(ItemID.LunarBar, 10)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
