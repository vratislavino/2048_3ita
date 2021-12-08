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
    public partial class Cell : UserControl
    {
        private int value;

        private static Dictionary<int, Color> colors = new Dictionary<int, Color>() {
            { 0, SystemColors.Control },
            { 2, Color.FromArgb(255,255,255) },
            { 4, Color.FromArgb(237,237,237) },
            { 8, Color.FromArgb(219,219,219) },
            { 16, Color.FromArgb(201,201,201) },
            { 32, Color.FromArgb(183,183,183) },
            { 64, Color.FromArgb(165,165,165) },
            { 128, Color.FromArgb(147,147,147) },
            { 256, Color.FromArgb(129,129,129) },
            { 512, Color.FromArgb(111,111,111) },
            { 1024, Color.FromArgb(93,93,93) },
            { 2048, Color.FromArgb(75,75,75) }
        };

        public int Value {
            get { return value; }
            set {
                this.value = value;
                this.label1.Text = value == 0 ? "" : value.ToString();
                AdjustColor();
            }
        }

        public Cell() {
            InitializeComponent();
            Value = 0;
            SetStyle(ControlStyles.Selectable, false);
        }

        private void AdjustColor() {
            this.BackColor = colors[Value];
        }
    }
}
