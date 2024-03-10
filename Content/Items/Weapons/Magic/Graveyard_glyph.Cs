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
            // //DisplayName.SetDefault("Desert Bow");
            //Tooltip.SetDefault(""
            //+ "\nTransform normal arrows in desert arrows");

            // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Arc du désert");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
            //+ "\nTransformez les flèches normales en flèches du désert");

            // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Arco Desertico");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
            //+ "\nTransforma flechas normales en flechas del deserticas");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 10;
            Item.rare = ItemRarityID.White;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.maxStack = 9999;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<Graveyard_proj>();
            Item.UseSound = SoundID.Item39;
            Item.value = Item.sellPrice(silver: 50);
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.knockBack = 0.1f;
            Item.consumable = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe(2)
            .AddRecipeGroup("Tumbas")
            .Register();
        }
    }
}
