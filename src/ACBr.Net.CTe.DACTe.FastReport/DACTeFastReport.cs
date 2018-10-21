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
using System.Collections.Generic;
using System.IO;
using ACBr.Net.CTe.DACTe.FastReport.Properties;

namespace ACBr.Net.CTe.DACTe.FastReport
{
    public sealed class DACTeFastReport : DACTeBase
    {
        #region Fields

        private Report internalReport;

        #endregion Fields

        #region Events

        public event EventHandler<DACTeEventArgs> OnGetDACTe;

        #endregion Events

        #region Properties

        public bool ShowDesign { get; set; }

        #endregion Properties

        #region Methods

        public override void Imprimir(CTeProc[] conhecimentos)
        {
            PreparaReport(conhecimentos, CTeLayout.DACTe);
            Print();
        }

        public override void ImprimirPDF(CTeProc[] conhecimentos)
        {
            Filtro = FiltroDFeReport.PDF;
            PreparaReport(conhecimentos, CTeLayout.DACTe);
            Print();
        }

        public override void ImprimirEvento(CTeProcEvento[] eventos)
        {
            throw new NotImplementedException();
        }

        public override void ImprimirEventoPDF(CTeProcEvento[] eventos)
        {
            throw new NotImplementedException();
        }

        public override void ImprimirInutilizacao(InutilizaoResposta inutilizao)
        {
            throw new NotImplementedException();
        }

        public override void ImprimirInutilizacaoPDF(InutilizaoResposta inutilizao)
        {
            throw new NotImplementedException();
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

        private void PreparaReport(IEnumerable<CTeProc> conhecimentos, CTeLayout tipo)
        {
            internalReport = new Report();

            var e = new DACTeEventArgs(tipo, LayoutImpressao);
            OnGetDACTe.Raise(this, e);

            if (e.FilePath.IsEmpty())
            {
                MemoryStream ms;
                switch (tipo)
                {
                    case CTeLayout.DACTe:
                        switch (LayoutImpressao)
                        {
                            case DACTeLayout.Retrato:
                                ms = new MemoryStream(Resources.DACTeRetrato);
                                break;

                            case DACTeLayout.Paisagem:
                                ms = new MemoryStream(Resources.DACTePaisagem);
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;

                    case CTeLayout.Evento:
                        ms = new MemoryStream(Resources.DACTeEvento);
                        break;

                    case CTeLayout.Inutilizacao:
                        ms = new MemoryStream(Resources.DACTeInutilizacao);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(tipo), tipo, null);
                }

                internalReport.Load(ms);
            }
            else
                internalReport.Load(e.FilePath);

            internalReport.RegisterData(conhecimentos, "CTeProc");
            internalReport.SetParameterValue("Logo", Logo.ToByteArray());
            internalReport.SetParameterValue("QuebrarLinhasObservacao", QuebrarLinhasObservacao);
            internalReport.SetParameterValue("SoftwareHouse", SoftwareHouse);
            internalReport.SetParameterValue("Site", Site);

            internalReport.PrintSettings.Copies = NumeroCopias;
            internalReport.PrintSettings.Printer = Impressora;
            internalReport.PrintSettings.ShowDialog = MostrarSetup;
        }

        #endregion Methods

        #region Overrides

        protected override void OnInitialize()
        {
            base.OnInitialize();

            ShowDesign = false;
        }

        #endregion Overrides
    }
}