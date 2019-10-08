// <copyright file="An8Material.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A material.
    /// </summary>
    public sealed class An8Material : An8Chunk
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the front surface.
        /// </summary>
        public An8Surface FrontSurface { get; set; }

        /// <summary>
        /// Gets or sets the back surface.
        /// </summary>
        public An8Surface BackSurface { get; set; }

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
                    case null:
                        this.Name = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "surface":
                        {
                            var surface = new An8Surface();
                            surface.ParseTokens(chunk.Tokens);
                            this.FrontSurface = surface;
                            break;
                        }

                    case "backsurface":
                        {
                            var surface = new An8Surface();
                            surface.ParseTokens(chunk.Tokens);
                            this.BackSurface = surface;
                            break;
                        }
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

            Tokenizer.BuildOpenChunk(tokens, "material");
            Tokenizer.BuildString(tokens, this.Name);
            Tokenizer.BuildIndent(tokens);

            if (this.FrontSurface != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.FrontSurface.BuildTokens());
            }

            if (this.BackSurface != null)
            {
                var surfaceTokens = this.BackSurface.BuildTokens();
                surfaceTokens[0] = "back" + surfaceTokens[0];

                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(surfaceTokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
