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
            Assert.Equal("0.85beta", file.Header.Version);
            Assert.Equal("2003.09.21", file.Header.Build);
            Assert.Null(file.Description);
            Assert.NotNull(file.Environment);
            Assert.Equal(30, file.Environment.Framerate);
            Assert.True(file.Environment.IsPlaybackFramerateLimited);
            Assert.True(file.Environment.IsAutoGridEnabled);
            Assert.Equal(10.0f, file.Environment.ModelingGridSpacing);
            Assert.Equal(50.0f, file.Environment.SceneEditorGridSpacing);
            Assert.Equal(50.0f, file.Environment.GroundFloorGridSize);
            Assert.NotNull(file.Textures);
            Assert.NotNull(file.Materials);
            Assert.NotNull(file.Objects);
            Assert.NotNull(file.Figures);
        }
    }
}
