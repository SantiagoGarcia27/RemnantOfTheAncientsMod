using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class TungstenSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.TungstenBroadsword);
        public override float[] DashStrength => [0.65f, 0.65f];
    }
}

