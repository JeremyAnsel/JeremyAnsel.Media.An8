// <copyright file="An8Sphere.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A sphere.
    /// </summary>
    public sealed class An8Sphere : An8Component
    {
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public An8Material? Material { get; set; }

        /// <summary>
        /// Gets or sets the number of divisions.
        /// </summary>
        public An8LongLat? LongLatDivisions { get; set; }

        /// <summary>
        /// Gets or sets the geodesic value.
        /// </summary>
        public int? Geodesic { get; set; }

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

                    case "longlat":
                        this.Geodesic = null;
                        this.LongLatDivisions = An8LongLat.ReadTokens(chunk.Tokens, ref index);
                        break;

                    case "geodesic":
                        this.LongLatDivisions = null;
                        this.Geodesic = Tokenizer.ReadInt(chunk.Tokens, ref index);
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

            Tokenizer.BuildOpenChunk(tokens, "sphere");
            Tokenizer.BuildIndent(tokens);

            tokens.AddRange(base.BuildTokens());

            if (this.Material != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.Material.BuildTokens());
            }

            if (this.LongLatDivisions != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.LongLatDivisions.BuildTokens());
            }

            if (this.Geodesic.HasValue)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "geodesic");
                Tokenizer.BuildInt(tokens, this.Geodesic.Value);
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
