using LINQtoTMX.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using LINQtoTMX;
using System.Collections.Generic;
using LINQtoTMX.Test.Test_References;
using System.Threading;
using LINQtoTMX.Serializable;

namespace LINQtoTMX.Test
{
    [TestClass]
    public class LanguageRepositoryTests
    {
        #region Setup/Tear down
        [TestInitialize]
        public void ScenarioSetup()
        {
            ScenarioTearDown();
            File.Copy(MockObjects.VALID_en_TmxFilePath.FullName, MockObjects.VALID_Update_TmxFilePath.FullName);
            File.Copy(MockObjects.VALID_en_TmxFilePath.FullName, MockObjects.VALID_Delete_TmxFilePath.FullName);
        }

        [TestCleanup]
        public void ScenarioTearDown()
        {
            // Add tests
            if (File.Exists(MockObjects.VALID_Add_TmxFilePath.FullName))
            {
                File.Delete(MockObjects.VALID_Add_TmxFilePath.FullName);
            }

            if (File.Exists(MockObjects.INVALID_Add_TmxFilePath.FullName))
            {
                File.Delete(MockObjects.INVALID_Add_TmxFilePath.FullName);
            }

            // Update tests
            if (File.Exists(MockObjects.VALID_Update_TmxFilePath.FullName))
            {
                File.Delete(MockObjects.VALID_Update_TmxFilePath.FullName);
            }

            // Delete tests
            if (File.Exists(MockObjects.VALID_Delete_TmxFilePath.FullName))
            {
                File.Delete(MockObjects.VALID_Delete_TmxFilePath.FullName);
            }
        }
        #endregion

        #region Get
        [TestMethod]
        public void Get_Valid_TmxFiles_By_Valid_LanguageCode_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();

            // Act
            List<TmxFile> result = repository.Get(MockObjects.VALID_LanguageRepositoryPath, "en");

            // Assert
            Assert.AreEqual(2, result.Count);
            foreach (TmxFile file in result)
            {
                Assert.AreEqual("en", file.LanguageCode);
            }
        }

        [TestMethod]
        public void Get_Valid_TmxFiles_By_Valid_LanguageCodePartial_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();

            // Act
            List<TmxFile> result = repository.Get(MockObjects.VALID_LanguageRepositoryPath, "en-en");

