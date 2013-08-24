using LINQtoTMX;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using LINQtoTMX.Test.Test_References;
using System.Linq;

namespace LINQtoTMX.Test
{
    [TestClass]
    public class TmxContextTest
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
            if (File.Exists(MockObjects.VALID_en_Add_ContextTmxFilePath.FullName))
            {
                File.Delete(MockObjects.VALID_en_Add_ContextTmxFilePath.FullName);
            }

            if (File.Exists(MockObjects.InvalidPath_en_Add_ContextTmxFilePath_Result.FullName))
            {
                File.Delete(MockObjects.InvalidPath_en_Add_ContextTmxFilePath_Result.FullName);
            }

            if (Directory.Exists(MockObjects.NewLanguageCode_la_Add_ContextTmxFilePath_Result.Directory.FullName))
            {
                Directory.Delete(MockObjects.NewLanguageCode_la_Add_ContextTmxFilePath_Result.Directory.FullName, true);
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

        [TestMethod]
        public void TmxContext_Constructor_Test()
        {
            // Act
            ITmxContext context = new TmxContext(MockObjects.VALID_LanguageRepositoryPath);

            // Assert
            Assert.AreEqual(MockObjects.VALID_LanguageRepositoryPath, context.Root);
        }

        #region Get
        [TestMethod]
        public void Valid_Get_Test()
        {
            // Assign
            TmxContext_Accessor context = new TmxContext_Accessor(MockObjects.VALID_LanguageRepositoryPath);
            string languageCode = "en";

            // Act
            List<TmxFile> result = context.Get(languageCode);

            // Assert
            Assert.AreEqual(2, result.Count);
            foreach (TmxFile file in result)
            {
                Assert.AreEqual("en", file.LanguageCode);
            }
            Assert.AreEqual(1, context.History.Count);
        }

        [TestMethod]
        public void Valid_Get_WithPrevious_Test()
        {
            // Assign
            TmxContext_Accessor context = new TmxContext_Accessor(MockObjects.VALID_LanguageRepositoryPath);
            string languageCode = "en";
            List<TmxFile> previous = context.Get(languageCode); // Make a call so that history is populated
            Assert.AreEqual(1, context.History.Count);

            // Act
            List<TmxFile> result = context.Get(languageCode);

            // Assert
            Assert.AreEqual(2, result.Count);
            for (int i = 0; i < previous.Count; i++)
            {
                Assert.AreEqual("en", result[i].LanguageCode);
                Assert.AreEqual(previous[i].Id, result[i].Id);
            }
            Assert.AreEqual(1, context.History.Count);
        }

        [TestMethod]
        public void Invalid_Get_Test()
        {
            // Assign
            TmxContext_Accessor context = new TmxContext_Accessor(MockObjects.VALID_LanguageRepositoryPath);
            string languageCode = "la";

            // Act
            List<TmxFile> result = context.Get(languageCode);

            // Assert
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(0, context.History.Count);
        }

        [TestMethod]
        public void GetAll_Test()
        {
            // Assign
            TmxContext_Accessor context = new TmxContext_Accessor(MockObjects.VALID_LanguageRepositoryPath);

            // Act
            List<TmxFile> result = context.GetAll();

            // Assert
            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(0, context.History.Count);
        }
        #endregion

        #region Add
        [TestMethod]
        public void Valid_Add_Test()
        {
            // Assign
            ITmxContext context = new TmxContext(MockObjects.VALID_LanguageRepositoryPath);
            int count = context.Get("en").Count;

            // Act
            bool result = context.Add(MockObjects.VALID_en_Add_ContextTmxFile);

            // Assert
            Assert.IsTrue(result);
            List<TmxFile> items = context.Get("en");
            Assert.AreEqual(count + 1, items.Count);
        }

        [TestMethod]
        public void InvalidPath_Add_Test()
        {
            // Assign
            ITmxContext context = new TmxContext(MockObjects.VALID_LanguageRepositoryPath);
            int count = context.Get("en").Count;
            TmxFile addFile = MockObjects.InvalidPath_en_Add_ContextTmxFile;

            // Act
            bool result = context.Add(addFile);

            // Assert
            Assert.IsTrue(result);
            List<TmxFile> items = context.Get("en");
            Assert.AreEqual(count + 1, items.Count);
            Assert.AreEqual(MockObjects.InvalidPath_en_Add_ContextTmxFilePath_Result.FullName, addFile.FileInfo.FullName);
        }

        [TestMethod]
        public void NewLanguageCode_Add_Test()
        {
            // Assign
            ITmxContext context = new TmxContext(MockObjects.VALID_LanguageRepositoryPath);
            int count = context.Get("la").Count;
            TmxFile addFile = MockObjects.NewLanguageCode_la_Add_ContextTmxFile;

            // Act
            bool result = context.Add(addFile);

            // Assert
            Assert.IsTrue(result);
            List<TmxFile> items = context.Get("la");
            Assert.AreEqual(count + 1, items.Count);
            Assert.AreEqual(MockObjects.NewLanguageCode_la_Add_ContextTmxFilePath_Result.FullName, addFile.FileInfo.FullName);
        }
        #endregion

        #region Update
        [TestMethod]
        public void Valid_Update_Test()
        {
            // Assign
            TmxContext_Accessor context = new TmxContext_Accessor(MockObjects.VALID_TestFilesPath);
            TmxFile current = (from tmx in context.Get("en")
                               where tmx.FileInfo.FullName == MockObjects.VALID_Update_TmxFilePath.FullName
                               select tmx).First<TmxFile>();

            // Act
            bool result = context.Update(current);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(current.isModified);
            Assert.IsFalse(current.isDeleted);
            Assert.IsFalse(current.isNew);
        }

        [TestMethod]
        public void ValidNew_Update_Test()
        {
            // Assign
            TmxContext_Accessor context = new TmxContext_Accessor(MockObjects.VALID_TestFilesPath);
            TmxFile newTmx = MockObjects.VALID_en_Add_ContextTmxFile;
            bool result = context.Add(newTmx);
            Assert.IsTrue(result, "Context Add method must be broken.");

            // Act
            result = context.Update(newTmx);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(newTmx.isModified);
            Assert.IsFalse(newTmx.isDeleted);
            Assert.IsTrue(newTmx.isNew);
        }

        [TestMethod]
        public void ValidDelete_Update_Test()
        {
            // Assign
            TmxContext_Accessor context = new TmxContext_Accessor(MockObjects.VALID_TestFilesPath);
            TmxFile current = (from tmx in context.Get("en")
                               where tmx.FileInfo.FullName == MockObjects.VALID_Update_TmxFilePath.FullName
                               select tmx).First<TmxFile>();
            bool result = context.Remove(current);
            Assert.IsTrue(result, "Context Delete method must be broken.");

            // Act
            result = context.Update(current);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(current.isModified);
            Assert.IsTrue(current.isDeleted);
            Assert.IsFalse(current.isNew);
        }
        #endregion

        #region Delete
        [TestMethod]
        public void Valid_Delete_Test()
        {
            // Assign
            TmxContext_Accessor context = new TmxContext_Accessor(MockObjects.VALID_TestFilesPath);
            int count = context.Get("en").Count;
            TmxFile current = (from tmx in context.Get("en")
                               where tmx.FileInfo.FullName == MockObjects.VALID_Delete_TmxFilePath.FullName
                               select tmx).First<TmxFile>();
            Guid id = current.Id;

            // Act
            bool result = context.Remove(current);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(current.isModified);
            Assert.IsTrue(current.isDeleted);
            Assert.IsFalse(current.isNew);
            Assert.AreEqual(count - 1, context.Get("en").Count);
        }

        [TestMethod]
        public void Invalid_Delete_Test()
        {
            // Assign
            TmxContext_Accessor context = new TmxContext_Accessor(MockObjects.VALID_TestFilesPath);
            int count = context.Get("en").Count;
            TmxFile fake = new TmxFile();
            Guid id = fake.Id;

            // Act
            bool result = context.Remove(fake);

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(fake.isModified);
            Assert.IsFalse(fake.isDeleted);
            Assert.IsFalse(fake.isNew);
            Assert.AreEqual(count, context.Get("en").Count);
        }
        #endregion

        #region Commit
        [TestMethod]
        public void ValidAdd_Commit_Test()
        {
            // Assign
            ITmxContext context = new TmxContext(MockObjects.VALID_TestFilesPath);
            bool pass = context.Add(MockObjects.VALID_en_Add_ContextTmxFile);
            Assert.IsTrue(pass, "Context Add method is broken");

            // Act
            int result = context.SaveChanges();

            // Assert
            Assert.AreEqual(1, result);
            Assert.IsTrue(File.Exists(MockObjects.VALID_en_Add_ContextTmxFilePath.FullName));
        }

        [TestMethod]
        public void ValidUpdate_Commit_Test()
        {
            // Assign
            ITmxContext context = new TmxContext(MockObjects.VALID_TestFilesPath);
            TmxFile update = (from tmx in context.Get("en")
                               where tmx.FileInfo.FullName == MockObjects.VALID_Update_TmxFilePath.FullName
                               select tmx).First<TmxFile>();
            bool pass = context.Update(update);
            Assert.IsTrue(pass, "Context Update method is broken");

            // Act
            int result = context.SaveChanges();

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ValidDelete_Commit_Test()
        {
            // Assign
            ITmxContext context = new TmxContext(MockObjects.VALID_TestFilesPath);
            TmxFile delete = (from tmx in context.Get("en")
                              where tmx.FileInfo.FullName == MockObjects.VALID_Delete_TmxFilePath.FullName
                              select tmx).First<TmxFile>();
            bool pass = context.Update(delete);
            Assert.IsTrue(pass, "Context Update method is broken");

            // Act
            int result = context.SaveChanges();

            // Assert
            Assert.AreEqual(1, result);
            Assert.IsTrue(File.Exists(MockObjects.VALID_Delete_TmxFilePath.FullName));
        }

        [TestMethod]
        public void ValidAll_Commit_Test()
        {
            // Assign
            ITmxContext context = new TmxContext(MockObjects.VALID_TestFilesPath);
            bool pass = context.Add(MockObjects.VALID_en_Add_ContextTmxFile);
            Assert.IsTrue(pass, "Context Add method is broken");
            TmxFile update = (from tmx in context.Get("en")
                              where tmx.FileInfo.FullName == MockObjects.VALID_Update_TmxFilePath.FullName
                              select tmx).First<TmxFile>();
            pass = context.Update(update);
            Assert.IsTrue(pass, "Context Update method is broken");
            TmxFile delete = (from tmx in context.Get("en")
                              where tmx.FileInfo.FullName == MockObjects.VALID_Delete_TmxFilePath.FullName
                              select tmx).First<TmxFile>();
            pass = context.Update(delete);
            Assert.IsTrue(pass, "Context Update method is broken");

            // Act
            int result = context.SaveChanges();

            // Assert
            Assert.AreEqual(3, result);
            Assert.IsTrue(File.Exists(MockObjects.VALID_en_Add_ContextTmxFilePath.FullName));
            Assert.IsTrue(File.Exists(MockObjects.VALID_Delete_TmxFilePath.FullName));
        }

        [TestMethod]
        public void ValidNoChanges_Commit_Test()
        {
            // Assign
            ITmxContext context = new TmxContext(MockObjects.VALID_LanguageRepositoryPath);
            List<TmxFile> files = context.Get("en");

            // Act
            int result = context.SaveChanges();

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ValidEmptyHistory_Commit_Test()
        {
            // Assign
            ITmxContext context = new TmxContext(MockObjects.VALID_LanguageRepositoryPath);

            // Act
            int result = context.SaveChanges();

            // Assert
            Assert.AreEqual(0, result);
        }
        #endregion
    }
}