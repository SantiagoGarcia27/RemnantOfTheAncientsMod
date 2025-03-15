using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;
using FargowiltasSouls.Core.AccessoryEffectSystem;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity
{
    public class FrostBarrier : ModItem
    {

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }
        //string Key = Utils1.TooltipHotkeyString(FargosKeybindSystem.TogleFrostBarrier);
        int DurationSeconds = 10;
       // public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Utils1.TooltipHotkeyString(FargosKeybindSystem.TogleFrostBarrier), DurationSeconds);
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
            Item.defense = 5;
            Item.accessory = true;
            Item.GetGlobalItem<CustomTooltip>().EternityItem = true;
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FrostBarrierEffect>(base.Item);
            player.GetModPlayer<RemnantFargosSoulsPlayer>().FrostBarrier = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
           Utils1.FindAndReplaceTooltip(tooltips,"{0}", Utils1.TooltipHotkeyString(FargosKeybindSystem.TogleFrostBarrier));
            Utils1.FindAndReplaceTooltip(tooltips,"{1}", DurationSeconds.ToString());
            base.ModifyTooltips(tooltips);
        }
    }
}