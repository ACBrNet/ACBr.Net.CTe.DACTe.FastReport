using System;

namespace ACBr.Net.CTe.DACTe.FastReport
{
    public sealed class DACTeEventArgs : EventArgs
    {
        #region Constructors

        public DACTeEventArgs(CTeLayout tipo, DACTeLayout layout)
        {
            Tipo = tipo;
            Layout = layout;
            FilePath = string.Empty;
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        /// Retorna o tipo de arquivo necessario.
        /// </summary>
        /// <value>The tipo.</value>
        public CTeLayout Tipo { get; internal set; }

        public DACTeLayout Layout { get; internal set; }

        /// <summary>
        /// Define ou retorna o caminho para o arquivo do FastReport.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath { get; set; }

        #endregion Propriedades
    }
}