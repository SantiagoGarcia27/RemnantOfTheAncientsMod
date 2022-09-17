using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.VanillaChanges;

namespace RemnantOfTheAncientsMod.Items.Mele
{
	public class Legendary_Great_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Legendary Great Sword");
			Tooltip.SetDefault("Inflict fire on enemies");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Grande épée légendaire");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Inflige le feu aux ennemis");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Gran espada legendaria");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije fuego a los enemigos");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 200;
			Item.DamageType = DamageClass.Melee;
			Item.width = 130;
			Item.height = 160;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 20;
			Item.scale = 1.80f;
			Item.UseSound = SoundID.Item45;
			Item.autoReuse = true;
			Item.value = Item.sellPrice(gold: 35);
			Item.GetGlobalItem<GlobalItem1>().customRarity = CustomRarity.Legendary;
			Item.GetGlobalItem<GlobalItem1>().LegendaryDrop = true;
			if (Item.prefix == PrefixID.Legendary) DisplayName.SetDefault("True Great Sword");
		}

		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 40);
		}
        public override void MeleeEffects(Player player, Rectangle hitbox) => Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Pixie);

    }
}
