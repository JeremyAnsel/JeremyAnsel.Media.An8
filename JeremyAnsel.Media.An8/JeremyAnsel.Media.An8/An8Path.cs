// <copyright file="An8Path.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A path.
    /// </summary>
    public sealed class An8Path : An8Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8Path"/> class.
        /// </summary>
        public An8Path()
        {
            this.Splines = new List<An8Bezier>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the path is extendable.
        /// </summary>
        public bool IsExtendable { get; set; }

        /// <summary>
        /// Gets the Bezier splines.
        /// </summary>
        public IList<An8Bezier> Splines { get; private set; }

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
                switch (chunk.Ident)
                {
                    case "extendable":
                        this.IsExtendable = true;
                        break;

                    case "bezier":
                        {
                            var spline = new An8Bezier();
                            spline.ParseTokens(chunk.Tokens);
                            this.Splines.Add(spline);
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

            Tokenizer.BuildOpenChunk(tokens, "path");
            Tokenizer.BuildIndent(tokens);

            tokens.AddRange(base.BuildTokens());

            if (this.IsExtendable)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "extendable");
                Tokenizer.BuildCloseChunk(tokens);
            }

            foreach (var spline in this.Splines)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(spline.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
