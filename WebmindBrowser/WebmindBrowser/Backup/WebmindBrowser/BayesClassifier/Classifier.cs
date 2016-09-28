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
using System.Text;

namespace WebmindBrowser.BayesClassifier
{
	/// <summary>
	/// Naive Bayesian classifier</summary>
	/// <remarks>
	/// It suppports exclusion of words but not Phrases 
	/// </remarks>
	public class Classifier : BayesClassifier.IClassifier
	{
		public SortedDictionary<string, ICategory> m_Categories;
		ExcludedWords m_ExcludedWords;
		
		public Classifier()
		{
			m_Categories = new SortedDictionary<string, ICategory>();
			m_ExcludedWords = new ExcludedWords();
			m_ExcludedWords.InitDefault();
		}

		/// <summary>
		/// Gets total number of word occurences over all categories</summary>
		int CountTotalWordsInCategories()
		{
			int total = 0;
			foreach (Category cat in m_Categories.Values)
			{
				total += cat.TotalWords;
			}
			return total;
		}

		/// <summary>
		/// Gets or creates a category</summary>
		ICategory GetOrCreateCategory(string cat)
		{
			ICategory c;
			if (!m_Categories.TryGetValue(cat, out c))
			{
				c = new Category(cat, m_ExcludedWords);
				m_Categories.Add(cat, c);
			}
			return c;
		}

		/// <summary>
		/// Trains this Category from a word or phrase<\summary>
		public void TeachPhrases(string cat, string[] phrases)
		{
			GetOrCreateCategory(cat).TeachPhrases(phrases);
		}

		/// <summary>
		/// Trains this Category from a word or phrase<\summary>
		public void TeachCategory(string cat, System.IO.TextReader tr)
		{
			GetOrCreateCategory(cat).TeachCategory(tr);
		}

		/// <summary>
		/// Classifies a text<\summary>
	    /// <returns>
		/// returns classification values for the text, the higher, the better is the match.</returns>
		public Dictionary<string, double> Classify(System.IO.StreamReader tr)
		{
            //  //所以这个整个过程就是算P(f1=x1,f2=x2...fn=xn|s=si)=P(f1=x1|s=si)*P(f2=x2|s=si)....*P(fn=xn|s=si)*P(s=si)
			Dictionary<string, double> score = new Dictionary<string, double>();
			foreach (KeyValuePair<string, ICategory> cat in m_Categories)
			{
				score.Add(cat.Value.Name, 0.0);
			}

			EnumerableCategory words_in_file = new EnumerableCategory("", m_ExcludedWords);
			words_in_file.TeachCategory(tr);//这个的含义是什么？m_Categories里已经算好了所有分类的统计啊。理解naive bayes后，我终于理解了，这个就是提取待分类文本的特征(即属性词)

			foreach (KeyValuePair<string, int> kvp1 in words_in_file)
			{
                String words_in_predictionfile = kvp1.Key;//算P(f1=x1|s=si)，其中words_in_predictionfile就是x1
				foreach (KeyValuePair<string, ICategory> kvp in m_Categories)
				{
					ICategory cat = kvp.Value;
                    int count = cat.GetPhraseCount(words_in_predictionfile);//这里每轮的words_in_predictionfile是待分类文本的特征词
					if (0 < count)
					{
						score[cat.Name] += System.Math.Log((double)count / (double)cat.TotalWords);//说到底还是按类别(cat1、cat2...)等分类统计概率,就是连乘P(f1=x1|s=si)
					}
					else//count==0,用0.01代替0防止log无意义
					{
						score[cat.Name] += System.Math.Log(0.01 / (double)cat.TotalWords);
					}
                    System.Diagnostics.Trace.WriteLine(words_in_predictionfile + "(" +
						cat.Name + ")" + score[cat.Name]);
				}


			}
			foreach (KeyValuePair<string, ICategory> kvp in m_Categories)//觉得这里写得很没意思，就是把cat1+cat2+cat3+cat4+cat5作为总和，然后分别用每个类别去除以这个总和，然后取对数
			{//更重要的，这里的含义我真不理解，签名是把每个类别的单词处于该类别的count，然后取对数，相加，然后又加上一个类别除以类别之和取对数
                //现在理解了，这就是算先验概率啊
				ICategory cat = kvp.Value;
				score[cat.Name] += System.Math.Log((double)cat.TotalWords / (double)this.CountTotalWordsInCategories());
			}
            //所以这个整个过程就是算P(f1=x1,f2=x2...fn=xn|s=si)=P(f1=x1|s=si)*P(f2=x2|s=si)....*P(fn=xn|s=si)*P(s=si)
			return score;
		}



	}
}
