// <copyright file="An8TextureParams.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A texture parameter.
    /// </summary>
    public sealed class An8TextureParams : An8Chunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8TextureParams"/> class.
        /// </summary>
        public An8TextureParams()
        {
            this.Percent = 100;
        }

        /// <summary>
        /// Gets or sets the blend mode.
        /// </summary>
        public An8BlendMode BlendMode { get; set; }

        /// <summary>
        /// Gets or sets the alpha mode.
        /// </summary>
        public An8AlphaMode AlphaMode { get; set; }

        /// <summary>
        /// Gets or sets the blend percent.
        /// </summary>
        public int Percent { get; set; }

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
                    case "blendmode":
                        {
                            string mode = chunk.Tokens[index++];

                            switch (mode)
                            {
                                case "decal":
                                default:
                                    this.BlendMode = An8BlendMode.Decal;
                                    break;

                                case "darken":
                                    this.BlendMode = An8BlendMode.Darken;
                                    break;

                                case "lighten":
                                    this.BlendMode = An8BlendMode.Lighten;
                                    break;
                            }

                            break;
                        }

                    case "alphamode":
                        {
                            string mode = chunk.Tokens[index++];

                            switch (mode)
                            {
                                case "none":
                                default:
                                    this.AlphaMode = An8AlphaMode.None;
                                    break;

                                case "layer":
                                    this.AlphaMode = An8AlphaMode.Layer;
                                    break;

                                case "final":
                                    this.AlphaMode = An8AlphaMode.Final;
                                    break;
                            }

                            break;
                        }

                    case "percent":
                        this.Percent = Tokenizer.ReadInt(chunk.Tokens, ref index);
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

            Tokenizer.BuildOpenChunk(tokens, "textureparams");
            Tokenizer.BuildIndent(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "blendmode");

            switch (this.BlendMode)
            {
                case An8BlendMode.Decal:
                default:
                    tokens.Add("decal");
                    break;

                case An8BlendMode.Darken:
                    tokens.Add("darken");
                    break;

                case An8BlendMode.Lighten:
                    tokens.Add("lighten");
                    break;
            }

            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "alphamode");

            switch (this.AlphaMode)
            {
                case An8AlphaMode.None:
                default:
                    tokens.Add("none");
                    break;

                case An8AlphaMode.Layer:
                    tokens.Add("layer");
                    break;

                case An8AlphaMode.Final:
                    tokens.Add("final");
                    break;
            }

            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "percent");
            Tokenizer.BuildInt(tokens, this.Percent);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
