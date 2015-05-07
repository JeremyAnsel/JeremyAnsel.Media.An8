// <copyright file="An8BoneTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Bone"/>.
    /// </summary>
    public class An8BoneTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewBone()
        {
            var bone = new An8Bone();

            Assert.Null(bone.Diameter);
            Assert.Null(bone.Orientation);
            Assert.False(bone.IsLocked);
            Assert.Null(bone.Influence);
            Assert.NotNull(bone.DegreesOfFreedom);
            Assert.NotNull(bone.Components);
            Assert.NotNull(bone.Bones);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var bone = new An8Bone();

            bone.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var bone = new An8Bone();

            bone.Parse(
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
            var bone = new An8Bone();

            bone.Parse(
@"
""abc""
");

            Assert.Equal("abc", bone.Name);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseLength()
        {
            var bone = new An8Bone();

            bone.Parse(
@"
length { 1.0 }
");

            Assert.Equal(1.0f, bone.Length);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseDiameter()
        {
            var bone = new An8Bone();

            bone.Parse(
@"
diameter { 1.0 }
");

            Assert.True(bone.Diameter.HasValue);
            Assert.Equal(1.0f, bone.Diameter.Value);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOrientation()
        {
            var bone = new An8Bone();

            bone.Parse(
@"
orientation { ( 1.0 2.0 3.0 4.0 ) }
");

            Assert.NotNull(bone.Orientation);
            Assert.Equal(1.0f, bone.Orientation.X);
            Assert.Equal(2.0f, bone.Orientation.Y);
            Assert.Equal(3.0f, bone.Orientation.Z);
            Assert.Equal(4.0f, bone.Orientation.W);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseLocked()
        {
            var bone = new An8Bone();

            bone.Parse(
@"
locked { }
");

            Assert.True(bone.IsLocked);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseDegreeOfFreedom()
        {
            var bone = new An8Bone();

            bone.Parse(
@"
dof { }
");

            Assert.Equal(1, bone.DegreesOfFreedom.Count);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseInfluence()
        {
            var bone = new An8Bone();

            bone.Parse(
@"
influence { }
");

            Assert.NotNull(bone.Influence);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseBone()
        {
            var bone = new An8Bone();

            bone.Parse(
@"
bone { }
");

            Assert.Equal(1, bone.Bones.Count);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseComponent()
        {
            var bone = new An8Bone();

            bone.Parse(
@"
group { }
");

            Assert.Equal(1, bone.Components.Count);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var bone = new An8Bone();

            var text = bone.GenerateText();

            string expected =
@"bone { """"
  length { 0.000000 }
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
            var bone = new An8Bone();
            bone.Name = "abc";

            var text = bone.GenerateText();

            string expected =
@"bone { ""abc""
  length { 0.000000 }
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
            var bone = new An8Bone();
            bone.Length = 1.0f;

            var text = bone.GenerateText();

            string expected =
@"bone { """"
  length { 1.000000 }
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
            var bone = new An8Bone();
            bone.Diameter = 1.0f;

            var text = bone.GenerateText();

            string expected =
@"bone { """"
  length { 0.000000 }
  diameter { 1.000000 }
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
        public void GenerateTextOrientation()
        {
            var bone = new An8Bone();
            bone.Orientation = new An8Quaternion
            {
                X = 1.0f,
                Y = 2.0f,
                Z = 3.0f,
                W = 4.0f
            };

            var text = bone.GenerateText();

            string expected =
@"bone { """"
  length { 0.000000 }
  orientation { ( 1.000000 2.000000 3.000000 4.000000 ) }
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
        public void GenerateTextLocked()
        {
            var bone = new An8Bone();
            bone.IsLocked = true;

            var text = bone.GenerateText();

            string expected =
@"bone { """"
  length { 0.000000 }
  locked { }
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
        public void GenerateTextDegreeOfFreedom()
        {
            var bone = new An8Bone();
            bone.DegreesOfFreedom.Add(new An8DegreeOfFreedom());

            var text = bone.GenerateText();

            string expected =
@"bone { """"
  length { 0.000000 }
  dof { """" 0.000000 0.000000 0.000000 }
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
        public void GenerateTextInfluence()
        {
            var bone = new An8Bone();
            bone.Influence = new An8Influence();

            var text = bone.GenerateText();

            string expected =
@"bone { """"
  length { 0.000000 }
  influence { 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 }
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
            var bone = new An8Bone();
            bone.Bones.Add(new An8Bone());

            var text = bone.GenerateText();

            string expected =
@"bone { """"
  length { 0.000000 }
  bone { """"
    length { 0.000000 }
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
            var bone = new An8Bone();
            bone.Components.Add(new An8Group());

            var text = bone.GenerateText();

            string expected =
@"bone { """"
  length { 0.000000 }
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
