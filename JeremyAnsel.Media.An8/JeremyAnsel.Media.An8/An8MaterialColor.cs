// <copyright file="An8MaterialColor.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A material color.
    /// </summary>
    public sealed class An8MaterialColor : An8Chunk
    {
        /// <summary>
        /// Gets or sets the red component.
        /// </summary>
        public byte Red { get; set; }

        /// <summary>
        /// Gets or sets the green component.
        /// </summary>
        public byte Green { get; set; }

        /// <summary>
        /// Gets or sets the blue component.
        /// </summary>
        public byte Blue { get; set; }

        /// <summary>
        /// Gets or sets the weighting factor.
        /// </summary>
        public float WeightingFactor { get; set; }

        /// <summary>
        /// Gets or sets the name of the texture.
        /// </summary>
        public string TextureName { get; set; }

        /// <summary>
        /// Gets or sets the texture parameters.
        /// </summary>
        public An8TextureParams TextureParams { get; set; }

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
                    case "rgb":
                        this.Red = (byte)Tokenizer.ReadInt(chunk.Tokens, ref index);
                        this.Green = (byte)Tokenizer.ReadInt(chunk.Tokens, ref index);
                        this.Blue = (byte)Tokenizer.ReadInt(chunk.Tokens, ref index);
                        break;

                    case "factor":
                        this.WeightingFactor = Tokenizer.ReadFloat(chunk.Tokens, ref index);
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

            Tokenizer.BuildOpenChunk(tokens, "ambiant");
            Tokenizer.BuildIndent(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "rgb");
            Tokenizer.BuildInt(tokens, this.Red);
            Tokenizer.BuildInt(tokens, this.Green);
            Tokenizer.BuildInt(tokens, this.Blue);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "factor");
            Tokenizer.BuildFloat(tokens, this.WeightingFactor);
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
