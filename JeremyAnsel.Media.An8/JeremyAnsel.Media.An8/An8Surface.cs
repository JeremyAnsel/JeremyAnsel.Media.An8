// <copyright file="An8Surface.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A surface.
    /// </summary>
    public sealed class An8Surface : An8Chunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8Surface"/> class.
        /// </summary>
        public An8Surface()
        {
            this.Maps = new List<An8Map>();
        }

        /// <summary>
        /// Gets or sets the $ambiant$ material color.
        /// </summary>
        public An8MaterialColor Ambiant { get; set; }

        /// <summary>
        /// Gets or sets the diffuse material color.
        /// </summary>
        public An8MaterialColor Diffuse { get; set; }

        /// <summary>
        /// Gets or sets the specular material color.
        /// </summary>
        public An8MaterialColor Specular { get; set; }

        /// <summary>
        /// Gets or sets the emissive material color.
        /// </summary>
        public An8MaterialColor Emissive { get; set; }

        /// <summary>
        /// Gets or sets the alpha transparency.
        /// </summary>
        public int Alpha { get; set; }

        /// <summary>
        /// Gets or sets the brilliance factor.
        /// </summary>
        public float Brilliance { get; set; }

        /// <summary>
        /// Gets or sets the $phong$ roughness factor.
        /// </summary>
        public float PhongRoughness { get; set; }

        /// <summary>
        /// Gets the maps.
        /// </summary>
        public IList<An8Map> Maps { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether diffuse is used for both diffuse and $ambiant$.
        /// </summary>
        public bool IsAmbiantDiffuseLocked { get; set; }

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
                    case "ambiant":
                        {
                            var color = new An8MaterialColor();
                            color.ParseTokens(chunk.Tokens);
                            this.Ambiant = color;
                            break;
                        }

                    case "diffuse":
                        {
                            var color = new An8MaterialColor();
                            color.ParseTokens(chunk.Tokens);
                            this.Diffuse = color;
                            break;
                        }

                    case "specular":
                        {
                            var color = new An8MaterialColor();
                            color.ParseTokens(chunk.Tokens);
                            this.Specular = color;
                            break;
                        }

                    case "emissive":
                        {
                            var color = new An8MaterialColor();
                            color.ParseTokens(chunk.Tokens);
                            this.Emissive = color;
                            break;
                        }

                    case "alpha":
                        this.Alpha = Tokenizer.ReadInt(chunk.Tokens, ref index);
                        break;

                    case "brilliance":
                        this.Brilliance = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "phongsize":
                        this.PhongRoughness = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "map":
                        {
                            var map = new An8Map();
                            map.ParseTokens(chunk.Tokens);
                            this.Maps.Add(map);
                            break;
                        }

                    case "lockambdiff":
                        this.IsAmbiantDiffuseLocked = true;
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

            Tokenizer.BuildOpenChunk(tokens, "surface");
            Tokenizer.BuildIndent(tokens);

            if (this.Ambiant != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.Ambiant.BuildTokens());
            }

            if (this.Diffuse != null)
            {
                var color = this.Diffuse.BuildTokens();
                color[0] = "diffuse";

                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(color);
            }

            if (this.Specular != null)
            {
                var color = this.Specular.BuildTokens();
                color[0] = "specular";

                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(color);
            }

            if (this.Emissive != null)
            {
                var color = this.Emissive.BuildTokens();
                color[0] = "emissive";

                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(color);
            }

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "alpha");
            Tokenizer.BuildInt(tokens, this.Alpha);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "brilliance");
            Tokenizer.BuildFloat(tokens, this.Brilliance);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "phongsize");
            Tokenizer.BuildFloat(tokens, this.PhongRoughness);
            Tokenizer.BuildCloseChunk(tokens);

            foreach (var map in this.Maps)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(map.BuildTokens());
            }

            if (this.IsAmbiantDiffuseLocked)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "lockambdiff");
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
