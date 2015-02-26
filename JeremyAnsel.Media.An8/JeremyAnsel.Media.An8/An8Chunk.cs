// <copyright file="An8Chunk.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    /// <summary>
    /// A chunk.
    /// </summary>
    public abstract class An8Chunk
    {
        /// <summary>
        /// Parses a chunk content.
        /// </summary>
        /// <param name="text">The chunk content.</param>
        public void Parse(string text)
        {
            this.ParseTokens(Tokenizer.Tokenize(text));
        }

        /// <summary>
        /// Generates the chunk content.
        /// </summary>
        /// <returns>The chunk content.</returns>
        public string GenerateText()
        {
            return Tokenizer.Build(this.BuildTokens());
        }

        /// <summary>
        /// Parses the tokens of the chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        internal abstract void ParseTokens(string[] tokens);

        /// <summary>
        /// Builds the tokens of the chunk.
        /// </summary>
        /// <returns>The tokens.</returns>
        internal abstract string[] BuildTokens();
    }
}
