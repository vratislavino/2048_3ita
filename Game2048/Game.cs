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
    public partial class Game : UserControl
    {
        private int size;
        Cell[,] cells;
        Random random = new Random();

        public Game() {
            InitializeComponent();
        }

        public Game(int size) : this() {
            this.size = size;
            GenerateCells();

            StartGame();
        }

        private void GenerateCells() {
            cells = new Cell[size, size];
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    Cell c = new Cell();
                    Controls.Add(c);
                    cells[i, j] = c;
                    c.Location = new Point(i * c.Width, j * c.Height);
                }
            }
        }

        public void ResizePanel() {
            this.Size = new Size(cells[0,0].Width * size, cells[0, 0].Height * size);
        }

        private void StartGame() {
            AddRandomNumber(GenerateNumber());
            AddRandomNumber(GenerateNumber());
        }

        private int GenerateNumber() {
            return random.Next(10) < 7 ? 2 : 4;
        }

        private void AddRandomNumber(int num) {
            int x = random.Next(cells.GetLength(0));
            int y = random.Next(cells.GetLength(1));
            
            if(cells[x,y].Value == 0) {
                cells[x, y].Value = num;
            } else {
                AddRandomNumber(num);
            }
        }
    }
}
