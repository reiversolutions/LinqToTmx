using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LINQtoTMX.Internal;

namespace LINQtoTMX
{
    /// <summary>
    /// Language repository
    /// </summary>
    public class TmxContext : ITmxContext
    {
        #region private variables
        private List<List<TmxFile>> History = new List<List<TmxFile>>();
        private AbstractLanguageRepository repository = new LanguageRepository();
        #endregion

        /// <summary>
        /// Root directory of language repository
        /// </summary>
        public DirectoryInfo Root { get; set; }

        /// <summary>
        /// TmxContext constructor
        /// </summary>
        /// <param name="languageRepositoryRoot">Root directory path for Language Repository</param>
        public TmxContext(DirectoryInfo languageRepositoryRoot)
        {
            Root = languageRepositoryRoot;
        }

        /// <summary>
        /// Add a tmx file to the language repository
        /// </summary>
        /// <param name="tmxFile">Tmx file object</param>
        /// <returns>Sucessful</returns>
        public bool Add(TmxFile tmxFile)
        {
            // Set modifiers
            tmxFile.isNew = true;
            tmxFile.isModified = false;
            tmxFile.isDeleted = false;
            tmxFile.Id = Guid.NewGuid();

            // Check language is in history
            foreach (List<TmxFile> files in History)
            {
                if (files.First<TmxFile>().LanguageCode == tmxFile.LanguageCode)
                {
                    // Add files to list in history
                    if (!tmxFile.FileInfo.Directory.Exists)
                    {
                        tmxFile.FileInfo = new FileInfo(Path.Combine(Root.FullName, tmxFile.LanguageCode, tmxFile.FileInfo.Name));
                    }
                    files.Add(tmxFile);
                    return true;
                }
            }

            List<TmxFile> repository = Get(tmxFile.LanguageCode);
            if (repository.Count != 0)
            {
                // New list will have been added to history so recall Add
                return Add(tmxFile);
            } else
            {
                // Create new repository folder
                if (!Directory.Exists(tmxFile.FileInfo.Directory.FullName))
                {
                    string path = Path.Combine(Root.FullName, tmxFile.LanguageCode);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    tmxFile.FileInfo = new FileInfo(Path.Combine(path, tmxFile.FileInfo.Name));
                }

                History.Add(new List<TmxFile>()
                {
                    tmxFile
                });
                return true;
            }
        }

        /// <summary>
        /// Get tmx files as objects filtering by language code.
        /// </summary>
        /// <param name="languageCode">Language code</param>
        /// <returns>List of Tmx files as objects</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when tmx file is incorrectly formatted</exception>
        public List<TmxFile> Get(string languageCode)
        {
            // Check to see if repository has already been fetched
            foreach (List<TmxFile> historyItem in History)
            {
                if (historyItem[0].LanguageCode == languageCode)
                {
                    // Only return items that have not be previously deleted
                    List<TmxFile> result = historyItem.FindAll(tmx => !tmx.isDeleted);

                    return result;
                }
            }

            // Fetch repository
            List<TmxFile> data = repository.Get(Root, languageCode);

            // If repository exists add it to the history
            if (data.Count > 0)
            {
                History.Add(data);
            }

            return data;
        }

        /// <summary>
        /// <para>Get tmx files as objects</para>
        /// <para>#warning This method commits the current context in order to stop data being returned that may have been modified or removed.</para>
        /// </summary>
        /// <returns>List of Tmx files as objects</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when tmx file is incorrectly formatted</exception>
        public List<TmxFile> GetAll()
        {
            return repository.Get(Root);
        }

        /// <summary>
        /// Update a tmx file
        /// </summary>
        /// <param name="tmxFile">Tmx file to update</param>
        /// <returns>Sucessful</returns>
        public bool Update(TmxFile tmxFile)
        {
            foreach (List<TmxFile> files in History)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].Id == tmxFile.Id)
                    {
                        // Set modifiers
                        tmxFile.isModified = tmxFile.isDeleted || tmxFile.isNew ? false : true;

                        // Replace file in history
                        files[i] = tmxFile;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Delete a tmx file
        /// </summary>
        /// <param name="tmxFile">Tmx file to delete</param>
        /// <returns>Sucessful</returns>
        public bool Remove(TmxFile tmxFile)
        {
            foreach (List<TmxFile> files in History)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].Id == tmxFile.Id)
                    {
                        // Set modifiers
                        tmxFile.isNew = false;
                        tmxFile.isModified = false;
                        tmxFile.isDeleted = true;

                        // Replace file in history
                        files[i] = tmxFile;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Commit changes to language repository
        /// </summary>
        /// <returns>Number of writes</returns>
        public int SaveChanges()
        {
            int count = 0;

            foreach (List<TmxFile> files in History)
            {
                foreach (TmxFile file in files)
                {
                    if (file.isNew)
                    {
                        // Add file to the language repository
                        if (repository.Add(file))
                        {
                            // Reset object modifiers
                            file.isDeleted = false;
                            file.isModified = false;
                            file.isNew = false;

                            count++;
                        }
                    } else if (file.isModified)
                    {
                        // Update file in the language repository
                        if (repository.Update(file))
                        {
                            // Reset object modifiers
                            file.isDeleted = false;
                            file.isModified = false;
                            file.isNew = false;

                            count++;
                        }
                    } else if (file.isDeleted)
                    {
                        // Remove file from language repository
                        if (repository.Delete(file))
                        {
                            // Remove file from history
                            files.Remove(file);

                            count++;
                        }
                    }
                }
            }

            return count;
        }
    }
}