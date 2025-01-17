using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class EndlessGoldFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofGold);
        public override char GramaticalCorrection => 'M';  
    }
    public class EndlessIchorFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofIchor);
        public override char GramaticalCorrection => 'M';
    }
    public class EndlessFireFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofFire);
        public override char GramaticalCorrection => 'M';
    }
    public class EndlessCurcedFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofCursedFlames);
        public override char GramaticalCorrection => 'M';
    }
    public class EndlessVenomFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofVenom);
        public override char GramaticalCorrection => 'M';
    }
    public class EndlessPartyFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofParty);
        public override char GramaticalCorrection => 'M';
    }
    public class EndlessPoisonFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofPoison);
        public override char GramaticalCorrection => 'M';
    }
    public class EndlessNaniteFlask : BasePotions
    {
        public override Item ItemBase => new(ItemID.FlaskofNanites);
        public override char GramaticalCorrection => 'M';
    }

}