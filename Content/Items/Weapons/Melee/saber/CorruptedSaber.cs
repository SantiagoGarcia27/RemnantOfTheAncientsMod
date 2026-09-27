using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using Terraria;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class CorruptedSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.LightsBane);
        public override float[] DashStrength => [0.8f, 0.77f];       
    }
}
