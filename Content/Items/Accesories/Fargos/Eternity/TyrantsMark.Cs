using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Global;
using Terraria.Localization;
using FargowiltasSouls.Core.AccessoryEffectSystem;

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
            Item.rare = -12;
            Item.accessory = true;
            Item.GetGlobalItem<CustomTooltip>().EternityItem = true;
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //player.GetModPlayer<RemnantFargosSoulsPlayer>().TyrantMarkEffect(player, MovmentSpeedBonusBonus, ProjectileSizeBonus, ProjectileSizeBonus);
            RemnantFargosSoulsPlayer.ProjectileSpeedBonus = ProjectileSpeedBonus;
            RemnantFargosSoulsPlayer.ProjectileSizeBonus = ProjectileSizeBonus;
            player.AddEffect<TyrantMarkEnemyProjectileSizeEffect>(base.Item);
            player.AddEffect<TyrantMarkEnemyProjectileSpeedEffect>(base.Item);

            AddEffects(player, MovmentSpeedBonusBonus);
        }
        public static void AddEffects(Player player, float MovmentSpeedBonusBonus)
        {
            player.moveSpeed *= 1f + MovmentSpeedBonusBonus / 100;
        }
    }
}