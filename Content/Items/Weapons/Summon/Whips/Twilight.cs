using CalamityMod.Projectiles.Magic;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Whips
{
    public class Twilight : ModItem
	{
        int tagDamage = 9;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(tagDamage);
        public override void SetStaticDefaults() 
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }  
        public override void SetDefaults()
		{
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 40;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<NightWhipProjectile>();
            Item.shootSpeed = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.value = new Item(ItemID.NightsEdge).value;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.channel = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = RemnantOfTheAncientsMod.CalamityMod != null;
        }
        public override void AddRecipes()
        {
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                CreateRecipe()
                .AddIngredient(ModContent.ItemType<NightBar>(), 10)
                .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 10)
                .AddTile(TileID.DemonAltar)
                .Register();

                Recipe.Create(ModContent.ItemType<Twilight>())
                .AddIngredient(ItemID.ThornWhip, 1)
                .AddIngredient(ItemID.BoneWhip, 1)
                .AddIngredient(ModContent.ItemType<FireStorm>())
                .AddRecipeGroup("anyCorruptWhip")
                .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 10)
                .AddTile(TileID.DemonAltar)
                .Register();
            }
            else
            {
                CreateRecipe()
                .AddIngredient<NightBar>(10)
                .AddTile(TileID.DemonAltar)
                .Register();

                Recipe.Create(ModContent.ItemType<Twilight>())
                .AddIngredient(ItemID.ThornWhip, 1)
                .AddIngredient(ItemID.BoneWhip, 1)
                .AddIngredient(ModContent.ItemType<FireStorm>())
                .AddRecipeGroup("anyCorruptWhip")
                .AddTile(TileID.DemonAltar)
                .Register();
            }
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
