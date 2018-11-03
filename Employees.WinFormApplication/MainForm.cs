using Employees.Core.Model;
using Employees.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
                        records = LoadModel.EmpExperianceFromLines(File.ReadAllLines(dlg.FileName));
                        
                        if (records.Count == 0)
                        {
                            MessageBox.Show("The file has 0 records");
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
        }

        private void dgTeams_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                TeamWork selectedRow = (TeamWork)e.Row.DataBoundItem;
                periodsSource = new BindingSource();
                periodsSource.DataSource = selectedRow.WorkTogether;

                dgPeriods.DataSource = periodsSource;
            }
        }
    }
}
