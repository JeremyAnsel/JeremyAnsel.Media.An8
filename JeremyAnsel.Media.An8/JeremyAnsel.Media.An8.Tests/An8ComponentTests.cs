// <copyright file="An8ComponentTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Component"/>.
    /// </summary>
    public class An8ComponentTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewComponent()
        {
            var com = new An8Group();

            Assert.Null(com.Name);
            Assert.Null(com.BaseOrigin);
            Assert.Null(com.BaseOrientation);
            Assert.Null(com.PivotOrigin);
            Assert.Null(com.PivotOrientation);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseComponents()
        {
            var group = new An8Group();

            group.Parse(
@"
other { }
mesh { }
sphere { }
cylinder { }
cube { }
subdivision { }
path { }
textcom { }
modifier { }
image { }
namedobject { }
group { }
");

            Assert.Equal(11, group.Components.Count);
            Assert.IsType(typeof(An8Mesh), group.Components[0]);
            Assert.IsType(typeof(An8Sphere), group.Components[1]);
            Assert.IsType(typeof(An8Cylinder), group.Components[2]);
            Assert.IsType(typeof(An8Cube), group.Components[3]);
            Assert.IsType(typeof(An8Subdivision), group.Components[4]);
            Assert.IsType(typeof(An8Path), group.Components[5]);
            Assert.IsType(typeof(An8TextCom), group.Components[6]);
            Assert.IsType(typeof(An8Modifier), group.Components[7]);
            Assert.IsType(typeof(An8Image), group.Components[8]);
            Assert.IsType(typeof(An8NamedObject), group.Components[9]);
            Assert.IsType(typeof(An8Group), group.Components[10]);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseName()
        {
            var com = new An8Group();

            com.Parse(
@"
name { ""abc"" }
");

            Assert.Equal("abc", com.Name);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseBase()
        {
            var com = new An8Group();

            com.Parse(
@"
base {
  other { }
  origin { ( 2.0 3.0 4.0 ) }
  orientation { ( 5.0 6.0 7.0 8.0 ) }
}
");

            Assert.NotNull(com.BaseOrigin);
            Assert.Equal(2.0f, com.BaseOrigin.X);
            Assert.Equal(3.0f, com.BaseOrigin.Y);
            Assert.Equal(4.0f, com.BaseOrigin.Z);
            Assert.NotNull(com.BaseOrientation);
            Assert.Equal(5.0f, com.BaseOrientation.X);
            Assert.Equal(6.0f, com.BaseOrientation.Y);
            Assert.Equal(7.0f, com.BaseOrientation.Z);
            Assert.Equal(8.0f, com.BaseOrientation.W);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParsePivot()
        {
            var com = new An8Group();

            com.Parse(
@"
pivot {
  other { }
  origin { ( 2.0 3.0 4.0 ) }
  orientation { ( 5.0 6.0 7.0 8.0 ) }
}
");

            Assert.NotNull(com.PivotOrigin);
            Assert.Equal(2.0f, com.PivotOrigin.X);
            Assert.Equal(3.0f, com.PivotOrigin.Y);
            Assert.Equal(4.0f, com.PivotOrigin.Z);
            Assert.NotNull(com.PivotOrientation);
            Assert.Equal(5.0f, com.PivotOrientation.X);
            Assert.Equal(6.0f, com.PivotOrientation.Y);
            Assert.Equal(7.0f, com.PivotOrientation.Z);
            Assert.Equal(8.0f, com.PivotOrientation.W);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextName()
        {
            var com = new An8Group();
            com.Name = "abc";

            var text = com.GenerateText();

            string expected =
@"group {
  name { ""abc"" }
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
        public void GenerateTextBaseOrigin()
        {
            var com = new An8Group();
            com.BaseOrigin = new An8Point { X = 2.0f, Y = 3.0f, Z = 4.0f };

            var text = com.GenerateText();

            string expected =
@"group {
  name { """" }
  base {
    origin { ( 2.000000 3.000000 4.000000 ) }
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
        public void GenerateTextBaseOrientation()
        {
            var com = new An8Group();
            com.BaseOrientation = new An8Quaternion { X = 5.0f, Y = 6.0f, Z = 7.0f, W = 8.0f };

            var text = com.GenerateText();

            string expected =
@"group {
  name { """" }
  base {
    orientation { ( 5.000000 6.000000 7.000000 8.000000 ) }
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
        public void GenerateTextPivotOrigin()
        {
            var com = new An8Group();
            com.PivotOrigin = new An8Point { X = 2.0f, Y = 3.0f, Z = 4.0f };

            var text = com.GenerateText();

            string expected =
@"group {
  name { """" }
  pivot {
    origin { ( 2.000000 3.000000 4.000000 ) }
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
        public void GenerateTextPivotOrientation()
        {
            var com = new An8Group();
            com.PivotOrientation = new An8Quaternion { X = 5.0f, Y = 6.0f, Z = 7.0f, W = 8.0f };

            var text = com.GenerateText();

            string expected =
@"group {
  name { """" }
  pivot {
    orientation { ( 5.000000 6.000000 7.000000 8.000000 ) }
  }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
