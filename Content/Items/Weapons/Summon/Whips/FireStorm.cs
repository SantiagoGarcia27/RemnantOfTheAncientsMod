using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Whips
{
    public class FireStorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Storm");
            Tooltip.SetDefault("8 summon tag damage" +
                "\nYour summons will focus struck enemies" +
                "\nStrike enemies inflict Fire debuff");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tempête de feu");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "8 dégâts de balise d'invocation" +
                "\nVos invocations concentreront les ennemis frappés" +
                "\nLes ennemis frappés infligent un debuff de feu");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tormenta ignea");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "8 daño de etiqueta de invocación" +
                "\nTu invocaciones se centrará en los enemigos golpeados." +
                "\nQuema al enemigo al atacarlo");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.knockBack = 2;
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<FireStormProjectile>();
            Item.shootSpeed = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = new Item(ItemID.FieryGreatsword).value;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.channel = false; // This is used for the charging functionality. Remove it if your whip shouldn't be chargeable.
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = RemnantOfTheAncientsMod.CalamityMod != null;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}
