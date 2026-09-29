using Microsoft.Xna.Framework;
using Terraria.GameContent.UI;

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