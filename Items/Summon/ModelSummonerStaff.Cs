using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

public abstract class ModelSummonerStaff : ModItem
{

	public abstract int ItemId { get; }
	public abstract int BuffId { get; }
	public abstract int MinionId { get; }
	public abstract int Damage { get; }
	public abstract int UseTime { get; }
	public abstract int Useanimation { get; }
	public abstract int Mana { get; }
	public abstract int Price { get; }


	public override void SetStaticDefaults()
	{
		ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
		ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
	}

	public override void SetDefaults()
	{
		Item.damage = Damage;
		Item.mana = Mana;
		Item.width = 32;
		Item.height = 32;
		Item.useTime = UseTime;
		Item.useAnimation = Useanimation;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = Price;
		Item.rare = ItemRarityID.Blue;
		Item.UseSound = SoundID.Item44;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Summon;
		Item.buffType = BuffId;
		Item.shoot = MinionId;
		if (ModLoader.TryGetMod("CalamityMod", out Mod mod))
		{
			Item.damage = Damage + 3;
		}
    }
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		player.AddBuff(Item.buffType, 2);
		var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
		projectile.originalDamage = Item.damage;
		return false;
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		player.AddBuff(Item.buffType, 2);
		position = Main.MouseWorld;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
		.AddIngredient(ModContent.ItemType<RemnantOfTheAncientsMod.Items.Summon.Saplings.ForestStaff>())
		.AddIngredient(ItemId, 8)
		.AddTile(TileID.Anvils)
		.Register();
	}
}