// <copyright file="An8File.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// An an8 file.
    /// </summary>
    public sealed class An8File
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8File"/> class.
        /// </summary>
        public An8File()
        {
            this.Header = new An8Header();
            this.Environment = new An8Environment();
            this.Textures = new List<An8Texture>();
            this.Materials = new List<An8Material>();
            this.Objects = new List<An8Object>();
            this.Figures = new List<An8Figure>();
        }

        /// <summary>
        /// Gets the file header.
        /// </summary>
        public An8Header Header { get; private set; }

        /// <summary>
        /// Gets or sets the file description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the environment settings.
        /// </summary>
        public An8Environment Environment { get; private set; }

        /// <summary>
        /// Gets the textures.
        /// </summary>
        public IList<An8Texture> Textures { get; private set; }

        /// <summary>
        /// Gets the global materials.
        /// </summary>
        public IList<An8Material> Materials { get; private set; }

        /// <summary>
        /// Gets the objects.
        /// </summary>
        public IList<An8Object> Objects { get; private set; }

        /// <summary>
        /// Gets the figures.
        /// </summary>
        public IList<An8Figure> Figures { get; private set; }

        /// <summary>
        /// Reads an .an8 file.
        /// </summary>
        /// <param name="fileName">The file path.</param>
        /// <returns>A <see cref="An8File"/>.</returns>
        public static An8File FromFile(string fileName)
        {
            string text = File.ReadAllText(fileName);

            An8File an8 = new An8File();
            an8.Parse(text);

            return an8;
        }

        /// <summary>
        /// Writes an .an8 file.
        /// </summary>
        /// <param name="fileName">The file path.</param>
        public void Save(string fileName)
        {
            string text = this.GenerateText();

            File.WriteAllText(fileName, text, Encoding.UTF8);
        }

        /// <summary>
        /// Parses a file content.
        /// </summary>
        /// <param name="text">The text.</param>
        public void Parse(string text)
        {
            var chunks = Tokenizer.SplitChunks(text);

            foreach (var chunk in chunks)
            {
                int index = 0;

                switch (chunk.Ident)
                {
                    case "header":
                        this.Header.ParseTokens(chunk.Tokens);
                        break;

                    case "description":
                        this.Description = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "environment":
                        this.Environment.ParseTokens(chunk.Tokens);
                        break;

                    case "texture":
                        {
                            var texture = new An8Texture();
                            texture.ParseTokens(chunk.Tokens);
                            this.Textures.Add(texture);
                            break;
                        }

                    case "material":
                        {
                            var material = new An8Material();
                            material.ParseTokens(chunk.Tokens);
                            this.Materials.Add(material);
                            break;
                        }

                    case "object":
                        {
                            var obj = new An8Object();
                            obj.ParseTokens(chunk.Tokens);
                            this.Objects.Add(obj);
                            break;
                        }

                    case "figure":
                        {
                            var figure = new An8Figure();
                            figure.ParseTokens(chunk.Tokens);
                            this.Figures.Add(figure);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Generates the file content.
        /// </summary>
        /// <returns>The generated text.</returns>
        public string GenerateText()
        {
            var tokens = new List<string>();

            tokens.AddRange(this.Header.BuildTokens());

            if (!string.IsNullOrEmpty(this.Description))
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "description");
                Tokenizer.BuildString(tokens, this.Description);
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildNewLine(tokens);
            tokens.AddRange(this.Environment.BuildTokens());

            foreach (var texture in this.Textures)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(texture.BuildTokens());
            }

            foreach (var material in this.Materials)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(material.BuildTokens());
            }

            foreach (var obj in this.Objects)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(obj.BuildTokens());
            }

            foreach (var figure in this.Figures)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(figure.BuildTokens());
            }

            Tokenizer.BuildNewLine(tokens);

            return Tokenizer.Build(tokens.ToArray());
        }
    }
}
