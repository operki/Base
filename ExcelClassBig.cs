using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using X15ac = DocumentFormat.OpenXml.Office2013.ExcelAc;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;
using X15 = DocumentFormat.OpenXml.Office2013.Excel;
using A = DocumentFormat.OpenXml.Drawing;

namespace WindowsFormsApp1
{
    public class ExcelClassBig
    {
        // Creates a SpreadsheetDocument.
        public void CreatePackage(string filePath)
        {
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                CreateParts(package);
            }
        }
        // Adds child parts and generates content of the specified part.
        private void CreateParts(SpreadsheetDocument document)
        {
            ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
            GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

            WorkbookPart workbookPart1 = document.AddWorkbookPart();
            GenerateWorkbookPart1Content(workbookPart1);

            WorkbookStylesPart workbookStylesPart1 = workbookPart1.AddNewPart<WorkbookStylesPart>("rId3");
            GenerateWorkbookStylesPart1Content(workbookStylesPart1);

            ThemePart themePart1 = workbookPart1.AddNewPart<ThemePart>("rId2");
            GenerateThemePart1Content(themePart1);

            WorksheetPart worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>("rId1");
            GenerateWorksheetPart1Content(worksheetPart1);

            TableDefinitionPart tableDefinitionPart1 = worksheetPart1.AddNewPart<TableDefinitionPart>("rId2");
            GenerateTableDefinitionPart1Content(tableDefinitionPart1);

            SpreadsheetPrinterSettingsPart spreadsheetPrinterSettingsPart1 = worksheetPart1.AddNewPart<SpreadsheetPrinterSettingsPart>("rId1");
            GenerateSpreadsheetPrinterSettingsPart1Content(spreadsheetPrinterSettingsPart1);

            SharedStringTablePart sharedStringTablePart1 = workbookPart1.AddNewPart<SharedStringTablePart>("rId4");
            GenerateSharedStringTablePart1Content(sharedStringTablePart1);

            SetPackageProperties(document);
        }

        // Generates content of extendedFilePropertiesPart1.
        private void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
        {
            Ap.Properties properties1 = new Ap.Properties();
            properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
            Ap.TotalTime totalTime1 = new Ap.TotalTime();
            totalTime1.Text = "0";
            Ap.Application application1 = new Ap.Application();
            application1.Text = "Microsoft Excel";
            Ap.DocumentSecurity documentSecurity1 = new Ap.DocumentSecurity();
            documentSecurity1.Text = "0";
            Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
            scaleCrop1.Text = "false";

            Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

            Vt.VTVector vTVector1 = new Vt.VTVector(){ BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)4U };

            Vt.Variant variant1 = new Vt.Variant();
            Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
            vTLPSTR1.Text = "Листы";

            variant1.Append(vTLPSTR1);

            Vt.Variant variant2 = new Vt.Variant();
            Vt.VTInt32 vTInt321 = new Vt.VTInt32();
            vTInt321.Text = "1";

            variant2.Append(vTInt321);

            Vt.Variant variant3 = new Vt.Variant();
            Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
            vTLPSTR2.Text = "Именованные диапазоны";

            variant3.Append(vTLPSTR2);

            Vt.Variant variant4 = new Vt.Variant();
            Vt.VTInt32 vTInt322 = new Vt.VTInt32();
            vTInt322.Text = "2";

            variant4.Append(vTInt322);

            vTVector1.Append(variant1);
            vTVector1.Append(variant2);
            vTVector1.Append(variant3);
            vTVector1.Append(variant4);

            headingPairs1.Append(vTVector1);

            Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

