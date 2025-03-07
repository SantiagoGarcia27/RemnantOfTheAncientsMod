using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
	public class PalladiumSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.PalladiumSword);
        public override float[] DashStrength => [1f, 0.75f];
	}
}

