using NT.Items.Essences;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NT.Items.Jutsu.First
{
    internal class Clone : ModItem
    {
       
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.UseSound = SoundID.DD2_BetsyFireballImpact;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.buffType = ModContent.BuffType<Buffs.FirstJutsu.CloneBuff>(); // Specify an existing buff to be applied when used.
            //Item.buffTime = 5400; // The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.buffTime = 2;
            //BuffID.Sets.TimeLeftDoesNotDecrease[ModContent.BuffType<Buffs.FirstJutsu.CloneBuff>()] = true;

            // Set other item.X values here
        }

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe().AddIngredient<CrudeEssence>(10).AddTile<Jutsu.InscriptionTableTile>();
            recipe.AddIngredient(ItemID.TargetDummy, 2);
            recipe.Register();
        }

    }
}
