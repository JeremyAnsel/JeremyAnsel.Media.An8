// <copyright file="An8AlphaMode.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8
{
    /// <summary>
    /// An alpha mode.
    /// </summary>
    public enum An8AlphaMode
    {
        /// <summary>
        /// The alpha component is ignored.
        /// </summary>
        None,

        /// <summary>
        /// The alpha components are multiplied.
        /// </summary>
        Layer,

        /// <summary>
        /// The alpha component is used.
        /// </summary>
        Final
    }
}
