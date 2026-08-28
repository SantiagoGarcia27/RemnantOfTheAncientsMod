using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using CalamityMod.Particles;
using System;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    public class Graveyard_glyph : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 10;
            Item.rare = ItemRarityID.White;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.maxStack = 1;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<Graveyard_proj>();
            Item.UseSound = SoundID.Item39;
            Item.value = 0;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.knockBack = 0.1f;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddRecipeGroup("Tumbas",5)
            .AddIngredient(ItemID.Deathweed, 2)
            .Register();
        }
    }
}
