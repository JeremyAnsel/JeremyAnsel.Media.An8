// <copyright file="An8Face.cs" company="Jérémy Ansel">
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
    /// A face.
    /// </summary>
    public sealed class An8Face
    {
        /// <summary>
        /// Gets or sets a value indicating whether the back face is shown.
        /// </summary>
        public bool IsBackShown { get; set; }

        /// <summary>
        /// Gets or sets the index of the material.
        /// </summary>
        public int MaterialIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the normal.
        /// </summary>
        public int FlatNormalIndex { get; set; }

        /// <summary>
        /// Gets or sets the point indexes.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Reviewed")]
        public int[] PointIndexes { get; set; }

        /// <summary>
        /// Gets or sets the normal indexes.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Reviewed")]
        public int[] NormalIndexes { get; set; }

        /// <summary>
        /// Gets a value indicating whether the face has normal indexes.
        /// </summary>
        public bool HasNormalIndexes
        {
            get { return this.NormalIndexes != null && this.NormalIndexes.Length == (this.PointIndexes == null ? 0 : this.PointIndexes.Length); }
        }

        /// <summary>
        /// Gets or sets the texture coordinates indexes.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Reviewed")]
        public int[] TexCoordIndexes { get; set; }

        /// <summary>
        /// Gets a value indicating whether the face has texture coordinates indexes.
        /// </summary>
        public bool HasTexCoordIndexes
        {
            get { return this.TexCoordIndexes != null && this.TexCoordIndexes.Length == (this.PointIndexes == null ? 0 : this.PointIndexes.Length); }
        }

        /// <summary>
        /// Reads tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="index">The index.</param>
        /// <returns>A face.</returns>
        internal static An8Face ReadTokens(string[] tokens, ref int index)
        {
            var face = new An8Face();

            int num = Tokenizer.ReadInt(tokens, ref index);
            int flags = Tokenizer.ReadInt(tokens, ref index);

            if ((flags & 1) != 0)
            {
                face.IsBackShown = true;
            }

            bool hasNormals = (flags & 2) != 0;
            bool hasTexCoords = (flags & 4) != 0;

            face.MaterialIndex = Tokenizer.ReadInt(tokens, ref index);
            face.FlatNormalIndex = Tokenizer.ReadInt(tokens, ref index);

            face.PointIndexes = new int[num];

            if (hasNormals)
            {
                face.NormalIndexes = new int[num];
            }
            else
            {
                face.NormalIndexes = null;
            }

            if (hasTexCoords)
            {
                face.TexCoordIndexes = new int[num];
            }
            else
            {
                face.TexCoordIndexes = null;
            }

            Tokenizer.ReadOpenData(tokens, ref index);

            for (int i = 0; i < num; i++)
            {
                Tokenizer.ReadOpenData(tokens, ref index);

                face.PointIndexes[i] = Tokenizer.ReadInt(tokens, ref index);

                if (hasNormals)
                {
                    face.NormalIndexes[i] = Tokenizer.ReadInt(tokens, ref index);
                }

                if (hasTexCoords)
                {
                    face.TexCoordIndexes[i] = Tokenizer.ReadInt(tokens, ref index);
                }

                Tokenizer.ReadCloseData(tokens, ref index);
            }

            Tokenizer.ReadCloseData(tokens, ref index);

            return face;
        }

        /// <summary>
        /// Builds tokens.
        /// </summary>
        /// <returns>The tokens.</returns>
        internal string[] BuildTokens()
        {
            int num = this.PointIndexes == null ? 0 : this.PointIndexes.Length;
            bool hasNormals = this.HasNormalIndexes;
            bool hasTexCoords = this.HasTexCoordIndexes;

            int flags = 0;

            if (this.IsBackShown)
            {
                flags |= 1;
            }

            if (hasNormals)
            {
                flags |= 2;
            }

            if (hasTexCoords)
            {
                flags |= 4;
            }

            var tokens = new List<string>();

            Tokenizer.BuildInt(tokens, num);
            Tokenizer.BuildInt(tokens, flags);
            Tokenizer.BuildInt(tokens, this.MaterialIndex);
            Tokenizer.BuildInt(tokens, this.FlatNormalIndex);

            Tokenizer.BuildOpenData(tokens);

            for (int i = 0; i < num; i++)
            {
                Tokenizer.BuildOpenData(tokens);

                Tokenizer.BuildInt(tokens, this.PointIndexes[i]);

                if (hasNormals)
                {
                    Tokenizer.BuildInt(tokens, this.NormalIndexes[i]);
                }

                if (hasTexCoords)
                {
                    Tokenizer.BuildInt(tokens, this.TexCoordIndexes[i]);
                }

                Tokenizer.BuildCloseData(tokens);
            }

            Tokenizer.BuildCloseData(tokens);

            return tokens.ToArray();
        }
    }
}
