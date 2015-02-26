// <copyright file="An8Texture.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A texture.
    /// </summary>
    public sealed class An8Texture : An8Chunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8Texture"/> class.
        /// </summary>
        public An8Texture()
        {
            this.Files = new List<string>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the images are inverted.
        /// </summary>
        public bool IsInverted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the texture is a cube map.
        /// </summary>
        public bool IsCubeMap { get; set; }

        /// <summary>
        /// Gets the texture filenames.
        /// </summary>
        public IList<string> Files { get; private set; }

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

                    case "invert":
                        this.IsInverted = true;
                        break;

                    case "cubemap":
                        this.IsCubeMap = true;
                        break;

                    case "file":
                        this.Files.Add(Tokenizer.ReadString(chunk.Tokens, ref index));
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

            Tokenizer.BuildOpenChunk(tokens, "texture");
            Tokenizer.BuildString(tokens, this.Name);
            Tokenizer.BuildIndent(tokens);

            if (this.IsInverted)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "invert");
                Tokenizer.BuildCloseChunk(tokens);
            }

            if (this.IsCubeMap)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "cubemap");
                Tokenizer.BuildCloseChunk(tokens);
            }

            foreach (var file in this.Files)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "file");
                Tokenizer.BuildString(tokens, file);
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
