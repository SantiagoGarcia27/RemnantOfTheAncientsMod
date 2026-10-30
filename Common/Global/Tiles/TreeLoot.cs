using Terraria;
using Terraria.Enums;
using Terraria.DataStructures;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;

namespace RemnantOfTheAncientsMod.Common.Global.Tiles
{
    public class TreeLoot : GlobalTile
    {
        public override bool ShakeTree(int x, int y, TreeTypes treeType)
        {
            if (treeType != TreeTypes.Forest)
                return false;

            if (Main.rand.NextBool(20))
            {
                Item.NewItem(new EntitySource_ShakeTree(x, y), x.ToCoordinatePosition(), y.ToCoordinatePosition(), 16, 16, ModContent.ItemType<ButterflySword>());
                return true;
            }

            return false;
        }
    }
}