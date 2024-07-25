// <copyright file="An8NamedObjectTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8NamedObject"/>.
    /// </summary>
    public class An8NamedObjectTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewNamedObject()
        {
            var named = new An8NamedObject();

            Assert.Null(named.ObjectName);
            Assert.Null(named.Material);
            Assert.NotNull(named.WeightedBy);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var named = new An8NamedObject();

            named.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var named = new An8NamedObject();

            named.Parse(
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
            var named = new An8NamedObject();

            named.Parse(
@"
""abc""
");

            Assert.Equal("abc", named.ObjectName);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMaterial()
        {
            var named = new An8NamedObject();

            named.Parse(
@"
material { }
");

            Assert.NotNull(named.Material);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseWeightedBy()
        {
            var named = new An8NamedObject();

            named.Parse(
@"
weightedby { ""abc"" }
");

            Assert.Single(named.WeightedBy);
            Assert.Equal("abc", named.WeightedBy[0]);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var named = new An8NamedObject();

            var text = named.GenerateText();

            string expected =
@"namedobject {
  name { """" }
  """"
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
            var named = new An8NamedObject();
            named.ObjectName = "abc";

            var text = named.GenerateText();

            string expected =
@"namedobject {
  name { """" }
  ""abc""
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
        public void GenerateTextmaterial()
        {
            var named = new An8NamedObject();
            named.Material = new An8Material();

            var text = named.GenerateText();

            string expected =
@"namedobject {
  name { """" }
  """"
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
        public void GenerateTextWeightedBy()
        {
            var named = new An8NamedObject();
            named.WeightedBy.Add("abc");

            var text = named.GenerateText();

            string expected =
@"namedobject {
  name { """" }
  """"
  weightedby { ""abc"" }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
