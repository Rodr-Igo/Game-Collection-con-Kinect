using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TetrisLibrary;
using System.IO;

namespace TetrisGame
{
	/// <summary>
	/// The class that draws and updates the Score of the user.
	/// </summary>
	public class ScoreSprite : DrawableGameComponent
	{
		Score score;
		private SpriteFont font;
		private SpriteFont fontTitle;
		private SpriteFont fontEm;
		private Game1 game;
		private SpriteBatch spriteBatch;
		private String gameover = "";
		private Color fontColour = Color.Silver;
		//the previous high score
		int highScore = 0;

		/// <summary>
		/// Initializes the ScoreSprite object.
		/// </summary>
		/// <param name="game">Reference to the Game1 object.</param>
		/// <param name="score">Logical representation of the score.</param>
		public ScoreSprite(Game1 game, Score score) : base(game)
		{
			this.game = game;
			this.score = score;
			//save the high score to a file when the game is over.
			game.Exiting += saveResults;
		}

		/// <summary>
		/// Allows to perform any initialization before the game is started.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
		}

		/// <summary>
		/// Loads all necessary content for the game.
		/// The title of the game has a different font than the rest of the text.
		/// Determines the previous high score.
		/// </summary>
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			font = game.Content.Load<SpriteFont>("scoreFont");
			fontTitle = game.Content.Load<SpriteFont>("scoreFontTitle");
			fontEm = game.Content.Load<SpriteFont>("scoreFontEm");
			findHighScore();

			base.LoadContent();
		}

		/// <summary>
		/// Draws the Score, including the score, the level, the number of cleared lines,
		/// the previous hign score, and the title of the game.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			Color baseTxt = Color.White;
			Color emTxt = Color.SeaGreen;

			spriteBatch.Begin();
			spriteBatch.DrawString(fontEm,
				"Puntuacion"+ score.ScoreValue + "\nNivel: " + score.Level + "\nLineas: " 
				+ score.Lines + gameover, new Vector2(20, GraphicsDevice.Viewport.Height - 90), fontColour);
			spriteBatch.DrawString(fontTitle, "Tetris", new Vector2(75, 15), Color.CadetBlue);

			spriteBatch.DrawString(font, "Forma siguiente:", new Vector2(230, 80), baseTxt);
			spriteBatch.DrawString(font, "Puntuacion mayor:", new Vector2(230, 180), baseTxt);
			spriteBatch.DrawString(fontTitle, "" + highScore, new Vector2(230, 200), emTxt);
			
			spriteBatch.DrawString(font, "Para pausar/Resumir\nEl juego : ", new Vector2(230, 250), baseTxt);
			spriteBatch.DrawString(fontEm, "P", new Vector2(310, 270), emTxt);
			spriteBatch.DrawString(font, "key.", new Vector2(340, 270), baseTxt);
			spriteBatch.DrawString(font, "o", new Vector2(230, 290), baseTxt);
			spriteBatch.DrawString(fontEm, "Enter.", new Vector2(270, 290), emTxt);

			spriteBatch.DrawString(font, "Para entrar a modo fantasma", new Vector2(230, 330), baseTxt);
			spriteBatch.DrawString(fontEm, "G", new Vector2(230, 350), emTxt);
			spriteBatch.DrawString(font, "key.", new Vector2(250, 350), baseTxt);


			spriteBatch.DrawString(font, "Para dejar caer\n presione", new Vector2(230, 390), baseTxt);
			spriteBatch.DrawString(fontEm, "Arriba.", new Vector2(330, 410), emTxt);

			spriteBatch.End();
			base.Draw(gameTime);
		}

		//Displays the game over message.
		public void HandleGameOver()
		{
			gameover = "\nGAME OVER";
			fontColour = Color.Red;
		}

		//Determines the previous high score from the file.
		private void findHighScore()
		{
			try
			{
				if (!File.Exists("score.txt"))
				{
					File.WriteAllLines("score.txt", new string[1] { "0" });
				}
				else
				{
					string[] array = File.ReadAllLines("score.txt");
					highScore = Int32.Parse(array[0]);
				}
			}
			catch (IOException io)
            {}
		}

		//If the current score is higher than the previous high score, saves it to the file.
		private void saveResults(object sender, EventArgs e)
		{
			if (score.ScoreValue > highScore)
			{
				try
				{
					File.WriteAllLines("score.txt", new string[1] {score.ScoreValue + "" });
				}
				catch (IOException io)
				{ }
			}
		}
	}
}
