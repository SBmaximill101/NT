using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NT.ClassContent;


namespace NT.Items.NinjaTools.Base
{
    public class Shuriken : ModItem
    {
       
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = 999;
            Item.value = 200;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 10;
            Item.useTime = 11;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<NinjaToolDamageClass>();
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<Projectiles.ShurikenProjectile>();
            Item.damage = 8;

            // Set other item.X values here
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25);
            recipe.AddIngredient(ItemID.IronBar);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe(25);
            recipe.AddIngredient(ItemID.LeadBar);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            // Recipes here. See Basic Recipe Guide
        }

    }
}