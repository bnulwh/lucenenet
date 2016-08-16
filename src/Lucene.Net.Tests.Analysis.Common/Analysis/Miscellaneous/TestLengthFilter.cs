﻿using Lucene.Net.Analysis.Core;
using Lucene.Net.Util;
using NUnit.Framework;
using System;
using System.IO;

namespace Lucene.Net.Analysis.Miscellaneous
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

    public class TestLengthFilter : BaseTokenStreamTestCase
    {

        [Test]
        public virtual void TestFilterNoPosIncr()
        {
            TokenStream stream = new MockTokenizer(new StringReader("short toolong evenmuchlongertext a ab toolong foo"), MockTokenizer.WHITESPACE, false);
            LengthFilter filter = new LengthFilter(LuceneVersion.LUCENE_43, false, stream, 2, 6);
            AssertTokenStreamContents(filter, new string[] { "short", "ab", "foo" }, new int[] { 1, 1, 1 });
        }

        [Test]
        public virtual void TestFilterWithPosIncr()
        {
            TokenStream stream = new MockTokenizer(new StringReader("short toolong evenmuchlongertext a ab toolong foo"), MockTokenizer.WHITESPACE, false);
            LengthFilter filter = new LengthFilter(TEST_VERSION_CURRENT, stream, 2, 6);
            AssertTokenStreamContents(filter, new string[] { "short", "ab", "foo" }, new int[] { 1, 4, 2 });
        }

        [Test]
        public virtual void TestEmptyTerm()
        {
            Analyzer a = new AnalyzerAnonymousInnerClassHelper(this);
            CheckOneTerm(a, "", "");
        }

        private class AnalyzerAnonymousInnerClassHelper : Analyzer
        {
            private readonly TestLengthFilter outerInstance;

            public AnalyzerAnonymousInnerClassHelper(TestLengthFilter outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            public override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
            {
                Tokenizer tokenizer = new KeywordTokenizer(reader);
                return new TokenStreamComponents(tokenizer, new LengthFilter(TEST_VERSION_CURRENT, tokenizer, 0, 5));
            }
        }

        /// <summary>
        /// checking the validity of constructor arguments
        /// </summary>
        [Test]
        [ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
        public virtual void TestIllegalArguments()
        {
            new LengthFilter(TEST_VERSION_CURRENT, new MockTokenizer(new StringReader("accept only valid arguments")), -4, -1);
        }
    }
}