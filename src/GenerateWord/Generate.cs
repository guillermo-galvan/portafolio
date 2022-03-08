using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GenerateWord
{
    public class Generate
    {
        private readonly int[] InitialCodeArray = { 0xE1F0, 0x1D0F, 0xCC9C, 0x84C0, 0x110C, 0x0E10, 0xF1CE, 0x313E, 0x1872, 0xE139, 0xD40F, 0x84F9, 0x280C, 0xA96A, 0x4EC3 };
        private readonly int[,] EncryptionMatrix = new int[15, 7]
{
            /* char 1  */ {0xAEFC, 0x4DD9, 0x9BB2, 0x2745, 0x4E8A, 0x9D14, 0x2A09},
            /* char 2  */ {0x7B61, 0xF6C2, 0xFDA5, 0xEB6B, 0xC6F7, 0x9DCF, 0x2BBF},
            /* char 3  */ {0x4563, 0x8AC6, 0x05AD, 0x0B5A, 0x16B4, 0x2D68, 0x5AD0},
            /* char 4  */ {0x0375, 0x06EA, 0x0DD4, 0x1BA8, 0x3750, 0x6EA0, 0xDD40},
            /* char 5  */ {0xD849, 0xA0B3, 0x5147, 0xA28E, 0x553D, 0xAA7A, 0x44D5},
            /* char 6  */ {0x6F45, 0xDE8A, 0xAD35, 0x4A4B, 0x9496, 0x390D, 0x721A},
            /* char 7  */ {0xEB23, 0xC667, 0x9CEF, 0x29FF, 0x53FE, 0xA7FC, 0x5FD9},
            /* char 8  */ {0x47D3, 0x8FA6, 0x0F6D, 0x1EDA, 0x3DB4, 0x7B68, 0xF6D0},
            /* char 9  */ {0xB861, 0x60E3, 0xC1C6, 0x93AD, 0x377B, 0x6EF6, 0xDDEC},
            /* char 10 */ {0x45A0, 0x8B40, 0x06A1, 0x0D42, 0x1A84, 0x3508, 0x6A10},
            /* char 11 */ {0xAA51, 0x4483, 0x8906, 0x022D, 0x045A, 0x08B4, 0x1168},
            /* char 12 */ {0x76B4, 0xED68, 0xCAF1, 0x85C3, 0x1BA7, 0x374E, 0x6E9C},
            /* char 13 */ {0x3730, 0x6E60, 0xDCC0, 0xA9A1, 0x4363, 0x86C6, 0x1DAD},
            /* char 14 */ {0x3331, 0x6662, 0xCCC4, 0x89A9, 0x0373, 0x06E6, 0x0DCC},
            /* char 15 */ {0x1021, 0x2042, 0x4084, 0x8108, 0x1231, 0x2462, 0x48C4}
 };

        public void GenerateWord(DataWord dataWord)
        {
            if (string.IsNullOrWhiteSpace(dataWord.PathTemplate) || !File.Exists(dataWord.PathTemplate))
            {
                throw new ArgumentNullException(nameof(dataWord.PathTemplate));
            }

            if (string.IsNullOrWhiteSpace(dataWord.PathFileFinaly))
            {
                throw new ArgumentNullException(nameof(dataWord.PathFileFinaly));
            }

            string pathBase = dataWord.PathFileFinaly;
            FileInfo fileInfo = new(dataWord.PathTemplate);
            dataWord.PathFileFinaly = $"{dataWord.PathFileFinaly}{Path.DirectorySeparatorChar}{DateTime.Now.Ticks}{fileInfo.Name}";
            File.Copy(dataWord.PathTemplate, dataWord.PathFileFinaly);

            using WordprocessingDocument doc = WordprocessingDocument.Open(dataWord.PathFileFinaly, true);

            if (!string.IsNullOrWhiteSpace(dataWord.PathEmbeddedPackageParts))
            {
                InsertFileEmbedded(dataWord, doc, ref pathBase);
                File.Delete(pathBase);
            }

            List<DocumentDetail> documentDetails = GetDocumentDetails(doc.MainDocumentPart.Document.Body);
            GenerateTableParagraph(doc.MainDocumentPart.Document.Body, documentDetails);
            GenerateHeaderParagraph(doc, documentDetails);
            GenerateFooterParagraph(doc, documentDetails);

            documentDetails.ForEach(det => SubstituteValues(det, dataWord.KeyValues));
            dataWord.DynamicTable?.ForEach(table => GenerateDynamicTable(doc.MainDocumentPart.Document.Body, table));

            SetChangeControl(doc, dataWord.Password);
        }

        private static void GenerateTableParagraph(OpenXmlElement body, List<DocumentDetail> documentDetails)
        {
            List<Table> tables = body.Elements<Table>().ToList();

            tables.ForEach(table => {

                List<TableRow> rows = table.Elements<TableRow>().ToList();

                rows.ForEach(row => {

                    List<TableCell> cells = row.Elements<TableCell>().ToList();

                    cells.ForEach(cell => {
                        documentDetails.AddRange(GetDocumentDetails(cell));
                    });

                });
            });
        }

        private static void GenerateHeaderParagraph(WordprocessingDocument doc, List<DocumentDetail> documentDetails)
        {
            doc.MainDocumentPart.HeaderParts.ToList().ForEach(headerpart => {
                Header header = headerpart.Header;
                documentDetails.AddRange(GetDocumentDetails(header));
            });
        }

        private static void GenerateFooterParagraph(WordprocessingDocument doc, List<DocumentDetail> documentDetails)
        {
            doc.MainDocumentPart.FooterParts.ToList().ForEach(footerpart => {
                Footer footer = footerpart.Footer;
                documentDetails.AddRange(GetDocumentDetails(footer));
            });
        }

        private static List<DocumentDetail> GetDocumentDetails(OpenXmlElement body)
        {
            List<DocumentDetail> detalleDocuemntos = new();

            List<Paragraph> paragraphs = body.Elements<Paragraph>().ToList();
            DocumentDetail detalle = null;

            paragraphs.ForEach(paragraph =>
            {
                detalle = new DocumentDetail
                {
                    Paragraph = paragraph,
                    Runs = paragraph.Elements<Run>().ToList(),
                    Indexs = new List<(int Index, string StringComlemento)>()
                };

                GenerateDynamicText(detalle);
                detalleDocuemntos.Add(detalle);
            });

            return detalleDocuemntos;
        }

        private static void SubstituteValues(DocumentDetail det, Dictionary<string, string> keyValues)
        {
            List<string> keys = keyValues.Keys.Where(x => det.Content.Contains(x)).Select(x => x).ToList();
            bool sisustituye = false;

            keys.ForEach(key => {

                do
                {
                    sisustituye = ReplaceText(det, key, keyValues);

                } while (sisustituye);

            });
        }

        private static bool ReplaceText(DocumentDetail det, string key, Dictionary<string, string> keyValues)
        {
            int indice = det.Indexs.FindIndex(x => x.StringComlemento.Contains(key));

            if (indice == -1)
            {
                return false;
            }

            List<Text> texts = null;
            bool result = false;

            if (det.Runs[indice].InnerText.Contains(key))
            {
                texts = det.Runs[indice].Elements<Text>().ToList();
                texts[0].Text = texts[0].Text.Replace(key, keyValues[key]);
                result = true;
            }
            else
            {
                result = ReplaceTextMultiplePositions(key, indice, det, keyValues);
            }

            GenerateDynamicText(det);
            return result;
        }

        private static bool ReplaceTextMultiplePositions(string key, int indice, DocumentDetail det, Dictionary<string, string> keyValues)
        {
            string borrar = key;
            List<int> totalIndices = new() { indice };
            borrar = key.Replace(det.Runs[indice].InnerText, "");
            indice--;
            string ultimoborrad = borrar;
            List<string> text = new();
            string join = string.Empty;
            List<Text> texts = new();

            while (indice >= 0)
            {
                if (borrar.LastIndexOf(det.Runs[indice].InnerText) > -1 ||
                    det.Runs[indice].InnerText.LastIndexOf(borrar) > -1)
                {
                    ultimoborrad = borrar;
                    if (borrar.LastIndexOf(det.Runs[indice].InnerText) > -1)
                    {
                        borrar = borrar.Replace(det.Runs[indice].InnerText, "");
                    }
                    else
                    {
                        string tmp = det.Runs[indice].InnerText[det.Runs[indice].InnerText.LastIndexOf(borrar)..];

                        borrar = borrar.Replace(tmp, "");
                    }

                    text.Clear();
                    totalIndices.ForEach(x => text.Add(det.Runs[x].InnerText));
                    text.Insert(0, det.Runs[indice].InnerText);

                    join = string.Join("", text);

                    if (key.IndexOf(join) > -1 || join.Contains(key))
                    {
                        totalIndices.Add(indice);
                        totalIndices = totalIndices.OrderBy(x => x).ToList();
                    }
                }

                if (string.IsNullOrWhiteSpace(borrar))
                {
                    indice = 0;
                }

                indice--;
            }

            totalIndices = totalIndices.OrderBy(x => x).ToList();
            indice = totalIndices.Min();

            totalIndices.ForEach(x =>
            {

                if (indice == x)
                {
                    texts = det.Runs[x].Elements<Text>().ToList();
                    if (texts[0].Text.LastIndexOf(ultimoborrad) > -1)
                    {
                        string tmpdatos = texts[0].Text.Substring(0, texts[0].Text.LastIndexOf(ultimoborrad));

                        texts[0].Text = $"{tmpdatos}{keyValues[key]}";
                    }
                    else
                    {
                        texts[0].Text = keyValues[key];
                    }
                }
                else
                {
                    texts = det.Runs[x].Elements<Text>().ToList();
                    texts[0].Text = string.Empty;
                }
            });

            return totalIndices.Count > 0;
        }

        private static void GenerateDynamicText(DocumentDetail det)
        {
            int index = 0;
            string strignParagraph = string.Empty;

            det.Indexs.Clear();
            det.Runs.ForEach(x => det.Indexs.Add((index++, GenerateText(x.InnerText, ref strignParagraph))));

            det.Content = strignParagraph;
        }

        private static string GenerateText(string concatText, ref string finallyText)
        {
            finallyText += concatText;

            return finallyText;
        }

        private DocumentProtection SetProtectionPassword(string password)
        {
            byte[] arrSalt = new byte[16];
            RandomNumberGenerator rand = new RNGCryptoServiceProvider();
            rand.GetNonZeroBytes(arrSalt);
            byte[] generatedKey = new byte[4];
            int intMaxPasswordLength = 15;

            password = password.Substring(0, Math.Min(password.Length, intMaxPasswordLength));

            byte[] arrByteChars = new byte[password.Length];

            for (int intLoop = 0; intLoop < password.Length; intLoop++)
            {
                int intTemp = Convert.ToInt32(password[intLoop]);
                arrByteChars[intLoop] = Convert.ToByte(intTemp & 0x00FF);
                if (arrByteChars[intLoop] == 0)
                    arrByteChars[intLoop] = Convert.ToByte((intTemp & 0xFF00) >> 8);
            }

            int intHighOrderWord = InitialCodeArray[arrByteChars.Length - 1];

            for (int intLoop = 0; intLoop < arrByteChars.Length; intLoop++)
            {
                int tmp = intMaxPasswordLength - arrByteChars.Length + intLoop;
                for (int intBit = 0; intBit < 7; intBit++)
                {
                    if ((arrByteChars[intLoop] & (0x0001 << intBit)) != 0)
                    {
                        intHighOrderWord ^= EncryptionMatrix[tmp, intBit];
                    }
                }
            }

            int intLowOrderWord = 0;

            for (int intLoopChar = arrByteChars.Length - 1; intLoopChar >= 0; intLoopChar--)
            {
                intLowOrderWord = (((intLowOrderWord >> 14) & 0x0001) | ((intLowOrderWord << 1) & 0x7FFF)) ^ arrByteChars[intLoopChar];
            }

            intLowOrderWord = (((intLowOrderWord >> 14) & 0x0001) | ((intLowOrderWord << 1) & 0x7FFF)) ^ arrByteChars.Length ^ 0xCE4B;

            int intCombinedkey = (intHighOrderWord << 16) + intLowOrderWord;

            for (int intTemp = 0; intTemp < 4; intTemp++)
            {
                generatedKey[intTemp] = Convert.ToByte(((uint)(intCombinedkey & (0x000000FF << (intTemp * 8)))) >> (intTemp * 8));
            }

            StringBuilder sb = new();
            for (int intTemp = 0; intTemp < 4; intTemp++)
            {
                sb.Append(Convert.ToString(generatedKey[intTemp], 16));
            }
            generatedKey = Encoding.Unicode.GetBytes(sb.ToString().ToUpper());

            byte[] tmpArray1 = generatedKey;
            byte[] tmpArray2 = arrSalt;
            byte[] tempKey = new byte[tmpArray1.Length + tmpArray2.Length];
            Buffer.BlockCopy(tmpArray2, 0, tempKey, 0, tmpArray2.Length);
            Buffer.BlockCopy(tmpArray1, 0, tempKey, tmpArray2.Length, tmpArray1.Length);
            generatedKey = tempKey;

            int iterations = 50000;

            HashAlgorithm sha1 = new SHA1Managed();
            generatedKey = sha1.ComputeHash(generatedKey);
            byte[] iterator = new byte[4];
            for (int intTmp = 0; intTmp < iterations; intTmp++)
            {

                //When iterating on the hash, you are supposed to append the current iteration number.
                iterator[0] = Convert.ToByte((intTmp & 0x000000FF) >> 0);
                iterator[1] = Convert.ToByte((intTmp & 0x0000FF00) >> 8);
                iterator[2] = Convert.ToByte((intTmp & 0x00FF0000) >> 16);
                iterator[3] = Convert.ToByte((intTmp & 0xFF000000) >> 24);

                generatedKey = ConcatByteArrays(iterator, generatedKey);
                generatedKey = sha1.ComputeHash(generatedKey);
            }

            DocumentProtection documentProtection = new ()
            {
                Edit = DocumentProtectionValues.TrackedChanges,
                Enforcement = new OnOffValue(true),
                CryptographicProviderType = CryptProviderValues.RsaFull,
                CryptographicAlgorithmClass = CryptAlgorithmClassValues.Hash,
                CryptographicAlgorithmType = CryptAlgorithmValues.TypeAny,
                CryptographicAlgorithmSid = 4, // SHA1 The iteration count is unsigned                
                CryptographicSpinCount = (uint)iterations,
                Hash = Convert.ToBase64String(generatedKey),
                Salt = Convert.ToBase64String(arrSalt)
            };

            return documentProtection;
        }

        private static byte[] ConcatByteArrays(byte[] array1, byte[] array2)
        {
            byte[] result = new byte[array1.Length + array2.Length];
            Buffer.BlockCopy(array2, 0, result, 0, array2.Length);
            Buffer.BlockCopy(array1, 0, result, array2.Length, array1.Length);
            return result;
        }

        private void SetChangeControlPath(string password, string path)
        {
            using WordprocessingDocument doc = WordprocessingDocument.Open(path, true);
            SetChangeControl(doc, password);
        }

        private void SetChangeControl(WordprocessingDocument doc, string password)
        {
            ProofState pfs = doc.MainDocumentPart.DocumentSettingsPart.Settings.ChildElements.First<ProofState>();
            pfs?.Remove();
            doc.MainDocumentPart.DocumentSettingsPart.Settings.AppendChild(new ProofState() { Spelling = ProofingStateValues.Clean, Grammar = ProofingStateValues.Clean });

            TrackRevisions tr = doc.MainDocumentPart.DocumentSettingsPart.Settings.ChildElements.First<TrackRevisions>();
            tr?.Remove();
            doc.MainDocumentPart.DocumentSettingsPart.Settings.AppendChild(new TrackRevisions());

            DocumentProtection dp = doc.MainDocumentPart.DocumentSettingsPart.Settings.ChildElements.First<DocumentProtection>();
            dp?.Remove();

            if (!string.IsNullOrWhiteSpace(password))
            {
                doc.MainDocumentPart.DocumentSettingsPart.Settings.AppendChild(SetProtectionPassword(password));
            }
        }

        private void InsertFileEmbedded(DataWord dataWord, WordprocessingDocument doc, ref string pathBase)
        {
            FileInfo fileInfo = new (dataWord.PathEmbeddedPackageParts);
            pathBase = $"{pathBase}{Path.DirectorySeparatorChar}{DateTime.Now}{fileInfo.Name}";
            File.Copy(fileInfo.FullName, pathBase);
            SetChangeControlPath(dataWord.Password, pathBase);

            IEnumerable<EmbeddedPackagePart> embeddedPackagePart = doc.MainDocumentPart.EmbeddedPackageParts;

            foreach (EmbeddedPackagePart part in embeddedPackagePart)
            {
                byte[] file = File.ReadAllBytes(pathBase);
                using MemoryStream stream = new(file);
                part.FeedData(stream);
                stream.Close();
            }
        }

        public static Paragraph GenerateParagraph()
        {
            Paragraph paragraph1 = new () { RsidParagraphAddition = "00E87930", RsidParagraphProperties = "00DA6DB8", RsidRunAdditionDefault = "00E87930", ParagraphId = "19F1A58A", TextId = "77777777" };

            ParagraphProperties paragraphProperties1 = new ();
            ParagraphStyleId paragraphStyleId1 = new () { Val = "Prrafodelista" };

            Tabs tabs1 = new ();
            TabStop tabStop1 = new () { Val = TabStopValues.Left, Position = 3326 };
            TabStop tabStop2 = new () { Val = TabStopValues.Left, Position = 6653 };

            tabs1.Append(tabStop1);
            tabs1.Append(tabStop2);
            Indentation indentation1 = new () { Left = "0" };
            Justification justification1 = new () { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ();
            RunFonts runFonts1 = new () { Ascii = "Arial", HighAnsi = "Arial", ComplexScript = "Arial" };
            FontSize fontSize1 = new () { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript1 = new () { Val = "16" };
            Languages languages1 = new () { Val = "es-ES_tradnl" };

            paragraphMarkRunProperties1.Append(runFonts1);
            paragraphMarkRunProperties1.Append(fontSize1);
            paragraphMarkRunProperties1.Append(fontSizeComplexScript1);
            paragraphMarkRunProperties1.Append(languages1);

            paragraphProperties1.Append(paragraphStyleId1);
            paragraphProperties1.Append(tabs1);
            paragraphProperties1.Append(indentation1);
            paragraphProperties1.Append(justification1);
            paragraphProperties1.Append(paragraphMarkRunProperties1);

            paragraph1.Append(paragraphProperties1);
            return paragraph1;
        }

        public static Run GenerateRun(string text)
        {
            Run run1 = new () { RsidRunProperties = "00E87930" };

            RunProperties runProperties1 = new ();
            RunFonts runFonts1 = new () { Ascii = "Arial", HighAnsi = "Arial", ComplexScript = "Arial" };
            FontSize fontSize1 = new () { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript1 = new () { Val = "16" };
            Highlight highlight1 = new () { Val = HighlightColorValues.Yellow };
            Languages languages1 = new () { Val = "es-ES_tradnl" };

            runProperties1.Append(runFonts1);
            runProperties1.Append(fontSize1);
            runProperties1.Append(fontSizeComplexScript1);
            runProperties1.Append(highlight1);
            runProperties1.Append(languages1);
            Text text1 = new ()
            {
                Text = text
            };

            run1.Append(runProperties1);
            run1.Append(text1);
            return run1;
        }

        private static void GenerateDynamicTable(OpenXmlElement body, DynamicTable dynamicTable)
        {
            if (dynamicTable.Rows == null || dynamicTable.Rows.Count == 0)
            {
                return;
            }

            List<Table> tables = body.Elements<Table>().ToList();
            List<(Table table, TableRow row)> tabla = new();

            tables.ForEach(table => {

                List<TableRow> rows = table.Elements<TableRow>().ToList();

                rows.ForEach(row => {

                    List<TableCell> cells = row.Elements<TableCell>().ToList();

                    cells.ForEach(cell => {
                        List<DocumentDetail> detalleDocuemntos = GetDocumentDetails(cell);

                        if (detalleDocuemntos.Any(x => x.Indexs.Any(x => x.StringComlemento.Contains(dynamicTable.TableName))))
                        {
                            tabla.Add((table, row));
                        }
                    });
                });
            });

            switch (dynamicTable.TypeIntegration)
            {
                case TypeIntegration.InRow:
                    IntegrationInRow(tabla, dynamicTable.Rows);

                    break;
                case TypeIntegration.InCell:
                    IntegrationInCell(tabla, dynamicTable.Rows);
                    break;
            }
        }

        private static void IntegrationInRow(List<(Table table, TableRow row)> table, List<Row> rows)
        {
            table.ForEach(detail => {
                rows.ForEach(fila => {

                    TableRow newRow = (TableRow)detail.row.Clone();

                    List<TableCell> cells = newRow.Elements<TableCell>().ToList();

                    for (int i = 0; i < cells.Count; i++)
                    {
                        List<Paragraph> paragraphs = cells[i].Elements<Paragraph>().ToList();

                        paragraphs.ForEach(paragraph => {

                            List<Run> runs = paragraph.Elements<Run>().ToList();
                            bool encontrado = false;

                            for (int y = 0; y < runs.Count; y++)
                            {
                                Text text = runs[y].Elements<Text>().FirstOrDefault();

                                if (text != null && !encontrado)
                                {
                                    encontrado = true;
                                    text.Text = fila.Columns[i];
                                }
                                else
                                {
                                    text.Text = string.Empty;
                                }
                            }
                        });
                    }

                    detail.table.AppendChild(newRow);
                });

                detail.row.Remove();
            });
        }

        private static void IntegrationInCell(List<(Table table, TableRow row)> table, List<Row> rows)
        {
            table.ForEach(detail =>
            {

                TableRow newRow = (TableRow)detail.row.Clone();

                List<TableCell> cells = newRow.Elements<TableCell>().ToList();

                cells.ForEach(cell =>
                {
                    List<Paragraph> paragraphs = cell.Elements<Paragraph>().ToList();
                    paragraphs.ForEach(para => para.Remove());
                });

                for (int i = 0; i < cells.Count; i++)
                {
                    int index = 0;
                    rows.ForEach(fila =>
                    {

                        if (index == 0)
                        {
                            cells[i].Append(GenerateParagraph());
                        }
                        else
                        {
                            cells[i].Append(GenerateParagraph());
                            cells[i].Append(GenerateParagraph());
                        }

                        Paragraph paragraph = GenerateParagraph();
                        paragraph.Append(GenerateRun(fila.Columns[i]));
                        cells[i].Append(paragraph);
                        index++;
                    });

                    cells[i].Append(GenerateParagraph());
                }

                detail.table.AppendChild(newRow);
                detail.row.Remove();
            });
        }
    }
}
