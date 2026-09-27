using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class WoodenSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.WoodenSword);
        public override float[] DashStrength => [0.5f, 0.5f];
	}
}
