// <copyright file="An8LongLat.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A value in vertical and horizontal directions.
    /// </summary>
    public sealed class An8LongLat
    {
        /// <summary>
        /// Gets or sets the value in vertical direction.
        /// </summary>
        public int VerticalDivisions { get; set; }

        /// <summary>
        /// Gets or sets the value in horizontal direction.
        /// </summary>
        public int HorizontalDivisions { get; set; }

        /// <summary>
        /// Reads tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="An8LongLat"/> value.</returns>
        internal static An8LongLat ReadTokens(string[] tokens, ref int index)
        {
            var longlat = new An8LongLat();

            Tokenizer.ReadOpenData(tokens, ref index);

            longlat.VerticalDivisions = Tokenizer.ReadInt(tokens, ref index);
            longlat.HorizontalDivisions = Tokenizer.ReadInt(tokens, ref index);

            Tokenizer.ReadCloseData(tokens, ref index);

            return longlat;
        }

        /// <summary>
        /// Builds tokens.
        /// </summary>
        /// <returns>The tokens.</returns>
        internal string[] BuildTokens()
        {
            var tokens = new List<string>();

            Tokenizer.BuildOpenChunk(tokens, "longlat");

            Tokenizer.BuildInt(tokens, this.VerticalDivisions);
            Tokenizer.BuildInt(tokens, this.HorizontalDivisions);

            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
