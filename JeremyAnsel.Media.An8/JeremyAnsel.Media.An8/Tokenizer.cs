// <copyright file="Tokenizer.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The tokenizer.
    /// </summary>
    internal static class Tokenizer
    {
        /// <summary>
        /// The delimiters of tokens.
        /// </summary>
        private static readonly char[] Delimiters = new char[] { '"', '{', '}', '(', ')', ',' };

        /// <summary>
        /// Specials values for tokens building.
        /// </summary>
        private static readonly string[] BuildTokens = new string[] { ">", "<" };

        /// <summary>
        /// Tokenizes a string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The tokens.</returns>
        public static string[] Tokenize(string text)
        {
            var tokens = new List<string>();

            if (string.IsNullOrEmpty(text))
            {
#if NET40
                return new string[0];
#else
                return Array.Empty<string>();
#endif
            }

            bool isInString = false;

            int currentIndex = 0;
            var currentString = new StringBuilder();
            bool isEscaping = false;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (isInString)
                {
                    if (isEscaping)
                    {
                        currentString.Append(c);
                        isEscaping = false;
                    }
                    else if (c == '\\')
                    {
                        isEscaping = true;
                    }
                    else if (c == '"')
                    {
                        tokens.Add(currentString.ToString());

                        currentIndex = i + 1;

                        isInString = false;
                    }
                    else
                    {
                        currentString.Append(c);
                    }
                }
                else if (Delimiters.Contains(c))
                {
                    if (i != currentIndex)
                    {
                        tokens.Add(text.Substring(currentIndex, i - currentIndex));
                    }

                    currentIndex = i + 1;

                    if (c == '"')
                    {
                        isInString = true;
                        currentString.Clear();
                        currentString.Append('"');
                    }
                    else
                    {
                        tokens.Add(text.Substring(i, 1));
                    }
                }
                else if (char.IsWhiteSpace(c))
                {
                    if (i != currentIndex)
                    {
                        tokens.Add(text.Substring(currentIndex, i - currentIndex));
                    }

                    currentIndex = i + 1;
                }
            }

            if (currentIndex != text.Length)
            {
                if (isInString)
                {
                    tokens.Add(currentString.ToString());
                }
                else
                {
                    tokens.Add(text.Substring(currentIndex, text.Length - currentIndex));
                }
            }

            return tokens.ToArray();
        }

        /// <summary>
        /// Splits chunks.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The chunks.</returns>
        public static List<An8ChunkTokens> SplitChunks(string text)
        {
            return Tokenizer.SplitChunks(Tokenizer.Tokenize(text));
        }

        /// <summary>
        /// Split tokens by chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The chunks.</returns>
        public static List<An8ChunkTokens> SplitChunks(string[] tokens)
        {
            var chunks = new List<An8ChunkTokens>();

            if (tokens == null)
            {
                return chunks;
            }

            var data = new List<string>();

            for (int index = 0; index < tokens.Length; index++)
            {
                if (index < tokens.Length - 1 && string.Equals(tokens[index + 1], "{", StringComparison.Ordinal))
                {
                    var chunk = Tokenizer.GetChunkTokens(tokens, index);
                    index += 2 + chunk.Tokens.Length;

                    chunks.Add(chunk);
                }
                else
                {
                    var token = tokens[index];

                    data.Add(token);
                }
            }

            if (data.Count != 0)
            {
                var chunk = new An8ChunkTokens
                {
                    Ident = null,
                    Tokens = data.ToArray()
                };

                chunks.Insert(0, chunk);
            }

            return chunks;
        }

        /// <summary>
        /// Gets a chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>A chunk.</returns>
        [SuppressMessage("Globalization", "CA1303:Ne pas passer de littéraux en paramètres localisés", Justification = "Reviewed.")]
        public static An8ChunkTokens GetChunkTokens(string[] tokens, int startIndex)
        {
            string chunkIdent = tokens[startIndex];

            var chunkTokens = new List<string>();

            if (!string.Equals(tokens[startIndex + 1], "{", StringComparison.Ordinal))
            {
                throw new InvalidDataException("open brace not found");
            }

            int level = 1;

            for (int index = startIndex + 2; index < tokens.Length; index++)
            {
                string token = tokens[index];

                if (string.Equals(token, "{", StringComparison.Ordinal))
                {
                    level++;
                }
                else if (string.Equals(token, "}", StringComparison.Ordinal))
                {
                    level--;
                }

                if (level == 0)
                {
                    chunkTokens.AddRange(tokens.Skip(startIndex + 2).Take(index - startIndex - 2));
                    break;
                }
            }

            return new An8ChunkTokens
            {
                Ident = chunkIdent,
                Tokens = chunkTokens.ToArray()
            };
        }

        /// <summary>
        /// Reads an open chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed")]
        [SuppressMessage("Globalization", "CA1303:Ne pas passer de littéraux en paramètres localisés", Justification = "Reviewed.")]
        public static void ReadOpenChunk(string[] tokens, ref int index)
        {
            if (!string.Equals(tokens[index++], "{", StringComparison.Ordinal))
            {
                throw new InvalidDataException("chunk is not opened");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the chunk is closed.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        /// <returns>A boolean.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed")]
        public static bool IsClosedChunk(string[] tokens, ref int index)
        {
            bool closed = string.Equals(tokens[index], "}", StringComparison.Ordinal);

            if (closed)
            {
                index++;
            }

            return closed;
        }

        /// <summary>
        /// Reads a close chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed")]
        [SuppressMessage("Globalization", "CA1303:Ne pas passer de littéraux en paramètres localisés", Justification = "Reviewed.")]
        public static void ReadCloseChunk(string[] tokens, ref int index)
        {
            if (!Tokenizer.IsClosedChunk(tokens, ref index))
            {
                throw new InvalidDataException("chunk is not closed");
            }
        }

        /// <summary>
        /// Reads an open data.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        [SuppressMessage("Globalization", "CA1303:Ne pas passer de littéraux en paramètres localisés", Justification = "Reviewed.")]
        public static void ReadOpenData(string[] tokens, ref int index)
        {
            if (!string.Equals(tokens[index++], "(", StringComparison.Ordinal))
            {
                throw new InvalidDataException("data is not opened");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the data is closed.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        /// <returns>A boolean.</returns>
        public static bool IsClosedData(string[] tokens, ref int index)
        {
            bool closed = string.Equals(tokens[index], ")", StringComparison.Ordinal);

            if (closed)
            {
                index++;
            }

            return closed;
        }

        /// <summary>
        /// Reads a close data.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        [SuppressMessage("Globalization", "CA1303:Ne pas passer de littéraux en paramètres localisés", Justification = "Reviewed.")]
        public static void ReadCloseData(string[] tokens, ref int index)
        {
            if (!Tokenizer.IsClosedData(tokens, ref index))
            {
                throw new InvalidDataException("data is not closed");
            }
        }

        /// <summary>
        /// Reads a string.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        /// <returns>A string.</returns>
        [SuppressMessage("Globalization", "CA1303:Ne pas passer de littéraux en paramètres localisés", Justification = "Reviewed.")]
        public static string ReadString(string[] tokens, ref int index)
        {
            string str = tokens[index++];

            if (str[0] != '"')
            {
                throw new InvalidDataException("invalid string");
            }

            return str.Substring(1);
        }

        /// <summary>
        /// Reads a unicode string.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        /// <returns>A unicode string.</returns>
        [SuppressMessage("Globalization", "CA1303:Ne pas passer de littéraux en paramètres localisés", Justification = "Reviewed.")]
        public static string ReadUnicodeString(string[] tokens, ref int index)
        {
            //// TODO: replace unicode characters

            if (!string.Equals(tokens[index++], "L", StringComparison.Ordinal))
            {
                throw new InvalidDataException("invalid unicode string");
            }

            string str = tokens[index++];

            if (str[0] != '"')
            {
                throw new InvalidDataException("invalid unicode string");
            }

            return str.Substring(1);
        }

        /// <summary>
        /// Reads a integer.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        /// <returns>A integer.</returns>
        public static int ReadInt(string[] tokens, ref int index)
        {
            string str = tokens[index++];

            return int.Parse(str, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Reads a floating-point value.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        /// <returns>A floating-point value.</returns>
        public static float ReadFloat(string[] tokens, ref int index)
        {
            string str = tokens[index++];

            return float.Parse(str, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Builds a content.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The generated text.</returns>
        public static string Build(string[] tokens)
        {
            var lines = new List<string>();

            int currentIndex = 0;

            for (int index = 0; index < tokens.Length; index++)
            {
                string token = tokens[index];

                if (BuildTokens.Contains(token, StringComparer.Ordinal))
                {
                    lines.Add(token);
                    currentIndex = index + 1;
                }
                else if (string.Equals(token, "\n", StringComparison.Ordinal))
                {
                    if (currentIndex != index)
                    {
                        lines.Add(string.Join(
                            " ",
                            tokens
                            .Skip(currentIndex)
                            .Take(index - currentIndex)
                            .Where(t => !BuildTokens.Contains(t, StringComparer.Ordinal))));
                    }

                    currentIndex = index + 1;
                }
            }

            if (currentIndex != tokens.Length)
            {
                lines.Add(string.Join(
                    " ",
                    tokens
                    .Skip(currentIndex)
                    .Take(tokens.Length - currentIndex)
                    .Where(t => !BuildTokens.Contains(t, StringComparer.Ordinal))));
            }

            var sb = new StringBuilder();

            int level = 0;

            foreach (var line in lines)
            {
                if (string.Equals(line, ">", StringComparison.Ordinal))
                {
                    level++;
                }
                else if (string.Equals(line, "<", StringComparison.Ordinal))
                {
                    level--;
                }
                else
                {
                    sb.Append(string.Concat(Enumerable.Repeat(" ", level * 2)));
                    sb.AppendLine(line);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Builds a new line.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public static void BuildNewLine(List<string> tokens)
        {
            tokens.Add("\n");
        }

        /// <summary>
        /// Builds an indentation.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public static void BuildIndent(List<string> tokens)
        {
            tokens.Add("\n");
            tokens.Add(">");
        }

        /// <summary>
        /// Builds an un-indentation.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public static void BuildUnindent(List<string> tokens)
        {
            tokens.Add("\n");
            tokens.Add("<");
        }

        /// <summary>
        /// Builds an open chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="ident">The chunk identifier</param>
        public static void BuildOpenChunk(List<string> tokens, string ident)
        {
            tokens.Add(ident);
            tokens.Add("{");
        }

        /// <summary>
        /// Builds a close chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public static void BuildCloseChunk(List<string> tokens)
        {
            tokens.Add("}");
        }

        /// <summary>
        /// Builds an open data.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public static void BuildOpenData(List<string> tokens)
        {
            tokens.Add("(");
        }

        /// <summary>
        /// Builds a close data.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public static void BuildCloseData(List<string> tokens)
        {
            tokens.Add(")");
        }

        /// <summary>
        /// Builds a string.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="str">The string.</param>
        public static void BuildString(List<string> tokens, string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                tokens.Add("\"\"");
            }
            else
            {
                tokens.Add(string.Concat(
                    "\"",
                    str.Replace("\\", "\\\\").Replace("\"", "\\\""),
                    "\""));
            }
        }

        /// <summary>
        /// Builds a unicode string.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="str">The unicode string.</param>
        public static void BuildUnicodeString(List<string> tokens, string str)
        {
            //// TODO: replace unicode characters

            if (string.IsNullOrEmpty(str))
            {
                tokens.Add("L\"\"");
            }
            else
            {
                tokens.Add(string.Concat(
                    "L\"",
                    str.Replace("\\", "\\\\").Replace("\"", "\\\""),
                    "\""));
            }
        }

        /// <summary>
        /// Builds a integer.
        /// </summary>
        /// <param name="tokens">The tokens</param>
        /// <param name="value">A integer.</param>
        public static void BuildInt(List<string> tokens, int value)
        {
            tokens.Add(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Builds a floating-point value.
        /// </summary>
        /// <param name="tokens">The tokens</param>
        /// <param name="value">A floating-point value.</param>
        public static void BuildFloat(List<string> tokens, float value)
        {
            tokens.Add(value.ToString("F6", CultureInfo.InvariantCulture));
        }
    }
}
