using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Mele
{
    public class TheSpiker : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Spiker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le Spiker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La punzante");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 90;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(gold: 30);
			Item.rare = -12;
			Item.scale = 2.0f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<InfernalSpikeF_f>();
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) => target.defense = target.defense / 2;
    }
}
