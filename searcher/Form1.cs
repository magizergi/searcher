using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using searcher.Model;

namespace searcher
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<string>> Dups;
        
        public Form1()
        {
            Dictionary<string, string> aa = new Dictionary<string, string>();
            
            InitializeComponent();
            btnSearch.Click += BtnSearch_Click;
            btnSelectFolder.Click += BtnSelectFolder_Click;
            dgvFileNames.RowEnter += DgvFileNames_RowEnter;
        }

        private void DgvFileNames_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            KeyValuePair<string, List<string>> i = (KeyValuePair<string, List<string>>)
                dgvFileNames.Rows[e.RowIndex].DataBoundItem;

            dgvFilePaths.DataSource = i.Value.Select(x => new FileInfoModel(x)).ToList();
            dgvFilePaths.AutoResizeColumns();
            dgvFilePaths.Refresh();
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Dups = new Dictionary<string, List<string>>();

                // Only get files that begin with the letter "c".
                string[] files = Directory.GetFiles(txtFile.Text, "*", 
                    SearchOption.AllDirectories);

                Console.WriteLine("The number of files starting with c is {0}.", files.Length);
                foreach (string dir in files)
                {
                    Console.WriteLine(dir);
                }

                foreach(var filePath in files)
                {
                    string fileName = Path.GetFileName(filePath);
                    
                    if(!Dups.ContainsKey(fileName))
                    {
                        Dups.Add(fileName, new List<string>());
                        Dups[fileName].Add(filePath);
                    }
                    else
                    {
                        Dups[fileName].Add(filePath);
                    }
                }

                List<string> nonDup = Dups
                    .Where(x => x.Value.Count == 1)
                    .Select(x => x.Key).ToList();

                foreach(var x in nonDup)
                {
                    Dups.Remove(x);
                }
                
                LoadData();
            }
            catch (Exception er)
            {
                Console.WriteLine("The process failed: {0}", er.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            dgvFileNames.DataSource = Dups.ToList();
            dgvFilePaths.AutoResizeColumns();
            dgvFileNames.Refresh();
        }
    }
}
