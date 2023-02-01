using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.VanillaChanges;
using RemnantOfTheAncientsMod.Buffs.Debuff;

namespace RemnantOfTheAncientsMod.Items.Mele
{
	public class Legendary_Great_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Legendary Great Sword");
			Tooltip.SetDefault("Inflict fire,ichor,venom,hellfire and poison on enemies");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Grande épée légendaire");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Inflige le feu aux ennemis");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Gran espada legendaria");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije fuego,ichor,ponsoña,fuego infernal y veneno a los enemigos");

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

			if(RemnantOfTheAncientsMod.CalamityMod != null)
			{
                Item.damage = 400;
            }
		}
        public override void PostReforge()
        {
            base.PostReforge();
          //  if (Item.prefix == PrefixID.Legendary) DisplayName.SetDefault("True Legendary Great Sword");

        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(ModContent.BuffType<Hell_Fire>(), 300);
            target.AddBuff(BuffID.Ichor, 300);
            target.AddBuff(BuffID.Venom, 300);
            target.AddBuff(BuffID.Poisoned, 300);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox) => Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Pixie);

    }
}
