namespace PayRollEntryApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms; 
    public partial class JVCreator : Form
    {
       private Stream myStream;
       private List<JVObject> jvsList = new List<JVObject>();
        public JVCreator()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                var documentNumber = B1Helper.CreateJournalEntry(jvsList, txtOutput);
                txtOutput.AppendText(string.Concat("\n A new JV created with ID: ", documentNumber));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Concat("\n Ann error occured at btnImport_Click", ex.Message));
                txtOutput.AppendText(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            btnImport.Enabled = true;
            ofdFileDialogue.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofdFileDialogue.FilterIndex = 2;
            ofdFileDialogue.RestoreDirectory = true;

            if (ofdFileDialogue.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = ofdFileDialogue.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            txtFilePath.Text = ofdFileDialogue.FileName;
                            jvsList = ReadDelimitedFile(myStream);
                        }
                    }
                }
                catch (Exception ex)
                {
                   txtOutput.AppendText( "\n Error: Could not read file from disk. Original error: " + ex.Message);
                    btnImport.Enabled = false;
                }
            }
        }

        private static List<JVObject> ReadDelimitedFile(Stream docStream)
        {
            var sepList = new List<JVObject>();
            int x = 0;
            using (var file = new StreamReader(docStream))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (x > 0)
                    {
                        var delimiters = new char[] { '\t' };
                        var segments = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                        JVObject jv = new JVObject();
                        jv.PayrollDate = DateTime.ParseExact(segments[0], "yyyyMMdd", CultureInfo.InvariantCulture);
                        jv.GLAccount = Convert.ToString(segments[1]);
                        jv.Debit = Convert.ToDouble(segments[2]);
                        jv.Credit = Convert.ToDouble(segments[3]);
                        jv.CostCenter = Convert.ToString(segments[4]);
                        sepList.Add(jv);
                    }
                    x++;
                }
                x = 0;
                file.Close();
            }
            return sepList;
        }

        private void Calculate(int i)
        {
            double pow = Math.Pow(i, i);
        }

 
    }
}
