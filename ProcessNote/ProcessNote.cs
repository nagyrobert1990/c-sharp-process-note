using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (commentBox.Text == "" || commentBox.Text.Equals(null)) {
                label1.ForeColor = Color.Red;
                label1.Text = "Fill the text box";
                label1.Visible = true;
            }
            else
            {
                note.Comment = commentBox.Text;
                label1.ForeColor = Color.Green;
                label1.Text = "You saved successfully!";
                label1.Visible = true;
                commentBox.Text = "";
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            checkCommentBox();
            loadSelectedProcess();
            dataGridView2.Visible = true;
            refreshSelected.Visible = true;
        }

        private void loadSelectedProcess()
        {
            int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            Process process = Process.GetProcessById(selectedId);

            var table = new DataTable("Selected Process");

            table.Columns.Add("Process ID");
            table.Columns.Add("Process Name");
            table.Columns.Add("CPU usage");
            table.Columns.Add("Memory usage");
            table.Columns.Add("Running time");
            table.Columns.Add("Start time");

            try
            {
                    table.Rows.Add(new object[] {
                    process.Id,
                    process.ProcessName,
                    process.TotalProcessorTime,
                    process.PagedMemorySize64,
                    (DateTime.Now - process.StartTime),
                    process.StartTime
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            table.AcceptChanges();
            BindingSource source = new BindingSource();
            source.DataSource = table;

            int scroll = dataGridView2.FirstDisplayedScrollingRowIndex;
            dataGridView2.DataSource = source;

            if (scroll != -1)
                dataGridView2.FirstDisplayedScrollingRowIndex = scroll;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            refreshSelected.Visible = false;

            checkCommentBox();

            var table = new DataTable("Process List");

            Process[] processes = Process.GetProcesses();

            table.Columns.Add("Id");
            table.Columns.Add("Name");

            for (int i = 0; i < processes.Length; ++i)
            {
                table.Rows.Add(new object[] { processes[i].Id, processes[i].ProcessName });
            }

            table.AcceptChanges();
            BindingSource source = new BindingSource();
            source.DataSource = table;

            int scroll = dataGridView1.FirstDisplayedScrollingRowIndex;
            dataGridView1.DataSource = source;

            if (scroll != -1)
                dataGridView1.FirstDisplayedScrollingRowIndex = scroll;
        }

        private void refreshSelected_Click(object sender, EventArgs e)
        {
            checkCommentBox();

            dataGridView2.Visible = false;
            loadSelectedProcess();
            dataGridView2.Visible = true;
        }

        private void checkCommentBox()
        {
            if (!commentBox.Text.Equals(null) && !commentBox.Text.Equals(""))
            {
                label1.Text = "You have an unsaved comment!";
                label1.ForeColor = Color.Red;
                label1.Visible = true;
            }
            else
            {
                label1.Visible = false;
            }
        }
    }
}
