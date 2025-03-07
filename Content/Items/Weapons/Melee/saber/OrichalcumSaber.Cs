using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class OrichalcumSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.OrichalcumSword);
        public override float[] DashStrength => [1.1f, 0.8f];
	}
}

