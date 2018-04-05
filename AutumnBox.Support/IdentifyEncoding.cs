/* =============================================================================*\
*
* Filename: IdentifyEncoding
* Description: 
*
* Version: 1.0
* Created: 2017/12/1 18:40:19 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Text;

namespace AutumnBox.Support
{
#if FUCK
    public class IdentifyEncoding
    {
        internal static int[][] GBFreq = new int[94][];
        internal static int[][] GBKFreq = new int[126][];
        internal static int[][] Big5Freq = new int[94][];
        internal static int[][] EUC_TWFreq = new int[94][];

        internal static string[] nicename = new string[]
        {
            "GB2312", "GBK", "HZ", "Big5", "CNS 11643", "ISO 2022CN", "UTF-8", "Unicode", "ASCII", "OTHER"
        };

        /// <summary>  
        /// 构造函数  
        /// </summary>  
        public IdentifyEncoding()
        {
            Initialize_Frequencies();
        }


        /// <summary>  
        /// 从指定的网页或文件中判断编码类型  
        /// </summary>  
        /// <param name="path">要判断的网页Url或者文件名</param>  
        /// <param name="isweb">是网页还是文件</param>  
        /// <returns>返回编码类型("GB2312", "GBK", "HZ", "Big5", "CNS 11643", "ISO 2022CN", "UTF-8", "Unicode", "ASCII", "OTHER")</returns>  
        /// <example>  
        /// 以下示例演示了如何调用该方法：  
        /// <code>  
        ///  IdentifyEncoding ide = new IdentifyEncoding();  
        ///  string encoding = ide.GetEncodingName("http://www.sina.com.cn",true);  
        /// </code>  
        /// </example>  
        public virtual string GetEncodingName(string path, bool isweb)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            path = path.Trim();
            if (string.IsNullOrEmpty(path))
                return null;

            if (isweb)
            {
                if (!path.ToLower().StartsWith("http://"))
                    path = "http://" + path.Trim();

                Uri uri = new Uri(path);
                return GetEncodingName(uri);
            }
            else
            {
                if (!System.IO.File.Exists(path))
                    return null;

                System.IO.FileInfo file = new System.IO.FileInfo(path);
                return GetEncodingName(file);
            }
        }

        /// <summary>  
        /// 从指定的网页中判断编码类型  
        /// </summary>  
        /// <param name="uri">要判断的网页Url</param>  
        /// <returns>返回编码类型("GB2312", "GBK", "HZ", "Big5", "CNS 11643", "ISO 2022CN", "UTF-8", "Unicode", "ASCII", "OTHER")</returns>  
        /// <example>  
        /// 以下示例演示了如何调用该方法：  
        /// <code>  
        ///  IdentifyEncoding ide = new IdentifyEncoding();  
        ///  string encoding = ide.GetEncodingName(new Uri("http://www.sina.com.cn"));  
        /// </code>  
        /// </example>  
        public virtual string GetEncodingName(Uri uri)
        {
            if (uri == null)
                return null;
            sbyte[] rawtext = new sbyte[1024];
            int bytesread = 0, byteoffset = 0;
            System.IO.Stream chinesestream;
            try
            {
                chinesestream = System.Net.WebRequest.Create(uri.AbsoluteUri).GetResponse().GetResponseStream();
                while ((bytesread = ReadInput(chinesestream, ref rawtext, byteoffset, rawtext.Length - byteoffset)) > 0)
                {
                    byteoffset += bytesread;
                }
                chinesestream.Close();
            }
            catch
            {
            }

            return GetEncodingName(rawtext);
        }

        /// <summary>  
        /// 从指定的文件中判断编码类型  
        /// </summary>  
        /// <param name="testfile">要判断的文件</param>  
        /// <returns>返回编码类型("GB2312", "GBK", "HZ", "Big5", "CNS 11643", "ISO 2022CN", "UTF-8", "Unicode", "ASCII", "OTHER")</returns>  
        /// <example>  
        /// 以下示例演示了如何调用该方法：  
        /// <code>  
        ///  IdentifyEncoding ide = new IdentifyEncoding();  
        ///  string encoding = ide.GetEncodingName(new System.IO.FileInfo(@"C:\test.txt"));    
        /// </code>  
        /// </example>  
        public virtual string GetEncodingName(System.IO.FileInfo testfile)
        {
            System.IO.FileStream chinesefile;
            sbyte[] rawtext;
            rawtext = new sbyte[(int)FileLength(testfile)];
            try
            {
                chinesefile = new System.IO.FileStream(testfile.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                ReadInput(chinesefile, ref rawtext, 0, rawtext.Length);
            }
            catch
            {
            }

            return GetEncodingName(rawtext);
        }

        public virtual Encoding GetEncoding(string encodingName)
        {
            Encoding _encode = Encoding.Default;
            if (encodingName.Length == 0)
                return _encode;
            switch (encodingName)
            {
                case "UTF-8":
                    _encode = Encoding.UTF8;
                    break;
                case "Unicode":
                    _encode = Encoding.Unicode;
                    break;
                default:
                    try
                    {
                        _encode = Encoding.GetEncoding(encodingName);
                    }
                    catch
                    {
                        _encode = Encoding.Default;
                    }
                    break;
            }
            return _encode;
        }


        /// <summary>  
        /// 从指定的字节数组中判断编码类型  
        /// </summary>  
        /// <param name="rawtext">要判断的字节数组</param>  
        /// <returns>返回编码类型("GB2312", "GBK", "HZ", "Big5", "CNS 11643", "ISO 2022CN", "UTF-8", "Unicode", "ASCII", "OTHER")</returns>  
        public virtual string GetEncodingName(sbyte[] rawtext)
        {
            int[] scores;
            int index, maxscore = 0;
            int encoding_guess = 0;

            scores = new int[10];
            //分析编码的概率  
            scores[0] = GB2312Probability(rawtext);
            scores[1] = GBKProbability(rawtext);
            scores[2] = HZProbability(rawtext);
            scores[3] = BIG5Probability(rawtext);
            scores[4] = ENCTWProbability(rawtext);
            scores[5] = ISO2022CNProbability(rawtext);
            scores[6] = UTF8Probability(rawtext);
            scores[7] = UnicodeProbability(rawtext);
            scores[8] = ASCIIProbability(rawtext);
            scores[9] = 0;

            // Tabulate Scores  
            for (index = 0; index < 10; index++)
            {
                if (scores[index] > maxscore)
                {
                    encoding_guess = index;
                    maxscore = scores[index];
                }
            }

            // Return OTHER if nothing scored above 50  
            if (maxscore <= 50)
            {
                encoding_guess = 9;
            }

            return nicename[encoding_guess];
        }

        /// <summary>  
        /// 判断是GB2312编码的可能性  
        /// </summary>  
        /// <param name="rawtext">要判断的 <see cref="sbyte"/> 字节数组</param>  
        /// <returns>返回 0 至 100 之间的可能性</returns>  
        internal virtual int GB2312Probability(sbyte[] rawtext)
        {
            int i, rawtextlen = 0;

            int dbchars = 1, gbchars = 1;
            long gbfreq = 0, totalfreq = 1;
            float rangeval = 0, freqval = 0;
            int row, column;

            // Stage 1:  Check to see if characters fit into acceptable ranges  

            rawtextlen = rawtext.Length;
            for (i = 0; i < rawtextlen - 1; i++)
            {
                if (rawtext[i] >= 0)
                {
                    //asciichars++;  
                }
                else
                {
                    dbchars++;
                    if ((sbyte)Identity(0xA1) <= rawtext[i] && rawtext[i] <= (sbyte)Identity(0xF7) && (sbyte)Identity(0xA1) <= rawtext[i + 1] && rawtext[i + 1] <= (sbyte)Identity(0xFE))
                    {
                        gbchars++;
                        totalfreq += 500;
                        row = rawtext[i] + 256 - 0xA1;
                        column = rawtext[i + 1] + 256 - 0xA1;
                        if (GBFreq[row][column] != 0)
                        {
                            gbfreq += GBFreq[row][column];
                        }
                        else if (15 <= row && row < 55)
                        {
                            gbfreq += 200;
                        }
                    }
                    i++;
                }
            }

            rangeval = 50 * ((float)gbchars / (float)dbchars);
            freqval = 50 * ((float)gbfreq / (float)totalfreq);


            return (int)(rangeval + freqval);
        }

        /// <summary>  
        /// 判断是GBK编码的可能性  
        /// </summary>  
        /// <param name="rawtext">要判断的 <see cref="sbyte"/> 字节数组</param>  
        /// <returns>返回 0 至 100 之间的可能性</returns>  
        internal virtual int GBKProbability(sbyte[] rawtext)
        {
            int i, rawtextlen = 0;

            int dbchars = 1, gbchars = 1;
            long gbfreq = 0, totalfreq = 1;
            float rangeval = 0, freqval = 0;
            int row, column;

            // Stage 1:  Check to see if characters fit into acceptable ranges  
            rawtextlen = rawtext.Length;
            for (i = 0; i < rawtextlen - 1; i++)
            {
                if (rawtext[i] >= 0)
                {
                    //asciichars++;  
                }
                else
                {
                    dbchars++;
                    if ((sbyte)Identity(0xA1) <= rawtext[i] && rawtext[i] <= (sbyte)Identity(0xF7) && (sbyte)Identity(0xA1) <= rawtext[i + 1] && rawtext[i + 1] <= (sbyte)Identity(0xFE))
                    {
                        gbchars++;
                        totalfreq += 500;
                        row = rawtext[i] + 256 - 0xA1;
                        column = rawtext[i + 1] + 256 - 0xA1;

                        if (GBFreq[row][column] != 0)
                        {
                            gbfreq += GBFreq[row][column];
                        }
                        else if (15 <= row && row < 55)
                        {
                            gbfreq += 200;
                        }
                    }
                    else if ((sbyte)Identity(0x81) <= rawtext[i] && rawtext[i] <= (sbyte)Identity(0xFE) && (((sbyte)Identity(0x80) <= rawtext[i + 1] && rawtext[i + 1] <= (sbyte)Identity(0xFE)) || ((sbyte)0x40 <= rawtext[i + 1] && rawtext[i + 1] <= (sbyte)0x7E)))
                    {
                        gbchars++;
                        totalfreq += 500;
                        row = rawtext[i] + 256 - 0x81;
                        if (0x40 <= rawtext[i + 1] && rawtext[i + 1] <= 0x7E)
                        {
                            column = rawtext[i + 1] - 0x40;
                        }
                        else
                        {
                            column = rawtext[i + 1] + 256 - 0x80;
                        }

                        if (GBKFreq[row][column] != 0)
                        {
                            gbfreq += GBKFreq[row][column];
                        }
                    }
                    i++;
                }
            }

            rangeval = 50 * ((float)gbchars / (float)dbchars);
            freqval = 50 * ((float)gbfreq / (float)totalfreq);

            return (int)(rangeval + freqval) - 1;
        }

        /// <summary>  
        /// 判断是HZ编码的可能性  
        /// </summary>  
        /// <param name="rawtext">要判断的 <see cref="sbyte"/> 字节数组</param>  
        /// <returns>返回 0 至 100 之间的可能性</returns>  
        internal virtual int HZProbability(sbyte[] rawtext)
        {
            int i, rawtextlen;
            int hzchars = 0, dbchars = 1;
            long hzfreq = 0, totalfreq = 1;
            float rangeval = 0, freqval = 0;
            int hzstart = 0, hzend = 0;
            int row, column;

            rawtextlen = rawtext.Length;

            for (i = 0; i < rawtextlen; i++)
            {
                if (rawtext[i] == '~')
                {
                    if (rawtext[i + 1] == '{')
                    {
                        hzstart++;
                        i += 2;
                        while (i < rawtextlen - 1)
                        {
                            if (rawtext[i] == 0x0A || rawtext[i] == 0x0D)
                            {
                                break;
                            }
                            else if (rawtext[i] == '~' && rawtext[i + 1] == '}')
                            {
                                hzend++;
                                i++;
                                break;
                            }
                            else if ((0x21 <= rawtext[i] && rawtext[i] <= 0x77) && (0x21 <= rawtext[i + 1] && rawtext[i + 1] <= 0x77))
                            {
                                hzchars += 2;
                                row = rawtext[i] - 0x21;
                                column = rawtext[i + 1] - 0x21;
                                totalfreq += 500;
                                if (GBFreq[row][column] != 0)
                                {
                                    hzfreq += GBFreq[row][column];
                                }
                                else if (15 <= row && row < 55)
                                {
                                    hzfreq += 200;
                                }
                            }
                            else if (((byte)0xA1 <= rawtext[i] && rawtext[i] <= (byte)0xF7) && ((byte)0xA1 <= rawtext[i + 1] && rawtext[i + 1] <= (byte)0xF7))
                            {
                                hzchars += 2;
                                row = rawtext[i] + 256 - 0xA1;
                                column = rawtext[i + 1] + 256 - 0xA1;
                                totalfreq += 500;
                                if (GBFreq[row][column] != 0)
                                {
                                    hzfreq += GBFreq[row][column];
                                }
                                else if (15 <= row && row < 55)
                                {
                                    hzfreq += 200;
                                }
                            }
                            dbchars += 2;
                            i += 2;
                        }
                    }
                    else if (rawtext[i + 1] == '}')
                    {
                        hzend++;
                        i++;
                    }
                    else if (rawtext[i + 1] == '~')
                    {
                        i++;
                    }
                }
            }

            if (hzstart > 4)
            {
                rangeval = 50;
            }
            else if (hzstart > 1)
            {
                rangeval = 41;
            }
            else if (hzstart > 0)
            {
                // Only 39 in case the sequence happened to occur  
                rangeval = 39; // in otherwise non-Hz text  
            }
            else
            {
                rangeval = 0;
            }
            freqval = 50 * ((float)hzfreq / (float)totalfreq);


            return (int)(rangeval + freqval);
        }

        /// <summary>  
        /// 判断是BIG5编码的可能性  
        /// </summary>  
        /// <param name="rawtext">要判断的 <see cref="sbyte"/> 字节数组</param>  
        /// <returns>返回 0 至 100 之间的可能性</returns>  
        internal virtual int BIG5Probability(sbyte[] rawtext)
        {
            int i, rawtextlen = 0;
            int dbchars = 1, bfchars = 1;
            float rangeval = 0, freqval = 0;
            long bffreq = 0, totalfreq = 1;
            int row, column;

            // Check to see if characters fit into acceptable ranges  

            rawtextlen = rawtext.Length;
            for (i = 0; i < rawtextlen - 1; i++)
            {
                if (rawtext[i] >= 0)
                {
                    //asciichars++;  
                }
                else
                {
                    dbchars++;
                    if ((sbyte)Identity(0xA1) <= rawtext[i] && rawtext[i] <= (sbyte)Identity(0xF9) && (((sbyte)0x40 <= rawtext[i + 1] && rawtext[i + 1] <= (sbyte)0x7E) || ((sbyte)Identity(0xA1) <= rawtext[i + 1] && rawtext[i + 1] <= (sbyte)Identity(0xFE))))
                    {
                        bfchars++;
                        totalfreq += 500;
                        row = rawtext[i] + 256 - 0xA1;
                        if (0x40 <= rawtext[i + 1] && rawtext[i + 1] <= 0x7E)
                        {
                            column = rawtext[i + 1] - 0x40;
                        }
                        else
                        {
                            column = rawtext[i + 1] + 256 - 0x61;
                        }
                        if (Big5Freq[row][column] != 0)
                        {
                            bffreq += Big5Freq[row][column];
                        }
                        else if (3 <= row && row <= 37)
                        {
                            bffreq += 200;
                        }
                    }
                    i++;
                }
            }

            rangeval = 50 * ((float)bfchars / (float)dbchars);
            freqval = 50 * ((float)bffreq / (float)totalfreq);


            return (int)(rangeval + freqval);
        }

        /// <summary>  
        /// 判断是CNS11643(台湾)编码的可能性  
        /// </summary>  
        /// <param name="rawtext">要判断的 <see cref="sbyte"/> 字节数组</param>  
        /// <returns>返回 0 至 100 之间的可能性</returns>  
        internal virtual int ENCTWProbability(sbyte[] rawtext)
        {
            int i, rawtextlen = 0;
            int dbchars = 1, cnschars = 1;
            long cnsfreq = 0, totalfreq = 1;
            float rangeval = 0, freqval = 0;
            int row, column;

            // Check to see if characters fit into acceptable ranges  
            // and have expected frequency of use  

            rawtextlen = rawtext.Length;
            for (i = 0; i < rawtextlen - 1; i++)
            {
                if (rawtext[i] >= 0)
                {
                    // in ASCII range  
                    //asciichars++;  
                }
                else
                {
                    // high bit set  
                    dbchars++;
                    if (i + 3 < rawtextlen && (sbyte)Identity(0x8E) == rawtext[i] && (sbyte)Identity(0xA1) <= rawtext[i + 1] && rawtext[i + 1] <= (sbyte)Identity(0xB0) && (sbyte)Identity(0xA1) <= rawtext[i + 2] && rawtext[i + 2] <= (sbyte)Identity(0xFE) && (sbyte)Identity(0xA1) <= rawtext[i + 3] && rawtext[i + 3] <= (sbyte)Identity(0xFE))
                    {
                        // Planes 1 - 16  

                        cnschars++;
                        // These are all less frequent chars so just ignore freq  
                        i += 3;
                    }
                    else if ((sbyte)Identity(0xA1) <= rawtext[i] && rawtext[i] <= (sbyte)Identity(0xFE) && (sbyte)Identity(0xA1) <= rawtext[i + 1] && rawtext[i + 1] <= (sbyte)Identity(0xFE))
                    {
                        cnschars++;
                        totalfreq += 500;
                        row = rawtext[i] + 256 - 0xA1;
                        column = rawtext[i + 1] + 256 - 0xA1;
                        if (EUC_TWFreq[row][column] != 0)
                        {
                            cnsfreq += EUC_TWFreq[row][column];
                        }
                        else if (35 <= row && row <= 92)
                        {
                            cnsfreq += 150;
                        }
                        i++;
                    }
                }
            }
            rangeval = 50 * ((float)cnschars / (float)dbchars);
            freqval = 50 * ((float)cnsfreq / (float)totalfreq);

            return (int)(rangeval + freqval);
        }

        /// <summary>  
        /// 判断是ISO2022CN编码的可能性  
        /// </summary>  
        /// <param name="rawtext">要判断的 <see cref="sbyte"/> 字节数组</param>  
        /// <returns>返回 0 至 100 之间的可能性</returns>  
        internal virtual int ISO2022CNProbability(sbyte[] rawtext)
        {
            int i, rawtextlen = 0;
            int dbchars = 1, isochars = 1;
            long isofreq = 0, totalfreq = 1;
            float rangeval = 0, freqval = 0;
            int row, column;

            // Check to see if characters fit into acceptable ranges  
            // and have expected frequency of use  

            rawtextlen = rawtext.Length;
            for (i = 0; i < rawtextlen - 1; i++)
            {
                if (rawtext[i] == (sbyte)0x1B && i + 3 < rawtextlen)
                {
                    // Escape char ESC  
                    if (rawtext[i + 1] == (sbyte)0x24 && rawtext[i + 2] == 0x29 && rawtext[i + 3] == (sbyte)0x41)
                    {
                        // GB Escape  $ ) A  
                        i += 4;
                        while (rawtext[i] != (sbyte)0x1B)
                        {
                            dbchars++;
                            if ((0x21 <= rawtext[i] && rawtext[i] <= 0x77) && (0x21 <= rawtext[i + 1] && rawtext[i + 1] <= 0x77))
                            {
                                isochars++;
                                row = rawtext[i] - 0x21;
                                column = rawtext[i + 1] - 0x21;
                                totalfreq += 500;
                                if (GBFreq[row][column] != 0)
                                {
                                    isofreq += GBFreq[row][column];
                                }
                                else if (15 <= row && row < 55)
                                {
                                    isofreq += 200;
                                }
                                i++;
                            }
                            i++;
                        }
                    }
                    else if (i + 3 < rawtextlen && rawtext[i + 1] == (sbyte)0x24 && rawtext[i + 2] == (sbyte)0x29 && rawtext[i + 3] == (sbyte)0x47)
                    {
                        // CNS Escape $ ) G  
                        i += 4;
                        while (rawtext[i] != (sbyte)0x1B)
                        {
                            dbchars++;
                            if ((sbyte)0x21 <= rawtext[i] && rawtext[i] <= (sbyte)0x7E && (sbyte)0x21 <= rawtext[i + 1] && rawtext[i + 1] <= (sbyte)0x7E)
                            {
                                isochars++;
                                totalfreq += 500;
                                row = rawtext[i] - 0x21;
                                column = rawtext[i + 1] - 0x21;
                                if (EUC_TWFreq[row][column] != 0)
                                {
                                    isofreq += EUC_TWFreq[row][column];
                                }
                                else if (35 <= row && row <= 92)
                                {
                                    isofreq += 150;
                                }
                                i++;
                            }
                            i++;
                        }
                    }
                    if (rawtext[i] == (sbyte)0x1B && i + 2 < rawtextlen && rawtext[i + 1] == (sbyte)0x28 && rawtext[i + 2] == (sbyte)0x42)
                    {
                        // ASCII:  ESC ( B  
                        i += 2;
                    }
                }
            }

            rangeval = 50 * ((float)isochars / (float)dbchars);
            freqval = 50 * ((float)isofreq / (float)totalfreq);

            return (int)(rangeval + freqval);
        }

        /// <summary>  
        /// 判断是UTF8编码的可能性  
        /// </summary>  
        /// <param name="rawtext">要判断的 <see cref="sbyte"/> 字节数组</param>  
        /// <returns>返回 0 至 100 之间的可能性</returns>  
        internal virtual int UTF8Probability(sbyte[] rawtext)
        {
            int score = 0;
            int i, rawtextlen = 0;
            int goodbytes = 0, asciibytes = 0;

            // Maybe also use UTF8 Byte Order Mark:  EF BB BF  

            // Check to see if characters fit into acceptable ranges  
            rawtextlen = rawtext.Length;
            for (i = 0; i < rawtextlen; i++)
            {
                if ((rawtext[i] & (sbyte)0x7F) == rawtext[i])
                {
                    // One byte  
                    asciibytes++;
                    // Ignore ASCII, can throw off count  
                }
                else if (-64 <= rawtext[i] && rawtext[i] <= -33 && i + 1 < rawtextlen && -128 <= rawtext[i + 1] && rawtext[i + 1] <= -65)
                {
                    goodbytes += 2;
                    i++;
                }
                else if (-32 <= rawtext[i] && rawtext[i] <= -17 && i + 2 < rawtextlen && -128 <= rawtext[i + 1] && rawtext[i + 1] <= -65 && -128 <= rawtext[i + 2] && rawtext[i + 2] <= -65)
                {
                    goodbytes += 3;
                    i += 2;
                }
            }

            if (asciibytes == rawtextlen)
            {
                return 0;
            }

            score = (int)(100 * ((float)goodbytes / (float)(rawtextlen - asciibytes)));

            // If not above 98, reduce to zero to prevent coincidental matches  
            // Allows for some (few) bad formed sequences  
            if (score > 98)
            {
                return score;
            }
            else if (score > 95 && goodbytes > 30)
            {
                return score;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>  
        /// 判断是Unicode编码的可能性  
        /// </summary>  
        /// <param name="rawtext">要判断的 <see cref="sbyte"/> 字节数组</param>  
        /// <returns>返回 0 至 100 之间的可能性</returns>  
        internal virtual int UnicodeProbability(sbyte[] rawtext)
        {
            //int score = 0;  
            //int i, rawtextlen = 0;  
            //int goodbytes = 0, asciibytes = 0;  

            if (((sbyte)Identity(0xFE) == rawtext[0] && (sbyte)Identity(0xFF) == rawtext[1]) || ((sbyte)Identity(0xFF) == rawtext[0] && (sbyte)Identity(0xFE) == rawtext[1]))
            {
                return 100;
            }

            return 0;
        }

        /// <summary>  
        /// 判断是ASCII编码的可能性  
        /// </summary>  
        /// <param name="rawtext">要判断的 <see cref="sbyte"/> 字节数组</param>  
        /// <returns>返回 0 至 100 之间的可能性</returns>  
        internal virtual int ASCIIProbability(sbyte[] rawtext)
        {
            int score = 70;
            int i, rawtextlen;

            rawtextlen = rawtext.Length;

            for (i = 0; i < rawtextlen; i++)
            {
                if (rawtext[i] < 0)
                {
                    score = score - 5;
                }
                else if (rawtext[i] == (sbyte)0x1B)
                {
                    // ESC (used by ISO 2022)  
                    score = score - 5;
                }
            }

            return score;
        }

        /// <summary>  
        /// 初始化必要的条件  
        /// </summary>  
        internal virtual void Initialize_Frequencies()
        {
            int i;
            if (GBFreq[0] == null)
            {
                for (i = 0; i < 94; i++)
                {
                    GBFreq[i] = new int[94];
                }

#region GBFreq[20][35] = 599;  

                GBFreq[49][26] = 598;
                GBFreq[41][38] = 597;
                GBFreq[17][26] = 596;
                GBFreq[32][42] = 595;
                GBFreq[39][42] = 594;
                GBFreq[45][49] = 593;
                GBFreq[51][57] = 592;
                GBFreq[50][47] = 591;
                GBFreq[42][90] = 590;
                GBFreq[52][65] = 589;
                GBFreq[53][47] = 588;
                GBFreq[19][82] = 587;
                GBFreq[31][19] = 586;
                GBFreq[40][46] = 585;
                GBFreq[24][89] = 584;
                GBFreq[23][85] = 583;
                GBFreq[20][28] = 582;
                GBFreq[42][20] = 581;
                GBFreq[34][38] = 580;
                GBFreq[45][9] = 579;
                GBFreq[54][50] = 578;
                GBFreq[25][44] = 577;
                GBFreq[35][66] = 576;
                GBFreq[20][55] = 575;
                GBFreq[18][85] = 574;
                GBFreq[20][31] = 573;
                GBFreq[49][17] = 572;
                GBFreq[41][16] = 571;
                GBFreq[35][73] = 570;
                GBFreq[20][34] = 569;
                GBFreq[29][44] = 568;
                GBFreq[35][38] = 567;
                GBFreq[49][9] = 566;
                GBFreq[46][33] = 565;
                GBFreq[49][51] = 564;
                GBFreq[40][89] = 563;
                GBFreq[26][64] = 562;
                GBFreq[54][51] = 561;
                GBFreq[54][36] = 560;
                GBFreq[39][4] = 559;
                GBFreq[53][13] = 558;
                GBFreq[24][92] = 557;
                GBFreq[27][49] = 556;
                GBFreq[48][6] = 555;
                GBFreq[21][51] = 554;
                GBFreq[30][40] = 553;
                GBFreq[42][92] = 552;
                GBFreq[31][78] = 551;
                GBFreq[25][82] = 550;
                GBFreq[47][0] = 549;
                GBFreq[34][19] = 548;
                GBFreq[47][35] = 547;
                GBFreq[21][63] = 546;
                GBFreq[43][75] = 545;
                GBFreq[21][87] = 544;
                GBFreq[35][59] = 543;
                GBFreq[25][34] = 542;
                GBFreq[21][27] = 541;
                GBFreq[39][26] = 540;
                GBFreq[34][26] = 539;
                GBFreq[39][52] = 538;
                GBFreq[50][57] = 537;
                GBFreq[37][79] = 536;
                GBFreq[26][24] = 535;
                GBFreq[22][1] = 534;
                GBFreq[18][40] = 533;
                GBFreq[41][33] = 532;
                GBFreq[53][26] = 531;
                GBFreq[54][86] = 530;
                GBFreq[20][16] = 529;
                GBFreq[46][74] = 528;
                GBFreq[30][19] = 527;
                GBFreq[45][35] = 526;
                GBFreq[45][61] = 525;
                GBFreq[30][9] = 524;
                GBFreq[41][53] = 523;
                GBFreq[41][13] = 522;
                GBFreq[50][34] = 521;
                GBFreq[53][86] = 520;
                GBFreq[47][47] = 519;
                GBFreq[22][28] = 518;
                GBFreq[50][53] = 517;
                GBFreq[39][70] = 516;
                GBFreq[38][15] = 515;
                GBFreq[42][88] = 514;
                GBFreq[16][29] = 513;
                GBFreq[27][90] = 512;
                GBFreq[29][12] = 511;
                GBFreq[44][22] = 510;
                GBFreq[34][69] = 509;
                GBFreq[24][10] = 508;
                GBFreq[44][11] = 507;
                GBFreq[39][92] = 506;
                GBFreq[49][48] = 505;
                GBFreq[31][46] = 504;
                GBFreq[19][50] = 503;
                GBFreq[21][14] = 502;
                GBFreq[32][28] = 501;
                GBFreq[18][3] = 500;
                GBFreq[53][9] = 499;
                GBFreq[34][80] = 498;
                GBFreq[48][88] = 497;
                GBFreq[46][53] = 496;
                GBFreq[22][53] = 495;
                GBFreq[28][10] = 494;
                GBFreq[44][65] = 493;
                GBFreq[20][10] = 492;
                GBFreq[40][76] = 491;
                GBFreq[47][8] = 490;
                GBFreq[50][74] = 489;
                GBFreq[23][62] = 488;
                GBFreq[49][65] = 487;
                GBFreq[28][87] = 486;
                GBFreq[15][48] = 485;
                GBFreq[22][7] = 484;
                GBFreq[19][42] = 483;
                GBFreq[41][20] = 482;
                GBFreq[26][55] = 481;
                GBFreq[21][93] = 480;
                GBFreq[31][76] = 479;
                GBFreq[34][31] = 478;
                GBFreq[20][66] = 477;
                GBFreq[51][33] = 476;
                GBFreq[34][86] = 475;
                GBFreq[37][67] = 474;
                GBFreq[53][53] = 473;
                GBFreq[40][88] = 472;
                GBFreq[39][10] = 471;
                GBFreq[24][3] = 470;
                GBFreq[27][25] = 469;
                GBFreq[26][15] = 468;
                GBFreq[21][88] = 467;
                GBFreq[52][62] = 466;
                GBFreq[46][81] = 465;
                GBFreq[38][72] = 464;
                GBFreq[17][30] = 463;
                GBFreq[52][92] = 462;
                GBFreq[34][90] = 461;
                GBFreq[21][7] = 460;
                GBFreq[36][13] = 459;
                GBFreq[45][41] = 458;
                GBFreq[32][5] = 457;
                GBFreq[26][89] = 456;
                GBFreq[23][87] = 455;
                GBFreq[20][39] = 454;
                GBFreq[27][23] = 453;
                GBFreq[25][59] = 452;
                GBFreq[49][20] = 451;
                GBFreq[54][77] = 450;
                GBFreq[27][67] = 449;
                GBFreq[47][33] = 448;
                GBFreq[41][17] = 447;
                GBFreq[19][81] = 446;
                GBFreq[16][66] = 445;
                GBFreq[45][26] = 444;
                GBFreq[49][81] = 443;
                GBFreq[53][55] = 442;
                GBFreq[16][26] = 441;
                GBFreq[54][62] = 440;
                GBFreq[20][70] = 439;
                GBFreq[42][35] = 438;
                GBFreq[20][57] = 437;
                GBFreq[34][36] = 436;
                GBFreq[46][63] = 435;
                GBFreq[19][45] = 434;
                GBFreq[21][10] = 433;
                GBFreq[52][93] = 432;
                GBFreq[25][2] = 431;
                GBFreq[30][57] = 430;
                GBFreq[41][24] = 429;
                GBFreq[28][43] = 428;
                GBFreq[45][86] = 427;
                GBFreq[51][56] = 426;
                GBFreq[37][28] = 425;
                GBFreq[52][69] = 424;
                GBFreq[43][92] = 423;
                GBFreq[41][31] = 422;
                GBFreq[37][87] = 421;
                GBFreq[47][36] = 420;
                GBFreq[16][16] = 419;
                GBFreq[40][56] = 418;
                GBFreq[24][55] = 417;
                GBFreq[17][1] = 416;
                GBFreq[35][57] = 415;
                GBFreq[27][50] = 414;
                GBFreq[26][14] = 413;
                GBFreq[50][40] = 412;
                GBFreq[39][19] = 411;
                GBFreq[19][89] = 410;
                GBFreq[29][91] = 409;
                GBFreq[17][89] = 408;
                GBFreq[39][74] = 407;
                GBFreq[46][39] = 406;
                GBFreq[40][28] = 405;
                GBFreq[45][68] = 404;
                GBFreq[43][10] = 403;
                GBFreq[42][13] = 402;
                GBFreq[44][81] = 401;
                GBFreq[41][47] = 400;
                GBFreq[48][58] = 399;
                GBFreq[43][68] = 398;
                GBFreq[16][79] = 397;
                GBFreq[19][5] = 396;
                GBFreq[54][59] = 395;
                GBFreq[17][36] = 394;
                GBFreq[18][0] = 393;
                GBFreq[41][5] = 392;
                GBFreq[41][72] = 391;
                GBFreq[16][39] = 390;
                GBFreq[54][0] = 389;
                GBFreq[51][16] = 388;
                GBFreq[29][36] = 387;
                GBFreq[47][5] = 386;
                GBFreq[47][51] = 385;
                GBFreq[44][7] = 384;
                GBFreq[35][30] = 383;
                GBFreq[26][9] = 382;
                GBFreq[16][7] = 381;
                GBFreq[32][1] = 380;
                GBFreq[33][76] = 379;
                GBFreq[34][91] = 378;
                GBFreq[52][36] = 377;
                GBFreq[26][77] = 376;
                GBFreq[35][48] = 375;
                GBFreq[40][80] = 374;
                GBFreq[41][92] = 373;
                GBFreq[27][93] = 372;
                GBFreq[15][17] = 371;
                GBFreq[16][76] = 370;
                GBFreq[51][12] = 369;
                GBFreq[18][20] = 368;
                GBFreq[15][54] = 367;
                GBFreq[50][5] = 366;
                GBFreq[33][22] = 365;
                GBFreq[37][57] = 364;
                GBFreq[28][47] = 363;
                GBFreq[42][31] = 362;
                GBFreq[18][2] = 361;
                GBFreq[43][64] = 360;
                GBFreq[23][47] = 359;
                GBFreq[28][79] = 358;
                GBFreq[25][45] = 357;
                GBFreq[23][91] = 356;
                GBFreq[22][19] = 355;
                GBFreq[25][46] = 354;
                GBFreq[22][36] = 353;
                GBFreq[54][85] = 352;
                GBFreq[46][20] = 351;
                GBFreq[27][37] = 350;
                GBFreq[26][81] = 349;
                GBFreq[42][29] = 348;
                GBFreq[31][90] = 347;
                GBFreq[41][59] = 346;
                GBFreq[24][65] = 345;
                GBFreq[44][84] = 344;
                GBFreq[24][90] = 343;
                GBFreq[38][54] = 342;
                GBFreq[28][70] = 341;
                GBFreq[27][15] = 340;
                GBFreq[28][80] = 339;
                GBFreq[29][8] = 338;
                GBFreq[45][80] = 337;
                GBFreq[53][37] = 336;
                GBFreq[28][65] = 335;
                GBFreq[23][86] = 334;
                GBFreq[39][45] = 333;
                GBFreq[53][32] = 332;
                GBFreq[38][68] = 331;
                GBFreq[45][78] = 330;
                GBFreq[43][7] = 329;
                GBFreq[46][82] = 328;
                GBFreq[27][38] = 327;
                GBFreq[16][62] = 326;
                GBFreq[24][17] = 325;
                GBFreq[22][70] = 324;
                GBFreq[52][28] = 323;
                GBFreq[23][40] = 322;
                GBFreq[28][50] = 321;
                GBFreq[42][91] = 320;
                GBFreq[47][76] = 319;
                GBFreq[15][42] = 318;
                GBFreq[43][55] = 317;
                GBFreq[29][84] = 316;
                GBFreq[44][90] = 315;
                GBFreq[53][16] = 314;
                GBFreq[22][93] = 313;
                GBFreq[34][10] = 312;
                GBFreq[32][53] = 311;
                GBFreq[43][65] = 310;
                GBFreq[28][7] = 309;
                GBFreq[35][46] = 308;
                GBFreq[21][39] = 307;
                GBFreq[44][18] = 306;
                GBFreq[40][10] = 305;
                GBFreq[54][53] = 304;
                GBFreq[38][74] = 303;
                GBFreq[28][26] = 302;
                GBFreq[15][13] = 301;
                GBFreq[39][34] = 300;
                GBFreq[39][46] = 299;
                GBFreq[42][66] = 298;
                GBFreq[33][58] = 297;
                GBFreq[15][56] = 296;
                GBFreq[18][51] = 295;
                GBFreq[49][68] = 294;
                GBFreq[30][37] = 293;
                GBFreq[51][84] = 292;
                GBFreq[51][9] = 291;
                GBFreq[40][70] = 290;
                GBFreq[41][84] = 289;
                GBFreq[28][64] = 288;
                GBFreq[32][88] = 287;
                GBFreq[24][5] = 286;
                GBFreq[53][23] = 285;
                GBFreq[42][27] = 284;
                GBFreq[22][38] = 283;
                GBFreq[32][86] = 282;
                GBFreq[34][30] = 281;
                GBFreq[38][63] = 280;
                GBFreq[24][59] = 279;
                GBFreq[22][81] = 278;
                GBFreq[32][11] = 277;
                GBFreq[51][21] = 276;
                GBFreq[54][41] = 275;
                GBFreq[21][50] = 274;
                GBFreq[23][89] = 273;
                GBFreq[19][87] = 272;
                GBFreq[26][7] = 271;
                GBFreq[30][75] = 270;
                GBFreq[43][84] = 269;
                GBFreq[51][25] = 268;
                GBFreq[16][67] = 267;
                GBFreq[32][9] = 266;
                GBFreq[48][51] = 265;
                GBFreq[39][7] = 264;
                GBFreq[44][88] = 263;
                GBFreq[52][24] = 262;
                GBFreq[23][34] = 261;
                GBFreq[32][75] = 260;
                GBFreq[19][10] = 259;
                GBFreq[28][91] = 258;
                GBFreq[32][83] = 257;
                GBFreq[25][75] = 256;
                GBFreq[53][45] = 255;
                GBFreq[29][85] = 254;
                GBFreq[53][59] = 253;
                GBFreq[16][2] = 252;
                GBFreq[19][78] = 251;
                GBFreq[15][75] = 250;
                GBFreq[51][42] = 249;
                GBFreq[45][67] = 248;
                GBFreq[15][74] = 247;
                GBFreq[25][81] = 246;
                GBFreq[37][62] = 245;
                GBFreq[16][55] = 244;
                GBFreq[18][38] = 243;
                GBFreq[23][23] = 242;

                GBFreq[38][30] = 241;
                GBFreq[17][28] = 240;
                GBFreq[44][73] = 239;
                GBFreq[23][78] = 238;
                GBFreq[40][77] = 237;
                GBFreq[38][87] = 236;
                GBFreq[27][19] = 235;
                GBFreq[38][82] = 234;
                GBFreq[37][22] = 233;
                GBFreq[41][30] = 232;
                GBFreq[54][9] = 231;
                GBFreq[32][30] = 230;
                GBFreq[30][52] = 229;
                GBFreq[40][84] = 228;
                GBFreq[53][57] = 227;
                GBFreq[27][27] = 226;
                GBFreq[38][64] = 225;
                GBFreq[18][43] = 224;
                GBFreq[23][69] = 223;
                GBFreq[28][12] = 222;
                GBFreq[50][78] = 221;
                GBFreq[50][1] = 220;
                GBFreq[26][88] = 219;
                GBFreq[36][40] = 218;
                GBFreq[33][89] = 217;
                GBFreq[41][28] = 216;
                GBFreq[31][77] = 215;
                GBFreq[46][1] = 214;
                GBFreq[47][19] = 213;
                GBFreq[35][55] = 212;
                GBFreq[41][21] = 211;
                GBFreq[27][10] = 210;
                GBFreq[32][77] = 209;
                GBFreq[26][37] = 208;
                GBFreq[20][33] = 207;
                GBFreq[41][52] = 206;
                GBFreq[32][18] = 205;
                GBFreq[38][13] = 204;
                GBFreq[20][18] = 203;
                GBFreq[20][24] = 202;
                GBFreq[45][19] = 201;
                GBFreq[18][53] = 200;

#endregion
            }

            if (GBKFreq[0] == null)
            {
                for (i = 0; i < 126; i++)
                {
                    GBKFreq[i] = new int[191];
                }

#region GBKFreq[52][132] = 600;  

                GBKFreq[73][135] = 599;
                GBKFreq[49][123] = 598;
                GBKFreq[77][146] = 597;
                GBKFreq[81][123] = 596;
                GBKFreq[82][144] = 595;
                GBKFreq[51][179] = 594;
                GBKFreq[83][154] = 593;
                GBKFreq[71][139] = 592;
                GBKFreq[64][139] = 591;
                GBKFreq[85][144] = 590;
                GBKFreq[52][125] = 589;
                GBKFreq[88][25] = 588;
                GBKFreq[81][106] = 587;
                GBKFreq[81][148] = 586;
                GBKFreq[62][137] = 585;
                GBKFreq[94][0] = 584;
                GBKFreq[1][64] = 583;
                GBKFreq[67][163] = 582;
                GBKFreq[20][190] = 581;
                GBKFreq[57][131] = 580;
                GBKFreq[29][169] = 579;
                GBKFreq[72][143] = 578;
                GBKFreq[0][173] = 577;
                GBKFreq[11][23] = 576;
                GBKFreq[61][141] = 575;
                GBKFreq[60][123] = 574;
                GBKFreq[81][114] = 573;
                GBKFreq[82][131] = 572;
                GBKFreq[67][156] = 571;
                GBKFreq[71][167] = 570;
                GBKFreq[20][50] = 569;
                GBKFreq[77][132] = 568;
                GBKFreq[84][38] = 567;
                GBKFreq[26][29] = 566;
                GBKFreq[74][187] = 565;
                GBKFreq[62][116] = 564;
                GBKFreq[67][135] = 563;
                GBKFreq[5][86] = 562;
                GBKFreq[72][186] = 561;
                GBKFreq[75][161] = 560;
                GBKFreq[78][130] = 559;
                GBKFreq[94][30] = 558;
                GBKFreq[84][72] = 557;
                GBKFreq[1][67] = 556;
                GBKFreq[75][172] = 555;
                GBKFreq[74][185] = 554;
                GBKFreq[53][160] = 553;
                GBKFreq[123][14] = 552;
                GBKFreq[79][97] = 551;
                GBKFreq[85][110] = 550;
                GBKFreq[78][171] = 549;
                GBKFreq[52][131] = 548;
                GBKFreq[56][100] = 547;
                GBKFreq[50][182] = 546;
                GBKFreq[94][64] = 545;
                GBKFreq[106][74] = 544;
                GBKFreq[11][102] = 543;
                GBKFreq[53][124] = 542;
                GBKFreq[24][3] = 541;
                GBKFreq[86][148] = 540;
                GBKFreq[53][184] = 539;
                GBKFreq[86][147] = 538;
                GBKFreq[96][161] = 537;
                GBKFreq[82][77] = 536;
                GBKFreq[59][146] = 535;
                GBKFreq[84][126] = 534;
                GBKFreq[79][132] = 533;
                GBKFreq[85][123] = 532;
                GBKFreq[71][101] = 531;
                GBKFreq[85][106] = 530;
                GBKFreq[6][184] = 529;
                GBKFreq[57][156] = 528;
                GBKFreq[75][104] = 527;
                GBKFreq[50][137] = 526;
                GBKFreq[79][133] = 525;
                GBKFreq[76][108] = 524;
                GBKFreq[57][142] = 523;
                GBKFreq[84][130] = 522;
                GBKFreq[52][128] = 521;
                GBKFreq[47][44] = 520;
                GBKFreq[52][152] = 519;
                GBKFreq[54][104] = 518;
                GBKFreq[30][47] = 517;
                GBKFreq[71][123] = 516;
                GBKFreq[52][107] = 515;
                GBKFreq[45][84] = 514;
                GBKFreq[107][118] = 513;
                GBKFreq[5][161] = 512;
                GBKFreq[48][126] = 511;
                GBKFreq[67][170] = 510;
                GBKFreq[43][6] = 509;
                GBKFreq[70][112] = 508;
                GBKFreq[86][174] = 507;
                GBKFreq[84][166] = 506;
                GBKFreq[79][130] = 505;
                GBKFreq[57][141] = 504;
                GBKFreq[81][178] = 503;
                GBKFreq[56][187] = 502;
                GBKFreq[81][162] = 501;
                GBKFreq[53][104] = 500;
                GBKFreq[123][35] = 499;
                GBKFreq[70][169] = 498;
                GBKFreq[69][164] = 497;
                GBKFreq[109][61] = 496;
                GBKFreq[73][130] = 495;
                GBKFreq[62][134] = 494;
                GBKFreq[54][125] = 493;
                GBKFreq[79][105] = 492;
                GBKFreq[70][165] = 491;
                GBKFreq[71][189] = 490;
                GBKFreq[23][147] = 489;
                GBKFreq[51][139] = 488;
                GBKFreq[47][137] = 487;
                GBKFreq[77][123] = 486;
                GBKFreq[86][183] = 485;
                GBKFreq[63][173] = 484;
                GBKFreq[79][144] = 483;
                GBKFreq[84][159] = 482;
                GBKFreq[60][91] = 481;
                GBKFreq[66][187] = 480;
                GBKFreq[73][114] = 479;
                GBKFreq[85][56] = 478;
                GBKFreq[71][149] = 477;
                GBKFreq[84][189] = 476;
                GBKFreq[104][31] = 475;
                GBKFreq[83][82] = 474;
                GBKFreq[68][35] = 473;
                GBKFreq[11][77] = 472;
                GBKFreq[15][155] = 471;
                GBKFreq[83][153] = 470;
                GBKFreq[71][1] = 469;
                GBKFreq[53][190] = 468;
                GBKFreq[50][135] = 467;
                GBKFreq[3][147] = 466;
                GBKFreq[48][136] = 465;
                GBKFreq[66][166] = 464;
                GBKFreq[55][159] = 463;
                GBKFreq[82][150] = 462;
                GBKFreq[58][178] = 461;
                GBKFreq[64][102] = 460;
                GBKFreq[16][106] = 459;
                GBKFreq[68][110] = 458;
                GBKFreq[54][14] = 457;
                GBKFreq[60][140] = 456;
                GBKFreq[91][71] = 455;
                GBKFreq[54][150] = 454;
                GBKFreq[78][177] = 453;
                GBKFreq[78][117] = 452;
                GBKFreq[104][12] = 451;
                GBKFreq[73][150] = 450;
                GBKFreq[51][142] = 449;
                GBKFreq[81][145] = 448;
                GBKFreq[66][183] = 447;
                GBKFreq[51][178] = 446;
                GBKFreq[75][107] = 445;
                GBKFreq[65][119] = 444;
                GBKFreq[69][176] = 443;
                GBKFreq[59][122] = 442;
                GBKFreq[78][160] = 441;
                GBKFreq[85][183] = 440;
                GBKFreq[105][16] = 439;
                GBKFreq[73][110] = 438;
                GBKFreq[104][39] = 437;
                GBKFreq[119][16] = 436;
                GBKFreq[76][162] = 435;
                GBKFreq[67][152] = 434;
                GBKFreq[82][24] = 433;
                GBKFreq[73][121] = 432;
                GBKFreq[83][83] = 431;
                GBKFreq[82][145] = 430;
                GBKFreq[49][133] = 429;
                GBKFreq[94][13] = 428;
                GBKFreq[58][139] = 427;
                GBKFreq[74][189] = 426;
                GBKFreq[66][177] = 425;
                GBKFreq[85][184] = 424;

                GBKFreq[55][183] = 423;
                GBKFreq[71][107] = 422;
                GBKFreq[11][98] = 421;
                GBKFreq[72][153] = 420;
                GBKFreq[2][137] = 419;
                GBKFreq[59][147] = 418;
                GBKFreq[58][152] = 417;
                GBKFreq[55][144] = 416;
                GBKFreq[73][125] = 415;
                GBKFreq[52][154] = 414;
                GBKFreq[70][178] = 413;
                GBKFreq[79][148] = 412;
                GBKFreq[63][143] = 411;
                GBKFreq[50][140] = 410;
                GBKFreq[47][145] = 409;
                GBKFreq[48][123] = 408;
                GBKFreq[56][107] = 407;
                GBKFreq[84][83] = 406;
                GBKFreq[59][112] = 405;
                GBKFreq[124][72] = 404;
                GBKFreq[79][99] = 403;
                GBKFreq[3][37] = 402;
                GBKFreq[114][55] = 401;
                GBKFreq[85][152] = 400;
                GBKFreq[60][47] = 399;
                GBKFreq[65][96] = 398;
                GBKFreq[74][110] = 397;
                GBKFreq[86][182] = 396;
                GBKFreq[50][99] = 395;
                GBKFreq[67][186] = 394;
                GBKFreq[81][74] = 393;
                GBKFreq[80][37] = 392;
                GBKFreq[21][60] = 391;
                GBKFreq[110][12] = 390;
                GBKFreq[60][162] = 389;
                GBKFreq[29][115] = 388;
                GBKFreq[83][130] = 387;
                GBKFreq[52][136] = 386;
                GBKFreq[63][114] = 385;
                GBKFreq[49][127] = 384;
                GBKFreq[83][109] = 383;
                GBKFreq[66][128] = 382;
                GBKFreq[78][136] = 381;
                GBKFreq[81][180] = 380;
                GBKFreq[76][104] = 379;
                GBKFreq[56][156] = 378;
                GBKFreq[61][23] = 377;
                GBKFreq[4][30] = 376;
                GBKFreq[69][154] = 375;
                GBKFreq[100][37] = 374;
                GBKFreq[54][177] = 373;
                GBKFreq[23][119] = 372;
                GBKFreq[71][171] = 371;
                GBKFreq[84][146] = 370;
                GBKFreq[20][184] = 369;
                GBKFreq[86][76] = 368;
                GBKFreq[74][132] = 367;
                GBKFreq[47][97] = 366;
                GBKFreq[82][137] = 365;
                GBKFreq[94][56] = 364;
                GBKFreq[92][30] = 363;
                GBKFreq[19][117] = 362;
                GBKFreq[48][173] = 361;
                GBKFreq[2][136] = 360;
                GBKFreq[7][182] = 359;
                GBKFreq[74][188] = 358;
                GBKFreq[14][132] = 357;
                GBKFreq[62][172] = 356;
                GBKFreq[25][39] = 355;
                GBKFreq[85][129] = 354;
                GBKFreq[64][98] = 353;
                GBKFreq[67][127] = 352;
                GBKFreq[72][167] = 351;
                GBKFreq[57][143] = 350;
                GBKFreq[76][187] = 349;
                GBKFreq[83][181] = 348;
                GBKFreq[84][10] = 347;
                GBKFreq[55][166] = 346;
                GBKFreq[55][188] = 345;
                GBKFreq[13][151] = 344;
                GBKFreq[62][124] = 343;
                GBKFreq[53][136] = 342;
                GBKFreq[106][57] = 341;
                GBKFreq[47][166] = 340;
                GBKFreq[109][30] = 339;
                GBKFreq[78][114] = 338;
                GBKFreq[83][19] = 337;
                GBKFreq[56][162] = 336;
                GBKFreq[60][177] = 335;
                GBKFreq[88][9] = 334;
                GBKFreq[74][163] = 333;
                GBKFreq[52][156] = 332;
                GBKFreq[71][180] = 331;
                GBKFreq[60][57] = 330;
                GBKFreq[72][173] = 329;
                GBKFreq[82][91] = 328;
                GBKFreq[51][186] = 327;
                GBKFreq[75][86] = 326;
                GBKFreq[75][78] = 325;
                GBKFreq[76][170] = 324;
                GBKFreq[60][147] = 323;
                GBKFreq[82][75] = 322;
                GBKFreq[80][148] = 321;
                GBKFreq[86][150] = 320;
                GBKFreq[13][95] = 319;
                GBKFreq[0][11] = 318;
                GBKFreq[84][190] = 317;
                GBKFreq[76][166] = 316;
                GBKFreq[14][72] = 315;
                GBKFreq[67][144] = 314;
                GBKFreq[84][44] = 313;
                GBKFreq[72][125] = 312;
                GBKFreq[66][127] = 311;
                GBKFreq[60][25] = 310;
                GBKFreq[70][146] = 309;
                GBKFreq[79][135] = 308;
                GBKFreq[54][135] = 307;
                GBKFreq[60][104] = 306;
                GBKFreq[55][132] = 305;
                GBKFreq[94][2] = 304;
                GBKFreq[54][133] = 303;
                GBKFreq[56][190] = 302;
                GBKFreq[58][174] = 301;
                GBKFreq[80][144] = 300;
                GBKFreq[85][113] = 299;

#endregion
            }

            if (Big5Freq[0] == null)
            {
                for (i = 0; i < 94; i++)
                {
                    Big5Freq[i] = new int[158];
                }

#region Big5Freq[9][89] = 600;  

                Big5Freq[11][15] = 599;
                Big5Freq[3][66] = 598;
                Big5Freq[6][121] = 597;
                Big5Freq[3][0] = 596;
                Big5Freq[5][82] = 595;
                Big5Freq[3][42] = 594;
                Big5Freq[5][34] = 593;
                Big5Freq[3][8] = 592;
                Big5Freq[3][6] = 591;
                Big5Freq[3][67] = 590;
                Big5Freq[7][139] = 589;
                Big5Freq[23][137] = 588;
                Big5Freq[12][46] = 587;
                Big5Freq[4][8] = 586;
                Big5Freq[4][41] = 585;
                Big5Freq[18][47] = 584;
                Big5Freq[12][114] = 583;
                Big5Freq[6][1] = 582;
                Big5Freq[22][60] = 581;
                Big5Freq[5][46] = 580;
                Big5Freq[11][79] = 579;
                Big5Freq[3][23] = 578;
                Big5Freq[7][114] = 577;
                Big5Freq[29][102] = 576;
                Big5Freq[19][14] = 575;
                Big5Freq[4][133] = 574;
                Big5Freq[3][29] = 573;
                Big5Freq[4][109] = 572;
                Big5Freq[14][127] = 571;
                Big5Freq[5][48] = 570;
                Big5Freq[13][104] = 569;
                Big5Freq[3][132] = 568;
                Big5Freq[26][64] = 567;
                Big5Freq[7][19] = 566;
                Big5Freq[4][12] = 565;
                Big5Freq[11][124] = 564;
                Big5Freq[7][89] = 563;
                Big5Freq[15][124] = 562;
                Big5Freq[4][108] = 561;
                Big5Freq[19][66] = 560;
                Big5Freq[3][21] = 559;
                Big5Freq[24][12] = 558;
                Big5Freq[28][111] = 557;
                Big5Freq[12][107] = 556;
                Big5Freq[3][112] = 555;
                Big5Freq[8][113] = 554;
                Big5Freq[5][40] = 553;
                Big5Freq[26][145] = 552;
                Big5Freq[3][48] = 551;
                Big5Freq[3][70] = 550;
                Big5Freq[22][17] = 549;
                Big5Freq[16][47] = 548;
                Big5Freq[3][53] = 547;
                Big5Freq[4][24] = 546;
                Big5Freq[32][120] = 545;
                Big5Freq[24][49] = 544;
                Big5Freq[24][142] = 543;
                Big5Freq[18][66] = 542;
                Big5Freq[29][150] = 541;
                Big5Freq[5][122] = 540;
                Big5Freq[5][114] = 539;
                Big5Freq[3][44] = 538;
                Big5Freq[10][128] = 537;
                Big5Freq[15][20] = 536;
                Big5Freq[13][33] = 535;
                Big5Freq[14][87] = 534;
                Big5Freq[3][126] = 533;
                Big5Freq[4][53] = 532;
                Big5Freq[4][40] = 531;
                Big5Freq[9][93] = 530;
                Big5Freq[15][137] = 529;
                Big5Freq[10][123] = 528;
                Big5Freq[4][56] = 527;
                Big5Freq[5][71] = 526;
                Big5Freq[10][8] = 525;
                Big5Freq[5][16] = 524;
                Big5Freq[5][146] = 523;
                Big5Freq[18][88] = 522;
                Big5Freq[24][4] = 521;
                Big5Freq[20][47] = 520;
                Big5Freq[5][33] = 519;
                Big5Freq[9][43] = 518;
                Big5Freq[20][12] = 517;
                Big5Freq[20][13] = 516;
                Big5Freq[5][156] = 515;
                Big5Freq[22][140] = 514;
                Big5Freq[8][146] = 513;
                Big5Freq[21][123] = 512;
                Big5Freq[4][90] = 511;
                Big5Freq[5][62] = 510;
                Big5Freq[17][59] = 509;
                Big5Freq[10][37] = 508;
                Big5Freq[18][107] = 507;
                Big5Freq[14][53] = 506;
                Big5Freq[22][51] = 505;
                Big5Freq[8][13] = 504;
                Big5Freq[5][29] = 503;
                Big5Freq[9][7] = 502;
                Big5Freq[22][14] = 501;
                Big5Freq[8][55] = 500;
                Big5Freq[33][9] = 499;
                Big5Freq[16][64] = 498;
                Big5Freq[7][131] = 497;
                Big5Freq[34][4] = 496;
                Big5Freq[7][101] = 495;
                Big5Freq[11][139] = 494;
                Big5Freq[3][135] = 493;
                Big5Freq[7][102] = 492;
                Big5Freq[17][13] = 491;
                Big5Freq[3][20] = 490;
                Big5Freq[27][106] = 489;
                Big5Freq[5][88] = 488;
                Big5Freq[6][33] = 487;
                Big5Freq[5][139] = 486;
                Big5Freq[6][0] = 485;
                Big5Freq[17][58] = 484;
                Big5Freq[5][133] = 483;
                Big5Freq[9][107] = 482;
                Big5Freq[23][39] = 481;
                Big5Freq[5][23] = 480;
                Big5Freq[3][79] = 479;
                Big5Freq[32][97] = 478;
                Big5Freq[3][136] = 477;
                Big5Freq[4][94] = 476;
                Big5Freq[21][61] = 475;
                Big5Freq[23][123] = 474;
                Big5Freq[26][16] = 473;
                Big5Freq[24][137] = 472;
                Big5Freq[22][18] = 471;
                Big5Freq[5][1] = 470;
                Big5Freq[20][119] = 469;
                Big5Freq[3][7] = 468;
                Big5Freq[10][79] = 467;
                Big5Freq[15][105] = 466;
                Big5Freq[3][144] = 465;
                Big5Freq[12][80] = 464;
                Big5Freq[15][73] = 463;
                Big5Freq[3][19] = 462;
                Big5Freq[8][109] = 461;
                Big5Freq[3][15] = 460;
                Big5Freq[31][82] = 459;
                Big5Freq[3][43] = 458;
                Big5Freq[25][119] = 457;
                Big5Freq[16][111] = 456;
                Big5Freq[7][77] = 455;
                Big5Freq[3][95] = 454;
                Big5Freq[24][82] = 453;
                Big5Freq[7][52] = 452;
                Big5Freq[9][151] = 451;
                Big5Freq[3][129] = 450;
                Big5Freq[5][87] = 449;
                Big5Freq[3][55] = 448;
                Big5Freq[8][153] = 447;
                Big5Freq[4][83] = 446;
                Big5Freq[3][114] = 445;
                Big5Freq[23][147] = 444;
                Big5Freq[15][31] = 443;
                Big5Freq[3][54] = 442;
                Big5Freq[11][122] = 441;
                Big5Freq[4][4] = 440;
                Big5Freq[34][149] = 439;
                Big5Freq[3][17] = 438;
                Big5Freq[21][64] = 437;
                Big5Freq[26][144] = 436;
                Big5Freq[4][62] = 435;
                Big5Freq[8][15] = 434;
                Big5Freq[35][80] = 433;
                Big5Freq[7][110] = 432;
                Big5Freq[23][114] = 431;
                Big5Freq[3][108] = 430;
                Big5Freq[3][62] = 429;
                Big5Freq[21][41] = 428;
                Big5Freq[15][99] = 427;
                Big5Freq[5][47] = 426;
                Big5Freq[4][96] = 425;
                Big5Freq[20][122] = 424;
                Big5Freq[5][21] = 423;
                Big5Freq[4][157] = 422;
                Big5Freq[16][14] = 421;
                Big5Freq[3][117] = 420;
                Big5Freq[7][129] = 419;
                Big5Freq[4][27] = 418;
                Big5Freq[5][30] = 417;
                Big5Freq[22][16] = 416;
                Big5Freq[5][64] = 415;
                Big5Freq[17][99] = 414;
                Big5Freq[17][57] = 413;
                Big5Freq[8][105] = 412;
                Big5Freq[5][112] = 411;
                Big5Freq[20][59] = 410;
                Big5Freq[6][129] = 409;
                Big5Freq[18][17] = 408;
                Big5Freq[3][92] = 407;
                Big5Freq[28][118] = 406;
                Big5Freq[3][109] = 405;
                Big5Freq[31][51] = 404;
                Big5Freq[13][116] = 403;
                Big5Freq[6][15] = 402;
                Big5Freq[36][136] = 401;
                Big5Freq[12][74] = 400;
                Big5Freq[20][88] = 399;
                Big5Freq[36][68] = 398;
                Big5Freq[3][147] = 397;
                Big5Freq[15][84] = 396;
                Big5Freq[16][32] = 395;
                Big5Freq[16][58] = 394;
                Big5Freq[7][66] = 393;
                Big5Freq[23][107] = 392;
                Big5Freq[9][6] = 391;
                Big5Freq[12][86] = 390;
                Big5Freq[23][112] = 389;
                Big5Freq[37][23] = 388;
                Big5Freq[3][138] = 387;
                Big5Freq[20][68] = 386;
                Big5Freq[15][116] = 385;
                Big5Freq[18][64] = 384;
                Big5Freq[12][139] = 383;
                Big5Freq[11][155] = 382;
                Big5Freq[4][156] = 381;
                Big5Freq[12][84] = 380;
                Big5Freq[18][49] = 379;
                Big5Freq[25][125] = 378;
                Big5Freq[25][147] = 377;
                Big5Freq[15][110] = 376;
                Big5Freq[19][96] = 375;
                Big5Freq[30][152] = 374;
                Big5Freq[6][31] = 373;
                Big5Freq[27][117] = 372;
                Big5Freq[3][10] = 371;
                Big5Freq[6][131] = 370;
                Big5Freq[13][112] = 369;
                Big5Freq[36][156] = 368;
                Big5Freq[4][60] = 367;
                Big5Freq[15][121] = 366;
                Big5Freq[4][112] = 365;
                Big5Freq[30][142] = 364;
                Big5Freq[23][154] = 363;
                Big5Freq[27][101] = 362;
                Big5Freq[9][140] = 361;
                Big5Freq[3][89] = 360;
                Big5Freq[18][148] = 359;
                Big5Freq[4][69] = 358;
                Big5Freq[16][49] = 357;
                Big5Freq[6][117] = 356;
                Big5Freq[36][55] = 355;
                Big5Freq[5][123] = 354;
                Big5Freq[4][126] = 353;
                Big5Freq[4][119] = 352;
                Big5Freq[9][95] = 351;
                Big5Freq[5][24] = 350;
                Big5Freq[16][133] = 349;
                Big5Freq[10][134] = 348;
                Big5Freq[26][59] = 347;
                Big5Freq[6][41] = 346;
                Big5Freq[6][146] = 345;
                Big5Freq[19][24] = 344;
                Big5Freq[5][113] = 343;
                Big5Freq[10][118] = 342;
                Big5Freq[34][151] = 341;
                Big5Freq[9][72] = 340;
                Big5Freq[31][25] = 339;
                Big5Freq[18][126] = 338;
                Big5Freq[18][28] = 337;
                Big5Freq[4][153] = 336;
                Big5Freq[3][84] = 335;
                Big5Freq[21][18] = 334;
                Big5Freq[25][129] = 333;
                Big5Freq[6][107] = 332;
                Big5Freq[12][25] = 331;
                Big5Freq[17][109] = 330;
                Big5Freq[7][76] = 329;
                Big5Freq[15][15] = 328;
                Big5Freq[4][14] = 327;
                Big5Freq[23][88] = 326;
                Big5Freq[18][2] = 325;
                Big5Freq[6][88] = 324;
                Big5Freq[16][84] = 323;
                Big5Freq[12][48] = 322;
                Big5Freq[7][68] = 321;
                Big5Freq[5][50] = 320;
                Big5Freq[13][54] = 319;
                Big5Freq[7][98] = 318;
                Big5Freq[11][6] = 317;
                Big5Freq[9][80] = 316;
                Big5Freq[16][41] = 315;
                Big5Freq[7][43] = 314;
                Big5Freq[28][117] = 313;
                Big5Freq[3][51] = 312;
                Big5Freq[7][3] = 311;
                Big5Freq[20][81] = 310;
                Big5Freq[4][2] = 309;
                Big5Freq[11][16] = 308;
                Big5Freq[10][4] = 307;
                Big5Freq[10][119] = 306;
                Big5Freq[6][142] = 305;
                Big5Freq[18][51] = 304;
                Big5Freq[8][144] = 303;
                Big5Freq[10][65] = 302;
                Big5Freq[11][64] = 301;
                Big5Freq[11][130] = 300;
                Big5Freq[9][92] = 299;
                Big5Freq[18][29] = 298;
                Big5Freq[18][78] = 297;
                Big5Freq[18][151] = 296;
                Big5Freq[33][127] = 295;
                Big5Freq[35][113] = 294;
                Big5Freq[10][155] = 293;
                Big5Freq[3][76] = 292;
                Big5Freq[36][123] = 291;
                Big5Freq[13][143] = 290;
                Big5Freq[5][135] = 289;
                Big5Freq[23][116] = 288;
                Big5Freq[6][101] = 287;
                Big5Freq[14][74] = 286;
                Big5Freq[7][153] = 285;
                Big5Freq[3][101] = 284;
                Big5Freq[9][74] = 283;
                Big5Freq[3][156] = 282;
                Big5Freq[4][147] = 281;
                Big5Freq[9][12] = 280;
                Big5Freq[18][133] = 279;
                Big5Freq[4][0] = 278;
                Big5Freq[7][155] = 277;
                Big5Freq[9][144] = 276;
                Big5Freq[23][49] = 275;
                Big5Freq[5][89] = 274;
                Big5Freq[10][11] = 273;
                Big5Freq[3][110] = 272;
                Big5Freq[3][40] = 271;
                Big5Freq[29][115] = 270;
                Big5Freq[9][100] = 269;
                Big5Freq[21][67] = 268;
                Big5Freq[23][145] = 267;
                Big5Freq[10][47] = 266;
                Big5Freq[4][31] = 265;
                Big5Freq[4][81] = 264;
                Big5Freq[22][62] = 263;
                Big5Freq[4][28] = 262;
                Big5Freq[27][39] = 261;
                Big5Freq[27][54] = 260;
                Big5Freq[32][46] = 259;
                Big5Freq[4][76] = 258;
                Big5Freq[26][15] = 257;
                Big5Freq[12][154] = 256;
                Big5Freq[9][150] = 255;
                Big5Freq[15][17] = 254;
                Big5Freq[5][129] = 253;
                Big5Freq[10][40] = 252;
                Big5Freq[13][37] = 251;
                Big5Freq[31][104] = 250;
                Big5Freq[3][152] = 249;
                Big5Freq[5][22] = 248;
                Big5Freq[8][48] = 247;
                Big5Freq[4][74] = 246;
                Big5Freq[6][17] = 245;
                Big5Freq[30][82] = 244;
                Big5Freq[4][116] = 243;
                Big5Freq[16][42] = 242;
                Big5Freq[5][55] = 241;
                Big5Freq[4][64] = 240;
                Big5Freq[14][19] = 239;
                Big5Freq[35][82] = 238;
                Big5Freq[30][139] = 237;
                Big5Freq[26][152] = 236;
                Big5Freq[32][32] = 235;
                Big5Freq[21][102] = 234;
                Big5Freq[10][131] = 233;
                Big5Freq[9][128] = 232;
                Big5Freq[3][87] = 231;
                Big5Freq[4][51] = 230;
                Big5Freq[10][15] = 229;
                Big5Freq[4][150] = 228;
                Big5Freq[7][4] = 227;
                Big5Freq[7][51] = 226;
                Big5Freq[7][157] = 225;
                Big5Freq[4][146] = 224;
                Big5Freq[4][91] = 223;
                Big5Freq[7][13] = 222;
                Big5Freq[17][116] = 221;
                Big5Freq[23][21] = 220;
                Big5Freq[5][106] = 219;
                Big5Freq[14][100] = 218;
                Big5Freq[10][152] = 217;
                Big5Freq[14][89] = 216;
                Big5Freq[6][138] = 215;
                Big5Freq[12][157] = 214;
                Big5Freq[10][102] = 213;
                Big5Freq[19][94] = 212;
                Big5Freq[7][74] = 211;
                Big5Freq[18][128] = 210;
                Big5Freq[27][111] = 209;
                Big5Freq[11][57] = 208;
                Big5Freq[3][131] = 207;
                Big5Freq[30][23] = 206;
                Big5Freq[30][126] = 205;
                Big5Freq[4][36] = 204;
                Big5Freq[26][124] = 203;
                Big5Freq[4][19] = 202;
                Big5Freq[9][152] = 201;

#endregion
            }

            if (EUC_TWFreq[0] == null)
            {
                for (i = 0; i < 94; i++)
                {
                    EUC_TWFreq[i] = new int[94];
                }

#region EUC_TWFreq[48][49] = 599;  

                EUC_TWFreq[35][65] = 598;
                EUC_TWFreq[41][27] = 597;
                EUC_TWFreq[35][0] = 596;
                EUC_TWFreq[39][19] = 595;
                EUC_TWFreq[35][42] = 594;
                EUC_TWFreq[38][66] = 593;
                EUC_TWFreq[35][8] = 592;
                EUC_TWFreq[35][6] = 591;
                EUC_TWFreq[35][66] = 590;
                EUC_TWFreq[43][14] = 589;
                EUC_TWFreq[69][80] = 588;
                EUC_TWFreq[50][48] = 587;
                EUC_TWFreq[36][71] = 586;
                EUC_TWFreq[37][10] = 585;
                EUC_TWFreq[60][52] = 584;
                EUC_TWFreq[51][21] = 583;
                EUC_TWFreq[40][2] = 582;
                EUC_TWFreq[67][35] = 581;
                EUC_TWFreq[38][78] = 580;
                EUC_TWFreq[49][18] = 579;
                EUC_TWFreq[35][23] = 578;
                EUC_TWFreq[42][83] = 577;
                EUC_TWFreq[79][47] = 576;
                EUC_TWFreq[61][82] = 575;
                EUC_TWFreq[38][7] = 574;
                EUC_TWFreq[35][29] = 573;
                EUC_TWFreq[37][77] = 572;
                EUC_TWFreq[54][67] = 571;
                EUC_TWFreq[38][80] = 570;
                EUC_TWFreq[52][74] = 569;
                EUC_TWFreq[36][37] = 568;
                EUC_TWFreq[74][8] = 567;
                EUC_TWFreq[41][83] = 566;
                EUC_TWFreq[36][75] = 565;
                EUC_TWFreq[49][63] = 564;
                EUC_TWFreq[42][58] = 563;
                EUC_TWFreq[56][33] = 562;
                EUC_TWFreq[37][76] = 561;
                EUC_TWFreq[62][39] = 560;
                EUC_TWFreq[35][21] = 559;
                EUC_TWFreq[70][19] = 558;
                EUC_TWFreq[77][88] = 557;
                EUC_TWFreq[51][14] = 556;
                EUC_TWFreq[36][17] = 555;
                EUC_TWFreq[44][51] = 554;
                EUC_TWFreq[38][72] = 553;
                EUC_TWFreq[74][90] = 552;
                EUC_TWFreq[35][48] = 551;
                EUC_TWFreq[35][69] = 550;
                EUC_TWFreq[66][86] = 549;
                EUC_TWFreq[57][20] = 548;
                EUC_TWFreq[35][53] = 547;
                EUC_TWFreq[36][87] = 546;
                EUC_TWFreq[84][67] = 545;
                EUC_TWFreq[70][56] = 544;
                EUC_TWFreq[71][54] = 543;
                EUC_TWFreq[60][70] = 542;
                EUC_TWFreq[80][1] = 541;
                EUC_TWFreq[39][59] = 540;
                EUC_TWFreq[39][51] = 539;
                EUC_TWFreq[35][44] = 538;
                EUC_TWFreq[48][4] = 537;
                EUC_TWFreq[55][24] = 536;
                EUC_TWFreq[52][4] = 535;
                EUC_TWFreq[54][26] = 534;
                EUC_TWFreq[36][31] = 533;
                EUC_TWFreq[37][22] = 532;
                EUC_TWFreq[37][9] = 531;
                EUC_TWFreq[46][0] = 530;
                EUC_TWFreq[56][46] = 529;
                EUC_TWFreq[47][93] = 528;
                EUC_TWFreq[37][25] = 527;
                EUC_TWFreq[39][8] = 526;
                EUC_TWFreq[46][73] = 525;
                EUC_TWFreq[38][48] = 524;
                EUC_TWFreq[39][83] = 523;
                EUC_TWFreq[60][92] = 522;
                EUC_TWFreq[70][11] = 521;
                EUC_TWFreq[63][84] = 520;
                EUC_TWFreq[38][65] = 519;
                EUC_TWFreq[45][45] = 518;
                EUC_TWFreq[63][49] = 517;
                EUC_TWFreq[63][50] = 516;
                EUC_TWFreq[39][93] = 515;
                EUC_TWFreq[68][20] = 514;
                EUC_TWFreq[44][84] = 513;
                EUC_TWFreq[66][34] = 512;
                EUC_TWFreq[37][58] = 511;
                EUC_TWFreq[39][0] = 510;
                EUC_TWFreq[59][1] = 509;
                EUC_TWFreq[47][8] = 508;
                EUC_TWFreq[61][17] = 507;
                EUC_TWFreq[53][87] = 506;
                EUC_TWFreq[67][26] = 505;
                EUC_TWFreq[43][46] = 504;
                EUC_TWFreq[38][61] = 503;
                EUC_TWFreq[45][9] = 502;
                EUC_TWFreq[66][83] = 501;
                EUC_TWFreq[43][88] = 500;
                EUC_TWFreq[85][20] = 499;
                EUC_TWFreq[57][36] = 498;
                EUC_TWFreq[43][6] = 497;
                EUC_TWFreq[86][77] = 496;
                EUC_TWFreq[42][70] = 495;
                EUC_TWFreq[49][78] = 494;
                EUC_TWFreq[36][40] = 493;
                EUC_TWFreq[42][71] = 492;
                EUC_TWFreq[58][49] = 491;
                EUC_TWFreq[35][20] = 490;
                EUC_TWFreq[76][20] = 489;
                EUC_TWFreq[39][25] = 488;
                EUC_TWFreq[40][34] = 487;
                EUC_TWFreq[39][76] = 486;
                EUC_TWFreq[40][1] = 485;
                EUC_TWFreq[59][0] = 484;
                EUC_TWFreq[39][70] = 483;
                EUC_TWFreq[46][14] = 482;
                EUC_TWFreq[68][77] = 481;
                EUC_TWFreq[38][55] = 480;
                EUC_TWFreq[35][78] = 479;
                EUC_TWFreq[84][44] = 478;
                EUC_TWFreq[36][41] = 477;
                EUC_TWFreq[37][62] = 476;
                EUC_TWFreq[65][67] = 475;
                EUC_TWFreq[69][66] = 474;
                EUC_TWFreq[73][55] = 473;
                EUC_TWFreq[71][49] = 472;
                EUC_TWFreq[66][87] = 471;
                EUC_TWFreq[38][33] = 470;
                EUC_TWFreq[64][61] = 469;
                EUC_TWFreq[35][7] = 468;
                EUC_TWFreq[47][49] = 467;
                EUC_TWFreq[56][14] = 466;
                EUC_TWFreq[36][49] = 465;
                EUC_TWFreq[50][81] = 464;
                EUC_TWFreq[55][76] = 463;
                EUC_TWFreq[35][19] = 462;
                EUC_TWFreq[44][47] = 461;
                EUC_TWFreq[35][15] = 460;
                EUC_TWFreq[82][59] = 459;
                EUC_TWFreq[35][43] = 458;
                EUC_TWFreq[73][0] = 457;
                EUC_TWFreq[57][83] = 456;
                EUC_TWFreq[42][46] = 455;
                EUC_TWFreq[36][0] = 454;
                EUC_TWFreq[70][88] = 453;
                EUC_TWFreq[42][22] = 452;
                EUC_TWFreq[46][58] = 451;
                EUC_TWFreq[36][34] = 450;
                EUC_TWFreq[39][24] = 449;
                EUC_TWFreq[35][55] = 448;
                EUC_TWFreq[44][91] = 447;
                EUC_TWFreq[37][51] = 446;
                EUC_TWFreq[36][19] = 445;
                EUC_TWFreq[69][90] = 444;
                EUC_TWFreq[55][35] = 443;
                EUC_TWFreq[35][54] = 442;
                EUC_TWFreq[49][61] = 441;
                EUC_TWFreq[36][67] = 440;
                EUC_TWFreq[88][34] = 439;
                EUC_TWFreq[35][17] = 438;
                EUC_TWFreq[65][69] = 437;
                EUC_TWFreq[74][89] = 436;
                EUC_TWFreq[37][31] = 435;
                EUC_TWFreq[43][48] = 434;
                EUC_TWFreq[89][27] = 433;
                EUC_TWFreq[42][79] = 432;
                EUC_TWFreq[69][57] = 431;
                EUC_TWFreq[36][13] = 430;
                EUC_TWFreq[35][62] = 429;
                EUC_TWFreq[65][47] = 428;
                EUC_TWFreq[56][8] = 427;
                EUC_TWFreq[38][79] = 426;
                EUC_TWFreq[37][64] = 425;
                EUC_TWFreq[64][64] = 424;
                EUC_TWFreq[38][53] = 423;
                EUC_TWFreq[38][31] = 422;
                EUC_TWFreq[56][81] = 421;
                EUC_TWFreq[36][22] = 420;
                EUC_TWFreq[43][4] = 419;
                EUC_TWFreq[36][90] = 418;
                EUC_TWFreq[38][62] = 417;
                EUC_TWFreq[66][85] = 416;
                EUC_TWFreq[39][1] = 415;
                EUC_TWFreq[59][40] = 414;
                EUC_TWFreq[58][93] = 413;
                EUC_TWFreq[44][43] = 412;
                EUC_TWFreq[39][49] = 411;
                EUC_TWFreq[64][2] = 410;
                EUC_TWFreq[41][35] = 409;
                EUC_TWFreq[60][22] = 408;
                EUC_TWFreq[35][91] = 407;
                EUC_TWFreq[78][1] = 406;
                EUC_TWFreq[36][14] = 405;
                EUC_TWFreq[82][29] = 404;
                EUC_TWFreq[52][86] = 403;
                EUC_TWFreq[40][16] = 402;
                EUC_TWFreq[91][52] = 401;
                EUC_TWFreq[50][75] = 400;
                EUC_TWFreq[64][30] = 399;
                EUC_TWFreq[90][78] = 398;
                EUC_TWFreq[36][52] = 397;
                EUC_TWFreq[55][87] = 396;
                EUC_TWFreq[57][5] = 395;
                EUC_TWFreq[57][31] = 394;
                EUC_TWFreq[42][35] = 393;
                EUC_TWFreq[69][50] = 392;
                EUC_TWFreq[45][8] = 391;
                EUC_TWFreq[50][87] = 390;
                EUC_TWFreq[69][55] = 389;
                EUC_TWFreq[92][3] = 388;
                EUC_TWFreq[36][43] = 387;
                EUC_TWFreq[64][10] = 386;
                EUC_TWFreq[56][25] = 385;
                EUC_TWFreq[60][68] = 384;
                EUC_TWFreq[51][46] = 383;
                EUC_TWFreq[50][0] = 382;
                EUC_TWFreq[38][30] = 381;
                EUC_TWFreq[50][85] = 380;
                EUC_TWFreq[60][54] = 379;
                EUC_TWFreq[73][6] = 378;
                EUC_TWFreq[73][28] = 377;
                EUC_TWFreq[56][19] = 376;
                EUC_TWFreq[62][69] = 375;
                EUC_TWFreq[81][66] = 374;
                EUC_TWFreq[40][32] = 373;
                EUC_TWFreq[76][31] = 372;
                EUC_TWFreq[35][10] = 371;
                EUC_TWFreq[41][37] = 370;
                EUC_TWFreq[52][82] = 369;
                EUC_TWFreq[91][72] = 368;
                EUC_TWFreq[37][29] = 367;
                EUC_TWFreq[56][30] = 366;
                EUC_TWFreq[37][80] = 365;
                EUC_TWFreq[81][56] = 364;
                EUC_TWFreq[70][3] = 363;
                EUC_TWFreq[76][15] = 362;
                EUC_TWFreq[46][47] = 361;
                EUC_TWFreq[35][88] = 360;
                EUC_TWFreq[61][58] = 359;
                EUC_TWFreq[37][37] = 358;
                EUC_TWFreq[57][22] = 357;
                EUC_TWFreq[41][23] = 356;
                EUC_TWFreq[90][66] = 355;
                EUC_TWFreq[39][60] = 354;
                EUC_TWFreq[38][0] = 353;
                EUC_TWFreq[37][87] = 352;
                EUC_TWFreq[46][2] = 351;
                EUC_TWFreq[38][56] = 350;
                EUC_TWFreq[58][11] = 349;
                EUC_TWFreq[48][10] = 348;
                EUC_TWFreq[74][4] = 347;
                EUC_TWFreq[40][42] = 346;
                EUC_TWFreq[41][52] = 345;
                EUC_TWFreq[61][92] = 344;
                EUC_TWFreq[39][50] = 343;
                EUC_TWFreq[47][88] = 342;
                EUC_TWFreq[88][36] = 341;
                EUC_TWFreq[45][73] = 340;
                EUC_TWFreq[82][3] = 339;
                EUC_TWFreq[61][36] = 338;
                EUC_TWFreq[60][33] = 337;
                EUC_TWFreq[38][27] = 336;
                EUC_TWFreq[35][83] = 335;
                EUC_TWFreq[65][24] = 334;
                EUC_TWFreq[73][10] = 333;
                EUC_TWFreq[41][13] = 332;
                EUC_TWFreq[50][27] = 331;
                EUC_TWFreq[59][50] = 330;
                EUC_TWFreq[42][45] = 329;
                EUC_TWFreq[55][19] = 328;
                EUC_TWFreq[36][77] = 327;
                EUC_TWFreq[69][31] = 326;
                EUC_TWFreq[60][7] = 325;
                EUC_TWFreq[40][88] = 324;
                EUC_TWFreq[57][56] = 323;
                EUC_TWFreq[50][50] = 322;
                EUC_TWFreq[42][37] = 321;
                EUC_TWFreq[38][82] = 320;
                EUC_TWFreq[52][25] = 319;
                EUC_TWFreq[42][67] = 318;
                EUC_TWFreq[48][40] = 317;
                EUC_TWFreq[45][81] = 316;
                EUC_TWFreq[57][14] = 315;
                EUC_TWFreq[42][13] = 314;
                EUC_TWFreq[78][0] = 313;
                EUC_TWFreq[35][51] = 312;
                EUC_TWFreq[41][67] = 311;
                EUC_TWFreq[64][23] = 310;
                EUC_TWFreq[36][65] = 309;
                EUC_TWFreq[48][50] = 308;
                EUC_TWFreq[46][69] = 307;
                EUC_TWFreq[47][89] = 306;
                EUC_TWFreq[41][48] = 305;
                EUC_TWFreq[60][56] = 304;
                EUC_TWFreq[44][82] = 303;
                EUC_TWFreq[47][35] = 302;
                EUC_TWFreq[49][3] = 301;
                EUC_TWFreq[49][69] = 300;
                EUC_TWFreq[45][93] = 299;
                EUC_TWFreq[60][34] = 298;
                EUC_TWFreq[60][82] = 297;
                EUC_TWFreq[61][61] = 296;
                EUC_TWFreq[86][42] = 295;
                EUC_TWFreq[89][60] = 294;
                EUC_TWFreq[48][31] = 293;
                EUC_TWFreq[35][75] = 292;
                EUC_TWFreq[91][39] = 291;
                EUC_TWFreq[53][19] = 290;
                EUC_TWFreq[39][72] = 289;
                EUC_TWFreq[69][59] = 288;
                EUC_TWFreq[41][7] = 287;
                EUC_TWFreq[54][13] = 286;
                EUC_TWFreq[43][28] = 285;
                EUC_TWFreq[36][6] = 284;
                EUC_TWFreq[45][75] = 283;
                EUC_TWFreq[36][61] = 282;
                EUC_TWFreq[38][21] = 281;
                EUC_TWFreq[45][14] = 280;
                EUC_TWFreq[61][43] = 279;
                EUC_TWFreq[36][63] = 278;
                EUC_TWFreq[43][30] = 277;
                EUC_TWFreq[46][51] = 276;
                EUC_TWFreq[68][87] = 275;
                EUC_TWFreq[39][26] = 274;
                EUC_TWFreq[46][76] = 273;
                EUC_TWFreq[36][15] = 272;
                EUC_TWFreq[35][40] = 271;
                EUC_TWFreq[79][60] = 270;
                EUC_TWFreq[46][7] = 269;
                EUC_TWFreq[65][72] = 268;
                EUC_TWFreq[69][88] = 267;
                EUC_TWFreq[47][18] = 266;
                EUC_TWFreq[37][0] = 265;
                EUC_TWFreq[37][49] = 264;
                EUC_TWFreq[67][37] = 263;
                EUC_TWFreq[36][91] = 262;
                EUC_TWFreq[75][48] = 261;
                EUC_TWFreq[75][63] = 260;
                EUC_TWFreq[83][87] = 259;
                EUC_TWFreq[37][44] = 258;
                EUC_TWFreq[73][54] = 257;
                EUC_TWFreq[51][61] = 256;
                EUC_TWFreq[46][57] = 255;
                EUC_TWFreq[55][21] = 254;
                EUC_TWFreq[39][66] = 253;
                EUC_TWFreq[47][11] = 252;
                EUC_TWFreq[52][8] = 251;
                EUC_TWFreq[82][81] = 250;
                EUC_TWFreq[36][57] = 249;
                EUC_TWFreq[38][54] = 248;
                EUC_TWFreq[43][81] = 247;
                EUC_TWFreq[37][42] = 246;
                EUC_TWFreq[40][18] = 245;
                EUC_TWFreq[80][90] = 244;
                EUC_TWFreq[37][84] = 243;
                EUC_TWFreq[57][15] = 242;
                EUC_TWFreq[38][87] = 241;
                EUC_TWFreq[37][32] = 240;
                EUC_TWFreq[53][53] = 239;
                EUC_TWFreq[89][29] = 238;
                EUC_TWFreq[81][53] = 237;
                EUC_TWFreq[75][3] = 236;
                EUC_TWFreq[83][73] = 235;
                EUC_TWFreq[66][13] = 234;
                EUC_TWFreq[48][7] = 233;
                EUC_TWFreq[46][35] = 232;
                EUC_TWFreq[35][86] = 231;
                EUC_TWFreq[37][20] = 230;
                EUC_TWFreq[46][80] = 229;
                EUC_TWFreq[38][24] = 228;
                EUC_TWFreq[41][68] = 227;
                EUC_TWFreq[42][21] = 226;
                EUC_TWFreq[43][32] = 225;
                EUC_TWFreq[38][20] = 224;
                EUC_TWFreq[37][59] = 223;
                EUC_TWFreq[41][77] = 222;
                EUC_TWFreq[59][57] = 221;
                EUC_TWFreq[68][59] = 220;
                EUC_TWFreq[39][43] = 219;
                EUC_TWFreq[54][39] = 218;
                EUC_TWFreq[48][28] = 217;
                EUC_TWFreq[54][28] = 216;
                EUC_TWFreq[41][44] = 215;
                EUC_TWFreq[51][64] = 214;
                EUC_TWFreq[47][72] = 213;
                EUC_TWFreq[62][67] = 212;
                EUC_TWFreq[42][43] = 211;
                EUC_TWFreq[61][38] = 210;
                EUC_TWFreq[76][25] = 209;
                EUC_TWFreq[48][91] = 208;
                EUC_TWFreq[36][36] = 207;
                EUC_TWFreq[80][32] = 206;
                EUC_TWFreq[81][40] = 205;
                EUC_TWFreq[37][5] = 204;
                EUC_TWFreq[74][69] = 203;
                EUC_TWFreq[36][82] = 202;
                EUC_TWFreq[46][59] = 201;

#endregion
            }
        }


        /// <summary>  
        /// 将此实例中的指定 <see cref="sbyte"/> 字符数组转换到 <see cref="byte"/> 字符数组。  
        /// </summary>  
        /// <param name="sbyteArray">要转换的 <see cref="sbyte"/> 字符数组</param>  
        /// <returns>返回转换后的 <see cref="byte"/> 字符数组</returns>  
        public static byte[] ToByteArray(sbyte[] sbyteArray)
        {
            byte[] byteArray = new byte[sbyteArray.Length];
            for (int index = 0; index < sbyteArray.Length; index++)
                byteArray[index] = (byte)sbyteArray[index];
            return byteArray;
        }

        /// <summary>  
        /// 将此实例中的指定字符串转换到 <see cref="byte"/> 字符数组。  
        /// </summary>  
        /// <param name="sourceString">要转换的字符串</param>  
        /// <returns>返回转换后的 <see cref="byte"/> 字符数组</returns>  
        public static byte[] ToByteArray(string sourceString)
        {
            byte[] byteArray = new byte[sourceString.Length];
            for (int index = 0; index < sourceString.Length; index++)
                byteArray[index] = (byte)sourceString[index];
            return byteArray;
        }

        /// <summary>  
        /// 将此实例中的指定 <see cref="object"/> 数组转换到 <see cref="byte"/> 字符数组。  
        /// </summary>  
        /// <param name="tempObjectArray">要转换的 <see cref="object"/> 字符数组</param>  
        /// <returns>返回转换后的 <see cref="byte"/> 字符数组</returns>  
        public static byte[] ToByteArray(object[] tempObjectArray)
        {
            byte[] byteArray = new byte[tempObjectArray.Length];
            for (int index = 0; index < tempObjectArray.Length; index++)
                byteArray[index] = (byte)tempObjectArray[index];
            return byteArray;
        }

        /// <summary>  
        /// 将此实例中的指定 <see cref="byte"/> 字符数组转换到 <see cref="sbyte"/> 字符数组。  
        /// </summary>  
        /// <param name="byteArray">要转换的 <see cref="byte"/> 字符数组</param>  
        /// <returns>返回转换后的 <see cref="sbyte"/> 字符数组</returns>  
        public static sbyte[] ToSByteArray(byte[] byteArray)
        {
            sbyte[] sbyteArray = new sbyte[byteArray.Length];
            for (int index = 0; index < byteArray.Length; index++)
                sbyteArray[index] = (sbyte)byteArray[index];
            return sbyteArray;
        }

        /// <summary>从流读取字节序列,并将此流中的位置提升读取的字节数.</summary>  
        /// <param name="sourceStream">要读取的流.</param>  
        /// <param name="target">字节数组。此方法返回时,该缓冲区包含指定的字符数组,该数组的 start 和 (start + count-1) 之间的值由从当前源中读取的字节替换。</param>  
        /// <param name="start">buffer 中的从零开始的字节偏移量,从此处开始存储从当前流中读取的数据。.</param>  
        /// <param name="count">要从当前流中最多读取的字节数。</param>  
        /// <returns>读入缓冲区中的总字节数。如果当前可用的字节数没有请求的字节数那么多,则总字节数可能小于请求的字节数,或者如果已到达流的末尾,则为零 (0)。</returns>  
        /// <exception cref="ArgumentException">start 与 count 的和大于缓冲区长度。</exception>  
        /// <exception cref="ArgumentNullException">target 为空引用(Visual Basic 中为 Nothing)。</exception>  
        /// <exception cref="ArgumentOutOfRangeException">offset 或 count 为负。</exception>  
        /// <exception cref="System.IO.IOException">发生 I/O 错误。</exception>  
        /// <exception cref="NotSupportedException">流不支持读取。</exception>  
        /// <exception cref="ObjectDisposedException">在流关闭后调用方法。</exception>  
        public static int ReadInput(System.IO.Stream sourceStream, ref sbyte[] target, int start, int count)
        {
            // Returns 0 bytes if not enough space in target  
            if (target.Length == 0)
                return 0;

            byte[] receiver = new byte[target.Length];
            int bytesRead = sourceStream.Read(receiver, start, count);

            // Returns -1 if EOF  
            if (bytesRead == 0)
                return -1;

            for (int i = start; i < start + bytesRead; i++)
                target[i] = (sbyte)receiver[i];

            return bytesRead;
        }

        /// <summary>从字符系列读取字节序列,并将此字符系列中的位置提升读取的字节数。</summary>  
        /// <param name="sourceTextReader">要读取的流。</param>  
        /// <param name="target">字节数组。此方法返回时,该缓冲区包含指定的字符数组,该数组的 start 和 (start + count-1) 之间的值由从当前源中读取的字节替换。</param>  
        /// <param name="start">buffer 中的从零开始的字节偏移量,从此处开始存储从当前流中读取的数据。.</param>  
        /// <param name="count">要从当前流中最多读取的字节数。</param>  
        /// <returns>读入缓冲区中的总字节数。如果当前可用的字节数没有请求的字节数那么多,则总字节数可能小于请求的字节数,或者如果已到达流的末尾,则为零 (0)。</returns>  
        /// <exception cref="ArgumentException">start 与 count 的和大于缓冲区长度。</exception>  
        /// <exception cref="ArgumentNullException">target 为空引用(Visual Basic 中为 Nothing)。</exception>  
        /// <exception cref="ArgumentOutOfRangeException">offset 或 count 为负。</exception>  
        /// <exception cref="System.IO.IOException">发生 I/O 错误。</exception>  
        /// <exception cref="NotSupportedException">流不支持读取。</exception>  
        /// <exception cref="ObjectDisposedException">在流关闭后调用方法。</exception>  
        public static int ReadInput(System.IO.TextReader sourceTextReader, ref sbyte[] target, int start, int count)
        {
            // Returns 0 bytes if not enough space in target  
            if (target.Length == 0)
                return 0;

            char[] charArray = new char[target.Length];
            int bytesRead = sourceTextReader.Read(charArray, start, count);

            // Returns -1 if EOF  
            if (bytesRead == 0)
                return -1;

            for (int index = start; index < start + bytesRead; index++)
                target[index] = (sbyte)charArray[index];

            return bytesRead;
        }

        /// <summary>  
        /// 检测当前文件的大小  
        /// </summary>  
        /// <param name="file">被检测的文件</param>  
        /// <returns>当前文件的大小。</returns>  
        public static long FileLength(System.IO.FileInfo file)
        {
            if (System.IO.Directory.Exists(file.FullName))
                return 0;
            else
                return file.Length;
        }

        /// <summary>  
        /// This method returns the literal value received  
        /// </summary>  
        /// <param name="literal">The literal to return</param>  
        /// <returns>The received value</returns>  
        public static long Identity(long literal)
        {
            return literal;
        }

        /// <summary>  
        /// This method returns the literal value received  
        /// </summary>  
        /// <param name="literal">The literal to return</param>  
        /// <returns>The received value</returns>  
        public static ulong Identity(ulong literal)
        {
            return literal;
        }

        /// <summary>  
        /// This method returns the literal value received  
        /// </summary>  
        /// <param name="literal">The literal to return</param>  
        /// <returns>The received value</returns>  
        public static float Identity(float literal)
        {
            return literal;
        }

        /// <summary>  
        /// This method returns the literal value received  
        /// </summary>  
        /// <param name="literal">The literal to return</param>  
        /// <returns>The received value</returns>  
        public static double Identity(double literal)
        {
            return literal;
        }
    }
#endif
}
