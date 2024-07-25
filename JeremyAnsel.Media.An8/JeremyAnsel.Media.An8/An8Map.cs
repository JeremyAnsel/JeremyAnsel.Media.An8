// <copyright file="An8Map.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A map.
    /// </summary>
    public sealed class An8Map : An8Chunk
    {
        /// <summary>
        /// Gets or sets the kind of the map.
        /// </summary>
        public string? Kind { get; set; }

        /// <summary>
        /// Gets or sets the texture name.
        /// </summary>
        public string? TextureName { get; set; }

        /// <summary>
        /// Gets or sets the texture parameters.
        /// </summary>
        public An8TextureParams? TextureParams { get; set; }

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
                    case "kind":
                        this.Kind = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "texturename":
                        this.TextureName = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "textureparams":
                        {
                            var textureParams = new An8TextureParams();
                            textureParams.ParseTokens(chunk.Tokens);
                            this.TextureParams = textureParams;
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

            Tokenizer.BuildOpenChunk(tokens, "map");
            Tokenizer.BuildIndent(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "kind");
            Tokenizer.BuildString(tokens, this.Kind);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "texturename");
            Tokenizer.BuildString(tokens, this.TextureName);
            Tokenizer.BuildCloseChunk(tokens);

            if (this.TextureParams != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.TextureParams.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
