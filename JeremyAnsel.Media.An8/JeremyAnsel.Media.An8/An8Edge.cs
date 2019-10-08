// <copyright file="An8Edge.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// An edge.
    /// </summary>
    public sealed class An8Edge
    {
        /// <summary>
        /// Gets or sets the index of the first point.
        /// </summary>
        public int Index1 { get; set; }

        /// <summary>
        /// Gets or sets the index of the second point.
        /// </summary>
        public int Index2 { get; set; }

        /// <summary>
        /// Gets or sets the sharpness.
        /// </summary>
        public int? Sharpness { get; set; }

        /// <summary>
        /// Reads tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The value.</param>
        /// <returns>An edge.</returns>
        internal static An8Edge ReadTokens(string[] tokens, ref int index)
        {
            var edge = new An8Edge();

            Tokenizer.ReadOpenData(tokens, ref index);

            edge.Index1 = Tokenizer.ReadInt(tokens, ref index);
            edge.Index2 = Tokenizer.ReadInt(tokens, ref index);

            if (!Tokenizer.IsClosedData(tokens, ref index))
            {
                edge.Sharpness = Tokenizer.ReadInt(tokens, ref index);

                Tokenizer.ReadCloseData(tokens, ref index);
            }

            return edge;
        }

        /// <summary>
        /// Builds tokens.
        /// </summary>
        /// <returns>The tokens.</returns>
        internal string[] BuildTokens()
        {
            var tokens = new List<string>();

            Tokenizer.BuildOpenData(tokens);

            Tokenizer.BuildInt(tokens, this.Index1);
            Tokenizer.BuildInt(tokens, this.Index2);

            if (this.Sharpness.HasValue)
            {
                Tokenizer.BuildInt(tokens, this.Sharpness.Value);
            }

            Tokenizer.BuildCloseData(tokens);

            return tokens.ToArray();
        }
    }
}
