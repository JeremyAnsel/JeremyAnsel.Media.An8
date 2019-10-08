// <copyright file="An8DegreeOfFreedom.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A degree of freedom.
    /// </summary>
    public sealed class An8DegreeOfFreedom : An8Chunk
    {
        /// <summary>
        /// Gets or sets the axis.
        /// </summary>
        public string Axis { get; set; }

        /// <summary>
        /// Gets or sets the minimum angle.
        /// </summary>
        public float MinimumAngle { get; set; }

        /// <summary>
        /// Gets or sets the default angle.
        /// </summary>
        public float DefaultAngle { get; set; }

        /// <summary>
        /// Gets or sets the maximum angle.
        /// </summary>
        public float MaximumAngle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the rotation is locked.
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the rotation is unlimited.
        /// </summary>
        public bool IsUnlimited { get; set; }

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
                        this.Axis = Tokenizer.ReadString(chunk.Tokens, ref index);
                        this.MinimumAngle = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        this.DefaultAngle = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        this.MaximumAngle = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "locked":
                        this.IsLocked = true;
                        break;

                    case "unlimited":
                        this.IsUnlimited = true;
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

            Tokenizer.BuildOpenChunk(tokens, "dof");

            Tokenizer.BuildString(tokens, this.Axis);
            Tokenizer.BuildFloat(tokens, this.MinimumAngle);
            Tokenizer.BuildFloat(tokens, this.DefaultAngle);
            Tokenizer.BuildFloat(tokens, this.MaximumAngle);

            if (this.IsLocked)
            {
                Tokenizer.BuildOpenChunk(tokens, "locked");
                Tokenizer.BuildCloseChunk(tokens);
            }

            if (this.IsUnlimited)
            {
                Tokenizer.BuildOpenChunk(tokens, "unlimited");
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