            Vt.VTVector vTVector2 = new Vt.VTVector(){ BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)3U };
            Vt.VTLPSTR vTLPSTR3 = new Vt.VTLPSTR();
            vTLPSTR3.Text = "Выгрузка с базы";
            Vt.VTLPSTR vTLPSTR4 = new Vt.VTLPSTR();
            vTLPSTR4.Text = "\'Выгрузка с базы\'!Заголовки_для_печати";
            Vt.VTLPSTR vTLPSTR5 = new Vt.VTLPSTR();
            vTLPSTR5.Text = "\'Выгрузка с базы\'!Область_печати";

            vTVector2.Append(vTLPSTR3);
            vTVector2.Append(vTLPSTR4);
            vTVector2.Append(vTLPSTR5);

            titlesOfParts1.Append(vTVector2);
            Ap.Company company1 = new Ap.Company();
            company1.Text = "SPecialiST RePack";
            Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
            linksUpToDate1.Text = "false";
            Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
            sharedDocument1.Text = "false";
            Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
            hyperlinksChanged1.Text = "false";
            Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
            applicationVersion1.Text = "15.0300";

            properties1.Append(totalTime1);
            properties1.Append(application1);
            properties1.Append(documentSecurity1);
            properties1.Append(scaleCrop1);
            properties1.Append(headingPairs1);
            properties1.Append(titlesOfParts1);
            properties1.Append(company1);
            properties1.Append(linksUpToDate1);
            properties1.Append(sharedDocument1);
            properties1.Append(hyperlinksChanged1);
            properties1.Append(applicationVersion1);

            extendedFilePropertiesPart1.Properties = properties1;
        }

        // Generates content of workbookPart1.
        private void GenerateWorkbookPart1Content(WorkbookPart workbookPart1)
        {
            Workbook workbook1 = new Workbook(){ MCAttributes = new MarkupCompatibilityAttributes(){ Ignorable = "x15" }  };
            workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            workbook1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            workbook1.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            FileVersion fileVersion1 = new FileVersion(){ ApplicationName = "xl", LastEdited = "6", LowestEdited = "5", BuildVersion = "14420" };
            WorkbookProperties workbookProperties1 = new WorkbookProperties(){ DefaultThemeVersion = (UInt32Value)124226U };

            AlternateContent alternateContent1 = new AlternateContent();
            alternateContent1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

            AlternateContentChoice alternateContentChoice1 = new AlternateContentChoice(){ Requires = "x15" };

            X15ac.AbsolutePath absolutePath1 = new X15ac.AbsolutePath(){ Url = "C:\\temp\\" };
            absolutePath1.AddNamespaceDeclaration("x15ac", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/ac");

            alternateContentChoice1.Append(absolutePath1);

            alternateContent1.Append(alternateContentChoice1);

            BookViews bookViews1 = new BookViews();
            WorkbookView workbookView1 = new WorkbookView(){ XWindow = 0, YWindow = 135, WindowWidth = (UInt32Value)28695U, WindowHeight = (UInt32Value)14055U };

            bookViews1.Append(workbookView1);

            Sheets sheets1 = new Sheets();
            Sheet sheet1 = new Sheet(){ Name = "Выгрузка с базы", SheetId = (UInt32Value)7U, Id = "rId1" };

            sheets1.Append(sheet1);

            DefinedNames definedNames1 = new DefinedNames();
            DefinedName definedName1 = new DefinedName(){ Name = "_xlnm.Print_Titles", LocalSheetId = (UInt32Value)0U };
            definedName1.Text = "\'Выгрузка с базы\'!$2:$2";
            DefinedName definedName2 = new DefinedName(){ Name = "_xlnm.Print_Area", LocalSheetId = (UInt32Value)0U };
            definedName2.Text = "\'Выгрузка с базы\'!$F$1:$I$5";

            definedNames1.Append(definedName1);
            definedNames1.Append(definedName2);
            CalculationProperties calculationProperties1 = new CalculationProperties(){ CalculationId = (UInt32Value)152511U, CalculationMode = CalculateModeValues.Manual, CalculationCompleted = false, CalculationOnSave = false };

            workbook1.Append(fileVersion1);
            workbook1.Append(workbookProperties1);
            workbook1.Append(alternateContent1);
            workbook1.Append(bookViews1);
            workbook1.Append(sheets1);
            workbook1.Append(definedNames1);
            workbook1.Append(calculationProperties1);

            workbookPart1.Workbook = workbook1;
        }

        // Generates content of workbookStylesPart1.
        private void GenerateWorkbookStylesPart1Content(WorkbookStylesPart workbookStylesPart1)
        {
            Stylesheet stylesheet1 = new Stylesheet(){ MCAttributes = new MarkupCompatibilityAttributes(){ Ignorable = "x14ac" }  };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            Fonts fonts1 = new Fonts(){ Count = (UInt32Value)4U, KnownFonts = true };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize(){ Val = 11D };
            Color color1 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName(){ Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering(){ Val = 2 };
            FontScheme fontScheme1 = new FontScheme(){ Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);

            Font font2 = new Font();
            FontSize fontSize2 = new FontSize(){ Val = 12D };
            Color color2 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName2 = new FontName(){ Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering(){ Val = 2 };
            FontCharSet fontCharSet1 = new FontCharSet(){ Val = 204 };

            font2.Append(fontSize2);
            font2.Append(color2);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);
            font2.Append(fontCharSet1);

            Font font3 = new Font();
            FontSize fontSize3 = new FontSize(){ Val = 14D };
            Color color3 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName3 = new FontName(){ Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering3 = new FontFamilyNumbering(){ Val = 2 };
            FontCharSet fontCharSet2 = new FontCharSet(){ Val = 204 };

            font3.Append(fontSize3);
            font3.Append(color3);
            font3.Append(fontName3);
            font3.Append(fontFamilyNumbering3);
            font3.Append(fontCharSet2);

            Font font4 = new Font();
            FontSize fontSize4 = new FontSize(){ Val = 16D };
            Color color4 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName4 = new FontName(){ Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering4 = new FontFamilyNumbering(){ Val = 2 };
            FontCharSet fontCharSet3 = new FontCharSet(){ Val = 204 };

            font4.Append(fontSize4);
            font4.Append(color4);
            font4.Append(fontName4);
            font4.Append(fontFamilyNumbering4);
            font4.Append(fontCharSet3);

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);
            fonts1.Append(font4);

            Fills fills1 = new Fills(){ Count = (UInt32Value)2U };

            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill(){ PatternType = PatternValues.None };

            fill1.Append(patternFill1);

            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill(){ PatternType = PatternValues.Gray125 };

            fill2.Append(patternFill2);

            fills1.Append(fill1);
            fills1.Append(fill2);

            Borders borders1 = new Borders(){ Count = (UInt32Value)1U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats(){ Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats(){ Count = (UInt32Value)14U };
            CellFormat cellFormat2 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true };

            CellFormat cellFormat3 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment1 = new Alignment(){ Vertical = VerticalAlignmentValues.Center };

            cellFormat3.Append(alignment1);

            CellFormat cellFormat4 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment2 = new Alignment(){ Vertical = VerticalAlignmentValues.Center };

            cellFormat4.Append(alignment2);

            CellFormat cellFormat5 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment3 = new Alignment(){ Horizontal = HorizontalAlignmentValues.CenterContinuous, Vertical = VerticalAlignmentValues.Center };

            cellFormat5.Append(alignment3);

            CellFormat cellFormat6 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment4 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center };

            cellFormat6.Append(alignment4);

            CellFormat cellFormat7 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment5 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center };

            cellFormat7.Append(alignment5);
            CellFormat cellFormat8 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };

            CellFormat cellFormat9 = new CellFormat(){ NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment6 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat9.Append(alignment6);

            CellFormat cellFormat10 = new CellFormat(){ NumberFormatId = (UInt32Value)49U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment7 = new Alignment(){ Vertical = VerticalAlignmentValues.Center };

            cellFormat10.Append(alignment7);

            CellFormat cellFormat11 = new CellFormat(){ NumberFormatId = (UInt32Value)49U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment8 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center };

            cellFormat11.Append(alignment8);

            CellFormat cellFormat12 = new CellFormat(){ NumberFormatId = (UInt32Value)14U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment9 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center };

            cellFormat12.Append(alignment9);

            CellFormat cellFormat13 = new CellFormat(){ NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment10 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center };

            cellFormat13.Append(alignment10);

            CellFormat cellFormat14 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment11 = new Alignment(){ Horizontal = HorizontalAlignmentValues.CenterContinuous };

            cellFormat14.Append(alignment11);

            CellFormat cellFormat15 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)3U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment12 = new Alignment(){ Horizontal = HorizontalAlignmentValues.CenterContinuous, Vertical = VerticalAlignmentValues.Center };

            cellFormat15.Append(alignment12);

            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);
            cellFormats1.Append(cellFormat6);
            cellFormats1.Append(cellFormat7);
            cellFormats1.Append(cellFormat8);
            cellFormats1.Append(cellFormat9);
            cellFormats1.Append(cellFormat10);
            cellFormats1.Append(cellFormat11);
            cellFormats1.Append(cellFormat12);
            cellFormats1.Append(cellFormat13);
            cellFormats1.Append(cellFormat14);
            cellFormats1.Append(cellFormat15);

            CellStyles cellStyles1 = new CellStyles(){ Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle(){ Name = "Обычный", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);

            DifferentialFormats differentialFormats1 = new DifferentialFormats(){ Count = (UInt32Value)33U };

            DifferentialFormat differentialFormat1 = new DifferentialFormat();

            Fill fill3 = new Fill();

            PatternFill patternFill3 = new PatternFill();
            BackgroundColor backgroundColor1 = new BackgroundColor(){ Theme = (UInt32Value)0U, Tint = -0.14996795556505021D };

            patternFill3.Append(backgroundColor1);

            fill3.Append(patternFill3);

            differentialFormat1.Append(fill3);

            DifferentialFormat differentialFormat2 = new DifferentialFormat();

            Font font5 = new Font();
            Strike strike1 = new Strike(){ Val = false };
            Outline outline1 = new Outline(){ Val = false };
            Shadow shadow1 = new Shadow(){ Val = false };
            Underline underline1 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment1 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize5 = new FontSize(){ Val = 14D };
            Color color5 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName5 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme2 = new FontScheme(){ Val = FontSchemeValues.None };

            font5.Append(strike1);
            font5.Append(outline1);
            font5.Append(shadow1);
            font5.Append(underline1);
            font5.Append(verticalTextAlignment1);
            font5.Append(fontSize5);
            font5.Append(color5);
            font5.Append(fontName5);
            font5.Append(fontScheme2);
            NumberingFormat numberingFormat1 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill4 = new Fill();

            PatternFill patternFill4 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor1 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor2 = new BackgroundColor(){ Auto = true };

            patternFill4.Append(foregroundColor1);
            patternFill4.Append(backgroundColor2);

            fill4.Append(patternFill4);
            Alignment alignment13 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat2.Append(font5);
            differentialFormat2.Append(numberingFormat1);
            differentialFormat2.Append(fill4);
            differentialFormat2.Append(alignment13);

            DifferentialFormat differentialFormat3 = new DifferentialFormat();

            Font font6 = new Font();
            Strike strike2 = new Strike(){ Val = false };
            Outline outline2 = new Outline(){ Val = false };
            Shadow shadow2 = new Shadow(){ Val = false };
            Underline underline2 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment2 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize6 = new FontSize(){ Val = 14D };
            Color color6 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName6 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme3 = new FontScheme(){ Val = FontSchemeValues.None };

            font6.Append(strike2);
            font6.Append(outline2);
            font6.Append(shadow2);
            font6.Append(underline2);
            font6.Append(verticalTextAlignment2);
            font6.Append(fontSize6);
            font6.Append(color6);
            font6.Append(fontName6);
            font6.Append(fontScheme3);
            NumberingFormat numberingFormat2 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill5 = new Fill();

            PatternFill patternFill5 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor2 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor3 = new BackgroundColor(){ Auto = true };

            patternFill5.Append(foregroundColor2);
            patternFill5.Append(backgroundColor3);

            fill5.Append(patternFill5);
            Alignment alignment14 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat3.Append(font6);
            differentialFormat3.Append(numberingFormat2);
            differentialFormat3.Append(fill5);
            differentialFormat3.Append(alignment14);

            DifferentialFormat differentialFormat4 = new DifferentialFormat();

            Font font7 = new Font();
            Strike strike3 = new Strike(){ Val = false };
            Outline outline3 = new Outline(){ Val = false };
            Shadow shadow3 = new Shadow(){ Val = false };
            Underline underline3 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment3 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize7 = new FontSize(){ Val = 14D };
            Color color7 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName7 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme4 = new FontScheme(){ Val = FontSchemeValues.None };

            font7.Append(strike3);
            font7.Append(outline3);
            font7.Append(shadow3);
            font7.Append(underline3);
            font7.Append(verticalTextAlignment3);
            font7.Append(fontSize7);
            font7.Append(color7);
            font7.Append(fontName7);
            font7.Append(fontScheme4);
            NumberingFormat numberingFormat3 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill6 = new Fill();

            PatternFill patternFill6 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor3 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor4 = new BackgroundColor(){ Auto = true };

            patternFill6.Append(foregroundColor3);
            patternFill6.Append(backgroundColor4);

            fill6.Append(patternFill6);
            Alignment alignment15 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat4.Append(font7);
            differentialFormat4.Append(numberingFormat3);
            differentialFormat4.Append(fill6);
            differentialFormat4.Append(alignment15);

            DifferentialFormat differentialFormat5 = new DifferentialFormat();

            Font font8 = new Font();
            Bold bold1 = new Bold(){ Val = false };
            Italic italic1 = new Italic(){ Val = false };
            Strike strike4 = new Strike(){ Val = false };
            Condense condense1 = new Condense(){ Val = false };
            Extend extend1 = new Extend(){ Val = false };
            Outline outline4 = new Outline(){ Val = false };
            Shadow shadow4 = new Shadow(){ Val = false };
            Underline underline4 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment4 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize8 = new FontSize(){ Val = 14D };
            Color color8 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName8 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme5 = new FontScheme(){ Val = FontSchemeValues.None };

            font8.Append(bold1);
            font8.Append(italic1);
            font8.Append(strike4);
            font8.Append(condense1);
            font8.Append(extend1);
            font8.Append(outline4);
            font8.Append(shadow4);
            font8.Append(underline4);
            font8.Append(verticalTextAlignment4);
            font8.Append(fontSize8);
            font8.Append(color8);
            font8.Append(fontName8);
            font8.Append(fontScheme5);
            NumberingFormat numberingFormat4 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill7 = new Fill();

            PatternFill patternFill7 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor4 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor5 = new BackgroundColor(){ Auto = true };

            patternFill7.Append(foregroundColor4);
            patternFill7.Append(backgroundColor5);

            fill7.Append(patternFill7);
            Alignment alignment16 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat5.Append(font8);
            differentialFormat5.Append(numberingFormat4);
            differentialFormat5.Append(fill7);
            differentialFormat5.Append(alignment16);

            DifferentialFormat differentialFormat6 = new DifferentialFormat();

            Font font9 = new Font();
            Bold bold2 = new Bold(){ Val = false };
            Italic italic2 = new Italic(){ Val = false };
            Strike strike5 = new Strike(){ Val = false };
            Condense condense2 = new Condense(){ Val = false };
            Extend extend2 = new Extend(){ Val = false };
            Outline outline5 = new Outline(){ Val = false };
            Shadow shadow5 = new Shadow(){ Val = false };
            Underline underline5 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment5 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize9 = new FontSize(){ Val = 14D };
            Color color9 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName9 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme6 = new FontScheme(){ Val = FontSchemeValues.None };

            font9.Append(bold2);
            font9.Append(italic2);
            font9.Append(strike5);
            font9.Append(condense2);
            font9.Append(extend2);
            font9.Append(outline5);
            font9.Append(shadow5);
            font9.Append(underline5);
            font9.Append(verticalTextAlignment5);
            font9.Append(fontSize9);
            font9.Append(color9);
            font9.Append(fontName9);
            font9.Append(fontScheme6);
            NumberingFormat numberingFormat5 = new NumberingFormat(){ NumberFormatId = (UInt32Value)19U, FormatCode = "dd/mm/yyyy" };

            Fill fill8 = new Fill();

            PatternFill patternFill8 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor5 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor6 = new BackgroundColor(){ Auto = true };

            patternFill8.Append(foregroundColor5);
            patternFill8.Append(backgroundColor6);

            fill8.Append(patternFill8);
            Alignment alignment17 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat6.Append(font9);
            differentialFormat6.Append(numberingFormat5);
            differentialFormat6.Append(fill8);
            differentialFormat6.Append(alignment17);

            DifferentialFormat differentialFormat7 = new DifferentialFormat();

            Font font10 = new Font();
            Bold bold3 = new Bold(){ Val = false };
            Italic italic3 = new Italic(){ Val = false };
            Strike strike6 = new Strike(){ Val = false };
            Condense condense3 = new Condense(){ Val = false };
            Extend extend3 = new Extend(){ Val = false };
            Outline outline6 = new Outline(){ Val = false };
            Shadow shadow6 = new Shadow(){ Val = false };
            Underline underline6 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment6 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize10 = new FontSize(){ Val = 14D };
            Color color10 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName10 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme7 = new FontScheme(){ Val = FontSchemeValues.None };

            font10.Append(bold3);
            font10.Append(italic3);
            font10.Append(strike6);
            font10.Append(condense3);
            font10.Append(extend3);
            font10.Append(outline6);
            font10.Append(shadow6);
            font10.Append(underline6);
            font10.Append(verticalTextAlignment6);
            font10.Append(fontSize10);
            font10.Append(color10);
            font10.Append(fontName10);
            font10.Append(fontScheme7);
            NumberingFormat numberingFormat6 = new NumberingFormat(){ NumberFormatId = (UInt32Value)19U, FormatCode = "dd/mm/yyyy" };

            Fill fill9 = new Fill();

            PatternFill patternFill9 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor6 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor7 = new BackgroundColor(){ Auto = true };

            patternFill9.Append(foregroundColor6);
            patternFill9.Append(backgroundColor7);

            fill9.Append(patternFill9);
            Alignment alignment18 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat7.Append(font10);
            differentialFormat7.Append(numberingFormat6);
            differentialFormat7.Append(fill9);
            differentialFormat7.Append(alignment18);

            DifferentialFormat differentialFormat8 = new DifferentialFormat();

            Font font11 = new Font();
            Strike strike7 = new Strike(){ Val = false };
            Outline outline7 = new Outline(){ Val = false };
            Shadow shadow7 = new Shadow(){ Val = false };
            Underline underline7 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment7 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize11 = new FontSize(){ Val = 14D };
            Color color11 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName11 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme8 = new FontScheme(){ Val = FontSchemeValues.None };

            font11.Append(strike7);
            font11.Append(outline7);
            font11.Append(shadow7);
            font11.Append(underline7);
            font11.Append(verticalTextAlignment7);
            font11.Append(fontSize11);
            font11.Append(color11);
            font11.Append(fontName11);
            font11.Append(fontScheme8);
            NumberingFormat numberingFormat7 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill10 = new Fill();

            PatternFill patternFill10 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor7 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor8 = new BackgroundColor(){ Auto = true };

            patternFill10.Append(foregroundColor7);
            patternFill10.Append(backgroundColor8);

            fill10.Append(patternFill10);
            Alignment alignment19 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat8.Append(font11);
            differentialFormat8.Append(numberingFormat7);
            differentialFormat8.Append(fill10);
            differentialFormat8.Append(alignment19);

            DifferentialFormat differentialFormat9 = new DifferentialFormat();

            Font font12 = new Font();
            Strike strike8 = new Strike(){ Val = false };
            Outline outline8 = new Outline(){ Val = false };
            Shadow shadow8 = new Shadow(){ Val = false };
            Underline underline8 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment8 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize12 = new FontSize(){ Val = 14D };
            Color color12 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName12 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme9 = new FontScheme(){ Val = FontSchemeValues.None };

            font12.Append(strike8);
            font12.Append(outline8);
            font12.Append(shadow8);
            font12.Append(underline8);
            font12.Append(verticalTextAlignment8);
            font12.Append(fontSize12);
            font12.Append(color12);
            font12.Append(fontName12);
            font12.Append(fontScheme9);
            NumberingFormat numberingFormat8 = new NumberingFormat(){ NumberFormatId = (UInt32Value)1U, FormatCode = "0" };

            Fill fill11 = new Fill();

            PatternFill patternFill11 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor8 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor9 = new BackgroundColor(){ Auto = true };

            patternFill11.Append(foregroundColor8);
            patternFill11.Append(backgroundColor9);

            fill11.Append(patternFill11);
            Alignment alignment20 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat9.Append(font12);
            differentialFormat9.Append(numberingFormat8);
            differentialFormat9.Append(fill11);
            differentialFormat9.Append(alignment20);

            DifferentialFormat differentialFormat10 = new DifferentialFormat();

            Font font13 = new Font();
            Strike strike9 = new Strike(){ Val = false };
            Outline outline9 = new Outline(){ Val = false };
            Shadow shadow9 = new Shadow(){ Val = false };
            Underline underline9 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment9 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize13 = new FontSize(){ Val = 14D };
            Color color13 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName13 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme10 = new FontScheme(){ Val = FontSchemeValues.None };

            font13.Append(strike9);
            font13.Append(outline9);
            font13.Append(shadow9);
            font13.Append(underline9);
            font13.Append(verticalTextAlignment9);
            font13.Append(fontSize13);
            font13.Append(color13);
            font13.Append(fontName13);
            font13.Append(fontScheme10);
            NumberingFormat numberingFormat9 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill12 = new Fill();

            PatternFill patternFill12 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor9 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor10 = new BackgroundColor(){ Auto = true };

            patternFill12.Append(foregroundColor9);
            patternFill12.Append(backgroundColor10);

            fill12.Append(patternFill12);
            Alignment alignment21 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat10.Append(font13);
            differentialFormat10.Append(numberingFormat9);
            differentialFormat10.Append(fill12);
            differentialFormat10.Append(alignment21);

            DifferentialFormat differentialFormat11 = new DifferentialFormat();

            Font font14 = new Font();
            Bold bold4 = new Bold(){ Val = false };
            Italic italic4 = new Italic(){ Val = false };
            Strike strike10 = new Strike(){ Val = false };
            Condense condense4 = new Condense(){ Val = false };
            Extend extend4 = new Extend(){ Val = false };
            Outline outline10 = new Outline(){ Val = false };
            Shadow shadow10 = new Shadow(){ Val = false };
            Underline underline10 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment10 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize14 = new FontSize(){ Val = 14D };
            Color color14 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName14 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme11 = new FontScheme(){ Val = FontSchemeValues.None };

            font14.Append(bold4);
            font14.Append(italic4);
            font14.Append(strike10);
            font14.Append(condense4);
            font14.Append(extend4);
            font14.Append(outline10);
            font14.Append(shadow10);
            font14.Append(underline10);
            font14.Append(verticalTextAlignment10);
            font14.Append(fontSize14);
            font14.Append(color14);
            font14.Append(fontName14);
            font14.Append(fontScheme11);
            NumberingFormat numberingFormat10 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill13 = new Fill();

            PatternFill patternFill13 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor10 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor11 = new BackgroundColor(){ Auto = true };

            patternFill13.Append(foregroundColor10);
            patternFill13.Append(backgroundColor11);

            fill13.Append(patternFill13);
            Alignment alignment22 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat11.Append(font14);
            differentialFormat11.Append(numberingFormat10);
            differentialFormat11.Append(fill13);
            differentialFormat11.Append(alignment22);

            DifferentialFormat differentialFormat12 = new DifferentialFormat();

            Font font15 = new Font();
            Bold bold5 = new Bold(){ Val = false };
            Italic italic5 = new Italic(){ Val = false };
            Strike strike11 = new Strike(){ Val = false };
            Condense condense5 = new Condense(){ Val = false };
            Extend extend5 = new Extend(){ Val = false };
            Outline outline11 = new Outline(){ Val = false };
            Shadow shadow11 = new Shadow(){ Val = false };
            Underline underline11 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment11 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize15 = new FontSize(){ Val = 14D };
            Color color15 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName15 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme12 = new FontScheme(){ Val = FontSchemeValues.None };

            font15.Append(bold5);
            font15.Append(italic5);
            font15.Append(strike11);
            font15.Append(condense5);
            font15.Append(extend5);
            font15.Append(outline11);
            font15.Append(shadow11);
            font15.Append(underline11);
            font15.Append(verticalTextAlignment11);
            font15.Append(fontSize15);
            font15.Append(color15);
            font15.Append(fontName15);
            font15.Append(fontScheme12);
            NumberingFormat numberingFormat11 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill14 = new Fill();

            PatternFill patternFill14 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor11 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor12 = new BackgroundColor(){ Indexed = (UInt32Value)65U };

            patternFill14.Append(foregroundColor11);
            patternFill14.Append(backgroundColor12);

            fill14.Append(patternFill14);
            Alignment alignment23 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat12.Append(font15);
            differentialFormat12.Append(numberingFormat11);
            differentialFormat12.Append(fill14);
            differentialFormat12.Append(alignment23);

            DifferentialFormat differentialFormat13 = new DifferentialFormat();

            Font font16 = new Font();
            Bold bold6 = new Bold(){ Val = false };
            Italic italic6 = new Italic(){ Val = false };
            Strike strike12 = new Strike(){ Val = false };
            Condense condense6 = new Condense(){ Val = false };
            Extend extend6 = new Extend(){ Val = false };
            Outline outline12 = new Outline(){ Val = false };
            Shadow shadow12 = new Shadow(){ Val = false };
            Underline underline12 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment12 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize16 = new FontSize(){ Val = 14D };
            Color color16 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName16 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme13 = new FontScheme(){ Val = FontSchemeValues.None };

            font16.Append(bold6);
            font16.Append(italic6);
            font16.Append(strike12);
            font16.Append(condense6);
            font16.Append(extend6);
            font16.Append(outline12);
            font16.Append(shadow12);
            font16.Append(underline12);
            font16.Append(verticalTextAlignment12);
            font16.Append(fontSize16);
            font16.Append(color16);
            font16.Append(fontName16);
            font16.Append(fontScheme13);
            NumberingFormat numberingFormat12 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill15 = new Fill();

            PatternFill patternFill15 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor12 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor13 = new BackgroundColor(){ Auto = true };

            patternFill15.Append(foregroundColor12);
            patternFill15.Append(backgroundColor13);

            fill15.Append(patternFill15);
            Alignment alignment24 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat13.Append(font16);
            differentialFormat13.Append(numberingFormat12);
            differentialFormat13.Append(fill15);
            differentialFormat13.Append(alignment24);

            DifferentialFormat differentialFormat14 = new DifferentialFormat();

            Font font17 = new Font();
            Bold bold7 = new Bold(){ Val = false };
            Italic italic7 = new Italic(){ Val = false };
            Strike strike13 = new Strike(){ Val = false };
            Condense condense7 = new Condense(){ Val = false };
            Extend extend7 = new Extend(){ Val = false };
            Outline outline13 = new Outline(){ Val = false };
            Shadow shadow13 = new Shadow(){ Val = false };
            Underline underline13 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment13 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize17 = new FontSize(){ Val = 14D };
            Color color17 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName17 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme14 = new FontScheme(){ Val = FontSchemeValues.None };

            font17.Append(bold7);
            font17.Append(italic7);
            font17.Append(strike13);
            font17.Append(condense7);
            font17.Append(extend7);
            font17.Append(outline13);
            font17.Append(shadow13);
            font17.Append(underline13);
            font17.Append(verticalTextAlignment13);
            font17.Append(fontSize17);
            font17.Append(color17);
            font17.Append(fontName17);
            font17.Append(fontScheme14);
            NumberingFormat numberingFormat13 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill16 = new Fill();

            PatternFill patternFill16 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor13 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor14 = new BackgroundColor(){ Indexed = (UInt32Value)65U };

            patternFill16.Append(foregroundColor13);
            patternFill16.Append(backgroundColor14);

            fill16.Append(patternFill16);
            Alignment alignment25 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat14.Append(font17);
            differentialFormat14.Append(numberingFormat13);
            differentialFormat14.Append(fill16);
            differentialFormat14.Append(alignment25);

            DifferentialFormat differentialFormat15 = new DifferentialFormat();

            Font font18 = new Font();
            Strike strike14 = new Strike(){ Val = false };
            Outline outline14 = new Outline(){ Val = false };
            Shadow shadow14 = new Shadow(){ Val = false };
            Underline underline14 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment14 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize18 = new FontSize(){ Val = 14D };
            Color color18 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName18 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme15 = new FontScheme(){ Val = FontSchemeValues.None };

            font18.Append(strike14);
            font18.Append(outline14);
            font18.Append(shadow14);
            font18.Append(underline14);
            font18.Append(verticalTextAlignment14);
            font18.Append(fontSize18);
            font18.Append(color18);
            font18.Append(fontName18);
            font18.Append(fontScheme15);
            NumberingFormat numberingFormat14 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill17 = new Fill();

            PatternFill patternFill17 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor14 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor15 = new BackgroundColor(){ Auto = true };

            patternFill17.Append(foregroundColor14);
            patternFill17.Append(backgroundColor15);

            fill17.Append(patternFill17);
            Alignment alignment26 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat15.Append(font18);
            differentialFormat15.Append(numberingFormat14);
            differentialFormat15.Append(fill17);
            differentialFormat15.Append(alignment26);

            DifferentialFormat differentialFormat16 = new DifferentialFormat();

            Font font19 = new Font();
            Bold bold8 = new Bold(){ Val = false };
            Italic italic8 = new Italic(){ Val = false };
            Strike strike15 = new Strike(){ Val = false };
            Condense condense8 = new Condense(){ Val = false };
            Extend extend8 = new Extend(){ Val = false };
            Outline outline15 = new Outline(){ Val = false };
            Shadow shadow15 = new Shadow(){ Val = false };
            Underline underline15 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment15 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize19 = new FontSize(){ Val = 14D };
            Color color19 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName19 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme16 = new FontScheme(){ Val = FontSchemeValues.None };

            font19.Append(bold8);
            font19.Append(italic8);
            font19.Append(strike15);
            font19.Append(condense8);
            font19.Append(extend8);
            font19.Append(outline15);
            font19.Append(shadow15);
            font19.Append(underline15);
            font19.Append(verticalTextAlignment15);
            font19.Append(fontSize19);
            font19.Append(color19);
            font19.Append(fontName19);
            font19.Append(fontScheme16);
            NumberingFormat numberingFormat15 = new NumberingFormat(){ NumberFormatId = (UInt32Value)19U, FormatCode = "dd/mm/yyyy" };

            Fill fill18 = new Fill();

            PatternFill patternFill18 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor15 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor16 = new BackgroundColor(){ Auto = true };

            patternFill18.Append(foregroundColor15);
            patternFill18.Append(backgroundColor16);

            fill18.Append(patternFill18);
            Alignment alignment27 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat16.Append(font19);
            differentialFormat16.Append(numberingFormat15);
            differentialFormat16.Append(fill18);
            differentialFormat16.Append(alignment27);

            DifferentialFormat differentialFormat17 = new DifferentialFormat();

            Font font20 = new Font();
            Bold bold9 = new Bold(){ Val = false };
            Italic italic9 = new Italic(){ Val = false };
            Strike strike16 = new Strike(){ Val = false };
            Condense condense9 = new Condense(){ Val = false };
            Extend extend9 = new Extend(){ Val = false };
            Outline outline16 = new Outline(){ Val = false };
            Shadow shadow16 = new Shadow(){ Val = false };
            Underline underline16 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment16 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize20 = new FontSize(){ Val = 14D };
            Color color20 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName20 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme17 = new FontScheme(){ Val = FontSchemeValues.None };

            font20.Append(bold9);
            font20.Append(italic9);
            font20.Append(strike16);
            font20.Append(condense9);
            font20.Append(extend9);
            font20.Append(outline16);
            font20.Append(shadow16);
            font20.Append(underline16);
            font20.Append(verticalTextAlignment16);
            font20.Append(fontSize20);
            font20.Append(color20);
            font20.Append(fontName20);
            font20.Append(fontScheme17);
            NumberingFormat numberingFormat16 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill19 = new Fill();

            PatternFill patternFill19 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor16 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor17 = new BackgroundColor(){ Auto = true };

            patternFill19.Append(foregroundColor16);
            patternFill19.Append(backgroundColor17);

            fill19.Append(patternFill19);
            Alignment alignment28 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat17.Append(font20);
            differentialFormat17.Append(numberingFormat16);
            differentialFormat17.Append(fill19);
            differentialFormat17.Append(alignment28);

            DifferentialFormat differentialFormat18 = new DifferentialFormat();

            Font font21 = new Font();
            Bold bold10 = new Bold(){ Val = false };
            Italic italic10 = new Italic(){ Val = false };
            Strike strike17 = new Strike(){ Val = false };
            Condense condense10 = new Condense(){ Val = false };
            Extend extend10 = new Extend(){ Val = false };
            Outline outline17 = new Outline(){ Val = false };
            Shadow shadow17 = new Shadow(){ Val = false };
            Underline underline17 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment17 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize21 = new FontSize(){ Val = 14D };
            Color color21 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName21 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme18 = new FontScheme(){ Val = FontSchemeValues.None };

            font21.Append(bold10);
            font21.Append(italic10);
            font21.Append(strike17);
            font21.Append(condense10);
            font21.Append(extend10);
            font21.Append(outline17);
            font21.Append(shadow17);
            font21.Append(underline17);
            font21.Append(verticalTextAlignment17);
            font21.Append(fontSize21);
            font21.Append(color21);
            font21.Append(fontName21);
            font21.Append(fontScheme18);
            NumberingFormat numberingFormat17 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill20 = new Fill();

            PatternFill patternFill20 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor17 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor18 = new BackgroundColor(){ Auto = true };

            patternFill20.Append(foregroundColor17);
            patternFill20.Append(backgroundColor18);

            fill20.Append(patternFill20);
            Alignment alignment29 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat18.Append(font21);
            differentialFormat18.Append(numberingFormat17);
            differentialFormat18.Append(fill20);
            differentialFormat18.Append(alignment29);

            DifferentialFormat differentialFormat19 = new DifferentialFormat();

            Font font22 = new Font();
            Strike strike18 = new Strike(){ Val = false };
            Outline outline18 = new Outline(){ Val = false };
            Shadow shadow18 = new Shadow(){ Val = false };
            Underline underline18 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment18 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize22 = new FontSize(){ Val = 14D };
            Color color22 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName22 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme19 = new FontScheme(){ Val = FontSchemeValues.None };

            font22.Append(strike18);
            font22.Append(outline18);
            font22.Append(shadow18);
            font22.Append(underline18);
            font22.Append(verticalTextAlignment18);
            font22.Append(fontSize22);
            font22.Append(color22);
            font22.Append(fontName22);
            font22.Append(fontScheme19);
            NumberingFormat numberingFormat18 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill21 = new Fill();

            PatternFill patternFill21 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor18 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor19 = new BackgroundColor(){ Auto = true };

            patternFill21.Append(foregroundColor18);
            patternFill21.Append(backgroundColor19);

            fill21.Append(patternFill21);
            Alignment alignment30 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat19.Append(font22);
            differentialFormat19.Append(numberingFormat18);
            differentialFormat19.Append(fill21);
            differentialFormat19.Append(alignment30);

            DifferentialFormat differentialFormat20 = new DifferentialFormat();

            Font font23 = new Font();
            Strike strike19 = new Strike(){ Val = false };
            Outline outline19 = new Outline(){ Val = false };
            Shadow shadow19 = new Shadow(){ Val = false };
            Underline underline19 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment19 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize23 = new FontSize(){ Val = 14D };
            Color color23 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName23 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme20 = new FontScheme(){ Val = FontSchemeValues.None };

            font23.Append(strike19);
            font23.Append(outline19);
            font23.Append(shadow19);
            font23.Append(underline19);
            font23.Append(verticalTextAlignment19);
            font23.Append(fontSize23);
            font23.Append(color23);
            font23.Append(fontName23);
            font23.Append(fontScheme20);

            Fill fill22 = new Fill();

            PatternFill patternFill22 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor19 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor20 = new BackgroundColor(){ Auto = true };

            patternFill22.Append(foregroundColor19);
            patternFill22.Append(backgroundColor20);

            fill22.Append(patternFill22);
            Alignment alignment31 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat20.Append(font23);
            differentialFormat20.Append(fill22);
            differentialFormat20.Append(alignment31);

            DifferentialFormat differentialFormat21 = new DifferentialFormat();

            Font font24 = new Font();
            Strike strike20 = new Strike(){ Val = false };
            Outline outline20 = new Outline(){ Val = false };
            Shadow shadow20 = new Shadow(){ Val = false };
            Underline underline20 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment20 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize24 = new FontSize(){ Val = 14D };
            Color color24 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName24 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme21 = new FontScheme(){ Val = FontSchemeValues.None };

            font24.Append(strike20);
            font24.Append(outline20);
            font24.Append(shadow20);
            font24.Append(underline20);
            font24.Append(verticalTextAlignment20);
            font24.Append(fontSize24);
            font24.Append(color24);
            font24.Append(fontName24);
            font24.Append(fontScheme21);
            NumberingFormat numberingFormat19 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill23 = new Fill();

            PatternFill patternFill23 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor20 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor21 = new BackgroundColor(){ Auto = true };

            patternFill23.Append(foregroundColor20);
            patternFill23.Append(backgroundColor21);

            fill23.Append(patternFill23);
            Alignment alignment32 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat21.Append(font24);
            differentialFormat21.Append(numberingFormat19);
            differentialFormat21.Append(fill23);
            differentialFormat21.Append(alignment32);

            DifferentialFormat differentialFormat22 = new DifferentialFormat();

            Font font25 = new Font();
            Strike strike21 = new Strike(){ Val = false };
            Outline outline21 = new Outline(){ Val = false };
            Shadow shadow21 = new Shadow(){ Val = false };
            Underline underline21 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment21 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize25 = new FontSize(){ Val = 14D };
            Color color25 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName25 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme22 = new FontScheme(){ Val = FontSchemeValues.None };

            font25.Append(strike21);
            font25.Append(outline21);
            font25.Append(shadow21);
            font25.Append(underline21);
            font25.Append(verticalTextAlignment21);
            font25.Append(fontSize25);
            font25.Append(color25);
            font25.Append(fontName25);
            font25.Append(fontScheme22);
            NumberingFormat numberingFormat20 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill24 = new Fill();

            PatternFill patternFill24 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor21 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor22 = new BackgroundColor(){ Auto = true };

            patternFill24.Append(foregroundColor21);
            patternFill24.Append(backgroundColor22);

            fill24.Append(patternFill24);
            Alignment alignment33 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat22.Append(font25);
            differentialFormat22.Append(numberingFormat20);
            differentialFormat22.Append(fill24);
            differentialFormat22.Append(alignment33);

            DifferentialFormat differentialFormat23 = new DifferentialFormat();

            Font font26 = new Font();
            Strike strike22 = new Strike(){ Val = false };
            Outline outline22 = new Outline(){ Val = false };
            Shadow shadow22 = new Shadow(){ Val = false };
            Underline underline22 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment22 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize26 = new FontSize(){ Val = 14D };
            Color color26 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName26 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme23 = new FontScheme(){ Val = FontSchemeValues.None };

            font26.Append(strike22);
            font26.Append(outline22);
            font26.Append(shadow22);
            font26.Append(underline22);
            font26.Append(verticalTextAlignment22);
            font26.Append(fontSize26);
            font26.Append(color26);
            font26.Append(fontName26);
            font26.Append(fontScheme23);
            NumberingFormat numberingFormat21 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill25 = new Fill();

            PatternFill patternFill25 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor22 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor23 = new BackgroundColor(){ Auto = true };

            patternFill25.Append(foregroundColor22);
            patternFill25.Append(backgroundColor23);

            fill25.Append(patternFill25);
            Alignment alignment34 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat23.Append(font26);
            differentialFormat23.Append(numberingFormat21);
            differentialFormat23.Append(fill25);
            differentialFormat23.Append(alignment34);

            DifferentialFormat differentialFormat24 = new DifferentialFormat();

            Font font27 = new Font();
            Strike strike23 = new Strike(){ Val = false };
            Outline outline23 = new Outline(){ Val = false };
            Shadow shadow23 = new Shadow(){ Val = false };
            Underline underline23 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment23 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize27 = new FontSize(){ Val = 14D };
            Color color27 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName27 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme24 = new FontScheme(){ Val = FontSchemeValues.None };

            font27.Append(strike23);
            font27.Append(outline23);
            font27.Append(shadow23);
            font27.Append(underline23);
            font27.Append(verticalTextAlignment23);
            font27.Append(fontSize27);
            font27.Append(color27);
            font27.Append(fontName27);
            font27.Append(fontScheme24);
            NumberingFormat numberingFormat22 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill26 = new Fill();

            PatternFill patternFill26 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor23 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor24 = new BackgroundColor(){ Auto = true };

            patternFill26.Append(foregroundColor23);
            patternFill26.Append(backgroundColor24);

            fill26.Append(patternFill26);
            Alignment alignment35 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat24.Append(font27);
            differentialFormat24.Append(numberingFormat22);
            differentialFormat24.Append(fill26);
            differentialFormat24.Append(alignment35);

            DifferentialFormat differentialFormat25 = new DifferentialFormat();

            Font font28 = new Font();
            Strike strike24 = new Strike(){ Val = false };
            Outline outline24 = new Outline(){ Val = false };
            Shadow shadow24 = new Shadow(){ Val = false };
            Underline underline24 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment24 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize28 = new FontSize(){ Val = 14D };
            Color color28 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName28 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme25 = new FontScheme(){ Val = FontSchemeValues.None };

            font28.Append(strike24);
            font28.Append(outline24);
            font28.Append(shadow24);
            font28.Append(underline24);
            font28.Append(verticalTextAlignment24);
            font28.Append(fontSize28);
            font28.Append(color28);
            font28.Append(fontName28);
            font28.Append(fontScheme25);
            NumberingFormat numberingFormat23 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill27 = new Fill();

            PatternFill patternFill27 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor24 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor25 = new BackgroundColor(){ Auto = true };

            patternFill27.Append(foregroundColor24);
            patternFill27.Append(backgroundColor25);

            fill27.Append(patternFill27);
            Alignment alignment36 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat25.Append(font28);
            differentialFormat25.Append(numberingFormat23);
            differentialFormat25.Append(fill27);
            differentialFormat25.Append(alignment36);

            DifferentialFormat differentialFormat26 = new DifferentialFormat();

            Font font29 = new Font();
            Strike strike25 = new Strike(){ Val = false };
            Outline outline25 = new Outline(){ Val = false };
            Shadow shadow25 = new Shadow(){ Val = false };
            Underline underline25 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment25 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize29 = new FontSize(){ Val = 14D };
            Color color29 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName29 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme26 = new FontScheme(){ Val = FontSchemeValues.None };

            font29.Append(strike25);
            font29.Append(outline25);
            font29.Append(shadow25);
            font29.Append(underline25);
            font29.Append(verticalTextAlignment25);
            font29.Append(fontSize29);
            font29.Append(color29);
            font29.Append(fontName29);
            font29.Append(fontScheme26);
            NumberingFormat numberingFormat24 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill28 = new Fill();

            PatternFill patternFill28 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor25 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor26 = new BackgroundColor(){ Auto = true };

            patternFill28.Append(foregroundColor25);
            patternFill28.Append(backgroundColor26);

            fill28.Append(patternFill28);
            Alignment alignment37 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat26.Append(font29);
            differentialFormat26.Append(numberingFormat24);
            differentialFormat26.Append(fill28);
            differentialFormat26.Append(alignment37);

            DifferentialFormat differentialFormat27 = new DifferentialFormat();

            Font font30 = new Font();
            Strike strike26 = new Strike(){ Val = false };
            Outline outline26 = new Outline(){ Val = false };
            Shadow shadow26 = new Shadow(){ Val = false };
            Underline underline26 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment26 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize30 = new FontSize(){ Val = 14D };
            Color color30 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName30 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme27 = new FontScheme(){ Val = FontSchemeValues.None };

            font30.Append(strike26);
            font30.Append(outline26);
            font30.Append(shadow26);
            font30.Append(underline26);
            font30.Append(verticalTextAlignment26);
            font30.Append(fontSize30);
            font30.Append(color30);
            font30.Append(fontName30);
            font30.Append(fontScheme27);
            NumberingFormat numberingFormat25 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill29 = new Fill();

            PatternFill patternFill29 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor26 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor27 = new BackgroundColor(){ Auto = true };

            patternFill29.Append(foregroundColor26);
            patternFill29.Append(backgroundColor27);

            fill29.Append(patternFill29);
            Alignment alignment38 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat27.Append(font30);
            differentialFormat27.Append(numberingFormat25);
            differentialFormat27.Append(fill29);
            differentialFormat27.Append(alignment38);

            DifferentialFormat differentialFormat28 = new DifferentialFormat();

            Font font31 = new Font();
            Strike strike27 = new Strike(){ Val = false };
            Outline outline27 = new Outline(){ Val = false };
            Shadow shadow27 = new Shadow(){ Val = false };
            Underline underline27 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment27 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize31 = new FontSize(){ Val = 14D };
            Color color31 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName31 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme28 = new FontScheme(){ Val = FontSchemeValues.None };

            font31.Append(strike27);
            font31.Append(outline27);
            font31.Append(shadow27);
            font31.Append(underline27);
            font31.Append(verticalTextAlignment27);
            font31.Append(fontSize31);
            font31.Append(color31);
            font31.Append(fontName31);
            font31.Append(fontScheme28);
            NumberingFormat numberingFormat26 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill30 = new Fill();

            PatternFill patternFill30 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor27 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor28 = new BackgroundColor(){ Auto = true };

            patternFill30.Append(foregroundColor27);
            patternFill30.Append(backgroundColor28);

            fill30.Append(patternFill30);
            Alignment alignment39 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat28.Append(font31);
            differentialFormat28.Append(numberingFormat26);
            differentialFormat28.Append(fill30);
            differentialFormat28.Append(alignment39);

            DifferentialFormat differentialFormat29 = new DifferentialFormat();

            Font font32 = new Font();
            Strike strike28 = new Strike(){ Val = false };
            Outline outline28 = new Outline(){ Val = false };
            Shadow shadow28 = new Shadow(){ Val = false };
            Underline underline28 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment28 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize32 = new FontSize(){ Val = 14D };
            Color color32 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName32 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme29 = new FontScheme(){ Val = FontSchemeValues.None };

            font32.Append(strike28);
            font32.Append(outline28);
            font32.Append(shadow28);
            font32.Append(underline28);
            font32.Append(verticalTextAlignment28);
            font32.Append(fontSize32);
            font32.Append(color32);
            font32.Append(fontName32);
            font32.Append(fontScheme29);
            NumberingFormat numberingFormat27 = new NumberingFormat(){ NumberFormatId = (UInt32Value)30U, FormatCode = "@" };

            Fill fill31 = new Fill();

            PatternFill patternFill31 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor28 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor29 = new BackgroundColor(){ Auto = true };

            patternFill31.Append(foregroundColor28);
            patternFill31.Append(backgroundColor29);

            fill31.Append(patternFill31);
            Alignment alignment40 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat29.Append(font32);
            differentialFormat29.Append(numberingFormat27);
            differentialFormat29.Append(fill31);
            differentialFormat29.Append(alignment40);

            DifferentialFormat differentialFormat30 = new DifferentialFormat();

            Font font33 = new Font();
            Strike strike29 = new Strike(){ Val = false };
            Outline outline29 = new Outline(){ Val = false };
            Shadow shadow29 = new Shadow(){ Val = false };
            Underline underline29 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment29 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize33 = new FontSize(){ Val = 14D };
            Color color33 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName33 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme30 = new FontScheme(){ Val = FontSchemeValues.None };

            font33.Append(strike29);
            font33.Append(outline29);
            font33.Append(shadow29);
            font33.Append(underline29);
            font33.Append(verticalTextAlignment29);
            font33.Append(fontSize33);
            font33.Append(color33);
            font33.Append(fontName33);
            font33.Append(fontScheme30);
            NumberingFormat numberingFormat28 = new NumberingFormat(){ NumberFormatId = (UInt32Value)1U, FormatCode = "0" };

            Fill fill32 = new Fill();

            PatternFill patternFill32 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor29 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor30 = new BackgroundColor(){ Auto = true };

            patternFill32.Append(foregroundColor29);
            patternFill32.Append(backgroundColor30);

            fill32.Append(patternFill32);
            Alignment alignment41 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat30.Append(font33);
            differentialFormat30.Append(numberingFormat28);
            differentialFormat30.Append(fill32);
            differentialFormat30.Append(alignment41);

            DifferentialFormat differentialFormat31 = new DifferentialFormat();

            Font font34 = new Font();
            Strike strike30 = new Strike(){ Val = false };
            Outline outline30 = new Outline(){ Val = false };
            Shadow shadow30 = new Shadow(){ Val = false };
            Underline underline30 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment30 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize34 = new FontSize(){ Val = 14D };
            Color color34 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName34 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme31 = new FontScheme(){ Val = FontSchemeValues.None };

            font34.Append(strike30);
            font34.Append(outline30);
            font34.Append(shadow30);
            font34.Append(underline30);
            font34.Append(verticalTextAlignment30);
            font34.Append(fontSize34);
            font34.Append(color34);
            font34.Append(fontName34);
            font34.Append(fontScheme31);

            Fill fill33 = new Fill();

            PatternFill patternFill33 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor30 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor31 = new BackgroundColor(){ Auto = true };

            patternFill33.Append(foregroundColor30);
            patternFill33.Append(backgroundColor31);

            fill33.Append(patternFill33);
            Alignment alignment42 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat31.Append(font34);
            differentialFormat31.Append(fill33);
            differentialFormat31.Append(alignment42);

            DifferentialFormat differentialFormat32 = new DifferentialFormat();

            Font font35 = new Font();
            Strike strike31 = new Strike(){ Val = false };
            Outline outline31 = new Outline(){ Val = false };
            Shadow shadow31 = new Shadow(){ Val = false };
            Underline underline31 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment31 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize35 = new FontSize(){ Val = 14D };
            Color color35 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName35 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme32 = new FontScheme(){ Val = FontSchemeValues.None };

            font35.Append(strike31);
            font35.Append(outline31);
            font35.Append(shadow31);
            font35.Append(underline31);
            font35.Append(verticalTextAlignment31);
            font35.Append(fontSize35);
            font35.Append(color35);
            font35.Append(fontName35);
            font35.Append(fontScheme32);

            Fill fill34 = new Fill();

            PatternFill patternFill34 = new PatternFill(){ PatternType = PatternValues.None };
            ForegroundColor foregroundColor31 = new ForegroundColor(){ Indexed = (UInt32Value)64U };
            BackgroundColor backgroundColor32 = new BackgroundColor(){ Auto = true };

            patternFill34.Append(foregroundColor31);
            patternFill34.Append(backgroundColor32);

            fill34.Append(patternFill34);
            Alignment alignment43 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat32.Append(font35);
            differentialFormat32.Append(fill34);
            differentialFormat32.Append(alignment43);

            DifferentialFormat differentialFormat33 = new DifferentialFormat();

            Border border2 = new Border();

            LeftBorder leftBorder2 = new LeftBorder(){ Style = BorderStyleValues.Thin };
            Color color36 = new Color(){ Auto = true };

            leftBorder2.Append(color36);

            RightBorder rightBorder2 = new RightBorder(){ Style = BorderStyleValues.Thin };
            Color color37 = new Color(){ Auto = true };

            rightBorder2.Append(color37);

            TopBorder topBorder2 = new TopBorder(){ Style = BorderStyleValues.Thin };
            Color color38 = new Color(){ Auto = true };

            topBorder2.Append(color38);

            BottomBorder bottomBorder2 = new BottomBorder(){ Style = BorderStyleValues.Thin };
            Color color39 = new Color(){ Auto = true };

            bottomBorder2.Append(color39);

            VerticalBorder verticalBorder1 = new VerticalBorder(){ Style = BorderStyleValues.Thin };
            Color color40 = new Color(){ Auto = true };

            verticalBorder1.Append(color40);

            HorizontalBorder horizontalBorder1 = new HorizontalBorder(){ Style = BorderStyleValues.Thin };
            Color color41 = new Color(){ Auto = true };

            horizontalBorder1.Append(color41);

            border2.Append(leftBorder2);
            border2.Append(rightBorder2);
            border2.Append(topBorder2);
            border2.Append(bottomBorder2);
            border2.Append(verticalBorder1);
            border2.Append(horizontalBorder1);

            differentialFormat33.Append(border2);

            differentialFormats1.Append(differentialFormat1);
            differentialFormats1.Append(differentialFormat2);
            differentialFormats1.Append(differentialFormat3);
            differentialFormats1.Append(differentialFormat4);
            differentialFormats1.Append(differentialFormat5);
            differentialFormats1.Append(differentialFormat6);
            differentialFormats1.Append(differentialFormat7);
            differentialFormats1.Append(differentialFormat8);
            differentialFormats1.Append(differentialFormat9);
            differentialFormats1.Append(differentialFormat10);
            differentialFormats1.Append(differentialFormat11);
            differentialFormats1.Append(differentialFormat12);
            differentialFormats1.Append(differentialFormat13);
            differentialFormats1.Append(differentialFormat14);
            differentialFormats1.Append(differentialFormat15);
            differentialFormats1.Append(differentialFormat16);
            differentialFormats1.Append(differentialFormat17);
            differentialFormats1.Append(differentialFormat18);
            differentialFormats1.Append(differentialFormat19);
            differentialFormats1.Append(differentialFormat20);
            differentialFormats1.Append(differentialFormat21);
            differentialFormats1.Append(differentialFormat22);
            differentialFormats1.Append(differentialFormat23);
            differentialFormats1.Append(differentialFormat24);
            differentialFormats1.Append(differentialFormat25);
            differentialFormats1.Append(differentialFormat26);
            differentialFormats1.Append(differentialFormat27);
            differentialFormats1.Append(differentialFormat28);
            differentialFormats1.Append(differentialFormat29);
            differentialFormats1.Append(differentialFormat30);
            differentialFormats1.Append(differentialFormat31);
            differentialFormats1.Append(differentialFormat32);
            differentialFormats1.Append(differentialFormat33);

            TableStyles tableStyles1 = new TableStyles(){ Count = (UInt32Value)1U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleLight16" };

            TableStyle tableStyle1 = new TableStyle(){ Name = "Стиль таблицы 1", Pivot = false, Count = (UInt32Value)1U };
            TableStyleElement tableStyleElement1 = new TableStyleElement(){ Type = TableStyleValues.WholeTable, FormatId = (UInt32Value)32U };

            tableStyle1.Append(tableStyleElement1);

            tableStyles1.Append(tableStyle1);

            StylesheetExtensionList stylesheetExtensionList1 = new StylesheetExtensionList();

            StylesheetExtension stylesheetExtension1 = new StylesheetExtension(){ Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            X14.SlicerStyles slicerStyles1 = new X14.SlicerStyles(){ DefaultSlicerStyle = "SlicerStyleLight1" };

            stylesheetExtension1.Append(slicerStyles1);

            StylesheetExtension stylesheetExtension2 = new StylesheetExtension(){ Uri = "{9260A510-F301-46a8-8635-F512D64BE5F5}" };
            stylesheetExtension2.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            X15.TimelineStyles timelineStyles1 = new X15.TimelineStyles(){ DefaultTimelineStyle = "TimeSlicerStyleLight1" };

            stylesheetExtension2.Append(timelineStyles1);

            stylesheetExtensionList1.Append(stylesheetExtension1);
            stylesheetExtensionList1.Append(stylesheetExtension2);

            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(stylesheetExtensionList1);

            workbookStylesPart1.Stylesheet = stylesheet1;
        }

        // Generates content of themePart1.
        private void GenerateThemePart1Content(ThemePart themePart1)
        {
            A.Theme theme1 = new A.Theme(){ Name = "Тема Office" };
            theme1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.ThemeElements themeElements1 = new A.ThemeElements();

            A.ColorScheme colorScheme1 = new A.ColorScheme(){ Name = "Стандартная" };

            A.Dark1Color dark1Color1 = new A.Dark1Color();
            A.SystemColor systemColor1 = new A.SystemColor(){ Val = A.SystemColorValues.WindowText, LastColor = "000000" };

            dark1Color1.Append(systemColor1);

            A.Light1Color light1Color1 = new A.Light1Color();
            A.SystemColor systemColor2 = new A.SystemColor(){ Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

            light1Color1.Append(systemColor2);

            A.Dark2Color dark2Color1 = new A.Dark2Color();
            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex(){ Val = "1F497D" };

            dark2Color1.Append(rgbColorModelHex1);

            A.Light2Color light2Color1 = new A.Light2Color();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex(){ Val = "EEECE1" };

            light2Color1.Append(rgbColorModelHex2);

            A.Accent1Color accent1Color1 = new A.Accent1Color();
            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex(){ Val = "4F81BD" };

            accent1Color1.Append(rgbColorModelHex3);

            A.Accent2Color accent2Color1 = new A.Accent2Color();
            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex(){ Val = "C0504D" };

            accent2Color1.Append(rgbColorModelHex4);

            A.Accent3Color accent3Color1 = new A.Accent3Color();
            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex(){ Val = "9BBB59" };

            accent3Color1.Append(rgbColorModelHex5);

            A.Accent4Color accent4Color1 = new A.Accent4Color();
            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex(){ Val = "8064A2" };

            accent4Color1.Append(rgbColorModelHex6);

            A.Accent5Color accent5Color1 = new A.Accent5Color();
            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex(){ Val = "4BACC6" };

            accent5Color1.Append(rgbColorModelHex7);

            A.Accent6Color accent6Color1 = new A.Accent6Color();
            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex(){ Val = "F79646" };

            accent6Color1.Append(rgbColorModelHex8);

            A.Hyperlink hyperlink1 = new A.Hyperlink();
            A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex(){ Val = "0000FF" };

            hyperlink1.Append(rgbColorModelHex9);

            A.FollowedHyperlinkColor followedHyperlinkColor1 = new A.FollowedHyperlinkColor();
            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex(){ Val = "800080" };

            followedHyperlinkColor1.Append(rgbColorModelHex10);

            colorScheme1.Append(dark1Color1);
            colorScheme1.Append(light1Color1);
            colorScheme1.Append(dark2Color1);
            colorScheme1.Append(light2Color1);
            colorScheme1.Append(accent1Color1);
            colorScheme1.Append(accent2Color1);
            colorScheme1.Append(accent3Color1);
            colorScheme1.Append(accent4Color1);
            colorScheme1.Append(accent5Color1);
            colorScheme1.Append(accent6Color1);
            colorScheme1.Append(hyperlink1);
            colorScheme1.Append(followedHyperlinkColor1);

            A.FontScheme fontScheme33 = new A.FontScheme(){ Name = "Стандартная" };

            A.MajorFont majorFont1 = new A.MajorFont();
            A.LatinFont latinFont1 = new A.LatinFont(){ Typeface = "Cambria", Panose = "020F0302020204030204" };
            A.EastAsianFont eastAsianFont1 = new A.EastAsianFont(){ Typeface = "" };
            A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont(){ Typeface = "" };
            A.SupplementalFont supplementalFont1 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
            A.SupplementalFont supplementalFont2 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont3 = new A.SupplementalFont(){ Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont4 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont5 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont6 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont7 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Tahoma" };
            A.SupplementalFont supplementalFont8 = new A.SupplementalFont(){ Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont9 = new A.SupplementalFont(){ Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont10 = new A.SupplementalFont(){ Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont11 = new A.SupplementalFont(){ Script = "Khmr", Typeface = "MoolBoran" };
            A.SupplementalFont supplementalFont12 = new A.SupplementalFont(){ Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont13 = new A.SupplementalFont(){ Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont14 = new A.SupplementalFont(){ Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont15 = new A.SupplementalFont(){ Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont16 = new A.SupplementalFont(){ Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont17 = new A.SupplementalFont(){ Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont18 = new A.SupplementalFont(){ Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont19 = new A.SupplementalFont(){ Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont20 = new A.SupplementalFont(){ Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont21 = new A.SupplementalFont(){ Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont22 = new A.SupplementalFont(){ Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont23 = new A.SupplementalFont(){ Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont24 = new A.SupplementalFont(){ Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont25 = new A.SupplementalFont(){ Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont26 = new A.SupplementalFont(){ Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont27 = new A.SupplementalFont(){ Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont28 = new A.SupplementalFont(){ Script = "Viet", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont29 = new A.SupplementalFont(){ Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont30 = new A.SupplementalFont(){ Script = "Geor", Typeface = "Sylfaen" };

            majorFont1.Append(latinFont1);
            majorFont1.Append(eastAsianFont1);
            majorFont1.Append(complexScriptFont1);
            majorFont1.Append(supplementalFont1);
            majorFont1.Append(supplementalFont2);
            majorFont1.Append(supplementalFont3);
            majorFont1.Append(supplementalFont4);
            majorFont1.Append(supplementalFont5);
            majorFont1.Append(supplementalFont6);
            majorFont1.Append(supplementalFont7);
            majorFont1.Append(supplementalFont8);
            majorFont1.Append(supplementalFont9);
            majorFont1.Append(supplementalFont10);
            majorFont1.Append(supplementalFont11);
            majorFont1.Append(supplementalFont12);
            majorFont1.Append(supplementalFont13);
            majorFont1.Append(supplementalFont14);
            majorFont1.Append(supplementalFont15);
            majorFont1.Append(supplementalFont16);
            majorFont1.Append(supplementalFont17);
            majorFont1.Append(supplementalFont18);
            majorFont1.Append(supplementalFont19);
            majorFont1.Append(supplementalFont20);
            majorFont1.Append(supplementalFont21);
            majorFont1.Append(supplementalFont22);
            majorFont1.Append(supplementalFont23);
            majorFont1.Append(supplementalFont24);
            majorFont1.Append(supplementalFont25);
            majorFont1.Append(supplementalFont26);
            majorFont1.Append(supplementalFont27);
            majorFont1.Append(supplementalFont28);
            majorFont1.Append(supplementalFont29);
            majorFont1.Append(supplementalFont30);

            A.MinorFont minorFont1 = new A.MinorFont();
            A.LatinFont latinFont2 = new A.LatinFont(){ Typeface = "Calibri", Panose = "020F0502020204030204" };
            A.EastAsianFont eastAsianFont2 = new A.EastAsianFont(){ Typeface = "" };
            A.ComplexScriptFont complexScriptFont2 = new A.ComplexScriptFont(){ Typeface = "" };
            A.SupplementalFont supplementalFont31 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
            A.SupplementalFont supplementalFont32 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont33 = new A.SupplementalFont(){ Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont34 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont35 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Arial" };
            A.SupplementalFont supplementalFont36 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Arial" };
            A.SupplementalFont supplementalFont37 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Tahoma" };
            A.SupplementalFont supplementalFont38 = new A.SupplementalFont(){ Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont39 = new A.SupplementalFont(){ Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont40 = new A.SupplementalFont(){ Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont41 = new A.SupplementalFont(){ Script = "Khmr", Typeface = "DaunPenh" };
            A.SupplementalFont supplementalFont42 = new A.SupplementalFont(){ Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont43 = new A.SupplementalFont(){ Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont44 = new A.SupplementalFont(){ Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont45 = new A.SupplementalFont(){ Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont46 = new A.SupplementalFont(){ Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont47 = new A.SupplementalFont(){ Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont48 = new A.SupplementalFont(){ Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont49 = new A.SupplementalFont(){ Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont50 = new A.SupplementalFont(){ Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont51 = new A.SupplementalFont(){ Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont52 = new A.SupplementalFont(){ Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont53 = new A.SupplementalFont(){ Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont54 = new A.SupplementalFont(){ Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont55 = new A.SupplementalFont(){ Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont56 = new A.SupplementalFont(){ Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont57 = new A.SupplementalFont(){ Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont58 = new A.SupplementalFont(){ Script = "Viet", Typeface = "Arial" };
            A.SupplementalFont supplementalFont59 = new A.SupplementalFont(){ Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont60 = new A.SupplementalFont(){ Script = "Geor", Typeface = "Sylfaen" };

            minorFont1.Append(latinFont2);
            minorFont1.Append(eastAsianFont2);
            minorFont1.Append(complexScriptFont2);
            minorFont1.Append(supplementalFont31);
            minorFont1.Append(supplementalFont32);
            minorFont1.Append(supplementalFont33);
            minorFont1.Append(supplementalFont34);
            minorFont1.Append(supplementalFont35);
            minorFont1.Append(supplementalFont36);
            minorFont1.Append(supplementalFont37);
            minorFont1.Append(supplementalFont38);
            minorFont1.Append(supplementalFont39);
            minorFont1.Append(supplementalFont40);
            minorFont1.Append(supplementalFont41);
            minorFont1.Append(supplementalFont42);
            minorFont1.Append(supplementalFont43);
            minorFont1.Append(supplementalFont44);
            minorFont1.Append(supplementalFont45);
            minorFont1.Append(supplementalFont46);
            minorFont1.Append(supplementalFont47);
            minorFont1.Append(supplementalFont48);
            minorFont1.Append(supplementalFont49);
            minorFont1.Append(supplementalFont50);
            minorFont1.Append(supplementalFont51);
            minorFont1.Append(supplementalFont52);
            minorFont1.Append(supplementalFont53);
            minorFont1.Append(supplementalFont54);
            minorFont1.Append(supplementalFont55);
            minorFont1.Append(supplementalFont56);
            minorFont1.Append(supplementalFont57);
            minorFont1.Append(supplementalFont58);
            minorFont1.Append(supplementalFont59);
            minorFont1.Append(supplementalFont60);

            fontScheme33.Append(majorFont1);
            fontScheme33.Append(minorFont1);

            A.FormatScheme formatScheme1 = new A.FormatScheme(){ Name = "Стандартная" };

            A.FillStyleList fillStyleList1 = new A.FillStyleList();

            A.SolidFill solidFill1 = new A.SolidFill();
            A.SchemeColor schemeColor1 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill1.Append(schemeColor1);

            A.GradientFill gradientFill1 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList1 = new A.GradientStopList();

            A.GradientStop gradientStop1 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor2 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint1 = new A.Tint(){ Val = 50000 };
            A.SaturationModulation saturationModulation1 = new A.SaturationModulation(){ Val = 300000 };

            schemeColor2.Append(tint1);
            schemeColor2.Append(saturationModulation1);

            gradientStop1.Append(schemeColor2);

            A.GradientStop gradientStop2 = new A.GradientStop(){ Position = 35000 };

            A.SchemeColor schemeColor3 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint2 = new A.Tint(){ Val = 37000 };
            A.SaturationModulation saturationModulation2 = new A.SaturationModulation(){ Val = 300000 };

            schemeColor3.Append(tint2);
            schemeColor3.Append(saturationModulation2);

            gradientStop2.Append(schemeColor3);

            A.GradientStop gradientStop3 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor4 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint3 = new A.Tint(){ Val = 15000 };
            A.SaturationModulation saturationModulation3 = new A.SaturationModulation(){ Val = 350000 };

            schemeColor4.Append(tint3);
            schemeColor4.Append(saturationModulation3);

            gradientStop3.Append(schemeColor4);

            gradientStopList1.Append(gradientStop1);
            gradientStopList1.Append(gradientStop2);
            gradientStopList1.Append(gradientStop3);
            A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill(){ Angle = 16200000, Scaled = true };

            gradientFill1.Append(gradientStopList1);
            gradientFill1.Append(linearGradientFill1);

            A.GradientFill gradientFill2 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList2 = new A.GradientStopList();

            A.GradientStop gradientStop4 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor5 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade1 = new A.Shade(){ Val = 51000 };
            A.SaturationModulation saturationModulation4 = new A.SaturationModulation(){ Val = 130000 };

            schemeColor5.Append(shade1);
            schemeColor5.Append(saturationModulation4);

            gradientStop4.Append(schemeColor5);

            A.GradientStop gradientStop5 = new A.GradientStop(){ Position = 80000 };

            A.SchemeColor schemeColor6 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade2 = new A.Shade(){ Val = 93000 };
            A.SaturationModulation saturationModulation5 = new A.SaturationModulation(){ Val = 130000 };

            schemeColor6.Append(shade2);
            schemeColor6.Append(saturationModulation5);

            gradientStop5.Append(schemeColor6);

            A.GradientStop gradientStop6 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor7 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade3 = new A.Shade(){ Val = 94000 };
            A.SaturationModulation saturationModulation6 = new A.SaturationModulation(){ Val = 135000 };

            schemeColor7.Append(shade3);
            schemeColor7.Append(saturationModulation6);

            gradientStop6.Append(schemeColor7);

            gradientStopList2.Append(gradientStop4);
            gradientStopList2.Append(gradientStop5);
            gradientStopList2.Append(gradientStop6);
            A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill(){ Angle = 16200000, Scaled = false };

            gradientFill2.Append(gradientStopList2);
            gradientFill2.Append(linearGradientFill2);

            fillStyleList1.Append(solidFill1);
            fillStyleList1.Append(gradientFill1);
            fillStyleList1.Append(gradientFill2);

            A.LineStyleList lineStyleList1 = new A.LineStyleList();

            A.Outline outline32 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill2 = new A.SolidFill();

            A.SchemeColor schemeColor8 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade4 = new A.Shade(){ Val = 95000 };
            A.SaturationModulation saturationModulation7 = new A.SaturationModulation(){ Val = 105000 };

            schemeColor8.Append(shade4);
            schemeColor8.Append(saturationModulation7);

            solidFill2.Append(schemeColor8);
            A.PresetDash presetDash1 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline32.Append(solidFill2);
            outline32.Append(presetDash1);

            A.Outline outline33 = new A.Outline(){ Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill3 = new A.SolidFill();
            A.SchemeColor schemeColor9 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill3.Append(schemeColor9);
            A.PresetDash presetDash2 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline33.Append(solidFill3);
            outline33.Append(presetDash2);

            A.Outline outline34 = new A.Outline(){ Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill4 = new A.SolidFill();
            A.SchemeColor schemeColor10 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill4.Append(schemeColor10);
            A.PresetDash presetDash3 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline34.Append(solidFill4);
            outline34.Append(presetDash3);

            lineStyleList1.Append(outline32);
            lineStyleList1.Append(outline33);
            lineStyleList1.Append(outline34);

            A.EffectStyleList effectStyleList1 = new A.EffectStyleList();

            A.EffectStyle effectStyle1 = new A.EffectStyle();

            A.EffectList effectList1 = new A.EffectList();

            A.OuterShadow outerShadow1 = new A.OuterShadow(){ BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha1 = new A.Alpha(){ Val = 38000 };

            rgbColorModelHex11.Append(alpha1);

            outerShadow1.Append(rgbColorModelHex11);

            effectList1.Append(outerShadow1);

            effectStyle1.Append(effectList1);

            A.EffectStyle effectStyle2 = new A.EffectStyle();

            A.EffectList effectList2 = new A.EffectList();

            A.OuterShadow outerShadow2 = new A.OuterShadow(){ BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex12 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha2 = new A.Alpha(){ Val = 35000 };

            rgbColorModelHex12.Append(alpha2);

            outerShadow2.Append(rgbColorModelHex12);

            effectList2.Append(outerShadow2);

            effectStyle2.Append(effectList2);

            A.EffectStyle effectStyle3 = new A.EffectStyle();

            A.EffectList effectList3 = new A.EffectList();

            A.OuterShadow outerShadow3 = new A.OuterShadow(){ BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex13 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha3 = new A.Alpha(){ Val = 35000 };

            rgbColorModelHex13.Append(alpha3);

            outerShadow3.Append(rgbColorModelHex13);

            effectList3.Append(outerShadow3);

            A.Scene3DType scene3DType1 = new A.Scene3DType();

            A.Camera camera1 = new A.Camera(){ Preset = A.PresetCameraValues.OrthographicFront };
            A.Rotation rotation1 = new A.Rotation(){ Latitude = 0, Longitude = 0, Revolution = 0 };

            camera1.Append(rotation1);

            A.LightRig lightRig1 = new A.LightRig(){ Rig = A.LightRigValues.ThreePoints, Direction = A.LightRigDirectionValues.Top };
            A.Rotation rotation2 = new A.Rotation(){ Latitude = 0, Longitude = 0, Revolution = 1200000 };

            lightRig1.Append(rotation2);

            scene3DType1.Append(camera1);
            scene3DType1.Append(lightRig1);

            A.Shape3DType shape3DType1 = new A.Shape3DType();
            A.BevelTop bevelTop1 = new A.BevelTop(){ Width = 63500L, Height = 25400L };

            shape3DType1.Append(bevelTop1);

            effectStyle3.Append(effectList3);
            effectStyle3.Append(scene3DType1);
            effectStyle3.Append(shape3DType1);

            effectStyleList1.Append(effectStyle1);
            effectStyleList1.Append(effectStyle2);
            effectStyleList1.Append(effectStyle3);

            A.BackgroundFillStyleList backgroundFillStyleList1 = new A.BackgroundFillStyleList();

            A.SolidFill solidFill5 = new A.SolidFill();
            A.SchemeColor schemeColor11 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill5.Append(schemeColor11);

            A.GradientFill gradientFill3 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList3 = new A.GradientStopList();

            A.GradientStop gradientStop7 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor12 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint4 = new A.Tint(){ Val = 40000 };
            A.SaturationModulation saturationModulation8 = new A.SaturationModulation(){ Val = 350000 };

            schemeColor12.Append(tint4);
            schemeColor12.Append(saturationModulation8);

            gradientStop7.Append(schemeColor12);

            A.GradientStop gradientStop8 = new A.GradientStop(){ Position = 40000 };

            A.SchemeColor schemeColor13 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint5 = new A.Tint(){ Val = 45000 };
            A.Shade shade5 = new A.Shade(){ Val = 99000 };
            A.SaturationModulation saturationModulation9 = new A.SaturationModulation(){ Val = 350000 };

            schemeColor13.Append(tint5);
            schemeColor13.Append(shade5);
            schemeColor13.Append(saturationModulation9);

            gradientStop8.Append(schemeColor13);

            A.GradientStop gradientStop9 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor14 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade6 = new A.Shade(){ Val = 20000 };
            A.SaturationModulation saturationModulation10 = new A.SaturationModulation(){ Val = 255000 };

            schemeColor14.Append(shade6);
            schemeColor14.Append(saturationModulation10);

            gradientStop9.Append(schemeColor14);

            gradientStopList3.Append(gradientStop7);
            gradientStopList3.Append(gradientStop8);
            gradientStopList3.Append(gradientStop9);

            A.PathGradientFill pathGradientFill1 = new A.PathGradientFill(){ Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle1 = new A.FillToRectangle(){ Left = 50000, Top = -80000, Right = 50000, Bottom = 180000 };

            pathGradientFill1.Append(fillToRectangle1);

            gradientFill3.Append(gradientStopList3);
            gradientFill3.Append(pathGradientFill1);

            A.GradientFill gradientFill4 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList4 = new A.GradientStopList();

            A.GradientStop gradientStop10 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor15 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint6 = new A.Tint(){ Val = 80000 };
            A.SaturationModulation saturationModulation11 = new A.SaturationModulation(){ Val = 300000 };

            schemeColor15.Append(tint6);
            schemeColor15.Append(saturationModulation11);

            gradientStop10.Append(schemeColor15);

            A.GradientStop gradientStop11 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor16 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade7 = new A.Shade(){ Val = 30000 };
            A.SaturationModulation saturationModulation12 = new A.SaturationModulation(){ Val = 200000 };

            schemeColor16.Append(shade7);
            schemeColor16.Append(saturationModulation12);

            gradientStop11.Append(schemeColor16);

            gradientStopList4.Append(gradientStop10);
            gradientStopList4.Append(gradientStop11);

            A.PathGradientFill pathGradientFill2 = new A.PathGradientFill(){ Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle2 = new A.FillToRectangle(){ Left = 50000, Top = 50000, Right = 50000, Bottom = 50000 };

            pathGradientFill2.Append(fillToRectangle2);

            gradientFill4.Append(gradientStopList4);
            gradientFill4.Append(pathGradientFill2);

            backgroundFillStyleList1.Append(solidFill5);
            backgroundFillStyleList1.Append(gradientFill3);
            backgroundFillStyleList1.Append(gradientFill4);

            formatScheme1.Append(fillStyleList1);
            formatScheme1.Append(lineStyleList1);
            formatScheme1.Append(effectStyleList1);
            formatScheme1.Append(backgroundFillStyleList1);

            themeElements1.Append(colorScheme1);
            themeElements1.Append(fontScheme33);
            themeElements1.Append(formatScheme1);
            A.ObjectDefaults objectDefaults1 = new A.ObjectDefaults();
            A.ExtraColorSchemeList extraColorSchemeList1 = new A.ExtraColorSchemeList();

            theme1.Append(themeElements1);
            theme1.Append(objectDefaults1);
            theme1.Append(extraColorSchemeList1);

            themePart1.Theme = theme1;
        }

        // Generates content of worksheetPart1.
        private void GenerateWorksheetPart1Content(WorksheetPart worksheetPart1)
        {
            Worksheet worksheet1 = new Worksheet(){ MCAttributes = new MarkupCompatibilityAttributes(){ Ignorable = "x14ac" }  };
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            worksheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            worksheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            SheetProperties sheetProperties1 = new SheetProperties();
            PageSetupProperties pageSetupProperties1 = new PageSetupProperties(){ FitToPage = true };

            sheetProperties1.Append(pageSetupProperties1);
            SheetDimension sheetDimension1 = new SheetDimension(){ Reference = "A1:AC5" };

            SheetViews sheetViews1 = new SheetViews();

            SheetView sheetView1 = new SheetView(){ TabSelected = true, ZoomScale = (UInt32Value)70U, ZoomScaleNormal = (UInt32Value)70U, WorkbookViewId = (UInt32Value)0U };
            Selection selection1 = new Selection(){ ActiveCell = "A3", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A3" } };

            sheetView1.Append(selection1);

            sheetViews1.Append(sheetView1);
            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties(){ DefaultRowHeight = 15D, DyDescent = 0.25D };

            Columns columns1 = new Columns();
            Column column1 = new Column(){ Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 7D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column2 = new Column(){ Min = (UInt32Value)2U, Max = (UInt32Value)5U, Width = 19.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column3 = new Column(){ Min = (UInt32Value)6U, Max = (UInt32Value)6U, Width = 27.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column4 = new Column(){ Min = (UInt32Value)7U, Max = (UInt32Value)7U, Width = 19.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column5 = new Column(){ Min = (UInt32Value)8U, Max = (UInt32Value)8U, Width = 49.42578125D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column6 = new Column(){ Min = (UInt32Value)9U, Max = (UInt32Value)9U, Width = 19.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column7 = new Column(){ Min = (UInt32Value)10U, Max = (UInt32Value)10U, Width = 14.85546875D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column8 = new Column(){ Min = (UInt32Value)11U, Max = (UInt32Value)11U, Width = 19.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column9 = new Column(){ Min = (UInt32Value)12U, Max = (UInt32Value)12U, Width = 35.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column10 = new Column(){ Min = (UInt32Value)13U, Max = (UInt32Value)13U, Width = 15.5703125D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column11 = new Column(){ Min = (UInt32Value)14U, Max = (UInt32Value)14U, Width = 7.5703125D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column12 = new Column(){ Min = (UInt32Value)15U, Max = (UInt32Value)15U, Width = 19.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column13 = new Column(){ Min = (UInt32Value)16U, Max = (UInt32Value)17U, Width = 35.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column14 = new Column(){ Min = (UInt32Value)18U, Max = (UInt32Value)18U, Width = 14.85546875D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column15 = new Column(){ Min = (UInt32Value)19U, Max = (UInt32Value)19U, Width = 12.140625D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column16 = new Column(){ Min = (UInt32Value)20U, Max = (UInt32Value)20U, Width = 19.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column17 = new Column(){ Min = (UInt32Value)21U, Max = (UInt32Value)21U, Width = 35.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column18 = new Column(){ Min = (UInt32Value)22U, Max = (UInt32Value)22U, Width = 9.140625D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column19 = new Column(){ Min = (UInt32Value)23U, Max = (UInt32Value)23U, Width = 35.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column20 = new Column(){ Min = (UInt32Value)24U, Max = (UInt32Value)25U, Width = 19.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column21 = new Column(){ Min = (UInt32Value)26U, Max = (UInt32Value)29U, Width = 35.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column22 = new Column(){ Min = (UInt32Value)30U, Max = (UInt32Value)16384U, Width = 9.140625D, Style = (UInt32Value)1U };

            columns1.Append(column1);
            columns1.Append(column2);
            columns1.Append(column3);
            columns1.Append(column4);
            columns1.Append(column5);
            columns1.Append(column6);
            columns1.Append(column7);
            columns1.Append(column8);
            columns1.Append(column9);
            columns1.Append(column10);
            columns1.Append(column11);
            columns1.Append(column12);
            columns1.Append(column13);
            columns1.Append(column14);
            columns1.Append(column15);
            columns1.Append(column16);
            columns1.Append(column17);
            columns1.Append(column18);
            columns1.Append(column19);
            columns1.Append(column20);
            columns1.Append(column21);
            columns1.Append(column22);

            SheetData sheetData1 = new SheetData();

            Row row1 = new Row(){ RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:29" }, StyleIndex = (UInt32Value)5U, CustomFormat = true, Height = 34.5D, CustomHeight = true, DyDescent = 0.25D };
            Cell cell1 = new Cell(){ CellReference = "A1", StyleIndex = (UInt32Value)4U };
            Cell cell2 = new Cell(){ CellReference = "B1", StyleIndex = (UInt32Value)4U };
            Cell cell3 = new Cell(){ CellReference = "C1", StyleIndex = (UInt32Value)4U };
            Cell cell4 = new Cell(){ CellReference = "D1", StyleIndex = (UInt32Value)4U };
            Cell cell5 = new Cell(){ CellReference = "E1", StyleIndex = (UInt32Value)4U };

            Cell cell6 = new Cell(){ CellReference = "F1", StyleIndex = (UInt32Value)13U, DataType = CellValues.SharedString };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = "0";

            cell6.Append(cellValue1);
            Cell cell7 = new Cell(){ CellReference = "G1", StyleIndex = (UInt32Value)13U };
            Cell cell8 = new Cell(){ CellReference = "H1", StyleIndex = (UInt32Value)13U };
            Cell cell9 = new Cell(){ CellReference = "I1", StyleIndex = (UInt32Value)13U };
            Cell cell10 = new Cell(){ CellReference = "J1", StyleIndex = (UInt32Value)4U };
            Cell cell11 = new Cell(){ CellReference = "K1", StyleIndex = (UInt32Value)4U };
            Cell cell12 = new Cell(){ CellReference = "L1", StyleIndex = (UInt32Value)4U };
            Cell cell13 = new Cell(){ CellReference = "M1", StyleIndex = (UInt32Value)4U };
            Cell cell14 = new Cell(){ CellReference = "N1", StyleIndex = (UInt32Value)4U };
            Cell cell15 = new Cell(){ CellReference = "O1", StyleIndex = (UInt32Value)4U };
            Cell cell16 = new Cell(){ CellReference = "P1", StyleIndex = (UInt32Value)4U };
            Cell cell17 = new Cell(){ CellReference = "Q1", StyleIndex = (UInt32Value)4U };
            Cell cell18 = new Cell(){ CellReference = "R1", StyleIndex = (UInt32Value)4U };
            Cell cell19 = new Cell(){ CellReference = "S1", StyleIndex = (UInt32Value)4U };
            Cell cell20 = new Cell(){ CellReference = "T1", StyleIndex = (UInt32Value)4U };
            Cell cell21 = new Cell(){ CellReference = "U1", StyleIndex = (UInt32Value)4U };
            Cell cell22 = new Cell(){ CellReference = "V1", StyleIndex = (UInt32Value)4U };
            Cell cell23 = new Cell(){ CellReference = "W1", StyleIndex = (UInt32Value)4U };
            Cell cell24 = new Cell(){ CellReference = "X1", StyleIndex = (UInt32Value)4U };
            Cell cell25 = new Cell(){ CellReference = "Y1", StyleIndex = (UInt32Value)4U };
            Cell cell26 = new Cell(){ CellReference = "Z1", StyleIndex = (UInt32Value)4U };
            Cell cell27 = new Cell(){ CellReference = "AA1", StyleIndex = (UInt32Value)4U };
            Cell cell28 = new Cell(){ CellReference = "AB1", StyleIndex = (UInt32Value)4U };

            row1.Append(cell1);
            row1.Append(cell2);
            row1.Append(cell3);
            row1.Append(cell4);
            row1.Append(cell5);
            row1.Append(cell6);
            row1.Append(cell7);
            row1.Append(cell8);
            row1.Append(cell9);
            row1.Append(cell10);
            row1.Append(cell11);
            row1.Append(cell12);
            row1.Append(cell13);
            row1.Append(cell14);
            row1.Append(cell15);
            row1.Append(cell16);
            row1.Append(cell17);
            row1.Append(cell18);
            row1.Append(cell19);
            row1.Append(cell20);
            row1.Append(cell21);
            row1.Append(cell22);
            row1.Append(cell23);
            row1.Append(cell24);
            row1.Append(cell25);
            row1.Append(cell26);
            row1.Append(cell27);
            row1.Append(cell28);

            Row row2 = new Row(){ RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "1:29" }, StyleIndex = (UInt32Value)2U, CustomFormat = true, Height = 18D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell29 = new Cell(){ CellReference = "A2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "1";

            cell29.Append(cellValue2);

            Cell cell30 = new Cell(){ CellReference = "B2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue3 = new CellValue();
            cellValue3.Text = "2";

            cell30.Append(cellValue3);

            Cell cell31 = new Cell(){ CellReference = "C2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "3";

            cell31.Append(cellValue4);

            Cell cell32 = new Cell(){ CellReference = "D2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "4";

            cell32.Append(cellValue5);

            Cell cell33 = new Cell(){ CellReference = "E2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue6 = new CellValue();
            cellValue6.Text = "5";

            cell33.Append(cellValue6);

            Cell cell34 = new Cell(){ CellReference = "F2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue7 = new CellValue();
            cellValue7.Text = "6";

            cell34.Append(cellValue7);

            Cell cell35 = new Cell(){ CellReference = "G2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue8 = new CellValue();
            cellValue8.Text = "7";

            cell35.Append(cellValue8);

            Cell cell36 = new Cell(){ CellReference = "H2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue9 = new CellValue();
            cellValue9.Text = "8";

            cell36.Append(cellValue9);

            Cell cell37 = new Cell(){ CellReference = "I2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue10 = new CellValue();
            cellValue10.Text = "9";

            cell37.Append(cellValue10);

            Cell cell38 = new Cell(){ CellReference = "J2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue11 = new CellValue();
            cellValue11.Text = "12";

            cell38.Append(cellValue11);

            Cell cell39 = new Cell(){ CellReference = "K2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue12 = new CellValue();
            cellValue12.Text = "13";

            cell39.Append(cellValue12);

            Cell cell40 = new Cell(){ CellReference = "L2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue13 = new CellValue();
            cellValue13.Text = "14";

            cell40.Append(cellValue13);

            Cell cell41 = new Cell(){ CellReference = "M2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue14 = new CellValue();
            cellValue14.Text = "21";

            cell41.Append(cellValue14);

            Cell cell42 = new Cell(){ CellReference = "N2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue15 = new CellValue();
            cellValue15.Text = "19";

            cell42.Append(cellValue15);

            Cell cell43 = new Cell(){ CellReference = "O2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue16 = new CellValue();
            cellValue16.Text = "20";

            cell43.Append(cellValue16);

            Cell cell44 = new Cell(){ CellReference = "P2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue17 = new CellValue();
            cellValue17.Text = "16";

            cell44.Append(cellValue17);

            Cell cell45 = new Cell(){ CellReference = "Q2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue18 = new CellValue();
            cellValue18.Text = "31";

            cell45.Append(cellValue18);

            Cell cell46 = new Cell(){ CellReference = "R2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue19 = new CellValue();
            cellValue19.Text = "22";

            cell46.Append(cellValue19);

            Cell cell47 = new Cell(){ CellReference = "S2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue20 = new CellValue();
            cellValue20.Text = "24";

            cell47.Append(cellValue20);

            Cell cell48 = new Cell(){ CellReference = "T2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue21 = new CellValue();
            cellValue21.Text = "23";

            cell48.Append(cellValue21);

            Cell cell49 = new Cell(){ CellReference = "U2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue22 = new CellValue();
            cellValue22.Text = "30";

            cell49.Append(cellValue22);

            Cell cell50 = new Cell(){ CellReference = "V2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue23 = new CellValue();
            cellValue23.Text = "29";

            cell50.Append(cellValue23);

            Cell cell51 = new Cell(){ CellReference = "W2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue24 = new CellValue();
            cellValue24.Text = "15";

            cell51.Append(cellValue24);

            Cell cell52 = new Cell(){ CellReference = "X2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue25 = new CellValue();
            cellValue25.Text = "26";

            cell52.Append(cellValue25);

            Cell cell53 = new Cell(){ CellReference = "Y2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue26 = new CellValue();
            cellValue26.Text = "27";

            cell53.Append(cellValue26);

            Cell cell54 = new Cell(){ CellReference = "Z2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue27 = new CellValue();
            cellValue27.Text = "28";

            cell54.Append(cellValue27);

            Cell cell55 = new Cell(){ CellReference = "AA2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue28 = new CellValue();
            cellValue28.Text = "17";

            cell55.Append(cellValue28);

            Cell cell56 = new Cell(){ CellReference = "AB2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue29 = new CellValue();
            cellValue29.Text = "18";

            cell56.Append(cellValue29);

            Cell cell57 = new Cell(){ CellReference = "AC2", StyleIndex = (UInt32Value)2U, DataType = CellValues.SharedString };
            CellValue cellValue30 = new CellValue();
            cellValue30.Text = "25";

            cell57.Append(cellValue30);

            row2.Append(cell29);
            row2.Append(cell30);
            row2.Append(cell31);
            row2.Append(cell32);
            row2.Append(cell33);
            row2.Append(cell34);
            row2.Append(cell35);
            row2.Append(cell36);
            row2.Append(cell37);
            row2.Append(cell38);
            row2.Append(cell39);
            row2.Append(cell40);
            row2.Append(cell41);
            row2.Append(cell42);
            row2.Append(cell43);
            row2.Append(cell44);
            row2.Append(cell45);
            row2.Append(cell46);
            row2.Append(cell47);
            row2.Append(cell48);
            row2.Append(cell49);
            row2.Append(cell50);
            row2.Append(cell51);
            row2.Append(cell52);
            row2.Append(cell53);
            row2.Append(cell54);
            row2.Append(cell55);
            row2.Append(cell56);
            row2.Append(cell57);

            Row row3 = new Row(){ RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:29" }, StyleIndex = (UInt32Value)2U, CustomFormat = true, Height = 18D, CustomHeight = true, DyDescent = 0.25D };
            Cell cell58 = new Cell(){ CellReference = "A3", StyleIndex = (UInt32Value)7U };
            Cell cell59 = new Cell(){ CellReference = "B3", StyleIndex = (UInt32Value)8U };
            Cell cell60 = new Cell(){ CellReference = "C3", StyleIndex = (UInt32Value)8U };
            Cell cell61 = new Cell(){ CellReference = "D3", StyleIndex = (UInt32Value)8U };
            Cell cell62 = new Cell(){ CellReference = "E3", StyleIndex = (UInt32Value)8U };
            Cell cell63 = new Cell(){ CellReference = "F3", StyleIndex = (UInt32Value)8U };
            Cell cell64 = new Cell(){ CellReference = "G3", StyleIndex = (UInt32Value)8U };
            Cell cell65 = new Cell(){ CellReference = "H3", StyleIndex = (UInt32Value)8U };
            Cell cell66 = new Cell(){ CellReference = "I3", StyleIndex = (UInt32Value)8U };
            Cell cell67 = new Cell(){ CellReference = "J3", StyleIndex = (UInt32Value)9U };
            Cell cell68 = new Cell(){ CellReference = "K3", StyleIndex = (UInt32Value)10U };
            Cell cell69 = new Cell(){ CellReference = "L3", StyleIndex = (UInt32Value)8U };
            Cell cell70 = new Cell(){ CellReference = "M3", StyleIndex = (UInt32Value)9U };
            Cell cell71 = new Cell(){ CellReference = "N3", StyleIndex = (UInt32Value)9U };
            Cell cell72 = new Cell(){ CellReference = "O3", StyleIndex = (UInt32Value)10U };
            Cell cell73 = new Cell(){ CellReference = "P3", StyleIndex = (UInt32Value)8U };
            Cell cell74 = new Cell(){ CellReference = "Q3", StyleIndex = (UInt32Value)8U };
            Cell cell75 = new Cell(){ CellReference = "R3", StyleIndex = (UInt32Value)8U };
            Cell cell76 = new Cell(){ CellReference = "S3", StyleIndex = (UInt32Value)8U };
            Cell cell77 = new Cell(){ CellReference = "T3", StyleIndex = (UInt32Value)8U };
            Cell cell78 = new Cell(){ CellReference = "U3", StyleIndex = (UInt32Value)8U };
            Cell cell79 = new Cell(){ CellReference = "V3", StyleIndex = (UInt32Value)11U };
            Cell cell80 = new Cell(){ CellReference = "W3", StyleIndex = (UInt32Value)8U };
            Cell cell81 = new Cell(){ CellReference = "X3", StyleIndex = (UInt32Value)10U };
            Cell cell82 = new Cell(){ CellReference = "Y3", StyleIndex = (UInt32Value)10U };
            Cell cell83 = new Cell(){ CellReference = "Z3", StyleIndex = (UInt32Value)8U };
            Cell cell84 = new Cell(){ CellReference = "AA3", StyleIndex = (UInt32Value)8U };
            Cell cell85 = new Cell(){ CellReference = "AB3", StyleIndex = (UInt32Value)8U };
            Cell cell86 = new Cell(){ CellReference = "AC3", StyleIndex = (UInt32Value)8U };

            row3.Append(cell58);
            row3.Append(cell59);
            row3.Append(cell60);
            row3.Append(cell61);
            row3.Append(cell62);
            row3.Append(cell63);
            row3.Append(cell64);
            row3.Append(cell65);
            row3.Append(cell66);
            row3.Append(cell67);
            row3.Append(cell68);
            row3.Append(cell69);
            row3.Append(cell70);
            row3.Append(cell71);
            row3.Append(cell72);
            row3.Append(cell73);
            row3.Append(cell74);
            row3.Append(cell75);
            row3.Append(cell76);
            row3.Append(cell77);
            row3.Append(cell78);
            row3.Append(cell79);
            row3.Append(cell80);
            row3.Append(cell81);
            row3.Append(cell82);
            row3.Append(cell83);
            row3.Append(cell84);
            row3.Append(cell85);
            row3.Append(cell86);

            Row row4 = new Row(){ RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "1:29" }, Height = 18D, CustomHeight = true, DyDescent = 0.25D };
            Cell cell87 = new Cell(){ CellReference = "B4", StyleIndex = (UInt32Value)6U };
            Cell cell88 = new Cell(){ CellReference = "C4", StyleIndex = (UInt32Value)6U };
            Cell cell89 = new Cell(){ CellReference = "D4", StyleIndex = (UInt32Value)6U };
            Cell cell90 = new Cell(){ CellReference = "E4", StyleIndex = (UInt32Value)6U };

            Cell cell91 = new Cell(){ CellReference = "F4", StyleIndex = (UInt32Value)12U, DataType = CellValues.SharedString };
            CellValue cellValue31 = new CellValue();
            cellValue31.Text = "10";

            cell91.Append(cellValue31);
            Cell cell92 = new Cell(){ CellReference = "G4", StyleIndex = (UInt32Value)12U };
            Cell cell93 = new Cell(){ CellReference = "H4", StyleIndex = (UInt32Value)12U };
            Cell cell94 = new Cell(){ CellReference = "I4", StyleIndex = (UInt32Value)12U };
            Cell cell95 = new Cell(){ CellReference = "J4", StyleIndex = (UInt32Value)6U };
            Cell cell96 = new Cell(){ CellReference = "K4", StyleIndex = (UInt32Value)6U };
            Cell cell97 = new Cell(){ CellReference = "L4", StyleIndex = (UInt32Value)6U };
            Cell cell98 = new Cell(){ CellReference = "M4", StyleIndex = (UInt32Value)6U };
            Cell cell99 = new Cell(){ CellReference = "N4", StyleIndex = (UInt32Value)6U };
            Cell cell100 = new Cell(){ CellReference = "O4", StyleIndex = (UInt32Value)6U };
            Cell cell101 = new Cell(){ CellReference = "P4", StyleIndex = (UInt32Value)6U };
            Cell cell102 = new Cell(){ CellReference = "Q4", StyleIndex = (UInt32Value)6U };
            Cell cell103 = new Cell(){ CellReference = "R4", StyleIndex = (UInt32Value)6U };
            Cell cell104 = new Cell(){ CellReference = "S4", StyleIndex = (UInt32Value)6U };
            Cell cell105 = new Cell(){ CellReference = "T4", StyleIndex = (UInt32Value)6U };
            Cell cell106 = new Cell(){ CellReference = "U4", StyleIndex = (UInt32Value)6U };
            Cell cell107 = new Cell(){ CellReference = "V4", StyleIndex = (UInt32Value)6U };
            Cell cell108 = new Cell(){ CellReference = "W4", StyleIndex = (UInt32Value)6U };
            Cell cell109 = new Cell(){ CellReference = "X4", StyleIndex = (UInt32Value)6U };
            Cell cell110 = new Cell(){ CellReference = "Y4", StyleIndex = (UInt32Value)6U };
            Cell cell111 = new Cell(){ CellReference = "Z4", StyleIndex = (UInt32Value)6U };
            Cell cell112 = new Cell(){ CellReference = "AA4", StyleIndex = (UInt32Value)6U };
            Cell cell113 = new Cell(){ CellReference = "AB4", StyleIndex = (UInt32Value)6U };

            row4.Append(cell87);
            row4.Append(cell88);
            row4.Append(cell89);
            row4.Append(cell90);
            row4.Append(cell91);
            row4.Append(cell92);
            row4.Append(cell93);
            row4.Append(cell94);
            row4.Append(cell95);
            row4.Append(cell96);
            row4.Append(cell97);
            row4.Append(cell98);
            row4.Append(cell99);
            row4.Append(cell100);
            row4.Append(cell101);
            row4.Append(cell102);
            row4.Append(cell103);
            row4.Append(cell104);
            row4.Append(cell105);
            row4.Append(cell106);
            row4.Append(cell107);
            row4.Append(cell108);
            row4.Append(cell109);
            row4.Append(cell110);
            row4.Append(cell111);
            row4.Append(cell112);
            row4.Append(cell113);

            Row row5 = new Row(){ RowIndex = (UInt32Value)5U, Spans = new ListValue<StringValue>() { InnerText = "1:29" }, Height = 18D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell114 = new Cell(){ CellReference = "F5", StyleIndex = (UInt32Value)3U, DataType = CellValues.SharedString };
            CellValue cellValue32 = new CellValue();
            cellValue32.Text = "11";

            cell114.Append(cellValue32);
            Cell cell115 = new Cell(){ CellReference = "G5", StyleIndex = (UInt32Value)3U };
            Cell cell116 = new Cell(){ CellReference = "H5", StyleIndex = (UInt32Value)3U };
            Cell cell117 = new Cell(){ CellReference = "I5", StyleIndex = (UInt32Value)3U };

            row5.Append(cell114);
            row5.Append(cell115);
            row5.Append(cell116);
            row5.Append(cell117);

            sheetData1.Append(row1);
            sheetData1.Append(row2);
            sheetData1.Append(row3);
            sheetData1.Append(row4);
            sheetData1.Append(row5);

            ConditionalFormatting conditionalFormatting1 = new ConditionalFormatting(){ SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1:AC3 A6:AC1048576 B4:AC5" } };

            ConditionalFormattingRule conditionalFormattingRule1 = new ConditionalFormattingRule(){ Type = ConditionalFormatValues.Expression, FormatId = (UInt32Value)0U, Priority = 1 };
            Formula formula1 = new Formula();
            formula1.Text = "INDIRECT(\"H\"&ROW())=\"ВАКАНТ\"";

            conditionalFormattingRule1.Append(formula1);

            conditionalFormatting1.Append(conditionalFormattingRule1);
            PageMargins pageMargins1 = new PageMargins(){ Left = 0.39370078740157483D, Right = 0.31496062992125984D, Top = 0.39370078740157483D, Bottom = 0.39370078740157483D, Header = 0D, Footer = 0D };
            PageSetup pageSetup1 = new PageSetup(){ PaperSize = (UInt32Value)9U, Scale = (UInt32Value)82U, FitToHeight = (UInt32Value)0U, Orientation = OrientationValues.Portrait, Id = "rId1" };

            TableParts tableParts1 = new TableParts(){ Count = (UInt32Value)1U };
            TablePart tablePart1 = new TablePart(){ Id = "rId2" };

            tableParts1.Append(tablePart1);

            worksheet1.Append(sheetProperties1);
            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(columns1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(conditionalFormatting1);
            worksheet1.Append(pageMargins1);
            worksheet1.Append(pageSetup1);
            worksheet1.Append(tableParts1);

            worksheetPart1.Worksheet = worksheet1;
        }

        // Generates content of tableDefinitionPart1.
        private void GenerateTableDefinitionPart1Content(TableDefinitionPart tableDefinitionPart1)
        {
            Table table1 = new Table(){ Id = (UInt32Value)1U, Name = "Таблица1", DisplayName = "Таблица1", Reference = "A2:AC3", TotalsRowShown = false, HeaderRowFormatId = (UInt32Value)31U, DataFormatId = (UInt32Value)30U };
            AutoFilter autoFilter1 = new AutoFilter(){ Reference = "A2:AC3" };

            TableColumns tableColumns1 = new TableColumns(){ Count = (UInt32Value)29U };
            TableColumn tableColumn1 = new TableColumn(){ Id = (UInt32Value)1U, Name = "№ п/п", DataFormatId = (UInt32Value)29U };
            TableColumn tableColumn2 = new TableColumn(){ Id = (UInt32Value)2U, Name = "Батальон", DataFormatId = (UInt32Value)28U };
            TableColumn tableColumn3 = new TableColumn(){ Id = (UInt32Value)3U, Name = "Рота", DataFormatId = (UInt32Value)27U };
            TableColumn tableColumn4 = new TableColumn(){ Id = (UInt32Value)4U, Name = "Взвод", DataFormatId = (UInt32Value)26U };
            TableColumn tableColumn5 = new TableColumn(){ Id = (UInt32Value)5U, Name = "Отделение", DataFormatId = (UInt32Value)25U };
            TableColumn tableColumn6 = new TableColumn(){ Id = (UInt32Value)6U, Name = "Должность", DataFormatId = (UInt32Value)24U };
            TableColumn tableColumn7 = new TableColumn(){ Id = (UInt32Value)7U, Name = "В/звание", DataFormatId = (UInt32Value)23U };
            TableColumn tableColumn8 = new TableColumn(){ Id = (UInt32Value)8U, Name = "Фамилия, имя и отчество", DataFormatId = (UInt32Value)22U };
            TableColumn tableColumn9 = new TableColumn(){ Id = (UInt32Value)9U, Name = "Примечание", DataFormatId = (UInt32Value)21U };
            TableColumn tableColumn10 = new TableColumn(){ Id = (UInt32Value)10U, Name = "Л.номер", DataFormatId = (UInt32Value)20U };
            TableColumn tableColumn11 = new TableColumn(){ Id = (UInt32Value)11U, Name = "Д.рождения", DataFormatId = (UInt32Value)19U };
            TableColumn tableColumn12 = new TableColumn(){ Id = (UInt32Value)12U, Name = "М.рождения", DataFormatId = (UInt32Value)18U };
            TableColumn tableColumn13 = new TableColumn(){ Id = (UInt32Value)21U, Name = "Телефон", DataFormatId = (UInt32Value)17U };
            TableColumn tableColumn14 = new TableColumn(){ Id = (UInt32Value)20U, Name = "Пол", DataFormatId = (UInt32Value)16U };
            TableColumn tableColumn15 = new TableColumn(){ Id = (UInt32Value)19U, Name = "В ВС РФ с", DataFormatId = (UInt32Value)15U };
            TableColumn tableColumn16 = new TableColumn(){ Id = (UInt32Value)13U, Name = "Приказ о назначении", DataFormatId = (UInt32Value)14U };
            TableColumn tableColumn17 = new TableColumn(){ Id = (UInt32Value)22U, Name = "Должность полностью", DataFormatId = (UInt32Value)13U };
            TableColumn tableColumn18 = new TableColumn(){ Id = (UInt32Value)28U, Name = "ВУС", DataFormatId = (UInt32Value)12U };
            TableColumn tableColumn19 = new TableColumn(){ Id = (UInt32Value)23U, Name = "Тариф", DataFormatId = (UInt32Value)11U };
            TableColumn tableColumn20 = new TableColumn(){ Id = (UInt32Value)27U, Name = "шдк", DataFormatId = (UInt32Value)10U };
            TableColumn tableColumn21 = new TableColumn(){ Id = (UInt32Value)14U, Name = "Образование", DataFormatId = (UInt32Value)9U };
            TableColumn tableColumn22 = new TableColumn(){ Id = (UInt32Value)15U, Name = "Год", DataFormatId = (UInt32Value)8U };
            TableColumn tableColumn23 = new TableColumn(){ Id = (UInt32Value)16U, Name = "Приказ на звание", DataFormatId = (UInt32Value)7U };
            TableColumn tableColumn24 = new TableColumn(){ Id = (UInt32Value)31U, Name = "Дата заключения", DataFormatId = (UInt32Value)6U };
            TableColumn tableColumn25 = new TableColumn(){ Id = (UInt32Value)30U, Name = "Дата окончания", DataFormatId = (UInt32Value)5U };
            TableColumn tableColumn26 = new TableColumn(){ Id = (UInt32Value)29U, Name = "Приказ на контракт", DataFormatId = (UInt32Value)4U };
            TableColumn tableColumn27 = new TableColumn(){ Id = (UInt32Value)17U, Name = "Состав семьи", DataFormatId = (UInt32Value)3U };
            TableColumn tableColumn28 = new TableColumn(){ Id = (UInt32Value)18U, Name = "Боевые действия", DataFormatId = (UInt32Value)2U };
            TableColumn tableColumn29 = new TableColumn(){ Id = (UInt32Value)25U, Name = "Медали", DataFormatId = (UInt32Value)1U };

            tableColumns1.Append(tableColumn1);
            tableColumns1.Append(tableColumn2);
            tableColumns1.Append(tableColumn3);
            tableColumns1.Append(tableColumn4);
            tableColumns1.Append(tableColumn5);
            tableColumns1.Append(tableColumn6);
            tableColumns1.Append(tableColumn7);
            tableColumns1.Append(tableColumn8);
            tableColumns1.Append(tableColumn9);
            tableColumns1.Append(tableColumn10);
            tableColumns1.Append(tableColumn11);
            tableColumns1.Append(tableColumn12);
            tableColumns1.Append(tableColumn13);
            tableColumns1.Append(tableColumn14);
            tableColumns1.Append(tableColumn15);
            tableColumns1.Append(tableColumn16);
            tableColumns1.Append(tableColumn17);
            tableColumns1.Append(tableColumn18);
            tableColumns1.Append(tableColumn19);
            tableColumns1.Append(tableColumn20);
            tableColumns1.Append(tableColumn21);
            tableColumns1.Append(tableColumn22);
            tableColumns1.Append(tableColumn23);
            tableColumns1.Append(tableColumn24);
            tableColumns1.Append(tableColumn25);
            tableColumns1.Append(tableColumn26);
            tableColumns1.Append(tableColumn27);
            tableColumns1.Append(tableColumn28);
            tableColumns1.Append(tableColumn29);
            TableStyleInfo tableStyleInfo1 = new TableStyleInfo(){ Name = "Стиль таблицы 1", ShowFirstColumn = false, ShowLastColumn = false, ShowRowStripes = true, ShowColumnStripes = false };

            table1.Append(autoFilter1);
            table1.Append(tableColumns1);
            table1.Append(tableStyleInfo1);

            tableDefinitionPart1.Table = table1;
        }

        // Generates content of spreadsheetPrinterSettingsPart1.
        private void GenerateSpreadsheetPrinterSettingsPart1Content(SpreadsheetPrinterSettingsPart spreadsheetPrinterSettingsPart1)
        {
            System.IO.Stream data = GetBinaryDataStream(spreadsheetPrinterSettingsPart1Data);
            spreadsheetPrinterSettingsPart1.FeedData(data);
            data.Close();
        }

        // Generates content of sharedStringTablePart1.
        private void GenerateSharedStringTablePart1Content(SharedStringTablePart sharedStringTablePart1)
        {
            SharedStringTable sharedStringTable1 = new SharedStringTable(){ Count = (UInt32Value)32U, UniqueCount = (UInt32Value)32U };

            SharedStringItem sharedStringItem1 = new SharedStringItem();
            Text text1 = new Text();
            text1.Text = "Штатно-должностной список войсковой части 71289";

            sharedStringItem1.Append(text1);

            SharedStringItem sharedStringItem2 = new SharedStringItem();
            Text text2 = new Text();
            text2.Text = "№ п/п";

            sharedStringItem2.Append(text2);

            SharedStringItem sharedStringItem3 = new SharedStringItem();
            Text text3 = new Text();
            text3.Text = "Батальон";

            sharedStringItem3.Append(text3);

            SharedStringItem sharedStringItem4 = new SharedStringItem();
            Text text4 = new Text();
            text4.Text = "Рота";

            sharedStringItem4.Append(text4);

            SharedStringItem sharedStringItem5 = new SharedStringItem();
            Text text5 = new Text();
            text5.Text = "Взвод";

            sharedStringItem5.Append(text5);

            SharedStringItem sharedStringItem6 = new SharedStringItem();
            Text text6 = new Text();
            text6.Text = "Отделение";

            sharedStringItem6.Append(text6);

            SharedStringItem sharedStringItem7 = new SharedStringItem();
            Text text7 = new Text();
            text7.Text = "Должность";

            sharedStringItem7.Append(text7);

            SharedStringItem sharedStringItem8 = new SharedStringItem();
            Text text8 = new Text();
            text8.Text = "В/звание";

            sharedStringItem8.Append(text8);

            SharedStringItem sharedStringItem9 = new SharedStringItem();
            Text text9 = new Text();
            text9.Text = "Фамилия, имя и отчество";

            sharedStringItem9.Append(text9);

            SharedStringItem sharedStringItem10 = new SharedStringItem();
            Text text10 = new Text();
            text10.Text = "Примечание";

            sharedStringItem10.Append(text10);

            SharedStringItem sharedStringItem11 = new SharedStringItem();
            Text text11 = new Text();
            text11.Text = "Начальник отделения кадров в/ч 71289";

            sharedStringItem11.Append(text11);

            SharedStringItem sharedStringItem12 = new SharedStringItem();
            Text text12 = new Text();
            text12.Text = "гв капитан                                                    А.Иванов";

            sharedStringItem12.Append(text12);

            SharedStringItem sharedStringItem13 = new SharedStringItem();
            Text text13 = new Text();
            text13.Text = "Л.номер";

            sharedStringItem13.Append(text13);

            SharedStringItem sharedStringItem14 = new SharedStringItem();
            Text text14 = new Text();
            text14.Text = "Д.рождения";

            sharedStringItem14.Append(text14);

            SharedStringItem sharedStringItem15 = new SharedStringItem();
            Text text15 = new Text();
            text15.Text = "М.рождения";

            sharedStringItem15.Append(text15);

            SharedStringItem sharedStringItem16 = new SharedStringItem();
            Text text16 = new Text();
            text16.Text = "Приказ на звание";

            sharedStringItem16.Append(text16);

            SharedStringItem sharedStringItem17 = new SharedStringItem();
            Text text17 = new Text();
            text17.Text = "Приказ о назначении";

            sharedStringItem17.Append(text17);

            SharedStringItem sharedStringItem18 = new SharedStringItem();
            Text text18 = new Text();
            text18.Text = "Состав семьи";

            sharedStringItem18.Append(text18);

            SharedStringItem sharedStringItem19 = new SharedStringItem();
            Text text19 = new Text();
            text19.Text = "Боевые действия";

            sharedStringItem19.Append(text19);

            SharedStringItem sharedStringItem20 = new SharedStringItem();
            Text text20 = new Text();
            text20.Text = "Пол";

            sharedStringItem20.Append(text20);

            SharedStringItem sharedStringItem21 = new SharedStringItem();
            Text text21 = new Text();
            text21.Text = "В ВС РФ с";

            sharedStringItem21.Append(text21);

            SharedStringItem sharedStringItem22 = new SharedStringItem();
            Text text22 = new Text();
            text22.Text = "Телефон";

            sharedStringItem22.Append(text22);

            SharedStringItem sharedStringItem23 = new SharedStringItem();
            Text text23 = new Text();
            text23.Text = "ВУС";

            sharedStringItem23.Append(text23);

            SharedStringItem sharedStringItem24 = new SharedStringItem();
            Text text24 = new Text();
            text24.Text = "шдк";

            sharedStringItem24.Append(text24);

            SharedStringItem sharedStringItem25 = new SharedStringItem();
            Text text25 = new Text();
            text25.Text = "Тариф";

            sharedStringItem25.Append(text25);

            SharedStringItem sharedStringItem26 = new SharedStringItem();
            Text text26 = new Text();
            text26.Text = "Медали";

            sharedStringItem26.Append(text26);

            SharedStringItem sharedStringItem27 = new SharedStringItem();
            Text text27 = new Text();
            text27.Text = "Дата заключения";

            sharedStringItem27.Append(text27);

            SharedStringItem sharedStringItem28 = new SharedStringItem();
            Text text28 = new Text();
            text28.Text = "Дата окончания";

            sharedStringItem28.Append(text28);

            SharedStringItem sharedStringItem29 = new SharedStringItem();
            Text text29 = new Text();
            text29.Text = "Приказ на контракт";

            sharedStringItem29.Append(text29);

            SharedStringItem sharedStringItem30 = new SharedStringItem();
            Text text30 = new Text();
            text30.Text = "Год";

            sharedStringItem30.Append(text30);

            SharedStringItem sharedStringItem31 = new SharedStringItem();
            Text text31 = new Text();
            text31.Text = "Образование";

            sharedStringItem31.Append(text31);

            SharedStringItem sharedStringItem32 = new SharedStringItem();
            Text text32 = new Text();
            text32.Text = "Должность полностью";

            sharedStringItem32.Append(text32);

            sharedStringTable1.Append(sharedStringItem1);
            sharedStringTable1.Append(sharedStringItem2);
            sharedStringTable1.Append(sharedStringItem3);
            sharedStringTable1.Append(sharedStringItem4);
            sharedStringTable1.Append(sharedStringItem5);
            sharedStringTable1.Append(sharedStringItem6);
            sharedStringTable1.Append(sharedStringItem7);
            sharedStringTable1.Append(sharedStringItem8);
            sharedStringTable1.Append(sharedStringItem9);
            sharedStringTable1.Append(sharedStringItem10);
            sharedStringTable1.Append(sharedStringItem11);
            sharedStringTable1.Append(sharedStringItem12);
            sharedStringTable1.Append(sharedStringItem13);
            sharedStringTable1.Append(sharedStringItem14);
            sharedStringTable1.Append(sharedStringItem15);
            sharedStringTable1.Append(sharedStringItem16);
            sharedStringTable1.Append(sharedStringItem17);
            sharedStringTable1.Append(sharedStringItem18);
            sharedStringTable1.Append(sharedStringItem19);
            sharedStringTable1.Append(sharedStringItem20);
            sharedStringTable1.Append(sharedStringItem21);
            sharedStringTable1.Append(sharedStringItem22);
            sharedStringTable1.Append(sharedStringItem23);
            sharedStringTable1.Append(sharedStringItem24);
            sharedStringTable1.Append(sharedStringItem25);
            sharedStringTable1.Append(sharedStringItem26);
            sharedStringTable1.Append(sharedStringItem27);
            sharedStringTable1.Append(sharedStringItem28);
            sharedStringTable1.Append(sharedStringItem29);
            sharedStringTable1.Append(sharedStringItem30);
            sharedStringTable1.Append(sharedStringItem31);
            sharedStringTable1.Append(sharedStringItem32);

            sharedStringTablePart1.SharedStringTable = sharedStringTable1;
        }

        private void SetPackageProperties(OpenXmlPackage document)
        {
            document.PackageProperties.Creator = "operki";
            document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2019-07-28T01:46:48Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2019-09-04T06:29:59Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.LastModifiedBy = "NOK";
            document.PackageProperties.LastPrinted = System.Xml.XmlConvert.ToDateTime("2019-09-04T06:25:16Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
        }

        #region Binary Data
        private string spreadsheetPrinterSettingsPart1Data = "WABlAHIAbwB4ACAAVwBvAHIAawBDAGUAbgB0AHIAZQAgADMANQA1ADAAIABQAEMATAAgADYAAAAAAAAAAAAAAAEEAATcAAcND9+BAQEACQCaCzQIZAABAAcAWAICAAEAAAADAAEAQQA0AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFBSSVYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAEAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHgALQAAAEEAcgBpAGEAbAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAICAgAAAAJABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABVAG4AdABpAHQAbABlAGQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAwADoAMAA6ADAAOgAwADoAMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAABkAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABYAgAAMgAyADIAMgAyADIAMgAyADIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEEEOARBBEIENQQ8BDAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAABBADQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAABwAAAAkAAAcAAAAJAAAHAAAACQAAAAAHADQImgs0CJoLAAAAAAAAAAAAAAAFBgQANAiaCwEAAgAAADQImgvDIKtuAAEAAAAAAAAA";

        private System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }

        #endregion

    }
}
