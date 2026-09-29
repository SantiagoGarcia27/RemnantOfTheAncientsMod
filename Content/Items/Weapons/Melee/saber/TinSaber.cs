using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
	public class TinSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.TinBroadsword);
        public override float[] DashStrength => [0.56f, 0.56f];
	}
}

