using Employees.Core.Model;
using Employees.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Employees.WinFormApplication
{
    public partial class MainForm : Form
    {
        List<EmployeeExp> records;
        BindingSource periodsSource;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        records = LoadModel.ParseEmployeeExp(File.ReadAllLines(dlg.FileName), tbDelimiter.Text, tbDateFormat.Text);

                        if (records.Count < 2)
                        {
                            MessageBox.Show("The file has not enoough records");
                        }
                        else
                        {
                            tbFilename.Text = Path.GetFileName(dlg.FileName);
                            tbCount.Text = records.Count.ToString();
                            LoadTeamsInGrid();
                            pMain.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }

                }
            }
        }

        private void LoadTeamsInGrid()
        {
            List<TeamWork> teamWork = Statistics.GetTeamWorkPeriods(records);
            BindingSource source = new BindingSource
            {
                DataSource = teamWork
            };

            dgTeams.DataSource = source;

            TeamWork maxDays = teamWork.OrderBy(x => x.TotalDays).Last();
            lblMax.Text = $"Team {maxDays.TeamKey} has the longest experiance together {maxDays.TotalDays} days.";
        }

        private void dgTeams_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                TeamWork selectedRow = (TeamWork)e.Row.DataBoundItem;
                periodsSource = new BindingSource
                {
                    DataSource = selectedRow.WorkTogether
                };

                dgPeriods.DataSource = periodsSource;
            }
        }

        private void cbDateFormat_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                tbDateFormat.Enabled = true;
            }
            else
            {
                tbDateFormat.Enabled = false;
                tbDateFormat.Text = string.Empty;

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                tbDelimiter.Enabled = true;
            }
            else
            {
                tbDelimiter.Enabled = false;
                tbDelimiter.Text = string.Empty;
            }
        }
    }
}
