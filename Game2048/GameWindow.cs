using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2048
{
    public partial class GameWindow : Form
    {
        private Game game;

        public GameWindow() {
            InitializeComponent();

            game = new Game(4);
            game.Resize += OnGameResized;
            game.ResizePanel();
            this.Controls.Add(game);
        }

        private void OnGameResized(object sender, EventArgs e) {
            this.Size = new Size(game.Width + 16, game.Height + 38);
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e) {
            MessageBox.Show("Key down!");
        }
    }
}
