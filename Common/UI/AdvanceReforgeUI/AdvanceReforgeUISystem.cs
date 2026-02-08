using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.AdvanceReforgeUI
{
    [Autoload(Side = ModSide.Client)]
    public class AdvanceReforgeUISystem : ModSystem
    {
        private UserInterface userInterface;
        internal AdvanceReforgeUIState advanceReforgeUI;
        private static bool Visible = false;

        public void ShowMyUI()
        {
            userInterface?.SetState(advanceReforgeUI);
            Visible = true;
        }

        public void HideMyUI()
        {
            userInterface?.SetState(null);
            Visible = false;
        }

        public bool IsVisible() => Visible;

        public override void Load()
        {
            userInterface = new UserInterface();
            advanceReforgeUI = new AdvanceReforgeUIState();
        }

        public override void PostSetupContent()
        {
            advanceReforgeUI.Activate();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (userInterface?.CurrentState != null)
            {
                if (!Main.playerInventory)
                {
                    HideMyUI();
                    return;
                }

                userInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "RemnantOfTheAncients: AdvanceReforgeUI",
                    delegate
                    {
                        if (userInterface?.CurrentState != null)
                            userInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
