using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles.Melee;

namespace RemnantOfTheAncientsMod.Items.Mele.Gem
{
	public class Topaz_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			////DisplayName.SetDefault("Topaz Sword");
			////DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée de topaze");
			////DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de Topacio");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 50;
			Item.height = 50;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.value = 1200;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Item.autoReuse = true;
				Item.scale = 2f;
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			Vector2 Position = Main.rand.NextVector2CircularEdge(target.width + 100, target.height + 100) + target.position;
			Vector2 Velocity = Position.DirectionTo(target.position) * 1.01f;
			Projectile.NewProjectileDirect(Projectile.GetSource_None(), Position, Velocity, ModContent.ProjectileType<SpiritGemSword>(), hit.Damage / 2, Main.myPlayer, player.whoAmI, 0, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.TinBar, 8)
						.AddIngredient(ItemID.Topaz, 10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
