using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class LeadSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.LeadBroadsword);
        public override float[] DashStrength => [0.6f, 0.6f];
	}
}

