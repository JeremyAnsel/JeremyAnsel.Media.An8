// <copyright file="An8NamedObject.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A named object.
    /// </summary>
    public sealed class An8NamedObject : An8Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8NamedObject"/> class.
        /// </summary>
        public An8NamedObject()
        {
            this.WeightedBy = new List<string>();
        }

        /// <summary>
        /// Gets or sets the object name.
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public An8Material Material { get; set; }

        /// <summary>
        /// Gets the weighted by.
        /// </summary>
        public IList<string> WeightedBy { get; private set; }

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
                    case null:
                        this.ObjectName = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "material":
                        {
                            var material = new An8Material();
                            material.ParseTokens(chunk.Tokens);
                            this.Material = material;
                            break;
                        }

                    case "weightedby":
                        this.WeightedBy.Add(Tokenizer.ReadString(chunk.Tokens, ref index));
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

            Tokenizer.BuildOpenChunk(tokens, "namedobject");
            Tokenizer.BuildIndent(tokens);

            tokens.AddRange(base.BuildTokens());

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildString(tokens, this.ObjectName);

            if (this.Material != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.Material.BuildTokens());
            }

            foreach (var weighted in this.WeightedBy)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "weightedby");
                Tokenizer.BuildString(tokens, weighted);
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
