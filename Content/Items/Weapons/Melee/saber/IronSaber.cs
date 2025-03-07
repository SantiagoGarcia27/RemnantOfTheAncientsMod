using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class IronSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.IronBroadsword);
        public override float[] DashStrength => [0.6f, 0.6f];
	}
}
