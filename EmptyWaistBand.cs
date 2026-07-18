using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NT.Items.NinjaArmor.Vanity
{
    [AutoloadEquip(EquipType.Waist)]
    public class EmptyWaistBand : ModItem
    {
       

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = 1;
            Item.value = 700;
            Item.defense = 0;
            Item.accessory = true;
            Item.vanity = true;
            // Set other item.X values here (EmptyHeadBand)
        }



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Silk, 2);
            recipe.AddIngredient(ItemID.IronBar);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Silk, 2);
            recipe.AddIngredient(ItemID.LeadBar);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            // Recipes here. See Basic Recipe Guide
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
           
        }
    }
}