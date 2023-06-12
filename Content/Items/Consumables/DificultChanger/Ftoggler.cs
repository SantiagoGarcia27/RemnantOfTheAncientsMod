using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.World;
using Terraria.GameContent.Creative;
using Terraria.Chat;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger
{
    public class Ftoggler : ModItem
    {

        private static readonly Color rarityColorOne = GetReaperColor(1);

        private static readonly Color rarityColorTwo = GetReaperColor(2);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaper");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "moissonneuse");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Segador");
            Tooltip.SetDefault("Active the Reaper mode"
            + "\nyou natural max hp set in 1"
            + "\nincrease all damage by 50%"
            + "\nmelee weapons size are increased by 150%"
            + "\n"+Utils1.IncreasedMinionTooltip(1)
            + "\nreduce flight time in half."
            + "\ndamage and max health of bosses are doubled"
            + "\nLegendary drops are more common"
            + "\nTreasure bags have unique items" 
            + "\nThe vanilla and mod bosses drop permanent reaper player upgrades");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Active the Reaper mode"
            + "\nvos PV max naturels sont définis sur 1"
            + "\naugmentez tous les dégâts de 50%"
            + "\nla taille des armes de mêlée est augmentée de 150%"
            + "\n" + Utils1.IncreasedMinionTooltip(1)
            + "\nréduisez le temps de vol de moitié "
            + "\nles dégâts et la santé max des boss sont doublés"
            + "\nDrops légendaires sont plus fréquents"
            + "\nLes sacs à trésors contiennent des objets uniques"
            + "\nLes patrons de vanille et les patrons de mod abandonnent des améliorations de personnage permanentes pour la moissonneuse");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Activa el modo Reaper"
            + "\ntu vida máxima se establece en 1"
            + "\naumenta todo el daño en un 50%"
            + "\nel tamaño de las armas cuerpo a cuerpo aumenta en un 150%"
            + "\n" + Utils1.IncreasedMinionTooltip(1)
            + "\nreduce el tiempo de vuelo a la mitad "
            + "\nel daño y la vida maxima de los jefes se ven duplicados"
            + "\nArticulos legendarios son más comunes "
            + "\nLas bolsas de tesoro tienen objetos únicos"
            + "\nLos jefes vanilla y los del mod sueltan mejoras permanentes de personaje para segador");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        internal static Color GetRarityColor()
        {
            return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        }
        public static Color GetReaperColor(int x)
        {
            Color color = new Color(100, 100, 100);
            if (x == 1) color = new Color(46, 45, 45);
            else if (x == 2) color = new Color(191, 187, 187);
            return color;
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = 1;
            Item.UseSound = SoundID.Item60;
            Item.consumable = false;
        }
        //      public override bool? UseItem(Player player)
        //{
        //	if (Main.netMode != 1)
        //	{
        //		if (!Utils1.IsAnyBossAlive())
        //		{
        ///*Reaper.ReaperMode = !Reaper.ReaperMode ? true : false;   --> no entendi para que esta esto*/
        //if (Reaper.ReaperMode == false)
        //{
        //	//Reaper.CanReaper = true;
        //	Reaper.ReaperMode = true;
        //	Item.buffTime = 1;
        //	Color gray = Color.DarkSlateGray;
        //	Main.NewText("Welcome to hell, now you're a reaper.", gray);

        //	if (player.GetModPlayer<RemnantPlayer>().ChaliceOn != true)
        //	{
        //		player.GetModPlayer<RemnantPlayer>().ReaperStarter();
        //		RemnantPlayer.ReaperFirstTime = true;
        //	}
        //}
        //else
        //{
        //                     // Reaper.CanReaper = false;
        //                      Reaper.ReaperMode = false;

        //	Color gray = Color.DarkSlateGray;
        //	Main.NewText("Well your soul is free... for now.", gray);
        //}
        //		}
        //		if (Main.netMode == NetmodeID.Server)
        //		{
        //NetMessage.SendData(MessageID.WorldData);
        //		}
        //	}
        //	return true;
        //}
        public override bool? UseItem(Player player)
        {
            //if (Main.netMode != 1)
            //{
            if (!Utils1.IsAnyBossAlive())
            {
                if (!Reaper.ReaperMode)
                {
                    Reaper.ReaperMode = true;
                    Item.buffTime = 1;
                    Color gray = Color.DarkSlateGray;
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Welcome to hell, now you're a reaper."), gray);
                    var modPlayer = player.GetModPlayer<RemnantPlayer>();
                    if (modPlayer.ChaliceOn != true)
                    {
                        modPlayer.ReaperStarter();
                        RemnantPlayer.ReaperFirstTime = true;
                    }
                }
                else
                {
                    Reaper.ReaperMode = false;
                    Color gray = Color.DarkSlateGray;

                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Well your soul is free... for now."), gray);
                }

                //  if (Main.netMode == NetmodeID.Server)
                //  {
                //      NetMessage.SendData(MessageID.WorldData);
                //  }
                // }
            }

            return true;
        }



        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }
}
