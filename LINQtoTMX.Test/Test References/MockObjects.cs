using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LINQtoTMX.Serializable;

namespace LINQtoTMX.Test.Test_References
{
    internal class MockObjects
    {
        #region TmxContext variables
        #region Commit specific

        #endregion

        internal static readonly FileInfo VALID_en_Add_ContextTmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Valid\en\valid_add.tmx");
        internal static readonly FileInfo InvalidPath_en_Add_ContextTmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\en\ERROR\ERROR\ERROR\invalidPath_add.tmx");
        internal static readonly FileInfo InvalidPath_en_Add_ContextTmxFilePath_Result = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Repository\en\invalidPath_add.tmx");
        internal static readonly FileInfo NewLanguageCode_la_Add_ContextTmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\en\ERROR\ERROR\ERROR\newLanguageCode_add.tmx");
        internal static readonly FileInfo NewLanguageCode_la_Add_ContextTmxFilePath_Result = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Repository\la\newLanguageCode_add.tmx");

        internal static readonly Tmx VALID_en_Add_ContextTmxObject = new Tmx()
        {
            Version = "1.4",
            Header = new Header()
            {
                CreationToolVersion = "1.0.0",
                DataType = "winres",
                SegmentType = "sentence",
                AdminLanguage = "en-us",
                SourceLanguage = "en-en",
                OriginalTranslationMemoryFormat = "abc",
                CreationTool = "XYZTool"
            },
            Body = new Body()
            {
                TranslationUnits = new List<TranslationUnit>
                {
                    new TranslationUnit()
                    {
                        TranslationUnitId = "navigation.sub.about",
                        TranslationUnitVariants = new List<TranslationUnitVariant>()
                        {
                            new TranslationUnitVariant()
                            {
                                Language = "en",
                                Segment = "About"
                            }
                        }
                    }
                }
            }
        };

        internal static readonly Tmx VALID_la_Add_ContextTmxObject = new Tmx()
        {
            Version = "1.4",
            Header = new Header()
            {
                CreationToolVersion = "1.0.0",
                DataType = "winres",
                SegmentType = "sentence",
                AdminLanguage = "en-us",
                SourceLanguage = "la-la",
                OriginalTranslationMemoryFormat = "abc",
                CreationTool = "XYZTool"
            },
            Body = new Body()
            {
                TranslationUnits = new List<TranslationUnit>
                {
                    new TranslationUnit()
                    {
                        TranslationUnitId = "navigation.sub.about",
                        TranslationUnitVariants = new List<TranslationUnitVariant>()
                        {
                            new TranslationUnitVariant()
                            {
                                Language = "la",
                                Segment = "About"
                            }
                        }
                    }
                }
            }
        };

        internal static readonly TmxFile VALID_en_Add_ContextTmxFile = new TmxFile()
        {
            FileInfo = VALID_en_Add_ContextTmxFilePath,
            LanguageCode = "en",
            Data = VALID_en_Add_ContextTmxObject
        };

        internal static readonly TmxFile InvalidPath_en_Add_ContextTmxFile = new TmxFile()
        {
            FileInfo = InvalidPath_en_Add_ContextTmxFilePath,
            LanguageCode = "en",
            Data = VALID_en_Add_ContextTmxObject
        };

        internal static readonly TmxFile NewLanguageCode_la_Add_ContextTmxFile = new TmxFile()
        {
            FileInfo = NewLanguageCode_la_Add_ContextTmxFilePath,
            LanguageCode = "la",
            Data = VALID_la_Add_ContextTmxObject
        };
        #endregion

        #region Parse file variables
        internal static readonly FileInfo VALID_en_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Valid\en\valid.tmx");
        internal static readonly FileInfo VALID_pseudo_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Valid\en\valid_pseudo.tmx");
        internal static readonly FileInfo NonEXIST_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Invalid\en\nonexist.tmx");
        internal static readonly FileInfo INVALID_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Invalid\en\invalid.tmx");
        internal static readonly FileInfo EMPTY_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Invalid\en\empty.tmx");
        internal static readonly FileInfo NO_SEGMENTS_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Invalid\en\noSegments.tmx");
        internal static readonly FileInfo NO_TRANSLATION_UNITS_VARIENTS_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Invalid\en\NoTranslationUnitVarients.tmx");
        internal static readonly FileInfo NO_TRANSLATION_UNITS_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Invalid\en\noTranslationUnits.tmx");

        internal static readonly Tmx VALID_en_TmxObject = new Tmx()
        {
            Version = "1.4",
            Header = new Header()
            {
                CreationToolVersion = "1.0.0",
                DataType = "winres",
                SegmentType = "sentence",
                AdminLanguage = "en-us",
                SourceLanguage = "en-en",
                OriginalTranslationMemoryFormat = "abc",
                CreationTool = "XYZTool"
            },
            Body = new Body()
            {
                TranslationUnits = new List<TranslationUnit>
                {
                    new TranslationUnit()
                    {
                        TranslationUnitId = "navigation.sub.about",
                        TranslationUnitVariants = new List<TranslationUnitVariant>()
                        {
                            new TranslationUnitVariant()
                            {
                                Language = "en",
                                Segment = "About"
                            }
                        }
                    },
                    new TranslationUnit()
                    {
                        TranslationUnitId = "navigation.sub.contact",
                        TranslationUnitVariants = new List<TranslationUnitVariant>()
                        {
                            new TranslationUnitVariant()
                            {
                                Language = "en",
                                Segment = "Contact"
                            }
                        }
                    },
                    new TranslationUnit()
                    {
                        TranslationUnitId = "navigation.sub.help",
                        TranslationUnitVariants = new List<TranslationUnitVariant>()
                        {
                            new TranslationUnitVariant()
                            {
                                Language = "en",
                                Segment = "Help"
                            }
                        }
                    }
                }
            }
        };

