// <copyright file="An8Cube.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A cube.
    /// </summary>
    public sealed class An8Cube : An8Component
    {
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public An8Material? Material { get; set; }

        /// <summary>
        /// Gets or sets the x dimension.
        /// </summary>
        public float ScaleX { get; set; }

        /// <summary>
        /// Gets or sets the y dimension.
        /// </summary>
        public float ScaleY { get; set; }

        /// <summary>
        /// Gets or sets the z dimension.
        /// </summary>
        public float ScaleZ { get; set; }

        /// <summary>
        /// Gets or sets the number of divisions along the x axis.
        /// </summary>
        public int DivisionsX { get; set; }

        /// <summary>
        /// Gets or sets the number of divisions along the y axis.
        /// </summary>
        public int DivisionsY { get; set; }

        /// <summary>
        /// Gets or sets the number of divisions along the z axis.
        /// </summary>
        public int DivisionsZ { get; set; }

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
                    case "material":
                        {
                            var material = new An8Material();
                            material.ParseTokens(chunk.Tokens);
                            this.Material = material;
                            break;
                        }

                    case "scale":
                        this.ScaleX = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        this.ScaleY = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        this.ScaleZ = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "divisions":
                        this.DivisionsX = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        this.DivisionsY = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        this.DivisionsZ = Tokenizer.ReadInt(chunk.Tokens, ref index);
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

            Tokenizer.BuildOpenChunk(tokens, "cube");
            Tokenizer.BuildIndent(tokens);

            tokens.AddRange(base.BuildTokens());

            if (this.Material != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.Material.BuildTokens());
            }

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "scale");
            Tokenizer.BuildFloat(tokens, this.ScaleX);
            Tokenizer.BuildFloat(tokens, this.ScaleY);
            Tokenizer.BuildFloat(tokens, this.ScaleZ);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "divisions");
            Tokenizer.BuildInt(tokens, this.DivisionsX);
            Tokenizer.BuildInt(tokens, this.DivisionsY);
            Tokenizer.BuildInt(tokens, this.DivisionsZ);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
