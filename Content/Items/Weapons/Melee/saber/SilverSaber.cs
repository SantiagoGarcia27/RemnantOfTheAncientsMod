using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class SilverSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.SilverBroadsword);
        public override float[] DashStrength => [0.65f, 0.65f];
	}
}

