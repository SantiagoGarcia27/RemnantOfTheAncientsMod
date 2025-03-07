using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
	public class TinSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.TinBroadsword);
        public override float[] DashStrength => [0.56f, 0.56f];
	}
}

