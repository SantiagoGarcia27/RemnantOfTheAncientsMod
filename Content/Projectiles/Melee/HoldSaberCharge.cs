using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{
    public class HoldSaberCharge : ModProjectile
    {
        public Item ItemBase { get; set; }

        public override string Texture => GetTextureFromItem(ItemBase);
        public string GetTextureFromItem(Item item)
        {
            if (item == null) 
                return "Terraria/Images/Item_0";
            string Texture = item.type < ItemID.Count ? "Terraria/Images/Item_" + item.type : ItemLoader.GetItem(item.type).Texture;   
            return Texture;
        }
        public override void SetStaticDefaults()
        {
            Projectile.light = 0;
        }
    }
}
