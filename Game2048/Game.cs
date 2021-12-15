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
                case Keys.A: MoveGroup(true, true); break;
                case Keys.D: MoveGroup(false, true); break;
                case Keys.W: MoveGroup(true, false); break;
                case Keys.S: MoveGroup(false, false); break;
            }
            if (!IsEnd()) {
                if (IsAnyPlaceEmpty()) {
                    AddRandomNumber(GenerateNumber());
                } else {
                    MessageBox.Show("Nebuď bukva, koukej trochu...");
                }
            } else {
                MessageBox.Show("Konec hry!");
            }
        }

        private bool IsAnyPlaceEmpty() {
            foreach (var cell in cells) {
                if (cell.Value == 0) // našlo se prázdné pole, není konec hry
                    return true;
            }
            return false;
        }

        private bool IsEnd() {
            foreach(var cell in cells) {
                if (cell.Value == 0) // našlo se prázdné pole, není konec hry
                    return false; 
            }

            List<Cell> group = new List<Cell>();
            for (int k = 0; k <= 1; k++) {
                for (int i = 0; i < size; i++) {
                    group = GetGroup(k == 0, i, group);
                    for (int j = 0; j < group.Count - 1; j++) {
                        if (group[j].Value == group[j + 1].Value) {
                            return false;
                        }
                    }
                    group.Clear();
                }
            }

            return true;
        }

        private Cell GetCell(bool isRow, int i, int j) {
            return isRow ? cells[j, i] : cells[i, j];
        }

        private List<Cell> GetGroup(bool isRow, int i, List<Cell> group) { 
            for (int j = 0; j < 4; j++) {
                if (GetCell(isRow, i, j).Value > 0)
                    group.Add(GetCell(isRow, i, j));
            }
            return group;
        }

        private void MoveGroup(bool isNegative, bool isRow) {
            List<Cell> group = new List<Cell>();
            for (int i = 0; i < size; i++) { // cyklus pro řádky
                group = GetGroup(isRow, i, group);
                // Máme list obsahující posloupnost nenulových čtverečků
                // Spojování
                if (group.Count > 1) {
                    if (isNegative)
                        group.Reverse(); // otočení listu, pokud potřebujeme jet zprava
                    for (int j = 0; j < group.Count - 1; j++) {
                        if (group[j].Value == group[j + 1].Value) {
                            group[j].Value *= 2;
                            group.RemoveAt(j + 1);
                            // SPOJIT 2 PRVKY
                        }
                    }
                    if (isNegative)
                        group.Reverse();
                }
                // srovnání na stranu
                if (isNegative) {
                    for (int j = 0; j < 4; j++) {
                        if (j < group.Count) {
                            GetCell(isRow, i, j).Value = group[j].Value;
                        } else {
                            GetCell(isRow, i, j).Value = 0;
                        }
                    }
                } else {
                    for (int j = 3; j >= 0; j--) {
                        if (group.Count > 0) {
                            GetCell(isRow, i, j).Value = group.Last().Value;
                            group.RemoveAt(group.Count - 1);
                        } else {
                            GetCell(isRow, i, j).Value = 0;
                        }
                    }
                }
                group.Clear();
            }
        }
    }
}
