using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Configs;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.ReaperUI
{
	// This UIHoverImageButton class inherits from UIImageButton. 
	// Inheriting is a great tool for UI design. 
	// By inheriting, we get the Image drawing, MouseOver sound, and fading for free from UIImageButton
	// We've added some code to allow the Button to show a text tooltip while hovered
	public class UIHoverImageButton : UIImageButton
	{
		// Tooltip text that will be shown on hover
		internal string hoverText;
		internal static bool _Active = false;
		internal static bool _Blocked = false;
        public NPC _Npc;
        public bool Reaper;


        public UIHoverImageButton(Asset<Texture2D> texture, string hoverText) : base(texture) {
            this.hoverText = hoverText;
		}
        public UIHoverImageButton(Asset<Texture2D> texture, string hoverText, NPC npc,bool reaper) : base(texture)
        {
            this.hoverText = hoverText;
            _Npc = npc;
            Reaper = reaper;
        }



        public bool Blocked()
		{
			return _Blocked;
        }
        public void Blocked(bool b)
        {
			_Blocked = b;
        }
        public bool Active()
        {
            return _Active;
        }
        public void Active(bool b)
        {
            _Active = b;
        }
		public void Npc(NPC npc)
		{
			_Npc = npc;
		}
        public NPC Npc()
        {
            return _Npc;
        }
        public override void LeftClick(UIMouseEvent evt)
        {
            ModLoader.TryGetMod("SangarUtilities", out Mod TerrariaMod);

            Player player = Main.player[Main.myPlayer];
            ModNPC ModNpc = _Npc.ModNPC;
            Mod mod;
            if (ModNpc == null) mod = TerrariaMod;
            else mod = _Npc.ModNPC.Mod;
            NPC npc = _Npc;
            mod ??= RemnantOfTheAncientsMod.Terraria;
            float value = player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoadedActive[npc.type];

            if (Reaper)
            {
                if (npc.type == NPCID.KingSlime)
                {
                    ReaperEffectsPlayer.SetSoulsToggle(npc.type, value == 0 ? 30 : 0);
                    player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoadedActive[ReaperSoulsPlayer.GetIndexFromLoadedBossById(npc.type)] = ModContent.GetInstance<ConfigReaperSouls>().ToggleKingSlimeSoul;
                }
                else if (npc.type == NPCID.SkeletronPrime)
                {
                    player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoadedActive[ReaperSoulsPlayer.GetIndexFromLoadedBossById(npc.type)] = ModContent.GetInstance<ConfigReaperSouls>().ToggleSkeletronPrimeSoul;
                    ReaperEffectsPlayer.SetSoulsToggle(npc.type, value == 0 ? 10 : 0);
                }
                else
                { 
                    player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoadedActive[ReaperSoulsPlayer.GetIndexFromLoadedBossById(npc.type)] = value == 0 ? 1 : 0;
                    ReaperEffectsPlayer.SetSoulsToggle(npc.type, value == 0);
                }
            }
            base.LeftClick(evt);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch) {
			// When you override UIElement methods, don't forget call the base method
			// This helps to keep the basic behavior of the UIElement
			base.DrawSelf(spriteBatch);

			// IsMouseHovering becomes true when the mouse hovers over the current UIElement
			if (IsMouseHovering)
				Main.hoverItemName = hoverText;
		}
	}
}
