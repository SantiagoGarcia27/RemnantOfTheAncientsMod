using System.Collections.Generic;
using System.Linq;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity
{
    [ExtendsFromMod("FargowiltasSouls")]
    [AutoloadEquip(EquipType.Wings)]
	public class SoulOfDivinity : FlightMasteryWings
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
		}
        public override bool Eternity => true;

        public override int NumFrames => 10;

        public static int WingSlotID { get; private set; }

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			WingSlotID = Item.wingSlot;
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 10,false));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
		}

		public override void SafeModifyTooltips(List<TooltipLine> tooltips)
		{
            if (Item.social)
            {
                return;
            }
            string description = Language.GetTextValue("Mods.FargowiltasSouls.Items.EternitySoul.Extra.Additional");
            description += "                                                                                                                                                       ";
            if (Main.GameUpdateCount % 5u == 0 || EternitySoulSystem.TooltipLines == null)
            {
                EternitySoulSystem.TooltipLines = [];
                for (int j = 0; j < 7; j++)
                {
                    string line = Utils.NextFromCollection(Main.rand, EternitySoulSystem.Tooltips.Where((string s) => s.Length < description.Length).ToList());
                    if (EternitySoulSystem.TooltipLines.Contains(line))
                    {
                        j--;
                    }
                    else
                    {
                        EternitySoulSystem.TooltipLines.Add(line);
                    }
                }
            }
            for (int i = 0; i < EternitySoulSystem.TooltipLines.Count; i++)
            {
                description = description + "\n" + EternitySoulSystem.TooltipLines[i];
            }
            tooltips.Add(new TooltipLine(Mod, "tooltip", description));
            tooltips.Add(new TooltipLine(Mod, "FlavorText", Language.GetTextValue("Mods.FargowiltasSouls.Items.EternitySoul.Extra.Flavor")));
        }

		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			return ModContent.GetInstance<EternitySoul>().PreDrawTooltipLine(line,ref yOffset);   
        }
        public override void SetDefaults()
		{
			base.SetDefaults();
			Item.rare = ItemRarityID.Red;
            Item.value = 200000000;
			Item.shieldSlot = 5;
			Item.defense = 100;
			Item.useStyle = 4;
			Item.useTime = 180;
			Item.useAnimation = 180;
			Item.UseSound = SoundID.Item6;
            Item.GetGlobalItem<CustomTooltip>().EternityItem = true;
        }

		public override void UseItemFrame(Player player)
		{
			SandsofTime.Use(player);
		}

		public override bool? UseItem(Player player)
		{
			return true;
		}

		public override void UpdateInventory(Player player)
		{
			ModContent.GetInstance<EternitySoul>().UpdateInventory(player);
		}

		public override void UpdateVanity(Player player)
		{
			ModContent.GetInstance<EternitySoul>().UpdateInventory(player);
		}  

        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            ModContent.GetInstance<ForceOfRemants>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GuardiansShield>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<EternitySoul>().UpdateAccessory(player, hideVisual);
            player.AddEffect<DivineAuraEffect>(Item);
            player.AddEffect<GodModeEffect>(Item);
        }

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<The_Legion>()
			.AddIngredient(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "EternitySoul"))
			.AddTile(CallUtils.GetTileFromMod(RemnantOfTheAncientsMod.FargowiltasMod, "CrucibleCosmosSheet"))
			.Register();
		}
	}
}

