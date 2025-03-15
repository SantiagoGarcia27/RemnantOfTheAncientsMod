using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Content.Items.Armor.ReinforcedIron;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos
{
    [ExtendsFromMod("FargowiltasSouls")]
    public class ReinforcedIronEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod) != null;
        }
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		float SpeedBonus = -50;
		float DamageReduction = 10;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpeedBonus, DamageReduction);
        public override Color nameColor => new Color(119, 119, 120);
        public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 1; 
			Item.value = Item.buyPrice(0, 0, 20, 72);  
			Item.rare = 2;
			Item.accessory = true;
			Item.defense = 3;
		}
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.AddEffect<ReinforcedIronBallistaEffect>(base.Item);
            player.AddEffect<ReinforcedIronDREffect>(base.Item);
        }

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<ReinforcedIronHelmet>()
            .AddIngredient<ReinforcedIronChesplate>()
			.AddIngredient<ReinforcedIronGreaves>()
			.AddIngredient<IronBallistaStaff>()
            .AddIngredient<IronBand>()
			.AddIngredient(ItemID.IronCrate)
            .AddTile(TileID.DemonAltar)
			.Register();
		}
        [ExtendsFromMod("FargowiltasSouls")]
        public class ReinforcedIronDREffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
            public override int ToggleItemType => ModContent.ItemType<ReinforcedIronEnchant>();
            float SpeedBonus;
            float DamageReduction = 10;
            public override void PostUpdateEquips(Player player)
            {
                SpeedBonus = player.FargoSouls().WizardEnchantActive? -10: -50;
                player.endurance += DamageReduction / 100;
                player.moveSpeed += SpeedBonus / 100;
            }
        }
        [ExtendsFromMod("FargowiltasSouls")]
        public class ReinforcedIronBallistaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
            public override int ToggleItemType => ModContent.ItemType<ReinforcedIronEnchant>();

            Projectile IronBallistaProj = null;
            public override void PostUpdateEquips(Player player)
            {

                player.GetModPlayer<RemnantFargosSoulsPlayer>().IronBallistaEnchantment = true;
                int minionCount = (player.ownedProjectileCounts[ModContent.ProjectileType<IronBallistMinion>()]);
                if (minionCount < 1)
                {
                    float damage = 30;

                    damage = player.GetTotalDamage(DamageClass.Summon).ApplyTo(damage);
                    IronBallistaProj = Projectile.NewProjectileDirect(Projectile.GetSource_None(), player.position, new Vector2(0, 2 * 16) * 10, ModContent.ProjectileType<IronBallistMinion>(), (int)damage, 1, Main.myPlayer);
                    IronBallistaProj.minionSlots = 0;
                    IronBallistaProj.timeLeft = 300;
                    IronBallistaProj.originalDamage = (int)damage;

                }
            }
        }
    }
}
