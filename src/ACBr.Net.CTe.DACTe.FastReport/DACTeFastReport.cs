// ***********************************************************************
// Assembly         : ACBr.Net.CTe.DACTe.FastReport
// Author           : RFTD
// Created          : 09-30-2018
//
// Last Modified By : RFTD
// Last Modified On : 09-30-2018
// ***********************************************************************
// <copyright file="DACTeFastReport.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2016 Grupo ACBr.Net
//
//	 Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//	 The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//	 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ACBr.Net.Core.Extensions;
using ACBr.Net.CTe.Services;
using ACBr.Net.DFe.Core.Common;
using FastReport;
using FastReport.Export.Html;
using FastReport.Export.Pdf;
using System;
using System.IO;

namespace ACBr.Net.CTe.DACTe.FastReport
{
    public sealed class DACTeFastReport : DACTeBase
    {
        #region Fields

        private Report internalReport;

        #endregion Fields

        #region Construtores
        public DACTeFastReport()
        {
            FilePath = string.Empty;
            QuebrarLinhasObservacao = true;
            ShowDesign = false;
        }
        #endregion

        #region Propriedades

        public string FilePath { get; set; }

        public bool QuebrarLinhasObservacao { get; set; }

        public bool ShowDesign { get; set; }        

        #endregion Propriedades

        public override void Imprimir(CTeProc[] conhecimentos)
        {
            PreparaReport(conhecimentos);            
            Print();
        }

        public override void ImprimirPDF(CTeProc[] conhecimentos)
        {
            Filtro = FiltroDFeReport.PDF;
            PreparaReport(conhecimentos);
            Print();
        }

        public override void ImprimirEvento(CTeProcEvento[] eventos)
        {
            throw new System.NotImplementedException();
        }

        public override void ImprimirEventoPDF(CTeProcEvento[] eventos)
        {
            throw new System.NotImplementedException();
        }

        public override void ImprimirInutilizacao(InutilizaoResposta inutilizao)
        {
            throw new System.NotImplementedException();
        }

        public override void ImprimirInutilizacaoPDF(InutilizaoResposta inutilizao)
        {
            throw new System.NotImplementedException();
        }

        private void Print()
        {
            if (ShowDesign)
            {
                internalReport.Design();
            }
            else
            {
                internalReport.Prepare();

                switch (Filtro)
                {
                    case FiltroDFeReport.Nenhum:
                        if (MostrarPreview)
                            internalReport.Show();
                        else
                            internalReport.Print();
                        break;

                    case FiltroDFeReport.PDF:
                        var pdfExport = new PDFExport
                        {
                            EmbeddingFonts = true,
                            ShowProgress = MostrarSetup,
                            PdfCompliance = PDFExport.PdfStandard.PdfA_3b,
                            OpenAfterExport = MostrarPreview
                        };

                        internalReport.Export(pdfExport, NomeArquivo);
                        break;

                    case FiltroDFeReport.HTML:
                        var htmlExport = new HTMLExport
                        {
                            Format = HTMLExportFormat.MessageHTML,
                            EmbedPictures = true,
                            Preview = MostrarPreview,
                            ShowProgress = MostrarSetup
                        };

                        internalReport.Export(htmlExport, NomeArquivo);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            internalReport.Dispose();
            internalReport = null;
        }

        private void PreparaReport(CTeProc[] conhecimentos)
        {
            internalReport = new Report();

            if (string.IsNullOrEmpty(FilePath))
            {
                MemoryStream ms;
                ms = new MemoryStream(Properties.Resources.DACTeRetrato);
                internalReport.Load(ms);
            }
            else
                internalReport.Load(FilePath);

            internalReport.RegisterData(conhecimentos, "CTeProc");
            internalReport.SetParameterValue("Logo", Logo.ToByteArray());
            internalReport.SetParameterValue("QuebrarLinhasObservacao", QuebrarLinhasObservacao);
            internalReport.SetParameterValue("SoftwareHouse", SoftwareHouse);
            internalReport.SetParameterValue("Site", Site);

            internalReport.PrintSettings.Copies = NumeroCopias;
            internalReport.PrintSettings.Printer = Impressora;
            internalReport.PrintSettings.ShowDialog = MostrarSetup;
        }

        #region Overrides

        protected override void OnInitialize()
        {
            //
        }

        protected override void OnDisposing()
        {
            //
        }

        #endregion Overrides
    }
}