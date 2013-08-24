using LINQtoTMX.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using LINQtoTMX;
using System.Collections.Generic;
using LINQtoTMX.Test.Test_References;
using LINQtoTMX.Serializable;

namespace LINQtoTMX.Test
{
    [TestClass]
    public class TmxParserTests
    {
        #region Parse method Tests
        [TestMethod]
        [DeploymentItem("LINQtoTMX.dll")]
        public void Parse_Valid_en_TmxFile_To_TmxObject_Test()
        {
            // Assign
            TmxParser_Accessor parser = new TmxParser_Accessor();
            Tmx expected = MockObjects.VALID_en_TmxObject;

            // Act
            Tmx result = parser.Parse(MockObjects.VALID_en_TmxFilePath);

            // Assert
            // -- Assert version
            Assert.AreEqual(expected.Version, result.Version);
            // -- Assert header
            Assert.AreEqual(expected.Header.CreationToolVersion, result.Header.CreationToolVersion);
            Assert.AreEqual(expected.Header.DataType, result.Header.DataType);
            Assert.AreEqual(expected.Header.SegmentType, result.Header.SegmentType);
            Assert.AreEqual(expected.Header.AdminLanguage, result.Header.AdminLanguage);
            Assert.AreEqual(expected.Header.SourceLanguage, result.Header.SourceLanguage);
            Assert.AreEqual(expected.Header.OriginalTranslationMemoryFormat, result.Header.OriginalTranslationMemoryFormat);
            Assert.AreEqual(expected.Header.CreationTool, result.Header.CreationTool);
            // -- Assert body
            // ---- Assert TranslationUnits
            foreach (TranslationUnit expectedTU in expected.Body.TranslationUnits)
            {
                TranslationUnit unit = result.Body.TranslationUnits
                    .First<TranslationUnit>(
                        tu => tu.TranslationUnitId == expectedTU.TranslationUnitId
                    );

                Assert.IsNotNull(unit);
                Assert.AreEqual(expectedTU.TranslationUnitId, unit.TranslationUnitId);

                // ---- Assert TranslationUnitVariant
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

        [TestMethod]
        [DeploymentItem("LINQtoTMX.dll")]
        public void Parse_Valid_pseudo_TmxFile_To_TmxObject_Test()
        {
            // Assign
            TmxParser_Accessor parser = new TmxParser_Accessor();
            Tmx expected = MockObjects.VALID_pseudo_TmxObject;

            // Act
            Tmx result = parser.Parse(MockObjects.VALID_pseudo_TmxFilePath);

            // Assert
            // -- Assert version
            Assert.AreEqual(expected.Version, result.Version);
            // -- Assert header
            Assert.AreEqual(expected.Header.CreationToolVersion, result.Header.CreationToolVersion);
            Assert.AreEqual(expected.Header.DataType, result.Header.DataType);
            Assert.AreEqual(expected.Header.SegmentType, result.Header.SegmentType);
            Assert.AreEqual(expected.Header.AdminLanguage, result.Header.AdminLanguage);
            Assert.AreEqual(expected.Header.SourceLanguage, result.Header.SourceLanguage);
            Assert.AreEqual(expected.Header.OriginalTranslationMemoryFormat, result.Header.OriginalTranslationMemoryFormat);
            Assert.AreEqual(expected.Header.CreationTool, result.Header.CreationTool);
            // -- Assert body
            // ---- Assert TranslationUnits
            foreach (TranslationUnit expectedTU in expected.Body.TranslationUnits)
            {
                TranslationUnit unit = result.Body.TranslationUnits
                    .First<TranslationUnit>(
                        tu => tu.TranslationUnitId == expectedTU.TranslationUnitId
                    );

                Assert.IsNotNull(unit);
                Assert.AreEqual(expectedTU.TranslationUnitId, unit.TranslationUnitId);

                // ---- Assert TranslationUnitVariant
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

        [TestMethod]
        [DeploymentItem("LINQtoTMX.dll")]
        public void Parse_NonExistTmxFile_To_TmxObject_Test()
        {
            // Assign
            TmxParser_Accessor parser = new TmxParser_Accessor();

            // Act
            Tmx result = parser.Parse(MockObjects.NonEXIST_TmxFilePath);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [DeploymentItem("LINQtoTMX.dll")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_EmptyTmxFile_To_TmxObject_Test()
        {
            // Assign
            TmxParser_Accessor parser = new TmxParser_Accessor();

            // Act
            Tmx result = parser.Parse(MockObjects.EMPTY_TmxFilePath);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [DeploymentItem("LINQtoTMX.dll")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_InvalidTmxFile_To_TmxObject_Test()
        {
            // Assign
            TmxParser_Accessor parser = new TmxParser_Accessor();

            // Act
            Tmx result = parser.Parse(MockObjects.INVALID_TmxFilePath);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [DeploymentItem("LINQtoTMX.dll")]
        public void Parse_TmxFileNoSegments_To_TmxObject_Test()
        {
            // Assign
            TmxParser_Accessor parser = new TmxParser_Accessor();
            Tmx expected = MockObjects.VALID_en_TmxObject;

            // Act
            Tmx result = parser.Parse(MockObjects.NO_SEGMENTS_TmxFilePath);

            // Assert
            // -- Assert version
            Assert.AreEqual(expected.Version, result.Version);
            // -- Assert header
            Assert.AreEqual(expected.Header.CreationToolVersion, result.Header.CreationToolVersion);
            Assert.AreEqual(expected.Header.DataType, result.Header.DataType);
            Assert.AreEqual(expected.Header.SegmentType, result.Header.SegmentType);
            Assert.AreEqual(expected.Header.AdminLanguage, result.Header.AdminLanguage);
            Assert.AreEqual(expected.Header.SourceLanguage, result.Header.SourceLanguage);
            Assert.AreEqual(expected.Header.OriginalTranslationMemoryFormat, result.Header.OriginalTranslationMemoryFormat);
            Assert.AreEqual(expected.Header.CreationTool, result.Header.CreationTool);
            // -- Assert body
            // ---- Assert TranslationUnits
            foreach (TranslationUnit expectedTU in expected.Body.TranslationUnits)
            {
                TranslationUnit unit = result.Body.TranslationUnits
                    .First<TranslationUnit>(
                        tu => tu.TranslationUnitId == expectedTU.TranslationUnitId
                    );

                Assert.IsNotNull(unit);
                Assert.AreEqual(expectedTU.TranslationUnitId, unit.TranslationUnitId);

                // ---- Assert TranslationUnitVariant
                foreach (TranslationUnitVariant tuv in unit.TranslationUnitVariants)
                {
                    Assert.AreEqual("en", tuv.Language);
                    Assert.IsNull(tuv.Segment);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("LINQtoTMX.dll")]
        public void Parse_TmxFileNoTranslationUnitVarients_To_TmxObject_Test()
        {
            // Assign
            TmxParser_Accessor parser = new TmxParser_Accessor();
            Tmx expected = MockObjects.VALID_en_TmxObject;

            // Act
            Tmx result = parser.Parse(MockObjects.NO_TRANSLATION_UNITS_VARIENTS_TmxFilePath);

            // Assert
            // -- Assert version
            Assert.AreEqual(expected.Version, result.Version);
            // -- Assert header
            Assert.AreEqual(expected.Header.CreationToolVersion, result.Header.CreationToolVersion);
            Assert.AreEqual(expected.Header.DataType, result.Header.DataType);
            Assert.AreEqual(expected.Header.SegmentType, result.Header.SegmentType);
            Assert.AreEqual(expected.Header.AdminLanguage, result.Header.AdminLanguage);
            Assert.AreEqual(expected.Header.SourceLanguage, result.Header.SourceLanguage);
            Assert.AreEqual(expected.Header.OriginalTranslationMemoryFormat, result.Header.OriginalTranslationMemoryFormat);
            Assert.AreEqual(expected.Header.CreationTool, result.Header.CreationTool);
            // -- Assert body
            // ---- Assert TranslationUnits
            foreach (TranslationUnit expectedTU in expected.Body.TranslationUnits)
            {
                TranslationUnit unit = result.Body.TranslationUnits
                    .First<TranslationUnit>(
                        tu => tu.TranslationUnitId == expectedTU.TranslationUnitId
                    );

                Assert.IsNotNull(unit);
                Assert.AreEqual(expectedTU.TranslationUnitId, unit.TranslationUnitId);

                // ---- Assert TranslationUnitVariant
                Assert.AreEqual(0, unit.TranslationUnitVariants.Count);
            }
        }

        [TestMethod]
        [DeploymentItem("LINQtoTMX.dll")]
        public void Parse_TmxFileNoTranslationUnits_To_TmxObject_Test()
        {
            // Assign
            TmxParser_Accessor parser = new TmxParser_Accessor();
            Tmx expected = MockObjects.VALID_en_TmxObject;

            // Act
            Tmx result = parser.Parse(MockObjects.NO_TRANSLATION_UNITS_TmxFilePath);

            // Assert
            // -- Assert version
            Assert.AreEqual(expected.Version, result.Version);
            // -- Assert header
            Assert.AreEqual(expected.Header.CreationToolVersion, result.Header.CreationToolVersion);
            Assert.AreEqual(expected.Header.DataType, result.Header.DataType);
            Assert.AreEqual(expected.Header.SegmentType, result.Header.SegmentType);
            Assert.AreEqual(expected.Header.AdminLanguage, result.Header.AdminLanguage);
            Assert.AreEqual(expected.Header.SourceLanguage, result.Header.SourceLanguage);
            Assert.AreEqual(expected.Header.OriginalTranslationMemoryFormat, result.Header.OriginalTranslationMemoryFormat);
            Assert.AreEqual(expected.Header.CreationTool, result.Header.CreationTool);
            // -- Assert body
            // ---- Assert TranslationUnits
            Assert.AreEqual(0, result.Body.TranslationUnits.Count);
        }
        #endregion

        #region ParseFile method
        [TestMethod]
        public void Parse_Valid_en_TmxFile_To_TmxFileObject_Test()
        {
            // Assign
            AbstractTmxParser parser = new TmxParser();
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

        [TestMethod]
        public void Parse_Valid_pseudo_TmxFile_To_TmxFileObject_Test()
        {
            // Assign
            AbstractTmxParser parser = new TmxParser();
            TmxFile expected = MockObjects.VALID_pseudo_TmxFile;

            // Act
            TmxFile result = parser.ParseFile(MockObjects.VALID_pseudo_TmxFilePath);

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

        [TestMethod]
        public void Parse_NonExistTmxFile_To_TmxFileObject_Test()
        {
            // Assign
            AbstractTmxParser parser = new TmxParser();

            // Act
            TmxFile result = parser.ParseFile(MockObjects.NonEXIST_TmxFilePath);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_EmptyTmxFile_To_TmxFileObject_Test()
        {
            // Assign
            AbstractTmxParser parser = new TmxParser();

            // Act
            TmxFile result = parser.ParseFile(MockObjects.EMPTY_TmxFilePath);
            
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_InvalidTmxFile_To_TmxFileObject_Test()
        {
            // Assign
            AbstractTmxParser parser = new TmxParser();

            // Act
            TmxFile result = parser.ParseFile(MockObjects.INVALID_TmxFilePath);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Parse_TmxFileNoSegments_To_TmxFileObject_Test()
        {
            // Assign
            AbstractTmxParser parser = new TmxParser();
            TmxFile expected = MockObjects.VALID_en_TmxFile;

            // Act
            TmxFile result = parser.ParseFile(MockObjects.NO_SEGMENTS_TmxFilePath);

            // Assert
            // -- Assert FileInfo
            Assert.AreEqual(MockObjects.NO_SEGMENTS_TmxFilePath, result.FileInfo);
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
                foreach (TranslationUnitVariant tuv in unit.TranslationUnitVariants)
                {
                    Assert.AreEqual("en", tuv.Language);
                    Assert.IsNull(tuv.Segment);
                }
            }
        }

        [TestMethod]
        public void Parse_TmxFileNoTranslationUnitVarients_To_TmxFileObject_Test()
        {
            // Assign
            AbstractTmxParser parser = new TmxParser();
            TmxFile expected = MockObjects.VALID_en_TmxFile;

            // Act
            TmxFile result = parser.ParseFile(MockObjects.NO_TRANSLATION_UNITS_VARIENTS_TmxFilePath);

            // Assert
            // -- Assert FileInfo
            Assert.AreEqual(MockObjects.NO_TRANSLATION_UNITS_VARIENTS_TmxFilePath, result.FileInfo);
            // -- Assert Language code
            Assert.AreEqual("Unkown", result.LanguageCode);
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
                Assert.AreEqual(0, unit.TranslationUnitVariants.Count);
            }
        }

        [TestMethod]
        public void Parse_TmxFileNoTranslationUnits_To_TmxFileObject_Test()
        {
            // Assign
            AbstractTmxParser parser = new TmxParser();
            TmxFile expected = MockObjects.VALID_en_TmxFile;

            // Act
            TmxFile result = parser.ParseFile(MockObjects.NO_TRANSLATION_UNITS_TmxFilePath);

            // Assert
            // -- Assert FileInfo
            Assert.AreEqual(MockObjects.NO_TRANSLATION_UNITS_TmxFilePath, result.FileInfo);
            // -- Assert Language code
            Assert.AreEqual("Unkown", result.LanguageCode);
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
            Assert.AreEqual(0, result.Data.Body.TranslationUnits.Count);
        }
        #endregion
    }
}