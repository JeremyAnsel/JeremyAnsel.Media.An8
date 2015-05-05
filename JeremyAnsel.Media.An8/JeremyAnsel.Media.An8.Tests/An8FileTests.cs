// <copyright file="An8FileTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8File"/>.
    /// </summary>
    public class An8FileTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewFile()
        {
            var file = new An8File();

            Assert.NotNull(file.Header);
            Assert.Null(file.Description);
            Assert.NotNull(file.Environment);
            Assert.NotNull(file.Textures);
            Assert.NotNull(file.Materials);
            Assert.NotNull(file.Objects);
            Assert.NotNull(file.Figures);
        }
    }
}
