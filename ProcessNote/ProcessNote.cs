using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessNote
{
    public partial class ProcessNote : Form
    {
        Note note = new Note();

        public ProcessNote()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (commentBox.Text == "" || commentBox.Text.Equals(null)) {
                label1.Text = "Fill the text box";
                label1.Visible = true;
            }
            else
            {
                note.Comment = commentBox.Text;
                label1.Text = "You saved successfully!";
                label1.Visible = true;
            }
        }
    }
}
