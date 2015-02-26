// <copyright file="An8ChunkTokens.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    /// <summary>
    /// A chunk's identifier and tokens.
    /// </summary>
    internal sealed class An8ChunkTokens
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Ident { get; set; }

        /// <summary>
        /// Gets or sets the tokens.
        /// </summary>
        public string[] Tokens { get; set; }
    }
}
