using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class CopperSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.CopperBroadsword);
        public override float[] DashStrength => [0.56f, 0.56f];
    }
}

