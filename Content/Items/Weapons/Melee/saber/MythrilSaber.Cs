using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class MythrilSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.MythrilSword);
        public override float[] DashStrength => [1.1f, 0.8f];    
	}
}

