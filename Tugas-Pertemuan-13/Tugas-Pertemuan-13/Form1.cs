using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas_Pertemuan_13
{
   public partial class Form1 : Form
   {
        
        public Form1()
        {
            InitializeComponent();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtNama.Text != "" || txtNim.Text != "" || cboJenisKelamin.Text != "" || cboKelas.Text != "" || cboProgramStudi.Text != "" || cboWaktuKuliah.Text != "")
            {
               
                using (var conn = new Parameter().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"Select* from TMahasiswa where[Nim] like '" + txtNama.Text + "%' and [Nama] like '" + txtNim.Text + "%' and [JenisKelamin] like '"
                        + cboJenisKelamin.Text + "%'  and [ProgramStudi] like '" + cboProgramStudi.Text + "%'  and [WaktuKuliah] like '" + cboWaktuKuliah.Text + "%' and [Kelas] like '" + cboKelas.Text + "%'";
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataSetMhs ds = new DataSetMhs();
                                while (reader.Read())
                                {
                                    var newRow = ds.Mahasiswa.NewMahasiswaRow();
                                    newRow.Nim = reader["Nim"].ToString();
                                    newRow.Nama = reader["Nama"].ToString();
                                    newRow.JenisKelamin = reader["JenisKelamin"].ToString();
                                    newRow.ProgramStudi = reader["ProgramStudi"].ToString();
                                    newRow.WaktuKuliah = reader["WaktuKuliah"].ToString();
                                    newRow.Kelas = reader["Kelas"].ToString();
                                    ds.Mahasiswa.AddMahasiswaRow(newRow);
                                }
                                RptMahasiswa rpt = new RptMahasiswa();
                                rpt.SetDataSource(ds);
                                this.CRV.ReportSource = rpt;
                            }
                            else
                            {
                                MessageBox.Show("Sorry, Data Kosong ..", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Sorry, Minimal Harus Ada Satu Kriteria..", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtNim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{tab}");
        }
        private void txtNama_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{tab}");
        }

        private void cboJenisKelamin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{tab}");
        }

        private void cboProgramStudi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{tab}");
        }

        private void cboWaktuKuliah_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{tab}");
        }

        private void cboKelas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{tab}");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtNim.Clear();
            this.txtNama.Clear();
            this.cboJenisKelamin.SelectedIndex = -1;
            this.cboProgramStudi.SelectedIndex = -1;
            this.cboWaktuKuliah.SelectedIndex = -1;
            this.cboKelas.SelectedIndex = -1;
        }
        

        

    }
}
