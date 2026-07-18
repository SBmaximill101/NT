using NT.Items.Essences;
using NT.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NT.Items.Jutsu.First
{
    internal class BodyReplacement : ModItem
    {

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.UseSound = SoundID.DD2_BetsyFireballImpact;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;

            // Set other item.X values here
        }

        public override void UseItemFrame(Player player)
        {
            if(player.GetModPlayer<ChakraPlayer>().ChakraCurrent >= 50)
            {
                player.AddBuff(ModContent.BuffType<Buffs.FirstJutsu.BodyReplacementBuff>(), 400);
            }
            player.GetModPlayer<ChakraPlayer>().ChakraCurrent -= 2;
            
        }

        public override void AddRecipes()
        {
            //recipe.AddIngredient(ItemID.SwiftnessPotion);
            Recipe recipe = CreateRecipe().AddIngredient<CrudeEssence>(25).AddTile<Jutsu.InscriptionTableTile>();
            recipe.AddIngredient(ItemID.IronskinPotion);
            recipe.AddIngredient(ItemID.Acorn);
            recipe.Register();
        }

    }
}
