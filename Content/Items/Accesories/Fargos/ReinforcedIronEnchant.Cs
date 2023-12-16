using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Armor.Reinforced_Iron;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos
{
    public class ReinforcedIronEnchant : ModItem
	{
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		float SpeedBonus = -50;
		float DamageReduction = 10;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpeedBonus, DamageReduction);

        public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 1; 
			Item.value = Item.buyPrice(0, 0, 20, 72);  
			Item.rare = 2;
			Item.accessory = true;
			Item.defense = 3;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.endurance += DamageReduction/100;
            player.moveSpeed += SpeedBonus / 100;
			player.GetModPlayer<RemnantPlayer>().IronBallistaEnchantment = true;


			int minionCount = (player.ownedProjectileCounts[ModContent.ProjectileType<IronBallistMinion>()]);

            if (minionCount < 1)
			{
				float damage = 30;

                damage = player.GetTotalDamage(DamageClass.Summon).ApplyTo(damage);
                Projectile projectile = Projectile.NewProjectileDirect(Projectile.GetSource_None(), player.position,new Vector2(0,2 *16) * 10, ModContent.ProjectileType<IronBallistMinion>(), (int)damage, 1, Main.myPlayer);
				projectile.minionSlots = 0;
				projectile.originalDamage = (int)damage;
            }
        }

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<Reinforced_iron_Helmet>()
            .AddIngredient<Reinforced_iron_chesplate>()
			.AddIngredient<Reinforced_Iron_Greaves>()
			.AddIngredient<IronBallistaStaff>()
            .AddIngredient<IronBand>()
			.AddIngredient(ItemID.IronCrate)
            .AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}
