// <copyright file="An8Object.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// An object.
    /// </summary>
    public sealed class An8Object : An8Chunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8Object"/> class.
        /// </summary>
        public An8Object()
        {
            this.Materials = new List<An8Material>();
            this.Components = new List<An8Component>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the materials.
        /// </summary>
        public IList<An8Material> Materials { get; private set; }

        /// <summary>
        /// Gets the components.
        /// </summary>
        public IList<An8Component> Components { get; private set; }

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
                        this.Name = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "material":
                        {
                            var material = new An8Material();
                            material.ParseTokens(chunk.Tokens);
                            this.Materials.Add(material);
                            break;
                        }
                }
            }

            foreach (var component in An8Component.ParseComponents(tokens))
            {
                this.Components.Add(component);
            }
        }

        /// <summary>
        /// Builds the tokens of the chunk.
        /// </summary>
        /// <returns>The tokens.</returns>
        internal override string[] BuildTokens()
        {
            var tokens = new List<string>();

            Tokenizer.BuildOpenChunk(tokens, "object");
            Tokenizer.BuildString(tokens, this.Name);
            Tokenizer.BuildIndent(tokens);

            foreach (var material in this.Materials)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(material.BuildTokens());
            }

            foreach (var component in this.Components)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(component.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
