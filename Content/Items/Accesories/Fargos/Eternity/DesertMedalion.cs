using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity
{
    [JITWhenModsEnabled("FargowiltasSouls")]
    public class DesertMedalion : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DesertMedalionEffect>(Item);
        }
    }

    [ExtendsFromMod("FargowiltasSouls")]
    public class DesertMedalionEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<GuardiansShieldHeader>();
        public override int ToggleItemType => ModContent.ItemType<DesertMedalion>();

        public override void PostUpdateEquips(Player player)
        {
            player.GetModPlayer<RemnantPlayer>().MinionsBuffInflict.Add(ModContent.BuffType<Burning_Sand>());

            player.GetModPlayer<RemnantFargosSoulsPlayer>().DesertMedalion = true;

            player.buffImmune[ModContent.BuffType<Burning_Sand>()] = true;
        }
    }
}