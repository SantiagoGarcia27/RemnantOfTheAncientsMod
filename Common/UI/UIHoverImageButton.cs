using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace RemnantOfTheAncientsMod.Common.UI.ReaperUI
{
	public class UIHoverImageButton : UIImageButton
	{
		internal string hoverText;
		internal bool _Active = false;
		internal bool _Blocked = false;
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
			if (_Blocked)
				SetVisibility(0.3f, 0.2f);
			else
				SetVisibility(1f, 0.4f);

			base.DrawSelf(spriteBatch);

			if (IsMouseHovering)
				Main.hoverItemName = hoverText;
		}
	}
}
