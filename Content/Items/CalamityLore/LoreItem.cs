using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.CalamityLore;
[ExtendsFromMod("CalamityMod")]
public abstract class LoreItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Lore";

	public override LocalizedText Tooltip => CalamityMod.CalamityUtils.GetText(LocalizationCategory + ".ShortTooltip");

	public virtual Color? LoreColor => null;

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemNoGravity[base.Item.type] = true;
	}

	public override bool CanUseItem(Player player)
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)12000;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		TooltipLine fullLore = new TooltipLine(Mod, "CalamityMod:Lore", this.GetLocalizedValue("Lore"));
		if (LoreColor.HasValue)
		{
			fullLore.OverrideColor = LoreColor.Value;
		}
		CalamityMod.CalamityUtils.HoldShiftTooltip(tooltips, new TooltipLine[1] { fullLore }, hideNormalTooltip: true);
	}
}
