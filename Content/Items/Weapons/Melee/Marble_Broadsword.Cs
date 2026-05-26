using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
	public class Marble_Broadsword : ModItem
	{
		public override void SetStaticDefaults()
		{
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
		}
		public override bool AltFunctionUse(Player player) => true;

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<Marble_erosion>(), 180);
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

