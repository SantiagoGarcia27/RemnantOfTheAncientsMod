using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.Gem
{
	public class Emerald_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Emerald Sword");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée d'émeraude");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de esmeralda");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
Item.damage = 21;
Item.DamageType = DamageClass.Melee;
Item.width = 50;
Item.height = 50;
Item.useTime = 18;
Item.useAnimation = 18;
Item.useStyle = ItemUseStyleID.Swing;
Item.knockBack = 1;
Item.value = 1000;
Item.rare = ItemRarityID.Blue;
Item.UseSound = SoundID.Item1;
Item.autoReuse = false;
if (RemnantOfTheAncientsMod.CalamityMod != null)
{
	Item.autoReuse = true;
	Item.scale = 2f;
}
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
            Vector2 Position = Main.rand.NextVector2CircularEdge(target.width + 100, target.height + 100) + target.position;
            Vector2 Velocity = Position.DirectionTo(target.position) * 1.01f;
            Projectile.NewProjectileDirect(Projectile.GetSource_None(), Position, Velocity, ModContent.ProjectileType<SpiritGemSword>(), damage / 2, Main.myPlayer, player.whoAmI, 0, 3);
        }
	

		public override void AddRecipes()
		{
CreateRecipe()
.AddIngredient(ItemID.TungstenBar,8)
            .AddIngredient(ItemID.Emerald,10)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}