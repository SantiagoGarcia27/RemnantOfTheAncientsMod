using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.VanillaChanges;

namespace RemnantOfTheAncientsMod.Items.Fmode
{
    internal class ReaperChalice : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaper Chalice");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "moissonneuse");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Segador");
            Tooltip.SetDefault("Active the Reaper mode"
            + "\n you natural max hp set in 1"
            + "\nincrease all damage by 50%"
            + "\nthe short range melee weapons size are increased by 150%"
            + "\nreduce flight time in half."
            + "\ndamage and max health of bosses are doubled"
            + "\nLegendary drops are more common");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Active the Reaper mode"
            + "\nvos PV max naturels sont définis sur 1"
            + "\naugmentez tous les dégâts de 50%"
            + "\nla taille des armes de mêlée à courte portée est augmentée de 150 % "
            + "\nréduisez le temps de vol de moitié "
            + "\nles dégâts et la santé max des boss sont doublés"
            + "\nDrops légendaires sont plus fréquents");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Activa el modo Reaper"
            + "\ntu vida máxima se establece en 1"
            + "\naumenta todo el daño en un 50%"
            + "\nel tamaño de las armas cuerpo a cuerpo de corto alcanze aumenta en un 150%"
            + "\nreduce el tiempo de vuelo a la mitad "
            + "\nel daño y la vida maxima de los jefes se ven duplicados"
            + "\nArticulos legendarios son más comunes ");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
        //Cree un public bool en el acesorio para cuando se este ocupando el slot la idea es que esta funcion lo detecte y active el reapermode (no se si funcione siquiera)

        /*public void ReaperToggle()
        {
            if (ReaperAccessory.ReferenceEquals() )
            {
                if (!Utils1.IsAnyBossAlive())
                {
                    if (world1.ReaperMode == false)
                    {
                        world1.ReaperMode = true;
                        Item.buffTime = 1;
                        Color gray = Color.DarkSlateGray;
                        Main.NewText("Welcome to hell, now you're a reaper.", gray);
                        Player1.ReaperFirstTime = true;
                    }
                    else
                    {
                        world1.ReaperMode = false;
                        Color gray = Color.DarkSlateGray;
                        Main.NewText("Well your soul is free... for now.", gray);
                    }
                }
            }
        }*/
        public override void SetDefaults()
        {
            Item.height = 34;
            Item.width = 30;
            Item.accessory = true;
            Item.GetGlobalItem<GlobalItem1>().ReaperAccesories=true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }
}
