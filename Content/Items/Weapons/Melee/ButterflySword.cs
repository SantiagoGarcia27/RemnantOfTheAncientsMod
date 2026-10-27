using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
	public class ButterflySword : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(silver: 27);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.scale = 0.7f;
		}
		public override bool AltFunctionUse(Player player) => true;
        public override bool CanShoot(Player player)
        {
			if (player.ownedProjectileCounts[Item.shoot] > 1) return false;		
            return base.CanShoot(player);
        }

		int HitCounter = 0;
		int HitCounterMax = 3;

		bool ShortMode = false;
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			int ProjType = ModContent.ProjectileType<NaturalSpiritButerfly>();
			if (!ShortMode && HitCounter < HitCounterMax)
			{
				HitCounter++;
                return;
			}
			else
			{
				if (player.ownedProjectileCounts[ProjType] < 1)
				{
					Projectile.NewProjectile(Item.GetSource_FromThis(), player.Center, Vector2.Zero, ProjType, Item.damage / 2, 1, player.whoAmI);
                    ChangeMode(player, true);
                }
				else
				{
					HitCounter = 0;

                }
            }
		}
        public override bool CanUseItem(Player player)
        {
            ChangeMode(player, ShortMode);
            return true;
        }
        public override void HoldItem(Player player)
        {
            ChangeMode(player, ShortMode);
        }

        private void ChangeMode(Player player, bool newMode)
        {
            if (ShortMode == newMode)
                return;

            ShortMode = newMode;

            Item.useStyle = ShortMode
                ? ItemUseStyleID.Thrust
                : ItemUseStyleID.Swing;

            if (player.HeldItem == Item)
            {
                player.itemAnimation = 0;
                player.itemTime = 0;
            }
        }

        public override void UpdateInventory(Player player)
        {
            int ProjType = ModContent.ProjectileType<NaturalSpiritButerfly>();
            if (player.ownedProjectileCounts[ProjType] < 1)
            {
                ChangeMode(player, false);
            }
            base.UpdateInventory(player);
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

