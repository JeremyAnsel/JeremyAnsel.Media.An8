// <copyright file="An8FigureTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Figure"/>.
    /// </summary>
    public class An8FigureTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewFigure()
        {
            var figure = new An8Figure();

            Assert.Null(figure.Name);
            Assert.NotNull(figure.Materials);
            Assert.Null(figure.RootBone);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var figure = new An8Figure();

            figure.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var figure = new An8Figure();

            figure.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseName()
        {
            var figure = new An8Figure();

            figure.Parse(
@"
""abc""
");

            Assert.Equal("abc", figure.Name);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMaterial()
        {
            var figure = new An8Figure();

            figure.Parse(
@"
material { }
");

            Assert.Single(figure.Materials);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseBone()
        {
            var figure = new An8Figure();

            figure.Parse(
@"
bone { }
");

            Assert.NotNull(figure.RootBone);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var figure = new An8Figure();

            var text = figure.GenerateText();

            string expected =
@"figure { """"
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
        public void GenerateTextName()
        {
            var figure = new An8Figure();
            figure.Name = "abc";

            var text = figure.GenerateText();

            string expected =
@"figure { ""abc""
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
        public void GenerateTextMaterial()
        {
            var figure = new An8Figure();
            figure.Materials.Add(new An8Material());

            var text = figure.GenerateText();

            string expected =
@"figure { """"
  material { """"
  }
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
        public void GenerateTextBone()
        {
            var figure = new An8Figure();
            figure.RootBone = new An8Bone();

            var text = figure.GenerateText();

            string expected =
@"figure { """"
  bone { """"
    length { 0.000000 }
  }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
