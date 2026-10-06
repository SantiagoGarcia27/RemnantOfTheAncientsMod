using FargowiltasSouls.Core.AccessoryEffectSystem;
using RemnantOfTheAncientsMod.Common.Global;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity
{
    public class TyrantsMark : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }
        int ProjectileSizeBonus = 50;
        int ProjectileSpeedBonus = 10;
        int MovmentSpeedBonusBonus = 10;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ProjectileSizeBonus, ProjectileSpeedBonus, MovmentSpeedBonusBonus);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 6, 70, 72);
            Item.rare = ItemRarityID.Expert;
            Item.accessory = true;
            Item.GetGlobalItem<CustomTooltip>().EternityItem = true;
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            RemnantFargosSoulsPlayer remnantFargosSoulsPlayer = player.GetModPlayer<RemnantFargosSoulsPlayer>();

            remnantFargosSoulsPlayer.ProjectileSpeedBonus = ProjectileSpeedBonus;
            remnantFargosSoulsPlayer.ProjectileSizeBonus = ProjectileSizeBonus;

            player.AddEffect<TyrantMarkEnemyProjectileSizeEffect>(Item);
            player.AddEffect<TyrantMarkEnemyProjectileSpeedEffect>(Item);

            AddEffects(player, MovmentSpeedBonusBonus);
        }
        public static void AddEffects(Player player, float MovmentSpeedBonusBonus)
        {
            player.moveSpeed *= 1f + MovmentSpeedBonusBonus / 100;
        }
    }
}