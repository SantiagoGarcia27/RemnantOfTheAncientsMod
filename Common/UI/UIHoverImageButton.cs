using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace RemnantOfTheAncientsMod.Common.UI
{
	public class UIHoverImageButton : UIImageButton
	{
		internal string hoverText;
		internal bool _Active = false;
		internal bool _Blocked = false;
		public NPC _Npc;
		public bool Reaper;



		public UIHoverImageButton(Asset<Texture2D> texture, string hoverText, NPC npc = null) : base(texture)
		{
			this.hoverText = hoverText;
			if (npc != null)
			{
				_Npc = npc;
				Reaper = true;
			}
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
