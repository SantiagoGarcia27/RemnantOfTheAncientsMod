using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Configs;
using RemnantOfTheAncientsMod.Common.UI.AdvanceReforgeUI;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;
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
