using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class CrimsonSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.BloodButcherer);
        public override float[] DashStrength => [0.8f, 0.75f];
        
	}
}
