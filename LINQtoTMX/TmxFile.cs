using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LINQtoTMX.Serializable;

namespace LINQtoTMX
{
    /// <summary>
    /// Tmx file object
    /// </summary>
    public class TmxFile
    {
        /// <summary>
        /// Uniquely identify a tmx file even if it 
        /// has been updated completely
        /// </summary>
        internal Guid Id { get; set; }

        /// <summary>
        /// Information about the file
        /// </summary>
        public FileInfo FileInfo { get; set; }

        /// <summary>
        /// Specfic Language code for tmx file
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Data in tmx file as an object
        /// </summary>
        public Tmx Data { get; set; }

        #region object modifiers
        internal bool isNew { get; set; }
        internal bool isModified { get; set; }
        internal bool isDeleted { get; set; }
        #endregion
    }
}
