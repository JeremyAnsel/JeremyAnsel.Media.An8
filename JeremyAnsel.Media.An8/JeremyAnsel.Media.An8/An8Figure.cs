// <copyright file="An8Figure.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A figure.
    /// </summary>
    public sealed class An8Figure : An8Chunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8Figure"/> class.
        /// </summary>
        public An8Figure()
        {
            this.Materials = new List<An8Material>();
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
        /// Gets or sets the root bone.
        /// </summary>
        public An8Bone RootBone { get; set; }

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

                    case "bone":
                        {
                            var bone = new An8Bone();
                            bone.ParseTokens(chunk.Tokens);
                            this.RootBone = bone;
                            break;
                        }
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

            Tokenizer.BuildOpenChunk(tokens, "figure");
            Tokenizer.BuildString(tokens, this.Name);
            Tokenizer.BuildIndent(tokens);

            foreach (var material in this.Materials)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(material.BuildTokens());
            }

            if (this.RootBone != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.RootBone.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
