using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
    public class Tuxonite_Shortsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tuxonite Shortsword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada corta de Tusonita");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.PlatinumShortsword);
            Item.damage += 3;
            Item.DamageType = DamageClass.Melee;
            Item.width += 10;
            Item.height += 10;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.knockBack = 8;
            Item.value = 1000;
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = RemnantOfTheAncientsMod.CalamityMod != null ? 2f : 1.5f;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<TuxoniteBar>(), 7)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}

