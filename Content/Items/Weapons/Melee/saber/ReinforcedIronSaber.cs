using Terraria;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class ReinforcedIronSaber : SaberBase
	{
        public override Item ItemBase => new(ModContent.ItemType<Reinforced_Iron_Broadsword>());
        public override float[] DashStrength => [0.5f, 0.4f];
	}
}

