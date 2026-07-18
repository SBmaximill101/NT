using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using System.Collections.Generic;
using NT.Common;

namespace ExampleMod.Common.UI.ChakraUI
{
	internal class ChakraBar : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize()
		{
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding.
			area = new UIElement();
			area.Left.Set(-area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(60, 0f);

			barFrame = new UIImage(ModContent.Request<Texture2D>("NT/UI/ChakraBarUI")); // Frame of our resource bar
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 1f);
			barFrame.Height.Set(34, 1f);

			text = new UIText("0/0", 0.8f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(20, 0f);

			//gradientA = new Color(41, 161, 186); // A dark Blue
			gradientA = new Color(0, 16, 186);
			gradientB = new Color(95, 205, 228); // A light Blue

			area.Append(text);
			area.Append(barFrame);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}

		// Here we draw our UI
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<ChakraPlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.ChakraCurrent / modPlayer.ChakraMax2; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 0;
			hitbox.Width -= 46; // was 24
			hitbox.Y += 0;
			hitbox.Height -= 27; //was 16

			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1)
			{
				// float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
			}
		}

		public override void Update(GameTime gameTime)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<ChakraPlayer>();
			// Setting the text per tick to update and show our resource values.
			text.SetText($"Chakra : {modPlayer.ChakraCurrent} / {modPlayer.ChakraMax2}");
			base.Update(gameTime);
		}
	}

	class ChakraUISystem : ModSystem
	{
		private UserInterface ChakraBarUserInterface;

		internal ChakraBar ChakraBar;

		public override void Load()
		{
			// All code below runs only if we're not loading on a server
			if (!Main.dedServ)
			{
				ChakraBar = new();
				ChakraBarUserInterface = new();
				ChakraBarUserInterface.SetState(ChakraBar);
			}
		}

		public override void UpdateUI(GameTime gameTime)
		{
			ChakraBarUserInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1)
			{
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"NT: Chakra Bar",
					delegate {
						ChakraBarUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}