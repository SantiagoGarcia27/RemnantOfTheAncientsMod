using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class GoldSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.GoldBroadsword);
        public override float[] DashStrength => [0.7f, 0.7f]; 
	}
}
