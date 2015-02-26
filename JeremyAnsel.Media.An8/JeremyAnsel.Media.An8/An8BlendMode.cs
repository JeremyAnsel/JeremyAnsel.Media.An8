// <copyright file="An8BlendMode.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    /// <summary>
    /// A blend mode.
    /// </summary>
    public enum An8BlendMode
    {
        /// <summary>
        /// The color is multiplied by the base color.
        /// </summary>
        Decal,

        /// <summary>
        /// The color replaces the base color.
        /// </summary>
        Darken,

        /// <summary>
        /// The color is added to the base color.
        /// </summary>
        Lighten
    }
}
