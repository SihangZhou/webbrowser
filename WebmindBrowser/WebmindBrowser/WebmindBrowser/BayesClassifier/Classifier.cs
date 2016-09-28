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
            //  //��������������̾�����P(f1=x1,f2=x2...fn=xn|s=si)=P(f1=x1|s=si)*P(f2=x2|s=si)....*P(fn=xn|s=si)*P(s=si)
			Dictionary<string, double> score = new Dictionary<string, double>();
			foreach (KeyValuePair<string, ICategory> cat in m_Categories)
			{
				score.Add(cat.Value.Name, 0.0);
			}

			EnumerableCategory words_in_file = new EnumerableCategory("", m_ExcludedWords);
			words_in_file.TeachCategory(tr);//����ĺ�����ʲô��m_Categories���Ѿ���������з����ͳ�ư������naive bayes������������ˣ����������ȡ�������ı�������(�����Դ�)

			foreach (KeyValuePair<string, int> kvp1 in words_in_file)
			{
                String words_in_predictionfile = kvp1.Key;//��P(f1=x1|s=si)������words_in_predictionfile����x1
				foreach (KeyValuePair<string, ICategory> kvp in m_Categories)
				{
					ICategory cat = kvp.Value;
                    int count = cat.GetPhraseCount(words_in_predictionfile);//����ÿ�ֵ�words_in_predictionfile�Ǵ������ı���������
					if (0 < count)
					{
						score[cat.Name] += System.Math.Log((double)count / (double)cat.TotalWords);//˵���׻��ǰ����(cat1��cat2...)�ȷ���ͳ�Ƹ���,��������P(f1=x1|s=si)
					}
					else//count==0,��0.01����0��ֹlog������
					{
						score[cat.Name] += System.Math.Log(0.01 / (double)cat.TotalWords);
					}
                    System.Diagnostics.Trace.WriteLine(words_in_predictionfile + "(" +
						cat.Name + ")" + score[cat.Name]);
				}


			}
			foreach (KeyValuePair<string, ICategory> kvp in m_Categories)//��������д�ú�û��˼�����ǰ�cat1+cat2+cat3+cat4+cat5��Ϊ�ܺͣ�Ȼ��ֱ���ÿ�����ȥ��������ܺͣ�Ȼ��ȡ����
			{//����Ҫ�ģ�����ĺ������治��⣬ǩ���ǰ�ÿ�����ĵ��ʴ��ڸ�����count��Ȼ��ȡ��������ӣ�Ȼ���ּ���һ�����������֮��ȡ����
                //��������ˣ��������������ʰ�
				ICategory cat = kvp.Value;
				score[cat.Name] += System.Math.Log((double)cat.TotalWords / (double)this.CountTotalWordsInCategories());
			}
            //��������������̾�����P(f1=x1,f2=x2...fn=xn|s=si)=P(f1=x1|s=si)*P(f2=x2|s=si)....*P(fn=xn|s=si)*P(s=si)
			return score;
		}



	}
}
