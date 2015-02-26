// <copyright file="An8Influence.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// An influence.
    /// </summary>
    public sealed class An8Influence : An8Chunk
    {
        /// <summary>
        /// Gets or sets the center of the lower end.
        /// </summary>
        public float LowerEndCenter { get; set; }

        /// <summary>
        /// Gets or sets the inner radius of the lower end.
        /// </summary>
        public float LowerEndInnerRadius { get; set; }

        /// <summary>
        /// Gets or sets the outer radius of the lower end.
        /// </summary>
        public float LowerEndOuterRadius { get; set; }

        /// <summary>
        /// Gets or sets the center of the upper end.
        /// </summary>
        public float UpperEndCenter { get; set; }

        /// <summary>
        /// Gets or sets the inner radius of the upper end.
        /// </summary>
        public float UpperEndInnerRadius { get; set; }

        /// <summary>
        /// Gets or sets the outer radius of the upper end.
        /// </summary>
        public float UpperEndOuterRadius { get; set; }

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
                        this.LowerEndCenter = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        this.LowerEndInnerRadius = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        this.LowerEndOuterRadius = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        this.UpperEndCenter = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        this.UpperEndInnerRadius = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        this.UpperEndOuterRadius = Tokenizer.ReadInt(chunk.Tokens, ref index);
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

            Tokenizer.BuildOpenChunk(tokens, "influence");

            Tokenizer.BuildFloat(tokens, this.LowerEndCenter);
            Tokenizer.BuildFloat(tokens, this.LowerEndInnerRadius);
            Tokenizer.BuildFloat(tokens, this.LowerEndOuterRadius);
            Tokenizer.BuildFloat(tokens, this.UpperEndCenter);
            Tokenizer.BuildFloat(tokens, this.UpperEndInnerRadius);
            Tokenizer.BuildFloat(tokens, this.UpperEndOuterRadius);

            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
