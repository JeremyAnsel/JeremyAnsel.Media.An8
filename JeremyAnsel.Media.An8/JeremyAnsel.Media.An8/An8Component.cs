// <copyright file="An8Component.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A component.
    /// </summary>
    public abstract class An8Component : An8Chunk
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the base location.
        /// </summary>
        public An8Point BaseOrigin { get; set; }

        /// <summary>
        /// Gets or sets the base orientation.
        /// </summary>
        public An8Quaternion BaseOrientation { get; set; }

        /// <summary>
        /// Gets or sets the pivot location.
        /// </summary>
        public An8Point PivotOrigin { get; set; }

        /// <summary>
        /// Gets or sets the pivot orientation.
        /// </summary>
        public An8Quaternion PivotOrientation { get; set; }

        /// <summary>
        /// Parses the tokens of components.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The components.</returns>
        internal static List<An8Component> ParseComponents(string[] tokens)
        {
            var components = new List<An8Component>();

            var chunks = Tokenizer.SplitChunks(tokens);

            foreach (var chunk in chunks)
            {
                An8Component component = null;

                switch (chunk.Ident)
                {
                    case "mesh":
                        component = new An8Mesh();
                        break;

                    case "sphere":
                        component = new An8Sphere();
                        break;

                    case "cylinder":
                        component = new An8Cylinder();
                        break;

                    case "cube":
                        component = new An8Cube();
                        break;

                    case "subdivision":
                        component = new An8Subdivision();
                        break;

                    case "path":
                        component = new An8Path();
                        break;

                    case "textcom":
                        component = new An8TextCom();
                        break;

                    case "modifier":
                        component = new An8Modifier();
                        break;

                    case "image":
                        component = new An8Image();
                        break;

                    case "namedobject":
                        component = new An8NamedObject();
                        break;

                    case "group":
                        component = new An8Group();
                        break;
                }

                if (component != null)
                {
                    component.ParseTokens(chunk.Tokens);
                    components.Add(component);
                }
            }

            return components;
        }

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
                    case "name":
                        this.Name = Tokenizer.ReadString(chunk.Tokens, ref index);
                        break;

                    case "base":
                        this.ParseBase(chunk.Tokens);
                        break;

                    case "pivot":
                        this.ParsePivot(chunk.Tokens);
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

            Tokenizer.BuildOpenChunk(tokens, "name");
            Tokenizer.BuildString(tokens, this.Name);
            Tokenizer.BuildCloseChunk(tokens);

            if (this.BaseOrigin != null || this.BaseOrientation != null)
            {
                Tokenizer.BuildNewLine(tokens);
                this.BuildBase(tokens);
            }

            if (this.PivotOrigin != null || this.PivotOrientation != null)
            {
                Tokenizer.BuildNewLine(tokens);
                this.BuildPivot(tokens);
            }

            return tokens.ToArray();
        }

        /// <summary>
        /// Parses a base chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        private void ParseBase(string[] tokens)
        {
            var chunks = Tokenizer.SplitChunks(tokens);

            foreach (var chunk in chunks)
            {
                int index = 0;

                switch (chunk.Ident)
                {
                    case "origin":
                        this.BaseOrigin = An8Point.ReadTokens(chunk.Tokens, ref index);
                        break;

                    case "orientation":
                        this.BaseOrientation = An8Quaternion.ReadTokens(chunk.Tokens, ref index);
                        break;
                }
            }
        }

        /// <summary>
        /// Builds a base chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        private void BuildBase(List<string> tokens)
        {
            Tokenizer.BuildOpenChunk(tokens, "base");
            Tokenizer.BuildIndent(tokens);

            if (this.BaseOrigin != null)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "origin");
                tokens.AddRange(this.BaseOrigin.BuildTokens());
                Tokenizer.BuildCloseChunk(tokens);
            }

            if (this.BaseOrientation != null)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "orientation");
                tokens.AddRange(this.BaseOrientation.BuildTokens());
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);
        }

        /// <summary>
        /// Parses a pivot chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        private void ParsePivot(string[] tokens)
        {
            var chunks = Tokenizer.SplitChunks(tokens);

            foreach (var chunk in chunks)
            {
                int index = 0;

                switch (chunk.Ident)
                {
                    case "origin":
                        this.PivotOrigin = An8Point.ReadTokens(chunk.Tokens, ref index);
                        break;

                    case "orientation":
                        this.PivotOrientation = An8Quaternion.ReadTokens(chunk.Tokens, ref index);
                        break;
                }
            }
        }

        /// <summary>
        /// Builds a pivot chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        private void BuildPivot(List<string> tokens)
        {
            Tokenizer.BuildOpenChunk(tokens, "pivot");
            Tokenizer.BuildIndent(tokens);

            if (this.PivotOrigin != null)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "origin");
                tokens.AddRange(this.PivotOrigin.BuildTokens());
                Tokenizer.BuildCloseChunk(tokens);
            }

            if (this.PivotOrientation != null)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "orientation");
                tokens.AddRange(this.PivotOrientation.BuildTokens());
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);
        }
    }
}
