/* ====================================================================
 * Copyright (c) 2006 Erich Guenther (erich_guenther@hotmail.com)
 * All rights reserved.
 *                       
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer. 
 *
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in
 *    the documentation and/or other materials provided with the
 *    distribution.
 * 
 * 3. The name of the author(s) must not be used to endorse or promote 
 *    products derived from this software without prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY
 * EXPRESSED OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE AUTHOR OR
 * ITS CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
 * OF THE POSSIBILITY OF SUCH DAMAGE. 
 */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebmindBrowser.BayesClassifier
{
    
	/// <summary>
	/// Represents a Enumerable Bayesian category - that is contains a list of phrases with their occurence counts <\summary>
	public class EnumerableCategory : Category, IEnumerable<KeyValuePair<string, int>>
	{
		public EnumerableCategory(string Cat, ExcludedWords Excluded) : base(Cat, Excluded)
		{
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
		{
			return m_Phrases.GetEnumerator();
		}
 
	}


	/// <summary>
	/// Represents a Bayesian category - that is contains a list of phrases with their occurence counts <\summary>
	public class Category : BayesClassifier.ICategory
	{
		//public System.Collections.Generic.SortedDictionary<string, PhraseCount> m_Phrases;//这个我想改了它
        //public SortedDictionary<string, int> m_Phrases;//这个我想改了它
        public Dictionary<string, int> m_Phrases;//为了减少时间，而且我觉得根本不需要在这里使用SortedDictionary
		int m_TotalWords;
		string m_Name;
		ExcludedWords m_Excluded;


		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}
		/// <value>
		/// Gets total number of word occurences in this category</value>
		public int TotalWords
		{
			get { return m_TotalWords; }
			set { m_TotalWords = value; }
		}

        public Category() { m_Phrases = new Dictionary<string, int>(); }
		public Category(string cat, ExcludedWords excluded)
		{
			m_Phrases = new Dictionary<string, int>();
			m_Excluded = excluded;
			m_Name = cat;
		}

		/// <summary>
		/// Gets a Count for Phrase or 0 if not present<\summary>
		public int GetPhraseCount(string phrase)
		{/*
			PhraseCount pc;
			if (m_Phrases.TryGetValue(phrase, out pc))
				return pc.Count;
			else
				return 0;
          * */
            int n;
            if (m_Phrases.TryGetValue(phrase, out n))
                return n;
            return 0;
		}

		/// <summary>
		/// Reset all trained data<\summary>
		public void Reset()
		{
			m_TotalWords = 0;
			m_Phrases.Clear();
		}

		Dictionary<string, int> Phrases
		{
			get { return m_Phrases; }
			//set { m_Phrases = value; }
		}

		/// <summary>
		/// Trains this Category from a file<\summary>
		public void TeachCategory(System.IO.TextReader reader)
		{
			//System.Diagnostics.Debug.Assert(line.Length < 512);
            //Regex re = new Regex(@"(\w+)\W*", RegexOptions.Compiled);//\W匹配任意不是字母，数字，下划线，汉字的字符
            String pattern = @"(\w+)";
            Regex re = new Regex(pattern, RegexOptions.Compiled);
            string line;////\w匹配字母或数字或下划线或汉字
            //终于明白了，为什么结果集里没小数了，是因为Regex里设置的匹配格式没有小数,把小数拆分了
			while (null != (line = reader.ReadLine()))
			{
				Match m = re.Match(line);
                m = m.NextMatch();//因为每行数据第一个是类别
				while (m.Success)
				{
					string word = m.Groups[1].Value;
                    TeachPhrase(word);//只要键用作 SortedDictionary<TKey, TValue> 中的键，它们就必须是不可变的。 SortedDictionary<TKey, TValue> 中的每个键必须是唯一的。 键不能为 null，但是如果值类型 TValue 为引用类型，该值则可以为空。
					m = m.NextMatch();
				}
			}
		}

        //<summary>
        //
        //</summary>
        public void TeachCategory(String fileContent)
        {
            //System.Diagnostics.Debug.Assert(line.Length < 512);
            //Regex re = new Regex(@"(\w+)\W*", RegexOptions.Compiled);//\W匹配任意不是字母，数字，下划线，汉字的字符
            String pattern = @"(\w+)";
            Regex re = new Regex(pattern, RegexOptions.Compiled);
          ////\w匹配字母或数字或下划线或汉字
            //终于明白了，为什么结果集里没小数了，是因为Regex里设置的匹配格式没有小数,把小数拆分了
            Match m = re.Match(fileContent);
            m = m.NextMatch();//因为每行数据第一个是类别
            while (m.Success)
            {
                string word = m.Groups[1].Value;
                TeachPhrase(word);//只要键用作 SortedDictionary<TKey, TValue> 中的键，它们就必须是不可变的。 SortedDictionary<TKey, TValue> 中的每个键必须是唯一的。 键不能为 null，但是如果值类型 TValue 为引用类型，该值则可以为空。
                m = m.NextMatch();
            }
            
        }

		/// <summary>
		/// Trains this Category from a word or phrase array<\summary>
		/// <seealso cref="DePhrase(string)">
		/// See DePhrase </seealso>
		public void TeachPhrases(string[] words)
		{
			foreach (string word in words)
			{
				TeachPhrase(word);
			}
		}

		/// <summary>
		/// Trains this Category from a word or phrase<\summary>
		/// <seealso cref="DePhrase(string)">
		/// See DePhrase </seealso>
		public void TeachPhrase(string rawPhrase)//所谓训练数据，不过是统计某个单词有多少个而已
		{
			if ((null != m_Excluded) && (m_Excluded.IsExcluded(rawPhrase)))//IsExcluded用来检查rawPhrase是否是规定的那些无关的词，如果是无关的词，直接返回，不必处理
				return;

			PhraseCount pc;
			string Phrase = DePhrase(rawPhrase);
            int n;
			if (!m_Phrases.TryGetValue(Phrase, out n))//m_Phrase是个SortedDictionary 这里是没有时的情况
			{
				m_Phrases.Add(Phrase, 1);
			}else
            {
                m_Phrases[Phrase] = n + 1;//重新赋值
            }
			
			m_TotalWords++;
		}

        static Regex ms_PhraseRegEx = new Regex(@"\W", RegexOptions.Compiled);
        //匹配字母或数字或下划线或汉字
        //匹配任意不是字母，数字，下划线，汉字的字符

		/// <summary>
		/// Checks if a string is a phrase (that is a string with whitespaces)<\summary>
		/// <returns>
		/// true or false</returns>
		/// <seealso cref="DePhrase(string)">
		/// See DePhrase </seealso>
		public static bool CheckIsPhrase(string s)
		{
			return ms_PhraseRegEx.IsMatch(s);
		}

		/// <summary>
		/// Trnasforms a string into a phrase (that is a string with whitespaces)<\summary>
		/// <returns>
		/// dephrased string</returns>
		/// <remarks>
		/// if something like "lone Rhino" is considered a sinlge Phrase, then our word matching algorithm is 
		/// is transforming it into a single Word "lone Rhino" -> "loneRhino"
		/// Currently this feature is not used!
		/// </remarks>
		public static string DePhrase(string s)
		{
			return ms_PhraseRegEx.Replace(s, @"");//应该是去掉""?
		}

	}
}
