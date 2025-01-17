using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Tuxonite;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos
{
    [ExtendsFromMod("FargowiltasSouls")]
    public class TuxoniteEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod) != null;
        }
        int CritBonus = 30;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CritBonus);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override Color nameColor => new Color(5, 0, 158);
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 2, 72);
            Item.rare = 1;
            Item.accessory = true;
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TuxoniteEffect>(base.Item);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<TuxoniteHelmet>()
            .AddIngredient<Tuxonite_chesplate>()
            .AddIngredient<Tuxonite_Legging>()
            .AddIngredient<minigun>()
            .AddIngredient(ItemID.SpikyBall, 10)
            .AddTile(TileID.DemonAltar)
            .Register();
        }
        [ExtendsFromMod("FargowiltasSouls")]
        public class TuxoniteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
            public override int ToggleItemType => ModContent.ItemType<TuxoniteEnchant>();

            int CritBonus = 30;
            public override void PostUpdateEquips(Player player)
            {
                if (RemnantFargosSoulsPlayer.tuxoniteEnchantCritCounter == 0)
                {
                    Dust.NewDustDirect(player.Center, 3, 10, DustID.MushroomSpray);
                    player.GetCritChance(DamageClass.Generic) += CritBonus;
                    if (RemnantFargosSoulsPlayer.tuxoniteEnchantCritDuration < Utils1.FormatTimeToTick(0, 0, 0, 10))
                    {
                        RemnantFargosSoulsPlayer.tuxoniteEnchantCritDuration++;
                    }
                    else
                    {
                        RemnantFargosSoulsPlayer.tuxoniteEnchantCritCounter = (int)Main.rand.NextFloat(player.FargoSouls().WizardEnchantActive? Utils1.FormatTimeToTick(0, 0, 0, 10) : Utils1.FormatTimeToTick(0, 0, 0, 30));
                        RemnantFargosSoulsPlayer.tuxoniteEnchantCritDuration = 0;
                    }
                }
                else
                {
                    RemnantFargosSoulsPlayer.tuxoniteEnchantCritCounter--;
                }
            }
        }
    }
}
