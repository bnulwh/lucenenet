﻿using Lucene.Net.Analysis.Util;
using NUnit.Framework;
using System.IO;

namespace Lucene.Net.Analysis.Bg
{
    /*
	 * Licensed to the Apache Software Foundation (ASF) under one or more
	 * contributor license agreements.  See the NOTICE file distributed with
	 * this work for additional information regarding copyright ownership.
	 * The ASF licenses this file to You under the Apache License, Version 2.0
	 * (the "License"); you may not use this file except in compliance with
	 * the License.  You may obtain a copy of the License at
	 *
	 *     http://www.apache.org/licenses/LICENSE-2.0
	 *
	 * Unless required by applicable law or agreed to in writing, software
	 * distributed under the License is distributed on an "AS IS" BASIS,
	 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	 * See the License for the specific language governing permissions and
	 * limitations under the License.
	 */

    /// <summary>
    /// Simple tests to ensure the Bulgarian stem filter factory is working.
    /// </summary>
    public class TestBulgarianStemFilterFactory : BaseTokenStreamFactoryTestCase
    {
        /// <summary>
        /// Ensure the filter actually stems text.
        /// </summary>
        [Test]
        public virtual void TestStemming()
        {
            TextReader reader = new StringReader("компютри");
            Tokenizer tokenizer = new MockTokenizer(reader, MockTokenizer.WHITESPACE, false);
            TokenStream stream = TokenFilterFactory("BulgarianStem").Create(tokenizer);
            AssertTokenStreamContents(stream, new string[] { "компютр" });
        }

        /// <summary>
        /// Test that bogus arguments result in exception </summary>
        [Test]
        public virtual void TestBogusArguments()
        {
            try
            {
                TokenFilterFactory("BulgarianStem", "bogusArg", "bogusValue");
                fail();
            }
            catch (System.ArgumentException expected)
            {
                assertTrue(expected.Message.Contains("Unknown parameters"));
            }
        }
    }
}