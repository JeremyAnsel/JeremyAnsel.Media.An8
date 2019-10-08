// <copyright file="An8Bezier.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A Bezier spline.
    /// </summary>
    public sealed class An8Bezier : An8Chunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8Bezier"/> class.
        /// </summary>
        public An8Bezier()
        {
            this.Knots = new List<An8Knot>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the spline is closed.
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Gets the knots.
        /// </summary>
        public IList<An8Knot> Knots { get; private set; }

        /// <summary>
        /// Parses the tokens of the chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        internal override void ParseTokens(string[] tokens)
        {
            var chunks = Tokenizer.SplitChunks(tokens);

            foreach (var chunk in chunks)
            {
                switch (chunk.Ident)
                {
                    case "closed":
                        this.IsClosed = true;
                        break;

                    case "knot":
                        {
                            var knot = new An8Knot();
                            knot.ParseTokens(chunk.Tokens);
                            this.Knots.Add(knot);
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

            Tokenizer.BuildOpenChunk(tokens, "bezier");
            Tokenizer.BuildIndent(tokens);

            if (this.IsClosed)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "closed");
                Tokenizer.BuildCloseChunk(tokens);
            }

            foreach (var knot in this.Knots)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(knot.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
