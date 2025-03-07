using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class CobaltSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.CobaltSword);
        public override float[] DashStrength => [1f, 0.75f];
	}
}

