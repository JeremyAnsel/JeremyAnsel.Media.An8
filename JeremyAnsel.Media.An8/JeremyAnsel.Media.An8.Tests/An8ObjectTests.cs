// <copyright file="An8ObjectTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Object"/>.
    /// </summary>
    public class An8ObjectTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewObject()
        {
            var obj = new An8Object();

            Assert.Null(obj.Name);
            Assert.NotNull(obj.Materials);
            Assert.NotNull(obj.Components);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var obj = new An8Object();

            obj.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var obj = new An8Object();

            obj.Parse(
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
            var obj = new An8Object();

            obj.Parse(
@"
""abc""
");

            Assert.Equal("abc", obj.Name);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMaterial()
        {
            var obj = new An8Object();

            obj.Parse(
@"
material { }
");

            Assert.Single(obj.Materials);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseComponent()
        {
            var obj = new An8Object();

            obj.Parse(
@"
group { }
");

            Assert.Single(obj.Components);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var obj = new An8Object();

            var text = obj.GenerateText();

            string expected =
@"object { """"
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
            var obj = new An8Object();
            obj.Name = "abc";

            var text = obj.GenerateText();

            string expected =
@"object { ""abc""
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
            var obj = new An8Object();
            obj.Materials.Add(new An8Material());

            var text = obj.GenerateText();

            string expected =
@"object { """"
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
        public void GenerateTextComponent()
        {
            var obj = new An8Object();
            obj.Components.Add(new An8Group());

            var text = obj.GenerateText();

            string expected =
@"object { """"
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
