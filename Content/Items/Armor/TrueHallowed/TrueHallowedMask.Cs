using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.TrueHallowed
{
	[AutoloadEquip(EquipType.Head)]
	public class TrueHallowedMask : ModItem
	{
        private readonly int MeleeDamageAndCritBonus = 13;
        private readonly int MeleeSpeedBonus = 13;
		
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageAndCritBonus, MeleeSpeedBonus);
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
			Item.defense = 26;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += MeleeDamageAndCritBonus/100f;
			player.GetCritChance(DamageClass.Melee) += MeleeDamageAndCritBonus;
			player.GetAttackSpeed(DamageClass.Melee) *= 1f + MeleeSpeedBonus/100f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<TrueHallowedPlateMail>() && legs.type == ModContent.ItemType<TrueHallowedGreaves>();
		}
        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = player.setBonus = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.SetBonus.TrueHallowed");
            player.onHitDodge = true;
            int cloneAmmount = player.ownedProjectileCounts[ModContent.ProjectileType<PlayerClone>()];
			if (cloneAmmount < 2 && player.HasBuff(BuffID.ShadowDodge))
			{
				int dir = cloneAmmount < 1 ? -1 : 1;

                Vector2 margin = new Vector2(dir * -2, -2).ToCoordenatePosition();

                var p = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center + margin, Vector2.Zero, ModContent.ProjectileType<PlayerClone>(), 20, 0, player.whoAmI);
                Main.projectile[p].localAI[1] = dir;
			}
		}

		public override void AddRecipes()
		{   
			CreateRecipe()
			.AddIngredient(ItemID.HallowedMask)
			.AddIngredient(ItemID.ChlorophyteBar,12)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}