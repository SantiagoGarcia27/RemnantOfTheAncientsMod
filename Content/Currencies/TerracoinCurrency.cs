using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Currencies
{
	// A custom currency type. This class is not autoloaded, an instance of it is registered in the system below.
	// Declaring a derivative such as this lets you override inner hooks to implement custom logic, but you can also just instantiate CustomCurrencySingleCoin directly.
	public class TerracoinCurrency : CustomCurrencySingleCoin
	{
		public TerracoinCurrency(int coinItemId, long currencyCap, string currencyTextKey, Color currencyTextColor) : base(coinItemId, currencyCap) {
			CurrencyTextKey = currencyTextKey;
			CurrencyTextColor = currencyTextColor;
		}

		// The base class contains multiple hooks for you to override if needed.
		// Type 'override' in your IDE and see all you can do.
	}

	// This system showcases registration of custom currencies, similar to the Defenders Medal.
	// For usage, see ExampleNPCShop.cs.
	public sealed class CustomCurrencies : ModSystem
	{
		// A static property where we store the ID of our currency.
		// This is what Item.shopSpecialCurrency will be set to to make a shop entry use our currency.
		public static int TerracoinCurrency { get; set; }

		// We use PostSetupContent to be sure that items' types have been initialized and are accessible. 
		public override void PostSetupContent() {
            // This call actually registers our custom currency.
            TerracoinCurrency = CustomCurrencyManager.RegisterCurrency(new TerracoinCurrency(
				// We assign ExampleItem as the item used for this currency, there is no "ExampleCustomCurrency" item.
				coinItemId: ModContent.ItemType<Terracoin>(),
				// The cap is the max amount of held currency that will be counted and displayed.
				// Item prices in shops should not be higher than this value if you want them to be ever affordable.
				currencyCap: 999,
				// Localization string and color that are used for this currency's name.
				currencyTextKey: "Mods.RemnantOfTheAncientsMod.Items.Terracoin.DisplayName",
				currencyTextColor: Color.Green
			));
		}
	}
}