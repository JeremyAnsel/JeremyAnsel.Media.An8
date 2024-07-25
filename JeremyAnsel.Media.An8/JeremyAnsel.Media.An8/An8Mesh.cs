// <copyright file="An8Mesh.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A mesh.
    /// </summary>
    public sealed class An8Mesh : An8Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8Mesh"/> class.
        /// </summary>
        public An8Mesh()
        {
            this.MaterialList = new List<string?>();
            this.Points = new List<An8Point>();
            this.Normals = new List<An8Point>();
            this.Edges = new List<An8Edge>();
            this.TexCoords = new List<An8TexCoord>();
            this.Faces = new List<An8Face>();
        }

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public An8Material? Material { get; set; }

        /// <summary>
        /// Gets or sets smooth angle threshold.
        /// </summary>
        public float SmoothAngleThreshold { get; set; }

        /// <summary>
        /// Gets the material list.
        /// </summary>
        public IList<string?> MaterialList { get; private set; }

        /// <summary>
        /// Gets the points.
        /// </summary>
        public IList<An8Point> Points { get; private set; }

        /// <summary>
        /// Gets the normals.
        /// </summary>
        public IList<An8Point> Normals { get; private set; }

        /// <summary>
        /// Gets the edges.
        /// </summary>
        public IList<An8Edge> Edges { get; private set; }

        /// <summary>
        /// Gets the texture coordinates.
        /// </summary>
        public IList<An8TexCoord> TexCoords { get; private set; }

        /// <summary>
        /// Gets the faces.
        /// </summary>
        public IList<An8Face> Faces { get; private set; }

        /// <summary>
        /// Parses the tokens of the chunk.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        internal override void ParseTokens(string[] tokens)
        {
            base.ParseTokens(tokens);

            var chunks = Tokenizer.SplitChunks(tokens);

            foreach (var chunk in chunks)
            {
                int index = 0;

                switch (chunk.Ident)
                {
                    case "material":
                        {
                            var material = new An8Material();
                            material.ParseTokens(chunk.Tokens);
                            this.Material = material;
                            break;
                        }

                    case "smoothangle":
                        this.SmoothAngleThreshold = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "materiallist":
                        this.MaterialList.Clear();
                        foreach (var material in Tokenizer.SplitChunks(chunk.Tokens)
                            .Where(t => string.Equals(t.Ident, "materialname", StringComparison.Ordinal))
                            .Select(t =>
                            {
                                int i = 0;
                                return Tokenizer.ReadString(t.Tokens, ref i);
                            }))
                        {
                            this.MaterialList.Add(material);
                        }

                        break;

                    case "points":
                        this.Points.Clear();
                        while (index < chunk.Tokens.Length)
                        {
                            this.Points.Add(An8Point.ReadTokens(chunk.Tokens, ref index));
                        }

                        break;

                    case "normals":
                        this.Normals.Clear();
                        while (index < chunk.Tokens.Length)
                        {
                            this.Normals.Add(An8Point.ReadTokens(chunk.Tokens, ref index));
                        }

                        break;

                    case "edges":
                        this.Edges.Clear();
                        while (index < chunk.Tokens.Length)
                        {
                            this.Edges.Add(An8Edge.ReadTokens(chunk.Tokens, ref index));
                        }

                        break;

                    case "texcoords":
                        this.TexCoords.Clear();
                        while (index < chunk.Tokens.Length)
                        {
                            this.TexCoords.Add(An8TexCoord.ReadTokens(chunk.Tokens, ref index));
                        }

                        break;

                    case "faces":
                        this.Faces.Clear();
                        while (index < chunk.Tokens.Length)
                        {
                            this.Faces.Add(An8Face.ReadTokens(chunk.Tokens, ref index));
                        }

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

            Tokenizer.BuildOpenChunk(tokens, "mesh");
            Tokenizer.BuildIndent(tokens);

            tokens.AddRange(base.BuildTokens());

            if (this.Material != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.Material.BuildTokens());
            }

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "smoothangle");
            Tokenizer.BuildFloat(tokens, this.SmoothAngleThreshold);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "materiallist");
            Tokenizer.BuildIndent(tokens);

            foreach (var materialName in this.MaterialList)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "materialname");
                Tokenizer.BuildString(tokens, materialName);
                Tokenizer.BuildCloseChunk(tokens);
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "points");
            Tokenizer.BuildIndent(tokens);

            foreach (var point in this.Points)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(point.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "normals");
            Tokenizer.BuildIndent(tokens);

            foreach (var normal in this.Normals)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(normal.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "edges");
            Tokenizer.BuildIndent(tokens);

            foreach (var edge in this.Edges)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(edge.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "texcoords");
            Tokenizer.BuildIndent(tokens);

            foreach (var texCoord in this.TexCoords)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(texCoord.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "faces");
            Tokenizer.BuildIndent(tokens);

            foreach (var face in this.Faces)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(face.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
