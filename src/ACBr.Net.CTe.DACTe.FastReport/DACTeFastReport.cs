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

using ACBr.Net.CTe.Services;

namespace ACBr.Net.CTe.DACTe.FastReport
{
    public sealed class DACTeFastReport : DACTeBase
    {
        public override void Imprimir(CTeProc[] conhecimentos)
        {
            throw new System.NotImplementedException();
        }

        public override void ImprimirPDF(CTeProc[] conhecimentos)
        {
            throw new System.NotImplementedException();
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

        protected override void OnInitialize()
        {
            //
        }

        protected override void OnDisposing()
        {
            //
        }
    }
}