// <copyright file="An8ModifierTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Modifier"/>.
    /// </summary>
    public class An8ModifierTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewModifier()
        {
            var modifier = new An8Modifier();

            Assert.Null(modifier.Method);
            Assert.Null(modifier.Component);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var modifier = new An8Modifier();

            modifier.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var modifier = new An8Modifier();

            modifier.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseLength()
        {
            var modifier = new An8Modifier();

            modifier.Parse(
@"
length { 2.0 }
");

            Assert.Equal(2.0f, modifier.Length);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseDiameter()
        {
            var modifier = new An8Modifier();

            modifier.Parse(
@"
diameter { 2.0 }
");

            Assert.Equal(2.0f, modifier.Diameter);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseSegments()
        {
            var modifier = new An8Modifier();

            modifier.Parse(
@"
segments { 2 }
");

            Assert.Equal(2, modifier.SegmentsCount);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMethod()
        {
            var modifier = new An8Modifier();

            modifier.Parse(
@"
method { }
");

            Assert.NotNull(modifier.Method);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseComponent()
        {
            var modifier = new An8Modifier();

            modifier.Parse(
@"
group { }
");

            Assert.NotNull(modifier.Component);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var modifier = new An8Modifier();

            var text = modifier.GenerateText();

            string expected =
@"modifier {
  name { """" }
  length { 0.000000 }
  diameter { 0.000000 }
  segments { 0 }
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
        public void GenerateTextLength()
        {
            var modifier = new An8Modifier();
            modifier.Length = 2.0f;

            var text = modifier.GenerateText();

            string expected =
@"modifier {
  name { """" }
  length { 2.000000 }
  diameter { 0.000000 }
  segments { 0 }
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
        public void GenerateTextDiameter()
        {
            var modifier = new An8Modifier();
            modifier.Diameter = 2.0f;

            var text = modifier.GenerateText();

            string expected =
@"modifier {
  name { """" }
  length { 0.000000 }
  diameter { 2.000000 }
  segments { 0 }
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
        public void GenerateTextSegments()
        {
            var modifier = new An8Modifier();
            modifier.SegmentsCount = 2;

            var text = modifier.GenerateText();

            string expected =
@"modifier {
  name { """" }
  length { 0.000000 }
  diameter { 0.000000 }
  segments { 2 }
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
        public void GenerateTextmethod()
        {
            var modifier = new An8Modifier();
            modifier.Method = new An8Method();

            var text = modifier.GenerateText();

            string expected =
@"modifier {
  name { """" }
  length { 0.000000 }
  diameter { 0.000000 }
  segments { 0 }
  method { modifier """"
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
        public void GenerateTextComponent()
        {
            var modifier = new An8Modifier();
            modifier.Component = new An8Group();

            var text = modifier.GenerateText();

            string expected =
@"modifier {
  name { """" }
  length { 0.000000 }
  diameter { 0.000000 }
  segments { 0 }
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