        internal static readonly Tmx VALID_pseudo_TmxObject = new Tmx()
        {
            Version = "1.4",
            Header = new Header()
            {
                CreationToolVersion = "1.0.0",
                DataType = "winres",
                SegmentType = "sentence",
                AdminLanguage = "en-us",
                SourceLanguage = "de-de",
                OriginalTranslationMemoryFormat = "abc",
                CreationTool = "XYZTool"
            },
            Body = new Body()
            {
                TranslationUnits = new List<TranslationUnit>
                {
                    new TranslationUnit()
                    {
                        TranslationUnitId = "navigation.sub.about",
                        TranslationUnitVariants = new List<TranslationUnitVariant>()
                        {
                            new TranslationUnitVariant()
                            {
                                Language = "de",
                                Segment = "[!! àьöù† !!]"
                            }
                        }
                    },
                    new TranslationUnit()
                    {
                        TranslationUnitId = "navigation.sub.contact",
                        TranslationUnitVariants = new List<TranslationUnitVariant>()
                        {
                            new TranslationUnitVariant()
                            {
                                Language = "de",
                                Segment = "[!! ¢öñ†à¢† !!]"
                            }
                        }
                    },
                    new TranslationUnit()
                    {
                        TranslationUnitId = "navigation.sub.help",
                        TranslationUnitVariants = new List<TranslationUnitVariant>()
                        {
                            new TranslationUnitVariant()
                            {
                                Language = "de",
                                Segment = "[!! तël¶ !!]"
                            }
                        }
                    }
                }
            }
        };

        internal static readonly TmxFile VALID_en_TmxFile = new TmxFile()
        {
            FileInfo = VALID_en_TmxFilePath,
            LanguageCode = "en",
            Data = VALID_en_TmxObject
        };

        internal static readonly TmxFile VALID_pseudo_TmxFile = new TmxFile()
        {
            FileInfo = VALID_pseudo_TmxFilePath,
            LanguageCode = "de",
            Data = VALID_pseudo_TmxObject
        };
        #endregion

        #region Language repository variables
        internal static readonly DirectoryInfo VALID_LanguageRepositoryPath = new DirectoryInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Repository");
        internal static readonly DirectoryInfo VALID_TestFilesPath = new DirectoryInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Valid");
        internal static readonly DirectoryInfo INVALID_TestFilesPath = new DirectoryInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Invalid");
        internal static readonly FileInfo VALID_Add_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Valid\en\valid_en_add.tmx");
        internal static readonly FileInfo INVALID_Add_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Invalid\en\invalid_en_add.tmx");
        internal static readonly FileInfo NonEXIST_Add_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\ERROR\ERROR\ERROR\nonexist.tmx");
        internal static readonly FileInfo VALID_Update_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Valid\en\valid_en_update.tmx");
        internal static readonly FileInfo VALID_Delete_TmxFilePath = new FileInfo(@"..\..\..\LINQtoTMX.Test\Test Resources\Valid\en\valid_en_delete.tmx");

        internal static readonly Tmx VALID_en_Update_TmxObject = new Tmx()
        {
            Version = "1.4",
            Header = new Header()
            {
                CreationToolVersion = "1.0.0",
                DataType = "winres",
                SegmentType = "sentence",
                AdminLanguage = "en-us",
                SourceLanguage = "en-en",
                OriginalTranslationMemoryFormat = "abc",
                CreationTool = "XYZTool"
            },
            Body = new Body()
            {
                TranslationUnits = new List<TranslationUnit>
                {
                    new TranslationUnit()
                    {
                        TranslationUnitId = "navigation.sub.about",
                        TranslationUnitVariants = new List<TranslationUnitVariant>()
                        {
                            new TranslationUnitVariant()
                            {
                                Language = "en",
                                Segment = "About"
                            }
                        }
                    }
                }
            }
        };

        internal static readonly TmxFile VALID_Add_TmxFile = new TmxFile()
        {
            FileInfo = VALID_Add_TmxFilePath,
            LanguageCode = "en",
            Data = VALID_en_TmxObject
        };

        internal static readonly TmxFile INVALID_Add_TmxFile = new TmxFile()
        {
            FileInfo = INVALID_Add_TmxFilePath,
            LanguageCode = "en",
            Data = new Tmx()
        };

        internal static readonly TmxFile NonEXIST_Add_TmxFile = new TmxFile()
        {
            FileInfo = NonEXIST_Add_TmxFilePath,
            LanguageCode = "en",
            Data = VALID_en_TmxObject
        };

        internal static readonly TmxFile VALID_Update_TmxFile = new TmxFile()
        {
            FileInfo = VALID_Update_TmxFilePath,
            LanguageCode = "en",
            Data = VALID_en_Update_TmxObject
        };

        internal static readonly TmxFile VALID_Delete_TmxFile = new TmxFile()
        {
            FileInfo = VALID_Delete_TmxFilePath,
            LanguageCode = "en",
            Data = VALID_en_TmxObject
        };
        #endregion
    }
}