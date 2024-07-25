// <copyright file="An8TextCom.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A text.
    /// </summary>
    public sealed class An8TextCom : An8Component
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the font face.
        /// </summary>
        public string? FontFamily { get; set; }

        /// <summary>
        /// Gets or sets the font size.
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the font is bold.
        /// </summary>
        public bool IsBold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the font is italic.
        /// </summary>
        public bool IsItalic { get; set; }

        /// <summary>
        /// Parses the tokens of the chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        internal override void ParseTokens(string[] tokens)
        {
            base.ParseTokens(tokens);

            var chunks = Tokenizer.SplitChunks(tokens);

            foreach (var chunk in chunks)
            {
                int index = 0;

                switch (chunk.Ident)
                {
                    case "string":
                        this.Text = Tokenizer.ReadUnicodeString(chunk.Tokens, ref index);
                        break;

                    case "typeface":
                        this.FontFamily = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "size":
                        this.FontSize = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        break;

                    case "bold":
                        this.IsBold = true;
                        break;

                    case "italic":
                        this.IsItalic = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Builds the tokens of the chunk.
        /// </summary>
        /// <returns>The tokens.</returns>
        internal override string[] BuildTokens()
        {
            var tokens = new List<string>();

            Tokenizer.BuildOpenChunk(tokens, "textcom");
            Tokenizer.BuildIndent(tokens);

            tokens.AddRange(base.BuildTokens());

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "string");
            Tokenizer.BuildUnicodeString(tokens, this.Text);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "typeface");
            Tokenizer.BuildString(tokens, this.FontFamily);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "size");
            Tokenizer.BuildInt(tokens, this.FontSize);
            Tokenizer.BuildCloseChunk(tokens);

            if (this.IsBold)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "bold");
                Tokenizer.BuildCloseChunk(tokens);
            }

            if (this.IsItalic)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "italic");
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