            // Assert
            Assert.AreEqual(2, result.Count);
            foreach (TmxFile file in result)
            {
                Assert.AreEqual("en", file.LanguageCode);
            }
        }

        [TestMethod]
        public void Get_Valid_TmxFiles_By_Invalid_LanguageCode_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();

            // Act
            List<TmxFile> result = repository.Get(MockObjects.VALID_LanguageRepositoryPath, "la");

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Get_Valid_TmxFiles_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();

            // Act
            List<TmxFile> result = repository.Get(MockObjects.VALID_LanguageRepositoryPath);

            // Assert
            Assert.AreEqual(6, result.Count);
        }
        #endregion

        #region Add
        [TestMethod]
        public void Add_Valid_TmxFile_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();

            // Act
            Assert.IsFalse(File.Exists(MockObjects.VALID_Add_TmxFilePath.FullName));
            bool result = repository.Add(MockObjects.VALID_Add_TmxFile);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(MockObjects.VALID_Add_TmxFilePath.FullName));
        }

        [TestMethod]
        public void Add_Invalid_TmxFile_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();

            // Act
            Assert.IsFalse(File.Exists(MockObjects.INVALID_Add_TmxFilePath.FullName));
            bool result = repository.Add(MockObjects.INVALID_Add_TmxFile);

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(File.Exists(MockObjects.INVALID_Add_TmxFilePath.FullName));
        }

        [TestMethod]
        public void Add_InvalidPath_TmxFile_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();

            // Act
            Assert.IsFalse(File.Exists(MockObjects.NonEXIST_Add_TmxFilePath.FullName));
            bool result = repository.Add(MockObjects.NonEXIST_Add_TmxFile);

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(File.Exists(MockObjects.NonEXIST_Add_TmxFilePath.FullName));
        }
        #endregion

        #region Update
        [TestMethod]
        public void Update_Valid_TmxFile_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();
            TmxFile current = (from tmx in repository.Get(MockObjects.VALID_TestFilesPath)
                               where tmx.FileInfo.FullName == MockObjects.VALID_Update_TmxFilePath.FullName
                               select tmx).First<TmxFile>();

            // Act
            bool result = repository.Update(MockObjects.VALID_Update_TmxFile);

            // Assert
            Assert.IsTrue(result);
            TmxFile updated = (from tmx in repository.Get(MockObjects.VALID_TestFilesPath)
                               where tmx.FileInfo.FullName == MockObjects.VALID_Update_TmxFilePath.FullName
                               select tmx).First<TmxFile>();
            Assert.AreNotEqual(current, updated);
            Assert.AreNotEqual(current.Data.Body.TranslationUnits.Count, updated.Data.Body.TranslationUnits.Count);
        }

        [TestMethod]
        public void Update_NonExist_TmxFile_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();

            // Act
            bool result = repository.Update(MockObjects.NonEXIST_Add_TmxFile);

            // Assert
            Assert.IsFalse(result);
        }
        #endregion

        #region Delete
        [TestMethod]
        public void Delete_Valid_TmxFile_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();

            // Act
            Assert.IsTrue(File.Exists(MockObjects.VALID_Delete_TmxFilePath.FullName));
            bool result = repository.Delete(MockObjects.VALID_Delete_TmxFile);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(File.Exists(MockObjects.VALID_Delete_TmxFilePath.FullName));
        }

        [TestMethod]
        public void Delete_NonExist_TmxFile_Test()
        {
            // Assign
            AbstractLanguageRepository repository = new LanguageRepository();

            // Act
            Assert.IsFalse(File.Exists(MockObjects.NonEXIST_Add_TmxFilePath.FullName));
            bool result = repository.Delete(MockObjects.NonEXIST_Add_TmxFile);

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(File.Exists(MockObjects.NonEXIST_Add_TmxFilePath.FullName));
        }
        #endregion

        #region Parse
        [TestMethod]
        [DeploymentItem("LINQtoTMX.dll")]
        public void Parse_Valid_Directory_Test()
        {
            // Assign
            LanguageRepository_Accessor parser = new LanguageRepository_Accessor();

            // Act
            List<TmxFile> result = parser.ParseDirectory(MockObjects.VALID_LanguageRepositoryPath);

            // Assert
            Assert.AreEqual(6, result.Count);
        }

        [TestMethod]
        [DeploymentItem("LINQtoTMX.dll")]
        public void Parse_Valid_File_Test()
        {
            // Assign
            LanguageRepository_Accessor parser = new LanguageRepository_Accessor();
            TmxFile expected = MockObjects.VALID_en_TmxFile;

            // Act
            TmxFile result = parser.ParseFile(MockObjects.VALID_en_TmxFilePath);

            // Assert
            // -- Assert FileInfo
            Assert.AreEqual(expected.FileInfo, result.FileInfo);
            // -- Assert Language code
            Assert.AreEqual(expected.LanguageCode, result.LanguageCode);
            Assert.AreNotEqual("Unkown", result.LanguageCode);
            // -- Assert Data
            // ---- Assert version
            Assert.AreEqual(expected.Data.Version, result.Data.Version);
            // ---- Assert header
            Assert.AreEqual(expected.Data.Header.CreationToolVersion, result.Data.Header.CreationToolVersion);
            Assert.AreEqual(expected.Data.Header.DataType, result.Data.Header.DataType);
            Assert.AreEqual(expected.Data.Header.SegmentType, result.Data.Header.SegmentType);
            Assert.AreEqual(expected.Data.Header.AdminLanguage, result.Data.Header.AdminLanguage);
            Assert.AreEqual(expected.Data.Header.SourceLanguage, result.Data.Header.SourceLanguage);
            Assert.AreEqual(expected.Data.Header.OriginalTranslationMemoryFormat, result.Data.Header.OriginalTranslationMemoryFormat);
            Assert.AreEqual(expected.Data.Header.CreationTool, result.Data.Header.CreationTool);
            // ---- Assert body
            // ------ Assert TranslationUnits
            foreach (TranslationUnit expectedTU in expected.Data.Body.TranslationUnits)
            {
                TranslationUnit unit = result.Data.Body.TranslationUnits
                    .First<TranslationUnit>(
                        tu => tu.TranslationUnitId == expectedTU.TranslationUnitId
                    );

                Assert.IsNotNull(unit);
                Assert.AreEqual(expectedTU.TranslationUnitId, unit.TranslationUnitId);

                // ------ Assert TranslationUnitVariant
                foreach (TranslationUnitVariant expectedTUV in expectedTU.TranslationUnitVariants)
                {
                    TranslationUnitVariant unitVar = unit.TranslationUnitVariants
                        .First<TranslationUnitVariant>(
                            tuv => tuv.Segment == expectedTUV.Segment
                        );

                    Assert.IsNotNull(unitVar);
                    Assert.AreEqual(expectedTUV.Language, unitVar.Language);
                    Assert.AreEqual(expectedTUV.Segment, unitVar.Segment);
                }
            }
        }
        #endregion
    }
}