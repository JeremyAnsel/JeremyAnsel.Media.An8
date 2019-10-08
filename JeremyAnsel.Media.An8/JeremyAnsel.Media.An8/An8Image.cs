// <copyright file="An8Image.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// An image.
    /// </summary>
    public sealed class An8Image : An8Component
    {
        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int Height { get; set; }

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
                    case null:
                        this.FileName = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "size":
                        this.Width = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        this.Height = Tokenizer.ReadInt(chunk.Tokens, ref index);
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

            Tokenizer.BuildOpenChunk(tokens, "image");
            Tokenizer.BuildIndent(tokens);

            tokens.AddRange(base.BuildTokens());

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildString(tokens, this.FileName);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "size");
            Tokenizer.BuildInt(tokens, this.Width);
            Tokenizer.BuildInt(tokens, this.Height);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
