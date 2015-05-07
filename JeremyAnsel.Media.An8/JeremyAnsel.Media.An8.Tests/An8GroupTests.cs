// <copyright file="An8GroupTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Group"/>.
    /// </summary>
    public class An8GroupTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewGroup()
        {
            var group = new An8Group();

            Assert.NotNull(group.Components);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var group = new An8Group();

            group.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var group = new An8Group();

            group.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseComponent()
        {
            var group = new An8Group();

            group.Parse(
@"
group { }
");

            Assert.Equal(1, group.Components.Count);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var group = new An8Group();

            var text = group.GenerateText();

            string expected =
@"group {
  name { """" }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextComponent()
        {
            var group = new An8Group();
            group.Components.Add(new An8Group());

            var text = group.GenerateText();

            string expected =
@"group {
  name { """" }
  group {
    name { """" }
  }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
