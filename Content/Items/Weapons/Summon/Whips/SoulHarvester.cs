using RemnantOfTheAncientsMod.Content.Buffs.Buffs;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Whips;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Whips
{
    public class SoulHarvester : ModItem
	{
		public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("Soul Harvester");
            Tooltip.SetDefault("Your summons will focus struck enemies" +
                "\nHas a chance to increase attack speed by 10%");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Moissonneur d'âmes");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vos invocations concentreront les ennemis frappés" +
                "\nA une chance d'augmenter la vitesse d'attaque de 10%");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cosechador de almas");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tu invocaciones se centrará en los enemigos golpeados." +
                "\nTiene la probabilidad de aumentar un 10% la velocidad de ataque");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 15;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<SoulHarvesterProjectile>();
            Item.shootSpeed = 4;
            Item.value = new Item(ItemID.LightsBane).value;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.channel = false; // This is used for the charging functionality. Remove it if your whip shouldn't be chargeable.
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.AddBuff(ModContent.BuffType<CombatAdrenalineBuff>(), 180);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrimtaneBar, 15)
                .AddIngredient(ItemID.TissueSample, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
