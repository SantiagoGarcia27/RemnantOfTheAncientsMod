using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using SangarUtilities.Common.UtilsTweaks;
using Microsoft.Xna.Framework;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.TrueHallowed
{
	[AutoloadEquip(EquipType.Head)]
	public class TrueHallowedHelmet : ModItem
	{
		int RangedDamageBonus = 17;
		int RangedCritBonus = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBonus, RangedCritBonus);
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 11;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Ranged) *= 1.17f;
            player.GetCritChance(DamageClass.Ranged) += 10f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<TrueHallowedPlateMail>() && legs.type == ModContent.ItemType<TrueHallowedGreaves>(); ;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = player.setBonus = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.SetBonus.TrueHallowed");
            player.onHitDodge = true;
            int cloneAmmount = player.ownedProjectileCounts[ModContent.ProjectileType<PlayerClone>()];
            if (cloneAmmount < 2 && player.HasBuff(BuffID.ShadowDodge))
            {
                int dir = cloneAmmount < 1 ? -1 : 1;

                Vector2 margin = new Vector2(dir * -2 * 16, -2 * 16);

                var p = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center + margin, Vector2.Zero, ModContent.ProjectileType<PlayerClone>(), 20, 0, player.whoAmI);
                Main.projectile[p].localAI[1] = dir;
            }
        }

	
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HallowedHelmet)
            .AddIngredient(ItemID.ChlorophyteBar, 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
	}
}