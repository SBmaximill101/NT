using NT.Items.Essences;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NT.Items.Jutsu.First
{
    internal class ChakraDirect : ModItem
    {


        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.UseSound = SoundID.DD2_BetsyFireballImpact;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.buffType = ModContent.BuffType<Buffs.FirstJutsu.ChakraDirectBuff>(); // Specify an existing buff to be applied when used.
            Item.buffTime = 5400; // The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
            Item.useStyle = ItemUseStyleID.Shoot;
            
            // Set other item.X values here
        }

        

        public override void AddRecipes()
        {
            //recipe.AddIngredient(ItemID.SwiftnessPotion);
            Recipe recipe = CreateRecipe().AddIngredient<CrudeEssence>(20).AddTile<Jutsu.InscriptionTableTile>();
            recipe.AddIngredient(ItemID.SwiftnessPotion);
            recipe.AddIngredient(ItemID.GravitationPotion);
            recipe.Register();
        }

        public override void UseAnimation(Player player)
        {
           //DEPRICATED Dust.NewDust(player.position, 30, 30, ModContent.DustType<ChakraDirectDust>());

        }
        
    }
}
