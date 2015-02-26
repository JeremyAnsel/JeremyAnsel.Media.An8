// <copyright file="An8Quaternion.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A quaternion.
    /// </summary>
    public sealed class An8Quaternion
    {
        /// <summary>
        /// Gets or sets the x component.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Justification = "Reviewed")]
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the y component.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Justification = "Reviewed")]
        public float Y { get; set; }

        /// <summary>
        /// Gets or sets the z component.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Z", Justification = "Reviewed")]
        public float Z { get; set; }

        /// <summary>
        /// Gets or sets the w component.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "W", Justification = "Reviewed")]
        public float W { get; set; }

        /// <summary>
        /// Reads tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        /// <returns>A quaternion.</returns>
        internal static An8Quaternion ReadTokens(string[] tokens, ref int index)
        {
            var quaternion = new An8Quaternion();

            Tokenizer.ReadOpenData(tokens, ref index);

            quaternion.X = Tokenizer.ReadFloat(tokens, ref index);
            quaternion.Y = Tokenizer.ReadFloat(tokens, ref index);
            quaternion.Z = Tokenizer.ReadFloat(tokens, ref index);
            quaternion.W = Tokenizer.ReadFloat(tokens, ref index);

            Tokenizer.ReadCloseData(tokens, ref index);

            return quaternion;
        }

        /// <summary>
        /// Builds tokens.
        /// </summary>
        /// <returns>The tokens.</returns>
        internal string[] BuildTokens()
        {
            var tokens = new List<string>();

            Tokenizer.BuildOpenData(tokens);

            Tokenizer.BuildFloat(tokens, this.X);
            Tokenizer.BuildFloat(tokens, this.Y);
            Tokenizer.BuildFloat(tokens, this.Z);
            Tokenizer.BuildFloat(tokens, this.W);

            Tokenizer.BuildCloseData(tokens);

            return tokens.ToArray();
        }
    }
}
