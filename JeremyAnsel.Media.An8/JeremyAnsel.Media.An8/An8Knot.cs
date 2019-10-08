// <copyright file="An8Knot.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A knot.
    /// </summary>
    public sealed class An8Knot : An8Chunk
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public An8Point Location { get; set; }

        /// <summary>
        /// Gets or sets the forward direction.
        /// </summary>
        public An8Point ForwardDirection { get; set; }

        /// <summary>
        /// Gets or sets the reverse direction.
        /// </summary>
        public An8Point ReverseDirection { get; set; }

        /// <summary>
        /// Gets or sets the segments count.
        /// </summary>
        public int? SegmentsCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the knot is a corner.
        /// </summary>
        public bool IsCorner { get; set; }

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
                        this.Location = An8Point.ReadTokens(chunk.Tokens, ref index);
                        this.ForwardDirection = An8Point.ReadTokens(chunk.Tokens, ref index);
                        this.ReverseDirection = An8Point.ReadTokens(chunk.Tokens, ref index);

                        if (index < chunk.Tokens.Length)
                        {
                            this.SegmentsCount = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        }

                        break;

                    case "corner":
                        this.IsCorner = true;
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

            Tokenizer.BuildOpenChunk(tokens, "knot");
            Tokenizer.BuildIndent(tokens);

            Tokenizer.BuildNewLine(tokens);
            tokens.AddRange((this.Location ?? new An8Point()).BuildTokens());
            tokens.AddRange((this.ForwardDirection ?? new An8Point()).BuildTokens());
            tokens.AddRange((this.ReverseDirection ?? new An8Point()).BuildTokens());

            if (this.SegmentsCount.HasValue)
            {
                Tokenizer.BuildInt(tokens, this.SegmentsCount.Value);
            }

            if (this.IsCorner)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "corner");
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
