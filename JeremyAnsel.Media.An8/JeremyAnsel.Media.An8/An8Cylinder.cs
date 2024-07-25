// <copyright file="An8Cylinder.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A cylinder.
    /// </summary>
    public sealed class An8Cylinder : An8Component
    {
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public An8Material? Material { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public float Length { get; set; }

        /// <summary>
        /// Gets or sets the diameter.
        /// </summary>
        public float Diameter { get; set; }

        /// <summary>
        /// Gets or sets the top diameter.
        /// </summary>
        public float TopDiameter { get; set; }

        /// <summary>
        /// Gets or sets the number of divisions.
        /// </summary>
        public An8LongLat? LongLatDivisions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the start is capped.
        /// </summary>
        public bool IsStartCapped { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the end is capped.
        /// </summary>
        public bool IsEndCapped { get; set; }

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

                    case "length":
                        this.Length = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "diameter":
                        this.Diameter = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "topdiameter":
                        this.TopDiameter = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "longlat":
                        this.LongLatDivisions = An8LongLat.ReadTokens(chunk.Tokens, ref index);
                        break;

                    case "capstart":
                        this.IsStartCapped = true;
                        break;

                    case "capend":
                        this.IsEndCapped = true;
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

            Tokenizer.BuildOpenChunk(tokens, "cylinder");
            Tokenizer.BuildIndent(tokens);

            tokens.AddRange(base.BuildTokens());

            if (this.Material != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.Material.BuildTokens());
            }

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "length");
            Tokenizer.BuildFloat(tokens, this.Length);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "diameter");
            Tokenizer.BuildFloat(tokens, this.Diameter);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "topdiameter");
            Tokenizer.BuildFloat(tokens, this.TopDiameter);
            Tokenizer.BuildCloseChunk(tokens);

            if (this.LongLatDivisions != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.LongLatDivisions.BuildTokens());
            }

            if (this.IsStartCapped)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "capstart");
                Tokenizer.BuildCloseChunk(tokens);
            }

            if (this.IsEndCapped)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "capend");
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
