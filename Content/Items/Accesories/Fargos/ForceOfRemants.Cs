using FargowiltasSouls.Core.AccessoryEffectSystem;
using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos
{
    public class ForceOfRemants : ModItem
	{
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 1; 
			Item.value = Item.buyPrice(0, 1, 10, 72);  
			Item.rare = 11;
			Item.accessory = true;
			Item.defense = 3;
		}
		
        float SpeedBonus = -20;
        float DamageReduction = 20;
        int CritBonus = 50;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SpeedBonus, DamageReduction, CritBonus);
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.AddEffect<DaylightEffect>(base.Item);
            player.AddEffect<TuxoniteEffect>(base.Item);
            player.AddEffect<NightEffect>(base.Item);
            player.AddEffect<ReinforcedIronDREffect>(base.Item);
            player.AddEffect<ReinforcedIronBallistaEffect>(base.Item);
            player.AddEffect<SandHeraldEffect>(base.Item);

            //player.GetModPlayer<RemnantFargosSoulsPlayer>().DesertEnchant(player);
            player.GetModPlayer<RemnantPlayer>().MinionsBuffInflict.Add(BuffID.Electrified);
            player.GetModPlayer<RemnantPlayer>().AllClassBuffInflict.Add(BuffID.Electrified); 
            //player.GetModPlayer<RemnantFargosSoulsPlayer>().TuxoniteEnchant(player,CritBonus);
            //player.GetModPlayer<RemnantFargosSoulsPlayer>().NightEnchant(player);
            //player.GetModPlayer<RemnantFargosSoulsPlayer>().DaylightEnchant(player);
            //player.GetModPlayer<RemnantFargosSoulsPlayer>().ReinforcedIronEnchant(player, SpeedBonus, DamageReduction);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utils1.FindAndReplaceTooltip(tooltips, "&0", Utils1.TooltipHotkeyString(FargosKeybindSystem.ToggNightTp),"Tooltip1");  
            base.ModifyTooltips(tooltips);
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<NightEnchant>()
            .AddIngredient<SandHeraldEnchant>()
            .AddIngredient<ReinforcedIronEnchant>()
            .AddIngredient<DaylightEnchant>()
            .AddIngredient<TuxoniteEnchant>()
            .AddTile(ExternalModCallUtils.GetTileFromMod(RemnantOfTheAncientsMod.FargowiltasMod, "CrucibleCosmosSheet"))
            .Register();
		}
	}
}
