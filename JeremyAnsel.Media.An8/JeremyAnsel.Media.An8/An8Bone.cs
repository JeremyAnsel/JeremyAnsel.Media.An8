// <copyright file="An8Bone.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    using System.Collections.Generic;

    /// <summary>
    /// A bone.
    /// </summary>
    public sealed class An8Bone : An8Chunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="An8Bone"/> class.
        /// </summary>
        public An8Bone()
        {
            this.DegreesOfFreedom = new List<An8DegreeOfFreedom>();
            this.Components = new List<An8Component>();
            this.Bones = new List<An8Bone>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public float Length { get; set; }

        /// <summary>
        /// Gets or sets the diameter.
        /// </summary>
        public float? Diameter { get; set; }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        public An8Quaternion? Orientation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bone is locked.
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Gets the degrees of freedom.
        /// </summary>
        public IList<An8DegreeOfFreedom> DegreesOfFreedom { get; private set; }

        /// <summary>
        /// Gets or sets the influence.
        /// </summary>
        public An8Influence? Influence { get; set; }

        /// <summary>
        /// Gets the components.
        /// </summary>
        public IList<An8Component> Components { get; private set; }

        /// <summary>
        /// Gets the bones.
        /// </summary>
        public IList<An8Bone> Bones { get; private set; }

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

                    case "length":
                        this.Length = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "diameter":
                        this.Diameter = Tokenizer.ReadFloat(chunk.Tokens, ref index);
                        break;

                    case "orientation":
                        this.Orientation = An8Quaternion.ReadTokens(chunk.Tokens, ref index);
                        break;

                    case "locked":
                        this.IsLocked = true;
                        break;

                    case "dof":
                        {
                            var dof = new An8DegreeOfFreedom();
                            dof.ParseTokens(chunk.Tokens);
                            this.DegreesOfFreedom.Add(dof);
                            break;
                        }

                    case "influence":
                        {
                            var influence = new An8Influence();
                            influence.ParseTokens(chunk.Tokens);
                            this.Influence = influence;
                            break;
                        }

                    case "bone":
                        {
                            var bone = new An8Bone();
                            bone.ParseTokens(chunk.Tokens);
                            this.Bones.Add(bone);
                            break;
                        }
                }
            }

            foreach (var component in An8Component.ParseComponents(tokens))
            {
                this.Components.Add(component);
            }
        }

        /// <summary>
        /// Builds the tokens of the chunk.
        /// </summary>
        /// <returns>The tokens.</returns>
        internal override string[] BuildTokens()
        {
            var tokens = new List<string>();

            Tokenizer.BuildOpenChunk(tokens, "bone");
            Tokenizer.BuildString(tokens, this.Name);
            Tokenizer.BuildIndent(tokens);

            Tokenizer.BuildNewLine(tokens);
            Tokenizer.BuildOpenChunk(tokens, "length");
            Tokenizer.BuildFloat(tokens, this.Length);
            Tokenizer.BuildCloseChunk(tokens);

            if (this.Diameter.HasValue)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "diameter");
                Tokenizer.BuildFloat(tokens, this.Diameter.Value);
                Tokenizer.BuildCloseChunk(tokens);
            }

            if (this.Orientation != null)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "orientation");
                tokens.AddRange(this.Orientation.BuildTokens());
                Tokenizer.BuildCloseChunk(tokens);
            }

            if (this.IsLocked)
            {
                Tokenizer.BuildNewLine(tokens);
                Tokenizer.BuildOpenChunk(tokens, "locked");
                Tokenizer.BuildCloseChunk(tokens);
            }

            foreach (var dof in this.DegreesOfFreedom)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(dof.BuildTokens());
            }

            if (this.Influence != null)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(this.Influence.BuildTokens());
            }

            foreach (var component in this.Components)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(component.BuildTokens());
            }

            foreach (var bone in this.Bones)
            {
                Tokenizer.BuildNewLine(tokens);
                tokens.AddRange(bone.BuildTokens());
            }

            Tokenizer.BuildUnindent(tokens);
            Tokenizer.BuildCloseChunk(tokens);

            return tokens.ToArray();
        }
    }
}
