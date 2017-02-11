// 
// Copyright (c) 2006 Damien Miller <djm@mindrot.org>
// Copyright (c) 2013 Ryan D. Emerle
// Copyright(c) 2016 Stephen Donaghy(.net Core port)
// 
// Permission to use, copy, modify, and distribute this software for any
// purpose with or without fee is hereby granted, provided that the above
// copyright notice and this permission notice appear in all copies.
//
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
// ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
// ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
// OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BCrypt.Net.Test
{
    /// <summary>
    /// BCrypt tests
    /// </summary>
    public class TestBCrypt
    {
        readonly string[,] _TestVectors = {
			{ "",                                   "$2b$06$DCq7YPn5Rq63x1Lad4cll.",    "$2b$06$DCq7YPn5Rq63x1Lad4cll.TV4S6ytwfsfvkgY8jIucDrjc8deX1s." },
			{ "",                                   "$2b$08$HqWuK6/Ng6sg9gQzbLrgb.",    "$2b$08$HqWuK6/Ng6sg9gQzbLrgb.Tl.ZHfXLhvt/SgVyWhQqgqcZ7ZuUtye" },
			{ "",                                   "$2b$10$k1wbIrmNyFAPwPVPSVa/ze",    "$2b$10$k1wbIrmNyFAPwPVPSVa/zecw2BCEnBwVS2GbrmgzxFUOqW9dk4TCW" },
			{ "",                                   "$2b$12$k42ZFHFWqBp3vWli.nIn8u",    "$2b$12$k42ZFHFWqBp3vWli.nIn8uYyIkbvYRvodzbfbK18SSsY.CsIQPlxO" },
			{ "a",                                  "$2b$06$m0CrhHm10qJ3lXRY.5zDGO",    "$2b$06$m0CrhHm10qJ3lXRY.5zDGO3rS2KdeeWLuGmsfGlMfOxih58VYVfxe" },
			{ "a",                                  "$2b$08$cfcvVd2aQ8CMvoMpP2EBfe",    "$2b$08$cfcvVd2aQ8CMvoMpP2EBfeodLEkkFJ9umNEfPD18.hUF62qqlC/V." },
			{ "a",                                  "$2b$10$k87L/MF28Q673VKh8/cPi.",    "$2b$10$k87L/MF28Q673VKh8/cPi.SUl7MU/rWuSiIDDFayrKk/1tBsSQu4u" },
			{ "a",                                  "$2b$12$8NJH3LsPrANStV6XtBakCe",    "$2b$12$8NJH3LsPrANStV6XtBakCez0cKHXVxmvxIlcz785vxAIZrihHZpeS" },
			{ "abc",                                "$2b$06$If6bvum7DFjUnE9p2uDeDu",    "$2b$06$If6bvum7DFjUnE9p2uDeDu0YHzrHM6tf.iqN8.yx.jNN1ILEf7h0i" },
			{ "abc",                                "$2b$08$Ro0CUfOqk6cXEKf3dyaM7O",    "$2b$08$Ro0CUfOqk6cXEKf3dyaM7OhSCvnwM9s4wIX9JeLapehKK5YdLxKcm" },
			{ "abc",                                "$2b$10$WvvTPHKwdBJ3uk0Z37EMR.",    "$2b$10$WvvTPHKwdBJ3uk0Z37EMR.hLA2W6N9AEBhEgrAOljy2Ae5MtaSIUi" },
			{ "abc",                                "$2b$12$EXRkfkdmXn2gzds2SSitu.",    "$2b$12$EXRkfkdmXn2gzds2SSitu.MW9.gAVqa9eLS1//RYtYCmB1eLHg.9q" },
			{ "abcdefghijklmnopqrstuvwxyz",         "$2b$06$.rCVZVOThsIa97pEDOxvGu",    "$2b$06$.rCVZVOThsIa97pEDOxvGuRRgzG64bvtJ0938xuqzv18d3ZpQhstC" },
			{ "abcdefghijklmnopqrstuvwxyz",         "$2b$08$aTsUwsyowQuzRrDqFflhge",    "$2b$08$aTsUwsyowQuzRrDqFflhgekJ8d9/7Z3GV3UcgvzQW3J5zMyrTvlz." },
			{ "abcdefghijklmnopqrstuvwxyz",         "$2b$10$fVH8e28OQRj9tqiDXs1e1u",    "$2b$10$fVH8e28OQRj9tqiDXs1e1uxpsjN0c7II7YPKXua2NAKYvM6iQk7dq" },
			{ "abcdefghijklmnopqrstuvwxyz",         "$2b$12$D4G5f18o7aMMfwasBL7Gpu",    "$2b$12$D4G5f18o7aMMfwasBL7GpuQWuP3pkrZrOAnqP.bmezbMng.QwJ/pG" },
			{ "~!@#$%^&*()      ~!@#$%^&*()PNBFRD", "$2b$06$fPIsBO8qRqkjj273rfaOI.",    "$2b$06$fPIsBO8qRqkjj273rfaOI.HtSV9jLDpTbZn782DC6/t7qT67P6FfO" },
			{ "~!@#$%^&*()      ~!@#$%^&*()PNBFRD", "$2b$08$Eq2r4G/76Wv39MzSX262hu",    "$2b$08$Eq2r4G/76Wv39MzSX262huzPz612MZiYHVUJe/OcOql2jo4.9UxTW" },
			{ "~!@#$%^&*()      ~!@#$%^&*()PNBFRD", "$2b$10$LgfYWkbzEvQ4JakH7rOvHe",    "$2b$10$LgfYWkbzEvQ4JakH7rOvHe0y8pHKF9OaFgwUZ2q7W2FFZmZzJYlfS" },
			{ "~!@#$%^&*()      ~!@#$%^&*()PNBFRD", "$2b$12$WApznUOJfkEGSmYRfnkrPO",    "$2b$12$WApznUOJfkEGSmYRfnkrPOr466oFDCaj4b6HY3EXGvfxm43seyhgC" },
		};

        readonly string[,] _DifferentRevisionTestVectors = {
            { "",                                   "$2a$06$DCq7YPn5Rq63x1Lad4cll.",    "$2b$06$DCq7YPn5Rq63x1Lad4cll.TV4S6ytwfsfvkgY8jIucDrjc8deX1s." },
            { "",                                   "$2b$06$DCq7YPn5Rq63x1Lad4cll.",    "$2b$06$DCq7YPn5Rq63x1Lad4cll.TV4S6ytwfsfvkgY8jIucDrjc8deX1s." },
            { "",                                   "$2x$06$DCq7YPn5Rq63x1Lad4cll.",    "$2b$06$DCq7YPn5Rq63x1Lad4cll.TV4S6ytwfsfvkgY8jIucDrjc8deX1s." },
            { "",                                   "$2y$06$DCq7YPn5Rq63x1Lad4cll.",    "$2b$06$DCq7YPn5Rq63x1Lad4cll.TV4S6ytwfsfvkgY8jIucDrjc8deX1s." },
            { "a",                                  "$2a$06$m0CrhHm10qJ3lXRY.5zDGO",    "$2b$06$m0CrhHm10qJ3lXRY.5zDGO3rS2KdeeWLuGmsfGlMfOxih58VYVfxe" },
            { "a",                                  "$2b$06$m0CrhHm10qJ3lXRY.5zDGO",    "$2b$06$m0CrhHm10qJ3lXRY.5zDGO3rS2KdeeWLuGmsfGlMfOxih58VYVfxe" },
            { "a",                                  "$2x$06$m0CrhHm10qJ3lXRY.5zDGO",    "$2b$06$m0CrhHm10qJ3lXRY.5zDGO3rS2KdeeWLuGmsfGlMfOxih58VYVfxe" },
            { "a",                                  "$2y$06$m0CrhHm10qJ3lXRY.5zDGO",    "$2b$06$m0CrhHm10qJ3lXRY.5zDGO3rS2KdeeWLuGmsfGlMfOxih58VYVfxe" },
            { "abc",                                "$2a$06$If6bvum7DFjUnE9p2uDeDu",    "$2b$06$If6bvum7DFjUnE9p2uDeDu0YHzrHM6tf.iqN8.yx.jNN1ILEf7h0i" },
            { "abc",                                "$2b$06$If6bvum7DFjUnE9p2uDeDu",    "$2b$06$If6bvum7DFjUnE9p2uDeDu0YHzrHM6tf.iqN8.yx.jNN1ILEf7h0i" },
            { "abc",                                "$2x$06$If6bvum7DFjUnE9p2uDeDu",    "$2b$06$If6bvum7DFjUnE9p2uDeDu0YHzrHM6tf.iqN8.yx.jNN1ILEf7h0i" },
            { "abc",                                "$2y$06$If6bvum7DFjUnE9p2uDeDu",    "$2b$06$If6bvum7DFjUnE9p2uDeDu0YHzrHM6tf.iqN8.yx.jNN1ILEf7h0i" },
            { "abcdefghijklmnopqrstuvwxyz",         "$2a$06$.rCVZVOThsIa97pEDOxvGu",    "$2b$06$.rCVZVOThsIa97pEDOxvGuRRgzG64bvtJ0938xuqzv18d3ZpQhstC" },
            { "abcdefghijklmnopqrstuvwxyz",         "$2b$06$.rCVZVOThsIa97pEDOxvGu",    "$2b$06$.rCVZVOThsIa97pEDOxvGuRRgzG64bvtJ0938xuqzv18d3ZpQhstC" },
            { "abcdefghijklmnopqrstuvwxyz",         "$2x$06$.rCVZVOThsIa97pEDOxvGu",    "$2b$06$.rCVZVOThsIa97pEDOxvGuRRgzG64bvtJ0938xuqzv18d3ZpQhstC" },
            { "abcdefghijklmnopqrstuvwxyz",         "$2y$06$.rCVZVOThsIa97pEDOxvGu",    "$2b$06$.rCVZVOThsIa97pEDOxvGuRRgzG64bvtJ0938xuqzv18d3ZpQhstC" },
            { "~!@#$%^&*()      ~!@#$%^&*()PNBFRD", "$2a$06$fPIsBO8qRqkjj273rfaOI.",    "$2b$06$fPIsBO8qRqkjj273rfaOI.HtSV9jLDpTbZn782DC6/t7qT67P6FfO" },
            { "~!@#$%^&*()      ~!@#$%^&*()PNBFRD", "$2b$06$fPIsBO8qRqkjj273rfaOI.",    "$2b$06$fPIsBO8qRqkjj273rfaOI.HtSV9jLDpTbZn782DC6/t7qT67P6FfO" },
            { "~!@#$%^&*()      ~!@#$%^&*()PNBFRD", "$2x$06$fPIsBO8qRqkjj273rfaOI.",    "$2b$06$fPIsBO8qRqkjj273rfaOI.HtSV9jLDpTbZn782DC6/t7qT67P6FfO" },
            { "~!@#$%^&*()      ~!@#$%^&*()PNBFRD", "$2y$06$fPIsBO8qRqkjj273rfaOI.",    "$2b$06$fPIsBO8qRqkjj273rfaOI.HtSV9jLDpTbZn782DC6/t7qT67P6FfO" },
        };

        /**
         * Test method for 'BCrypt.HashPassword(string, string)'
         */
        [Fact]
        public void TestHashPassword()
        {
            Trace.Write("BCrypt.HashPassword(): ");
            var sw = Stopwatch.StartNew();
            for (var i = 0; i < _TestVectors.Length / 3; i++)
            {
                var plain = _TestVectors[i, 0];
                var salt = _TestVectors[i, 1];
                var expected = _TestVectors[i, 2];
                var hashed = BCrypt.HashPassword(plain, salt);
                Assert.Equal(hashed, expected);
                Trace.Write(".");
            }
            Trace.WriteLine(sw.ElapsedMilliseconds);
            Trace.WriteLine("");
        }

        /**
         * Test method for 'BCrypt.GenerateSalt(int)'
         */
        [Fact]
        public void TestGenerateSaltWithWorkFactor()
        {
            Trace.Write("BCrypt.GenerateSalt(log_rounds):");
            for (var i = 4; i <= 12; i++)
            {
                Trace.Write(" " + i + ":");
                for (var j = 0; j < _TestVectors.Length / 3; j++)
                {
                    var plain = _TestVectors[j, 0];
                    var salt = BCrypt.GenerateSalt(i);
                    var hashed1 = BCrypt.HashPassword(plain, salt);
                    var hashed2 = BCrypt.HashPassword(plain, hashed1);
                    Assert.Equal(hashed1, hashed2);
                    Trace.Write(".");
                }
            }
            Trace.WriteLine("");
        }

        [Fact]
        public void TestGenerateSaltWithMaxWorkFactor()
        {
            Trace.Write("BCrypt.GenerateSalt(31):");
            for (var j = 0; j < _TestVectors.Length / 3; j++)
            {
                var plain = _TestVectors[j, 0];
                var salt = BCrypt.GenerateSalt(31);
                var hashed1 = BCrypt.HashPassword(plain, salt);
                var hashed2 = BCrypt.HashPassword(plain, hashed1);
                Assert.Equal(hashed1, hashed2);
                Trace.Write(".");
            }
            Trace.WriteLine("");
        }

        /**
         * Test method for 'BCrypt.GenerateSalt()'
         */
        [Fact]
        public void TestGenerateSalt()
        {
            Trace.Write("BCrypt.GenerateSalt(): ");
            for (var i = 0; i < _TestVectors.Length / 3; i++)
            {
                var plain = _TestVectors[i, 0];
                var salt = BCrypt.GenerateSalt();
                var hashed1 = BCrypt.HashPassword(plain, salt);
                var hashed2 = BCrypt.HashPassword(plain, hashed1);
                Assert.Equal(hashed1, hashed2);
                Trace.Write(".");
            }
            Trace.WriteLine("");
        }

        /**
         * Test method for 'BCrypt.VerifyPassword(string, string)'
         * expecting success
         */
        [Fact]
        public void TestVerifyPasswordSuccess()
        {
            Trace.Write("BCrypt.Verify w/ good passwords: ");
            for (var i = 0; i < _TestVectors.Length / 3; i++)
            {
                var plain = _TestVectors[i, 0];
                var expected = _TestVectors[i, 2];
                Assert.True(BCrypt.Verify(plain, expected));
                Trace.Write(".");
            }
            Trace.WriteLine("");
        }

        /**
 * Test method for 'BCrypt.VerifyPassword(string, string)'
 * expecting success
 */
        [Fact]
        public void TestVerifyPasswordWithDifferentRevisionsSuccess()
        {
            Trace.Write("BCrypt.Verify with good passwords from revisions a, x and y: ");
            for (var i = 0; i < _DifferentRevisionTestVectors.Length / 3; i++)
            {
                var plain = _DifferentRevisionTestVectors[i, 0];
                var expected = _DifferentRevisionTestVectors[i, 2];
                Assert.True(BCrypt.Verify(plain, expected));
                Trace.Write(".");
            }
            Trace.WriteLine("");
        }

        /**
         * Test method for 'BCrypt.VerifyPassword(string, string)'
         * expecting failure
         */
        [Fact]
        public void TestVerifyPasswordFailure()
        {
            Trace.Write("BCrypt.Verify w/ bad passwords: ");
            for (var i = 0; i < _TestVectors.Length / 3; i++)
            {
                var brokenIndex = (i + 4) % (_TestVectors.Length / 3);
                var plain = _TestVectors[i, 0];
                var expected = _TestVectors[brokenIndex, 2];
                Assert.False(BCrypt.Verify(plain, expected));
                Trace.Write(".");
            }
            Trace.WriteLine("");
        }

        /**
         * Test for correct hashing of non-US-ASCII passwords
         */
        [Fact]
        public void TestInternationalChars()
        {
            Trace.Write("BCrypt.HashPassword w/ international chars: ");
            var pw1 = "ππππππππ";
            var pw2 = "????????";

            var h1 = BCrypt.HashPassword(pw1, BCrypt.GenerateSalt());
            Assert.False(BCrypt.Verify(pw2, h1));
            Trace.Write(".");

            var h2 = BCrypt.HashPassword(pw2, BCrypt.GenerateSalt());
            Assert.False(BCrypt.Verify(pw1, h2));
            Trace.Write(".");
            Trace.WriteLine("");
        }
    }
}
