using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACBr.Net.CTe.DACTe.FastReport.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var dlg = new OpenFileDialog
            {
                Title = "Carrgar imagem",
                FileName = "",
                DefaultExt = ".png",
                Filter = "Imagem (.png) | *.png"
            };
            dlg.ShowDialog();
            string img = dlg.FileName;
            if (!File.Exists(img))
                return;

            pcbLogotipo.Image = Bitmap.FromFile(img);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pcbLogotipo.Image = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Carrgar relatório",
                FileName = "",
                DefaultExt = ".frx",
                Filter = "Relatório em Fast Reports (.frx) | *.frx"
            };
            dlg.ShowDialog();
            string frx = dlg.FileName;
            if (File.Exists(frx))
                txtArquivo.Text = frx;
        }

        private CTeProc GetCTe()
        {
            var dlg = new OpenFileDialog
            {
                Title = "Carrgar xml CTeProc",
                FileName = "",
                DefaultExt = ".xml",
                Filter = "Arquivo XML(.xml) | *.xml"
            };
            dlg.ShowDialog();
            string xml = dlg.FileName;
            if (!File.Exists(xml))
                return null;

            return CTeProc.Load(xml);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cte = GetCTe();

            if (cte != null)
            {
                var dacte = new DACTeFastReport()
                {
                    Filtro = DFe.Core.Common.FiltroDFeReport.Nenhum,
                    FilePath = txtArquivo.Text,
                    QuebrarLinhasObservacao = chbQuebrarLinhaObservacao.Checked,
                    MostrarPreview = (sender == button1),
                    ShowDesign = (sender == button5),
                    Logo = pcbLogotipo.Image,
                    SoftwareHouse = txtSoftwareHouse.Text,
                    Site = txtSite.Text,
                };

                dacte.Imprimir(new CTeProc[] { cte });
            }
        }

    }
}
