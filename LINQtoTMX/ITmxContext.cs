using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LINQtoTMX
{
    /// <summary>
    /// Interface to a language repository
    /// </summary>
    public interface ITmxContext
    {
        /// <summary>
        /// Root directory of language repository
        /// </summary>
        DirectoryInfo Root { get; set; }

        /// <summary>
        /// Add a tmx file to the language repository
        /// </summary>
        /// <param name="tmxFile">Tmx file object</param>
        /// <returns>Sucessful</returns>
        bool Add(TmxFile tmxFile);

        /// <summary>
        /// Get tmx files as objects filtering by language code.
        /// </summary>
        /// <param name="languageCode">Language code</param>
        /// <returns>List of Tmx files as objects</returns>
        List<TmxFile> Get(string languageCode);

        /// <summary>
        /// Get tmx files as objects
        /// </summary>
        /// <returns>List of Tmx files as objects</returns>
        List<TmxFile> GetAll();

        /// <summary>
        /// Update a tmx file
        /// </summary>
        /// <param name="tmxFile">Tmx file to update</param>
        /// <returns>Sucessful</returns>
        bool Update(TmxFile tmxFile);

        /// <summary>
        /// Delete a tmx file
        /// </summary>
        /// <param name="tmxFile">Tmx file to delete</param>
        /// <returns>Sucessful</returns>
        bool Remove(TmxFile tmxFile);

        /// <summary>
        /// Commit changes to language repository
        /// </summary>
        /// <returns>Number of writes</returns>
        int SaveChanges();
    }
}
