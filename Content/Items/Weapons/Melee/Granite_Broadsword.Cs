using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
	public class Granite_Broadsword : ModItem
	{
		public override void SetStaticDefaults()
		{	
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 12;
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
			Item.autoReuse = false;
		}
  
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.ZoneGranite) damage *= 2f;
        }

        public override float UseSpeedMultiplier(Player player)
        {
            return player.ZoneGranite ? 2f : 1f;
        }

        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
        {
            if (player.ZoneGranite) knockback *= 2f;
        }
        public override void UseItemFrame(Player player)
        {
            Item.scale = player.ZoneGranite ? 2 : 1;
            base.UseItemFrame(player);
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.GraniteBlock, 20)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}

