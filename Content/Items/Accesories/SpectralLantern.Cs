using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
	public class SpectralLantern : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 100;
			Item.value = 500;
			Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			Lighting.AddLight(player.Center, 1);
			player.hasLucyTheAxe = true;
			player.findTreasure = true;
			player.blackout = true;
			player.AddBuff(BuffID.Blackout, 1, true, false);
			player.GetModPlayer<RemnantPlayer>().SpectralLantern = true;         
            base.UpdateAccessory(player, hideVisual);
        }
    }
}
