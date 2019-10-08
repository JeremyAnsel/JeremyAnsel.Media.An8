// <copyright file="An8TexCoord.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A texture coordinate.
    /// </summary>
    public sealed class An8TexCoord
    {
        /// <summary>
        /// Gets or sets the u component.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "U", Justification = "Reviewed")]
        public float U { get; set; }

        /// <summary>
        /// Gets or sets the v component.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "V", Justification = "Reviewed")]
        public float V { get; set; }

        /// <summary>
        /// Reads tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        /// <returns>A texture coordinate.</returns>
        internal static An8TexCoord ReadTokens(string[] tokens, ref int index)
        {
            var coord = new An8TexCoord();

            Tokenizer.ReadOpenData(tokens, ref index);

            coord.U = Tokenizer.ReadFloat(tokens, ref index);
            coord.V = Tokenizer.ReadFloat(tokens, ref index);

            Tokenizer.ReadCloseData(tokens, ref index);

            return coord;
        }

        /// <summary>
        /// Builds tokens.
        /// </summary>
        /// <returns>The tokens.</returns>
        internal string[] BuildTokens()
        {
            var tokens = new List<string>();

            Tokenizer.BuildOpenData(tokens);

            Tokenizer.BuildFloat(tokens, this.U);
            Tokenizer.BuildFloat(tokens, this.V);

            Tokenizer.BuildCloseData(tokens);

            return tokens.ToArray();
        }
    }
}
