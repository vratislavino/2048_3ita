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
            this.Focus();
        }

        private void GenerateCells() {
            cells = new Cell[size, size];
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    Cell c = new Cell();
                    Controls.Add(c);
                    cells[i, j] = c;
                    c.Location = new Point(i * c.Width, j * c.Height);
                }
            }
        }

        public void ResizePanel() {
            this.Size = new Size(cells[0, 0].Width * size, cells[0, 0].Height * size);
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

            if (cells[x, y].Value == 0) {
                cells[x, y].Value = num;
            } else {
                AddRandomNumber(num);
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e) {
            var key = e.KeyCode;
            switch (key) {
                case Keys.A: MoveRow(true); break;
                case Keys.D: MoveRow(false); break;
                case Keys.W: MoveColumn(true); break;
                case Keys.S: MoveColumn(false); break;
            }
        }

        private void MoveRow(bool isNegative) {
            List<Cell> row = new List<Cell>();
            for (int i = 0; i < 4; i++) { // cyklus pro řádky
                for (int j = 0; j < 4; j++) {
                    if (cells[j, i].Value > 0)
                        row.Add(cells[j, i]);
                }
                // Máme list obsahující posloupnost nenulových čtverečků
                // Spojování
                if (row.Count > 1) {
                    if(isNegative)
                        row.Reverse(); // otočení listu, pokud potřebujeme jet zprava
                    for (int j = 0; j < row.Count - 1; j++) {
                        if (row[j].Value == row[j + 1].Value) {
                            row[j].Value *= 2;
                            row.RemoveAt(j + 1);
                            // SPOJIT 2 PRVKY
                        }
                    }
                    if(isNegative)
                        row.Reverse();
                }
                // srovnání na stranu
                if (isNegative) {
                    for (int j = 0; j < 4; j++) {
                        if (j < row.Count) {
                            cells[j, i].Value = row[j].Value;
                        } else {
                            cells[j, i].Value = 0;
                        }
                    }
                } else {
                    for (int j = 3; j >= 0; j--) {
                        if (row.Count > 0) {
                            cells[j, i].Value = row.Last().Value;
                            row.RemoveAt(row.Count - 1);
                        } else {
                            cells[j, i].Value = 0;
                        }
                    }
                }
                row.Clear();
            }
        }

        private void MoveColumn(bool isNegative) {
            MessageBox.Show("Column" + isNegative);
        }
    }
}
