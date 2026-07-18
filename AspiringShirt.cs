using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NT.Items.NinjaArmor.Base.Aspiring
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class AspiringShirt : ModItem
	{
		

		public override void SetDefaults()
		{
			Item.width = 36; // Width of the item
			Item.height = 36; // Height of the item
			Item.value = Item.sellPrice(silver: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.White; // The rarity of the item
			Item.defense = 3; // The amount of defense the item will give when equipped
			
		}

		public override void UpdateEquip(Player player)
		{
		 
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<NinjaTools.Base.Shuriken>());
			recipe.AddIngredient(ItemID.Silk);
			recipe.Register();
		}
	}
	
}
