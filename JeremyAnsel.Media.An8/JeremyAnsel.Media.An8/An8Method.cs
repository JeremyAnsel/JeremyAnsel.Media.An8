// <copyright file="An8Method.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A method.
    /// </summary>
    public sealed class An8Method : An8Chunk
    {
        /// <summary>
        /// The kind of the method.
        /// </summary>
        private string? kind;

        /// <summary>
        /// Initializes a new instance of the <see cref="An8Method"/> class.
        /// </summary>
        public An8Method()
        {
            this.Kind = "modifier";
            this.Parameters = new SortedDictionary<string, float>();
        }

        /// <summary>
        /// Gets or sets the kind of the method.
        /// </summary>
        public string? Kind
        {
            get
            {
                return this.kind;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                this.kind = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        public IDictionary<string, float> Parameters { get; private set; }

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
                        this.Kind = chunk.Tokens[index++];
                        this.Name = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "parameter":
                        {
                            string name = Tokenizer.ReadString(chunk.Tokens, ref index);
                            float value = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                            this.Parameters[name] = value;
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

            Tokenizer.BuildOpenChunk(tokens, "method");
            tokens.Add(this.Kind ?? string.Empty);
            Tokenizer.BuildString(tokens, this.Name);
            Tokenizer.BuildIndent(tokens);

            foreach (var parameter in this.Parameters)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "parameter");
                Tokenizer.BuildString(tokens, parameter.Key);
                Tokenizer.BuildFloat(tokens, parameter.Value);
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
