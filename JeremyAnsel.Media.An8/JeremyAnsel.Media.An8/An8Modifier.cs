// <copyright file="An8Modifier.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A modifier.
    /// </summary>
    public sealed class An8Modifier : An8Component
    {
        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public float Length { get; set; }

        /// <summary>
        /// Gets or sets the diameter.
        /// </summary>
        public float Diameter { get; set; }

        /// <summary>
        /// Gets or sets the segments count.
        /// </summary>
        public int SegmentsCount { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        public An8Method Method { get; set; }

        /// <summary>
        /// Gets or sets the component.
        /// </summary>
        public An8Component Component { get; set; }

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
                    case "length":
                        this.Length = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "diameter":
                        this.Diameter = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "segments":
                        this.SegmentsCount = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        break;

                    case "method":
                        {
                            var method = new An8Method();
                            method.ParseTokens(chunk.Tokens);
                            this.Method = method;
                            break;
                        }
                }
            }

            this.Component = An8Component.ParseComponents(tokens).LastOrDefault();
        }

        /// <summary>
        /// Builds the tokens of the chunk.
        /// </summary>
        /// <returns>The tokens.</returns>
        internal override string[] BuildTokens()
        {
            var tokens = new List<string>();

            Tokenizer.BuildOpenChunk(tokens, "modifier");
            Tokenizer.BuildIndent(tokens);

            tokens.AddRange(base.BuildTokens());

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "length");
            Tokenizer.BuildFloat(tokens, this.Length);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "diameter");
            Tokenizer.BuildFloat(tokens, this.Diameter);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "segments");
            Tokenizer.BuildInt(tokens, this.SegmentsCount);
            Tokenizer.BuildCloseChunk(tokens);

            if (this.Method != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.Method.BuildTokens());
            }

            if (this.Component != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.Component.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
