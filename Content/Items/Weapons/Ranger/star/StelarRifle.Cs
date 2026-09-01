using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.star
{
    public class StelarRifle : ModItem
    {
        public int NotConsumeAmmoChance = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(NotConsumeAmmoChance);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        int counter = 0;
        int counterMax = Utils1.FormatTimeToTick(Second: 3);

        public override void SetDefaults()
        {
            Item.damage = 600;//305
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 30 - 4;
            Item.width = 80;
            Item.height = 40;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 100000;
            Item.rare = ItemRarityID.Red +1;
            Item.UseSound = SoundID.Item10;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.SuperStar;
            Item.shootSpeed = 30f;
            Item.useAmmo = AmmoID.FallenStar;
            Item.channel = true;
        }
        List<int> stars =
        [
            ProjectileID.SuperStar,
            ProjectileID.Starfury,
            ModContent.ProjectileType<CloneStarWrath>(),
           
        ];
        int index = 0;
        int speedBoost = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            if (counter >= counterMax)
            {
                if (index < stars.Count - 1) index++;
                else if(speedBoost < 30) speedBoost += 10;
                counter = 0;
            }
            return base.Shoot(player,source,position,velocity,type,damage,knockback);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = stars[index];
            damage *= index + 1;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override float UseTimeMultiplier(Player player)
        {
            float multiplier = 1 / (1 + speedBoost / 100f);
            multiplier = multiplier == 0 ? 1f : multiplier;
            return base.UseTimeMultiplier(player) * multiplier;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 0);
        }
        public override void HoldItem(Player player)
        {
            if (player.controlUseItem)
            {
                counter++;
            }
            else
            {
                counter = 0;
                index = 0;
                speedBoost = 0;
            }
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= NotConsumeAmmoChance / 100f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<StarCannonV3>())
            .AddIngredient(ItemID.SniperRifle, 1)
            .AddIngredient<CelestialAmalgamate>(5)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
    }
}
