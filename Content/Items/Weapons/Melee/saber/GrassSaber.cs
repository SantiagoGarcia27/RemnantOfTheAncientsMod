using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class GrassSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.BladeofGrass);
        public override float[] DashStrength => [0.6f, 1.25f];
    }
}
