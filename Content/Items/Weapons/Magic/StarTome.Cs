using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    public class StarTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.mana = 5;
            Item.width = 28;
            Item.height = 32;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.shoot = ProjectileID.FallingStar;
            Item.shootSpeed = 14f;
            Item.DamageType = DamageClass.Magic;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = 50000;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.channel = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 26;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FallenStar, 60)
            .AddIngredient(ItemID.Book, 1)
            .AddTile(TileID.Bookcases)
            .Register();
        }
    }
}