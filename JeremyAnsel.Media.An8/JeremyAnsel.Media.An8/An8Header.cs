// <copyright file="An8Header.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A header.
    /// </summary>
    public sealed class An8Header : An8Chunk
    {
        /// <summary>
        /// Gets or sets the version string.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the build string.
        /// </summary>
        public string Build { get; set; }

        /// <summary>
        /// Parses the tokens of the chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        internal override void ParseTokens(string[] tokens)
        {
            var chunks = Tokenizer.SplitChunks(tokens);

            foreach (var chunk in chunks)
            {
                int index = 0;

                switch (chunk.Ident)
                {
                    case "version":
                        this.Version = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "build":
                        this.Build = Tokenizer.ReadString(chunk.Tokens, ref index);
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

            Tokenizer.BuildOpenChunk(tokens, "header");
            Tokenizer.BuildIndent(tokens);

            if (!string.IsNullOrEmpty(this.Version))
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "version");
                Tokenizer.BuildString(tokens, this.Version);
                Tokenizer.BuildCloseChunk(tokens);
            }

            if (!string.IsNullOrEmpty(this.Build))
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "build");
                Tokenizer.BuildString(tokens, this.Build);
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
