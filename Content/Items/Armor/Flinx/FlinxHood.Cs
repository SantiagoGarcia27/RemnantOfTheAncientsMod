using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Microsoft.Xna.Framework;
using Humanizer;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Flinx
{
    [AutoloadEquip(EquipType.Head)]
	public class FlinxHood : ModItem
	{
		public override void SetStaticDefaults()
		{
			
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public int SummonDamageBonus = 3;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SummonDamageBonus);
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Green;
			Item.defense = 1;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.FlinxFurCoat && legs.type == ModContent.ItemType<FlinxPants>();
		}
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) *= 1.03f;
        }
        public override void UpdateArmorSet(Player player)
		{
			int lifeBonus = 40;
			player.setBonus = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.SetBonus.Flinx").FormatWith(lifeBonus);

			player.statLifeMax2 += 40;
			//player.flinxMinion = true;
			player.GetModPlayer<RemnantPlayer>().FlinxArmorSetBonus = true;

            if (player.ownedProjectileCounts[ModContent.ProjectileType<FlinxMinionClone>()] < 1 )
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<FlinxMinionClone>(), 20, 0, player.whoAmI);
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
            .AddRecipeGroup("GoldBar", 4)
            .AddIngredient(ItemID.FlinxFur, 4)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.Loom)
            .Register();
		}
	}
}