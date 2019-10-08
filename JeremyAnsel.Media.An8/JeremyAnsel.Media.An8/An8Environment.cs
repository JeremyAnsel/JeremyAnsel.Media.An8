// <copyright file="An8Environment.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// An environment.
    /// </summary>
    public sealed class An8Environment : An8Chunk
    {
        /// <summary>
        /// Gets or sets a value indicating whether auto grid is enabled.
        /// </summary>
        public bool IsAutoGridEnabled { get; set; }

        /// <summary>
        /// Gets or sets the spacing of the modeling grid.
        /// </summary>
        public float ModelingGridSpacing { get; set; }

        /// <summary>
        /// Gets or sets the spacing of the scene editor grid.
        /// </summary>
        public float SceneEditorGridSpacing { get; set; }

        /// <summary>
        /// Gets or sets the size of the ground floor grid.
        /// </summary>
        public float GroundFloorGridSize { get; set; }

        /// <summary>
        /// Gets or sets the framerate.
        /// </summary>
        public int Framerate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the playback framerate is limited.
        /// </summary>
        public bool IsPlaybackFramerateLimited { get; set; }

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
                    case "grid":
                        this.IsAutoGridEnabled = Tokenizer.ReadInt(chunk.Tokens, ref index) != 0;

                        if (!this.IsAutoGridEnabled || chunk.Tokens.Length >= 4)
                        {
                            this.ModelingGridSpacing = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                            this.SceneEditorGridSpacing = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                            this.GroundFloorGridSize = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        }

                        break;

                    case "framerate":
                        this.Framerate = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        break;

                    case "limitplayback":
                        this.IsPlaybackFramerateLimited = true;
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

            Tokenizer.BuildOpenChunk(tokens, "environment");
            Tokenizer.BuildIndent(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "grid");
            Tokenizer.BuildInt(tokens, this.IsAutoGridEnabled ? 1 : 0);
            Tokenizer.BuildFloat(tokens, this.ModelingGridSpacing);
            Tokenizer.BuildFloat(tokens, this.SceneEditorGridSpacing);
            Tokenizer.BuildFloat(tokens, this.GroundFloorGridSize);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "framerate");
            Tokenizer.BuildInt(tokens, this.Framerate);
            Tokenizer.BuildCloseChunk(tokens);

            if (this.IsPlaybackFramerateLimited)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "limitplayback");
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
