using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class Adamantite_saber : SaberBase
    {
        public override Item ItemBase => new(ItemID.AdamantiteSword);
        public override float[] DashStrength => new float[2] { 1.2f, 0.8f };
    }

}

