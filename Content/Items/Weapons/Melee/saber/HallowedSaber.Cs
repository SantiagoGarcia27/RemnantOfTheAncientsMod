using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class HallowedSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.Excalibur);
        public override float[] DashStrength => [1.5f, 0.75f];   
	}
}

