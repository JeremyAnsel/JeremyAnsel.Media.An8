// <copyright file="An8Group.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A group.
    /// </summary>
    public sealed class An8Group : An8Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8Group"/> class.
        /// </summary>
        public An8Group()
        {
            this.Components = new List<An8Component>();
        }

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
            base.ParseTokens(tokens);

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

            Tokenizer.BuildOpenChunk(tokens, "group");
            Tokenizer.BuildIndent(tokens);

            tokens.AddRange(base.BuildTokens());

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
