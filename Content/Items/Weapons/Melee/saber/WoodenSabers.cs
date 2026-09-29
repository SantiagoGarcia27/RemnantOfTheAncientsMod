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
    public class EbonwoodSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.EbonwoodSword);
        public override float[] DashStrength => [0.51f, 0.5f];
    }
    public class PearlwoodSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.PearlwoodSword);
        public override float[] DashStrength => [0.51f, 0.5f];
    }
    public class PalmWoodSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.PalmWoodSword);
        public override float[] DashStrength => [0.5f, 0.51f];
    }
    public class RichMahoganySaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.RichMahoganySword);
        public override float[] DashStrength => [0.51f, 0.5f];
    }
    public class ShadewoodSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.ShadewoodSword);
        public override float[] DashStrength => [0.51f, 0.5f];
    }
    public class BorealWoodSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.BorealWoodSword);
        public override float[] DashStrength => [0.51f, 0.5f];
    }
    public class AshWoodSaber : SaberBase
    {
        public override Item ItemBase => new(ItemID.AshWoodSword);
        public override float[] DashStrength => [0.51f, 0.5f];
    }
}
