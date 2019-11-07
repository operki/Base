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
    class ExcelClassShdkShort
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
            definedName2.Text = "\'Выгрузка с базы\'!$A:$E";

            definedNames1.Append(definedName1);
            definedNames1.Append(definedName2);
            CalculationProperties calculationProperties1 = new CalculationProperties(){ CalculationId = (UInt32Value)145621U, CalculationMode = CalculateModeValues.Manual, CalculationCompleted = false, CalculationOnSave = false };

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
            FontSize fontSize4 = new FontSize(){ Val = 12D };
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

            CellFormats cellFormats1 = new CellFormats(){ Count = (UInt32Value)6U };
            CellFormat cellFormat2 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true };

            CellFormat cellFormat3 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment1 = new Alignment(){ Vertical = VerticalAlignmentValues.Center };

            cellFormat3.Append(alignment1);

            CellFormat cellFormat4 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)3U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment2 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat4.Append(alignment2);

            CellFormat cellFormat5 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)3U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment3 = new Alignment(){ Vertical = VerticalAlignmentValues.Center };

            cellFormat5.Append(alignment3);

            CellFormat cellFormat6 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment4 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat6.Append(alignment4);

            CellFormat cellFormat7 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment5 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat7.Append(alignment5);

            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);
            cellFormats1.Append(cellFormat6);
            cellFormats1.Append(cellFormat7);

            CellStyles cellStyles1 = new CellStyles(){ Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle(){ Name = "Обычный", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);

            DifferentialFormats differentialFormats1 = new DifferentialFormats(){ Count = (UInt32Value)12U };

            DifferentialFormat differentialFormat1 = new DifferentialFormat();

            Font font5 = new Font();
            Bold bold1 = new Bold();
            Italic italic1 = new Italic(){ Val = false };

            font5.Append(bold1);
            font5.Append(italic1);

            Border border2 = new Border();
            LeftBorder leftBorder2 = new LeftBorder();
            RightBorder rightBorder2 = new RightBorder();
            VerticalBorder verticalBorder1 = new VerticalBorder();
            HorizontalBorder horizontalBorder1 = new HorizontalBorder();

            border2.Append(leftBorder2);
            border2.Append(rightBorder2);
            border2.Append(verticalBorder1);
            border2.Append(horizontalBorder1);

            differentialFormat1.Append(font5);
            differentialFormat1.Append(border2);

            DifferentialFormat differentialFormat2 = new DifferentialFormat();

            Fill fill3 = new Fill();

            PatternFill patternFill3 = new PatternFill();
            BackgroundColor backgroundColor1 = new BackgroundColor(){ Theme = (UInt32Value)0U, Tint = -0.14996795556505021D };

            patternFill3.Append(backgroundColor1);

            fill3.Append(patternFill3);

            differentialFormat2.Append(fill3);

            DifferentialFormat differentialFormat3 = new DifferentialFormat();

            Border border3 = new Border();
            LeftBorder leftBorder3 = new LeftBorder();
            RightBorder rightBorder3 = new RightBorder();
            VerticalBorder verticalBorder2 = new VerticalBorder();
            HorizontalBorder horizontalBorder2 = new HorizontalBorder();

            border3.Append(leftBorder3);
            border3.Append(rightBorder3);
            border3.Append(verticalBorder2);
            border3.Append(horizontalBorder2);

            differentialFormat3.Append(border3);

            DifferentialFormat differentialFormat4 = new DifferentialFormat();

            Fill fill4 = new Fill();

            PatternFill patternFill4 = new PatternFill();
            BackgroundColor backgroundColor2 = new BackgroundColor(){ Theme = (UInt32Value)0U, Tint = -0.14996795556505021D };

            patternFill4.Append(backgroundColor2);

            fill4.Append(patternFill4);

            differentialFormat4.Append(fill4);

            DifferentialFormat differentialFormat5 = new DifferentialFormat();

            Font font6 = new Font();
            Strike strike1 = new Strike(){ Val = false };
            Outline outline1 = new Outline(){ Val = false };
            Shadow shadow1 = new Shadow(){ Val = false };
            Underline underline1 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment1 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize5 = new FontSize(){ Val = 12D };
            Color color5 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName5 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme2 = new FontScheme(){ Val = FontSchemeValues.None };

            font6.Append(strike1);
            font6.Append(outline1);
            font6.Append(shadow1);
            font6.Append(underline1);
            font6.Append(verticalTextAlignment1);
            font6.Append(fontSize5);
            font6.Append(color5);
            font6.Append(fontName5);
            font6.Append(fontScheme2);
            Alignment alignment6 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat5.Append(font6);
            differentialFormat5.Append(alignment6);

            DifferentialFormat differentialFormat6 = new DifferentialFormat();

            Font font7 = new Font();
            Strike strike2 = new Strike(){ Val = false };
            Outline outline2 = new Outline(){ Val = false };
            Shadow shadow2 = new Shadow(){ Val = false };
            Underline underline2 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment2 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize6 = new FontSize(){ Val = 12D };
            Color color6 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName6 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme3 = new FontScheme(){ Val = FontSchemeValues.None };

            font7.Append(strike2);
            font7.Append(outline2);
            font7.Append(shadow2);
            font7.Append(underline2);
            font7.Append(verticalTextAlignment2);
            font7.Append(fontSize6);
            font7.Append(color6);
            font7.Append(fontName6);
            font7.Append(fontScheme3);
            Alignment alignment7 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat6.Append(font7);
            differentialFormat6.Append(alignment7);

            DifferentialFormat differentialFormat7 = new DifferentialFormat();

            Font font8 = new Font();
            Strike strike3 = new Strike(){ Val = false };
            Outline outline3 = new Outline(){ Val = false };
            Shadow shadow3 = new Shadow(){ Val = false };
            Underline underline3 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment3 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize7 = new FontSize(){ Val = 12D };
            Color color7 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName7 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme4 = new FontScheme(){ Val = FontSchemeValues.None };

            font8.Append(strike3);
            font8.Append(outline3);
            font8.Append(shadow3);
            font8.Append(underline3);
            font8.Append(verticalTextAlignment3);
            font8.Append(fontSize7);
            font8.Append(color7);
            font8.Append(fontName7);
            font8.Append(fontScheme4);
            Alignment alignment8 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat7.Append(font8);
            differentialFormat7.Append(alignment8);

            DifferentialFormat differentialFormat8 = new DifferentialFormat();

            Font font9 = new Font();
            Strike strike4 = new Strike(){ Val = false };
            Outline outline4 = new Outline(){ Val = false };
            Shadow shadow4 = new Shadow(){ Val = false };
            Underline underline4 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment4 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize8 = new FontSize(){ Val = 12D };
            Color color8 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName8 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme5 = new FontScheme(){ Val = FontSchemeValues.None };

            font9.Append(strike4);
            font9.Append(outline4);
            font9.Append(shadow4);
            font9.Append(underline4);
            font9.Append(verticalTextAlignment4);
            font9.Append(fontSize8);
            font9.Append(color8);
            font9.Append(fontName8);
            font9.Append(fontScheme5);
            Alignment alignment9 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat8.Append(font9);
            differentialFormat8.Append(alignment9);

            DifferentialFormat differentialFormat9 = new DifferentialFormat();

            Font font10 = new Font();
            Strike strike5 = new Strike(){ Val = false };
            Outline outline5 = new Outline(){ Val = false };
            Shadow shadow5 = new Shadow(){ Val = false };
            Underline underline5 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment5 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize9 = new FontSize(){ Val = 12D };
            Color color9 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName9 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme6 = new FontScheme(){ Val = FontSchemeValues.None };

            font10.Append(strike5);
            font10.Append(outline5);
            font10.Append(shadow5);
            font10.Append(underline5);
            font10.Append(verticalTextAlignment5);
            font10.Append(fontSize9);
            font10.Append(color9);
            font10.Append(fontName9);
            font10.Append(fontScheme6);
            Alignment alignment10 = new Alignment(){ Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat9.Append(font10);
            differentialFormat9.Append(alignment10);

            DifferentialFormat differentialFormat10 = new DifferentialFormat();

            Font font11 = new Font();
            Strike strike6 = new Strike(){ Val = false };
            Outline outline6 = new Outline(){ Val = false };
            Shadow shadow6 = new Shadow(){ Val = false };
            Underline underline6 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment6 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize10 = new FontSize(){ Val = 12D };
            Color color10 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName10 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme7 = new FontScheme(){ Val = FontSchemeValues.None };

            font11.Append(strike6);
            font11.Append(outline6);
            font11.Append(shadow6);
            font11.Append(underline6);
            font11.Append(verticalTextAlignment6);
            font11.Append(fontSize10);
            font11.Append(color10);
            font11.Append(fontName10);
            font11.Append(fontScheme7);
            Alignment alignment11 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat10.Append(font11);
            differentialFormat10.Append(alignment11);

            DifferentialFormat differentialFormat11 = new DifferentialFormat();

            Font font12 = new Font();
            Strike strike7 = new Strike(){ Val = false };
            Outline outline7 = new Outline(){ Val = false };
            Shadow shadow7 = new Shadow(){ Val = false };
            Underline underline7 = new Underline(){ Val = UnderlineValues.None };
            VerticalTextAlignment verticalTextAlignment7 = new VerticalTextAlignment(){ Val = VerticalAlignmentRunValues.Baseline };
            FontSize fontSize11 = new FontSize(){ Val = 12D };
            Color color11 = new Color(){ Theme = (UInt32Value)1U };
            FontName fontName11 = new FontName(){ Val = "Arial" };
            FontScheme fontScheme8 = new FontScheme(){ Val = FontSchemeValues.None };

            font12.Append(strike7);
            font12.Append(outline7);
            font12.Append(shadow7);
            font12.Append(underline7);
            font12.Append(verticalTextAlignment7);
            font12.Append(fontSize11);
            font12.Append(color11);
            font12.Append(fontName11);
            font12.Append(fontScheme8);
            Alignment alignment12 = new Alignment(){ Horizontal = HorizontalAlignmentValues.General, Vertical = VerticalAlignmentValues.Center, TextRotation = (UInt32Value)0U, WrapText = false, Indent = (UInt32Value)0U, JustifyLastLine = false, ShrinkToFit = false, ReadingOrder = (UInt32Value)0U };

            differentialFormat11.Append(font12);
            differentialFormat11.Append(alignment12);

            DifferentialFormat differentialFormat12 = new DifferentialFormat();

            Border border4 = new Border();

            LeftBorder leftBorder4 = new LeftBorder(){ Style = BorderStyleValues.Thin };
            Color color12 = new Color(){ Auto = true };

            leftBorder4.Append(color12);

            RightBorder rightBorder4 = new RightBorder(){ Style = BorderStyleValues.Thin };
            Color color13 = new Color(){ Auto = true };

            rightBorder4.Append(color13);

            TopBorder topBorder2 = new TopBorder(){ Style = BorderStyleValues.Thin };
            Color color14 = new Color(){ Auto = true };

            topBorder2.Append(color14);

            BottomBorder bottomBorder2 = new BottomBorder(){ Style = BorderStyleValues.Thin };
            Color color15 = new Color(){ Auto = true };

            bottomBorder2.Append(color15);

            VerticalBorder verticalBorder3 = new VerticalBorder(){ Style = BorderStyleValues.Thin };
            Color color16 = new Color(){ Auto = true };

            verticalBorder3.Append(color16);

            HorizontalBorder horizontalBorder3 = new HorizontalBorder(){ Style = BorderStyleValues.Thin };
            Color color17 = new Color(){ Auto = true };

            horizontalBorder3.Append(color17);

            border4.Append(leftBorder4);
            border4.Append(rightBorder4);
            border4.Append(topBorder2);
            border4.Append(bottomBorder2);
            border4.Append(verticalBorder3);
            border4.Append(horizontalBorder3);

            differentialFormat12.Append(border4);

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

            TableStyles tableStyles1 = new TableStyles(){ Count = (UInt32Value)1U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleLight16" };

            TableStyle tableStyle1 = new TableStyle(){ Name = "Стиль таблицы 1", Pivot = false, Count = (UInt32Value)1U };
            TableStyleElement tableStyleElement1 = new TableStyleElement(){ Type = TableStyleValues.WholeTable, FormatId = (UInt32Value)11U };

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

            A.FontScheme fontScheme9 = new A.FontScheme(){ Name = "Стандартная" };

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

            fontScheme9.Append(majorFont1);
            fontScheme9.Append(minorFont1);

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

            A.Outline outline8 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill2 = new A.SolidFill();

            A.SchemeColor schemeColor8 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade4 = new A.Shade(){ Val = 95000 };
            A.SaturationModulation saturationModulation7 = new A.SaturationModulation(){ Val = 105000 };

            schemeColor8.Append(shade4);
            schemeColor8.Append(saturationModulation7);

            solidFill2.Append(schemeColor8);
            A.PresetDash presetDash1 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline8.Append(solidFill2);
            outline8.Append(presetDash1);

            A.Outline outline9 = new A.Outline(){ Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill3 = new A.SolidFill();
            A.SchemeColor schemeColor9 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill3.Append(schemeColor9);
            A.PresetDash presetDash2 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline9.Append(solidFill3);
            outline9.Append(presetDash2);

            A.Outline outline10 = new A.Outline(){ Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill4 = new A.SolidFill();
            A.SchemeColor schemeColor10 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill4.Append(schemeColor10);
            A.PresetDash presetDash3 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline10.Append(solidFill4);
            outline10.Append(presetDash3);

            lineStyleList1.Append(outline8);
            lineStyleList1.Append(outline9);
            lineStyleList1.Append(outline10);

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
            themeElements1.Append(fontScheme9);
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
            SheetDimension sheetDimension1 = new SheetDimension(){ Reference = "A1:E5" };

            SheetViews sheetViews1 = new SheetViews();

            SheetView sheetView1 = new SheetView(){ TabSelected = true, ZoomScale = (UInt32Value)90U, ZoomScaleNormal = (UInt32Value)90U, WorkbookViewId = (UInt32Value)0U };
            Selection selection1 = new Selection(){ ActiveCell = "A3", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A3" } };

            sheetView1.Append(selection1);

            sheetViews1.Append(sheetView1);
            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties(){ DefaultRowHeight = 15D, DyDescent = 0.25D };

            Columns columns1 = new Columns();
            Column column1 = new Column(){ Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 6D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column2 = new Column(){ Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = 27.140625D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column3 = new Column(){ Min = (UInt32Value)3U, Max = (UInt32Value)3U, Width = 17.85546875D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column4 = new Column(){ Min = (UInt32Value)4U, Max = (UInt32Value)4U, Width = 49.42578125D, Style = (UInt32Value)1U, BestFit = true, CustomWidth = true };
            Column column5 = new Column(){ Min = (UInt32Value)5U, Max = (UInt32Value)5U, Width = 19.5703125D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column6 = new Column(){ Min = (UInt32Value)6U, Max = (UInt32Value)16384U, Width = 9.140625D, Style = (UInt32Value)1U };

            columns1.Append(column1);
            columns1.Append(column2);
            columns1.Append(column3);
            columns1.Append(column4);
            columns1.Append(column5);
            columns1.Append(column6);

            SheetData sheetData1 = new SheetData();

            Row row1 = new Row(){ RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:5" }, Height = 31.5D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell1 = new Cell(){ CellReference = "A1", StyleIndex = (UInt32Value)4U, DataType = CellValues.SharedString };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = "0";

            cell1.Append(cellValue1);
            Cell cell2 = new Cell(){ CellReference = "B1", StyleIndex = (UInt32Value)4U };
            Cell cell3 = new Cell(){ CellReference = "C1", StyleIndex = (UInt32Value)4U };
            Cell cell4 = new Cell(){ CellReference = "D1", StyleIndex = (UInt32Value)4U };
            Cell cell5 = new Cell(){ CellReference = "E1", StyleIndex = (UInt32Value)4U };

            row1.Append(cell1);
            row1.Append(cell2);
            row1.Append(cell3);
            row1.Append(cell4);
            row1.Append(cell5);

            Row row2 = new Row(){ RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "1:5" }, DyDescent = 0.25D };

            Cell cell6 = new Cell(){ CellReference = "A2", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "1";

            cell6.Append(cellValue2);

            Cell cell7 = new Cell(){ CellReference = "B2", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue3 = new CellValue();
            cellValue3.Text = "2";

            cell7.Append(cellValue3);

            Cell cell8 = new Cell(){ CellReference = "C2", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "3";

            cell8.Append(cellValue4);

            Cell cell9 = new Cell(){ CellReference = "D2", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "4";

            cell9.Append(cellValue5);

            Cell cell10 = new Cell(){ CellReference = "E2", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
            CellValue cellValue6 = new CellValue();
            cellValue6.Text = "5";

            cell10.Append(cellValue6);

            row2.Append(cell6);
            row2.Append(cell7);
            row2.Append(cell8);
            row2.Append(cell9);
            row2.Append(cell10);

            Row row3 = new Row(){ RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:5" }, DyDescent = 0.25D };
            Cell cell11 = new Cell(){ CellReference = "A3", StyleIndex = (UInt32Value)2U };
            Cell cell12 = new Cell(){ CellReference = "B3", StyleIndex = (UInt32Value)3U };
            Cell cell13 = new Cell(){ CellReference = "C3", StyleIndex = (UInt32Value)3U };
            Cell cell14 = new Cell(){ CellReference = "D3", StyleIndex = (UInt32Value)3U };
            Cell cell15 = new Cell(){ CellReference = "E3", StyleIndex = (UInt32Value)3U };

            row3.Append(cell11);
            row3.Append(cell12);
            row3.Append(cell13);
            row3.Append(cell14);
            row3.Append(cell15);

            Row row4 = new Row(){ RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "1:5" }, DyDescent = 0.25D };

            Cell cell16 = new Cell(){ CellReference = "A4", StyleIndex = (UInt32Value)5U, DataType = CellValues.SharedString };
            CellValue cellValue7 = new CellValue();
            cellValue7.Text = "6";

            cell16.Append(cellValue7);
            Cell cell17 = new Cell(){ CellReference = "B4", StyleIndex = (UInt32Value)5U };
            Cell cell18 = new Cell(){ CellReference = "C4", StyleIndex = (UInt32Value)5U };
            Cell cell19 = new Cell(){ CellReference = "D4", StyleIndex = (UInt32Value)5U };
            Cell cell20 = new Cell(){ CellReference = "E4", StyleIndex = (UInt32Value)5U };

            row4.Append(cell16);
            row4.Append(cell17);
            row4.Append(cell18);
            row4.Append(cell19);
            row4.Append(cell20);

            Row row5 = new Row(){ RowIndex = (UInt32Value)5U, Spans = new ListValue<StringValue>() { InnerText = "1:5" }, DyDescent = 0.25D };

            Cell cell21 = new Cell(){ CellReference = "A5", StyleIndex = (UInt32Value)5U, DataType = CellValues.SharedString };
            CellValue cellValue8 = new CellValue();
            cellValue8.Text = "7";

            cell21.Append(cellValue8);
            Cell cell22 = new Cell(){ CellReference = "B5", StyleIndex = (UInt32Value)5U };
            Cell cell23 = new Cell(){ CellReference = "C5", StyleIndex = (UInt32Value)5U };
            Cell cell24 = new Cell(){ CellReference = "D5", StyleIndex = (UInt32Value)5U };
            Cell cell25 = new Cell(){ CellReference = "E5", StyleIndex = (UInt32Value)5U };

            row5.Append(cell21);
            row5.Append(cell22);
            row5.Append(cell23);
            row5.Append(cell24);
            row5.Append(cell25);

            sheetData1.Append(row1);
            sheetData1.Append(row2);
            sheetData1.Append(row3);
            sheetData1.Append(row4);
            sheetData1.Append(row5);

            MergeCells mergeCells1 = new MergeCells(){ Count = (UInt32Value)3U };
            MergeCell mergeCell1 = new MergeCell(){ Reference = "A1:E1" };
            MergeCell mergeCell2 = new MergeCell(){ Reference = "A4:E4" };
            MergeCell mergeCell3 = new MergeCell(){ Reference = "A5:E5" };

            mergeCells1.Append(mergeCell1);
            mergeCells1.Append(mergeCell2);
            mergeCells1.Append(mergeCell3);

            ConditionalFormatting conditionalFormatting1 = new ConditionalFormatting(){ SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1:E1048576" } };

            ConditionalFormattingRule conditionalFormattingRule1 = new ConditionalFormattingRule(){ Type = ConditionalFormatValues.Expression, FormatId = (UInt32Value)1U, Priority = 2 };
            Formula formula1 = new Formula();
            formula1.Text = "INDIRECT(\"D\"&ROW())=\"ВАКАНТ\"";

            conditionalFormattingRule1.Append(formula1);

            conditionalFormatting1.Append(conditionalFormattingRule1);

            ConditionalFormatting conditionalFormatting2 = new ConditionalFormatting(){ SequenceOfReferences = new ListValue<StringValue>() { InnerText = "B1:D1048576" } };

            ConditionalFormattingRule conditionalFormattingRule2 = new ConditionalFormattingRule(){ Type = ConditionalFormatValues.Expression, FormatId = (UInt32Value)0U, Priority = 1 };
            Formula formula2 = new Formula();
            formula2.Text = "INDIRECT(\"D\"&ROW())=\"\"";

            conditionalFormattingRule2.Append(formula2);

            conditionalFormatting2.Append(conditionalFormattingRule2);
            PageMargins pageMargins1 = new PageMargins(){ Left = 0.39370078740157483D, Right = 0.31496062992125984D, Top = 0.39370078740157483D, Bottom = 0.39370078740157483D, Header = 0D, Footer = 0D };
            PageSetup pageSetup1 = new PageSetup(){ PaperSize = (UInt32Value)9U, Scale = (UInt32Value)80U, FitToHeight = (UInt32Value)0U, Orientation = OrientationValues.Landscape, Id = "rId1" };

            TableParts tableParts1 = new TableParts(){ Count = (UInt32Value)1U };
            TablePart tablePart1 = new TablePart(){ Id = "rId2" };

            tableParts1.Append(tablePart1);

            worksheet1.Append(sheetProperties1);
            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(columns1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(mergeCells1);
            worksheet1.Append(conditionalFormatting1);
            worksheet1.Append(conditionalFormatting2);
            worksheet1.Append(pageMargins1);
            worksheet1.Append(pageSetup1);
            worksheet1.Append(tableParts1);

            worksheetPart1.Worksheet = worksheet1;
        }

        // Generates content of tableDefinitionPart1.
        private void GenerateTableDefinitionPart1Content(TableDefinitionPart tableDefinitionPart1)
        {
            Table table1 = new Table(){ Id = (UInt32Value)1U, Name = "Таблица1", DisplayName = "Таблица1", Reference = "A2:E3", TotalsRowShown = false, HeaderRowFormatId = (UInt32Value)10U, DataFormatId = (UInt32Value)9U };
            AutoFilter autoFilter1 = new AutoFilter(){ Reference = "A2:E3" };

            TableColumns tableColumns1 = new TableColumns(){ Count = (UInt32Value)5U };
            TableColumn tableColumn1 = new TableColumn(){ Id = (UInt32Value)1U, Name = "№ п/п", DataFormatId = (UInt32Value)8U };
            TableColumn tableColumn2 = new TableColumn(){ Id = (UInt32Value)6U, Name = "Должность", DataFormatId = (UInt32Value)7U };
            TableColumn tableColumn3 = new TableColumn(){ Id = (UInt32Value)7U, Name = "В/звание", DataFormatId = (UInt32Value)6U };
            TableColumn tableColumn4 = new TableColumn(){ Id = (UInt32Value)8U, Name = "Фамилия, имя и отчество", DataFormatId = (UInt32Value)5U };
            TableColumn tableColumn5 = new TableColumn(){ Id = (UInt32Value)9U, Name = "Примечание", DataFormatId = (UInt32Value)4U };

            tableColumns1.Append(tableColumn1);
            tableColumns1.Append(tableColumn2);
            tableColumns1.Append(tableColumn3);
            tableColumns1.Append(tableColumn4);
            tableColumns1.Append(tableColumn5);
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
            SharedStringTable sharedStringTable1 = new SharedStringTable(){ Count = (UInt32Value)8U, UniqueCount = (UInt32Value)8U };

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
            text3.Text = "Должность";

            sharedStringItem3.Append(text3);

            SharedStringItem sharedStringItem4 = new SharedStringItem();
            Text text4 = new Text();
            text4.Text = "В/звание";

            sharedStringItem4.Append(text4);

            SharedStringItem sharedStringItem5 = new SharedStringItem();
            Text text5 = new Text();
            text5.Text = "Фамилия, имя и отчество";

            sharedStringItem5.Append(text5);

            SharedStringItem sharedStringItem6 = new SharedStringItem();
            Text text6 = new Text();
            text6.Text = "Примечание";

            sharedStringItem6.Append(text6);

            SharedStringItem sharedStringItem7 = new SharedStringItem();
            Text text7 = new Text();
            text7.Text = "Начальник отделения кадров в/ч 71289";

            sharedStringItem7.Append(text7);

            SharedStringItem sharedStringItem8 = new SharedStringItem();
            Text text8 = new Text();
            text8.Text = "гв капитан                                                    А.Иванов";

            sharedStringItem8.Append(text8);

            sharedStringTable1.Append(sharedStringItem1);
            sharedStringTable1.Append(sharedStringItem2);
            sharedStringTable1.Append(sharedStringItem3);
            sharedStringTable1.Append(sharedStringItem4);
            sharedStringTable1.Append(sharedStringItem5);
            sharedStringTable1.Append(sharedStringItem6);
            sharedStringTable1.Append(sharedStringItem7);
            sharedStringTable1.Append(sharedStringItem8);

            sharedStringTablePart1.SharedStringTable = sharedStringTable1;
        }

        private void SetPackageProperties(OpenXmlPackage document)
        {
            document.PackageProperties.Creator = "operki";
            document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2019-07-28T01:46:48Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2019-09-04T23:18:04Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.LastModifiedBy = "NOK";
            document.PackageProperties.LastPrinted = System.Xml.XmlConvert.ToDateTime("2019-07-28T04:10:11Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
        }

        #region Binary Data
        private string spreadsheetPrinterSettingsPart1Data = "WABlAHIAbwB4ACAAVwBvAHIAawBDAGUAbgB0AHIAZQAgADMANQA1ADAAIABQAEMATAAgADYAAAAAAAAAAAAAAAEEAATcAAcND9+BAQIACQCaCzQIZAABAAcAWAICAAEAAAADAAEAQQA0AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFBSSVYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAEAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHgALQAAAEEAcgBpAGEAbAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAICAgAAAAJABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABVAG4AdABpAHQAbABlAGQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAwADoAMAA6ADAAOgAwADoAMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAABkAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABYAgAAMgAyADIAMgAyADIAMgAyADIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEEEOARBBEIENQQ8BDAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAABBADQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAABwAAAAkAAAcAAAAJAAAHAAAACQAAAAAHADQImgs0CJoLAAAAAAAAAAAAAAAFBgQANAiaCwEAAgAAADQImgvEIEJ8AAEAAAAAAAAA";

        private System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }

        #endregion

    }
}