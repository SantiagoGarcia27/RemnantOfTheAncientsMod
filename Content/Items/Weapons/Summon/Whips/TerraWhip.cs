using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Whips
{
    public class TerraWhip : ModItem
	{
		public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("Terra Whip");
            Tooltip.SetDefault("18 summon tag damage" +
                "\nYour summons will focus struck enemies");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Fouet Terra");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "18 dégâts de balise d'invocation" +
                "\nVos invocations concentreront les ennemis frappés");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Latigo terra");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "18 daño de etiqueta de invocación" +
                "\nTu invocaciones se centrará en los enemigos golpeados.");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        
        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 160;
            Item.knockBack = 5;
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<TerraWhipProjectile>();
            Item.shootSpeed = 4;
            Item.value = new Item(ItemID.TerraBlade).value;
            Item.useStyle = ItemUseStyleID.Swing;
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
                .AddIngredient(ItemID.BrokenHeroSword, 1)
                .AddIngredient(ModContent.ItemType<TrueTwilight>(), 1)
                .AddIngredient(ModContent.ItemType<TrueDurendal>(), 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
