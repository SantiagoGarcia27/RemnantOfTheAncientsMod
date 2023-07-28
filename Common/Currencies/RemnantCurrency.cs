using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Currencies
{
	public class RemnantCurrency : CustomCurrencySingleCoin
	{
		public RemnantCurrency(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap) {
this.CurrencyTextKey = CurrencyTextKey;
CurrencyTextColor = Color.BlueViolet;
		}
	}
}