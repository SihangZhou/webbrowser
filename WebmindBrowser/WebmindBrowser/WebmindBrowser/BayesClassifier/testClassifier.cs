using System;
using System.Collections.Generic;
using System.IO;
using WebmindBrowser.CSegment;
using System.Text;
using WebmindBrowser.CSegment;

namespace WebmindBrowser.BayesClassifier
{
    class testClassifier
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]

        Classifier m_Classifier = new Classifier();
        const String token = " ";
        const String CLASSIFIER_BEGIN = "######";
        const String CLASSIFIER_END = "******";
        const int data_count_per_time = 10;//定义训练结果里每行10对数据
        Dictionary<String, int> Content_INT = new Dictionary<string, int>();
        Dictionary<String, int> Emotion_INT = new Dictionary<string, int>();
        SortedDictionary<string, ICategory> m_Categories_emotion;
        SortedDictionary<string, ICategory> m_Categories_content;
        string train_content2 = @"..\..\train_content2.txt";
        string train_emotion2 = @"..\..\train_emotion2.txt";
        private static testClassifier Singleton=null;

        private testClassifier()//这里使用private作为访问限制是非常必要的，因为单态模式需要如此
        {
            //1 : 财经 //2 : IT //3 : 健康 //4 : 体育
            //5 : 旅游 //6 : 教育 //7 : 招聘 //8 : 文化 //9 : 军事
            
            Content_INT["财经"] = 1;
            Content_INT["科技"] = 2;
            Content_INT["健康"] = 3;
            Content_INT["体育"] = 4;
            Content_INT["教育"] = 6;
            Content_INT["旅游"] = 5;
            Content_INT["招聘"] = 7;
            Content_INT["文化"] = 8;
            Content_INT["军事"] = 9;
            Emotion_INT["愤怒"] = 0;
            Emotion_INT["搞笑"] = 1;
            Emotion_INT["难过"] = 2;
            Emotion_INT["无聊"] = 3;
            Emotion_INT["新奇"] = 4;
            m_Categories_content = loadData(train_content2);//这个时间需要近0.18秒，这是不可饶恕的
            m_Categories_emotion = loadData(train_emotion2);//这个时间需要近0.08秒，这是不可饶恕的
        }
        //<summary>
        //我尝试使用单态模式，正好用了下原来设计模式里的内容,因为一次浏览器启动我只想用一次
        //</summary>
        public static testClassifier getSingleObject()
        {
            if (Singleton == null)
                Singleton = new testClassifier();
            return Singleton;
        }
        private void storeData(string filename)//这里我用六个#号作为一个训练类别的标志
        {

            SortedDictionary<string, ICategory> cats = m_Classifier.m_Categories;//
            using (StreamWriter sw = File.CreateText(filename))
            {
                foreach (KeyValuePair<string, ICategory> kvp in cats)//
                {
                    string catName = kvp.Key;
                    sw.WriteLine(CLASSIFIER_BEGIN + token + catName);
                    Category cat = (Category)kvp.Value;
                    foreach (KeyValuePair<string,int> kvp1 in cat.m_Phrases)
                    {
                        string name = kvp1.Key;
                        if (kvp1.Value > 1 && name[0] > '9' || name[0] < '0')//我想去掉数组
                            sw.WriteLine(name + token + kvp1.Value);
                    }
                    sw.WriteLine(CLASSIFIER_END + token + catName);
                }
                sw.Close();
            }
        }

        //<summary>
        //以一行一对数据的方式读取字典,最低时间是1.40s
        //待我改过SortedDictionary->Dictionary后，可以做到0.276s,而且分类是正确的
        //</summary>
        /*
        public SortedDictionary<string, ICategory> loadData(string filename)
        {
            SortedDictionary<string, ICategory> sd = new SortedDictionary<string, ICategory>();
            if (!File.Exists(filename))
            {
                Console.WriteLine("{0} does not exist.", filename);
                return null;
            }
            using (StreamReader sr = File.OpenText(filename))
            {
                String input, input1;
                while ((input = sr.ReadLine()) != null)
                {
                    String[] words = input.Split(' ');
                    if (words[0] == CLASSIFIER_BEGIN)
                    {
                        Category cat = new Category();
                        Dictionary<string,int> m_Phrases=new Dictionary<string,int>();
                        cat.Name = words[1];
                        int total = 0;
                        double totalTime = 0;  
                       
                        while ((input1 = sr.ReadLine()) != null)
                        {
                            String[] wordss = input1.Split(' ');
                            if (wordss[0] == CLASSIFIER_END)
                                break;
                            m_Phrases[wordss[0]] = Int32.Parse(wordss[1]);//估计这里能优化点
                            total += Int32.Parse(wordss[1]);
                        }
                        cat.m_Phrases = m_Phrases;
                        cat.TotalWords = total;
                        sd[words[1]] = cat;
                        
                    }
                   
            
                }
                sr.Close();
            }
            return sd;
        }
         */
       
        //<summary>
        //以多对数据放一行的方式读取字典，最低要
        //200:1.288s 300:1.322s 100;1.290 150:1.283
        //我把各类别总数算出来后，还需要1.26s
        //经过多次艰苦卓绝的测试，我终于发现，耗费时间的元凶在于 cat.m_Phrases[words[i]]，几乎95%的时间耗费在这个上
        //改进一：我在loadData函数里做了一个SortedDictionary，然后每次直接用本地的SortedDictionary赋值，然后等全部完成后，再在循环外赋值给category那个类里的SortedDictionary,这样可以减少到0.73s
        //改进二：我考察整个程序后，发现使用SoretedDictonary根本没有必要，而SortedDictionary非常耗费时间(排序O(nlog(n)))，所以我直接改成Dictionary,现在改进到了0.16s。哈哈哈哈
        
        //</summary>
        public SortedDictionary<string, ICategory> loadData(string filename)
        {
            SortedDictionary<string, ICategory> sd = new SortedDictionary<string, ICategory>();
            Dictionary<string, int> m_Phrases=null;
            if (!File.Exists(filename))
            {
                Console.WriteLine("{0} does not exist.", filename);
                return null;
            }
            using (StreamReader sr = File.OpenText(filename))
            {
                String input;
                //int total=0;
                Category cat=null;
                double totalTime = 0;
                while ((input = sr.ReadLine()) != null)//每次读入data_count_per_time对数据(除了最后一行) 表示类别时另外起一行
                {
                    String[] words = input.Split(' ');
                    if (words.Length != 2)
                    {
                        for (int i = 0; i < words.Length; i += 2)
                        {
                            m_Phrases[words[i]] = Int32.Parse(words[i + 1]);//估计这里能优化点
                        }
                    }
                    else if (words[0]==CLASSIFIER_BEGIN)
                        {
                            cat = new Category();
                            cat.Name = words[1];
                            m_Phrases = new Dictionary<string, int>();
                        }
                        else if (words[0]==CLASSIFIER_END)
                        {
                            cat.TotalWords = Int32.Parse(words[1]);
                            cat.m_Phrases = m_Phrases;
                            sd[cat.Name] = cat;
                        }
                        else
                        {
                            m_Phrases[words[0]] = Int32.Parse(words[1]);  
                        }
                }
                
                sr.Close();
            }
            return sd;
        }
         
        private Dictionary<string, double> makePrediction(String fileContent, SortedDictionary<string, ICategory> sd)
        {
           ExcludedWords m_ExcludedWords = new ExcludedWords();
            m_ExcludedWords.InitDefault();
            EnumerableCategory words_in_file = new EnumerableCategory("", m_ExcludedWords);
            words_in_file.TeachCategory(fileContent);//理解naive bayes后，我终于理解了，这个就是提取待分类文本的特征(即属性词)
            //万事俱备，只欠计算
            Dictionary<string, double> score = new Dictionary<string, double>();
            foreach (KeyValuePair<string, ICategory> cat in sd)
            {
                score.Add(cat.Key, 0.0);
            }


            foreach (KeyValuePair<string, int> kvp1 in words_in_file)
            {
               // PhraseCount pc_in_file = kvp1.Value;
                String words_in_predictionfile = kvp1.Key;//算P(f1=x1|s=si)，其中words_in_predictionfile就是x1
                foreach (KeyValuePair<string, ICategory> kvp in sd)
                {
                    ICategory cat = kvp.Value;
                    int count = cat.GetPhraseCount(words_in_predictionfile);//这里每轮的words_in_predictionfile是待分类文本的特征词
                    if (0 < count)
                    {
                        score[kvp.Key] += System.Math.Log((double)count / (double)cat.TotalWords);//说到底还是按类别(cat1、cat2...)等分类统计概率,就是连乘P(f1=x1|s=si)
                    }
                    else//count==0,用0.01代替0防止log无意义
                    {
                        score[kvp.Key] += System.Math.Log(0.01 / (double)cat.TotalWords);
                    }
                    System.Diagnostics.Trace.WriteLine(words_in_predictionfile + "(" +
                        kvp.Key + ")" + score[kvp.Key]);
                }


            }
            int total = 0;
            foreach (Category cat in sd.Values)
            {
                total += cat.TotalWords;
            }

            foreach (KeyValuePair<string, ICategory> kvp in sd)//觉得这里写得很没意思，就是把cat1+cat2+cat3+cat4+cat5作为总和，然后分别用每个类别去除以这个总和，然后取对数
            {//更重要的，这里的含义我真不理解，签名是把每个类别的单词处于该类别的count，然后取对数，相加，然后又加上一个类别除以类别之和取对数
                //现在理解了，这就是算先验概率啊
                ICategory cat = kvp.Value;
                score[kvp.Key] += System.Math.Log((double)cat.TotalWords / (double)total);
            }
            return score;

        }

        private void TeachCategory(String cla, String dir)
        {
            String[] files = Directory.GetFiles(dir);
            for (int i = 0; i < files.Length; i++)
            {
                m_Classifier.TeachCategory(cla, new System.IO.StreamReader(files[i], Encoding.UTF8));
            }

        }
        public void testTime()
        {
            string prediction = @"..\..\prediction.txt";
            string train_content1 = @"..\..\train_content1.txt";
            string train_content2 = @"..\..\train_content2.txt";
            string train_emotion1 = @"..\..\train_emotion1.txt";
            string train_emotion2 = @"..\..\train_emotion2.txt";

            String fileContent=CSegment.SegmentApi.SegmentAPI(ref prediction);

            //下面这个是用来训练模型的，由于在后台已经训练好了，现在在这里不必用了
            /*
            String t_dir = @"..\..\Reduced";
            String[] dirs = Directory.GetDirectories(t_dir);
            for (int i = 0; i < dirs.Length; i++)
            {
                TeachCategory(dirs[i], dirs[i]);
            }
            storeData(trainresults);
            */
 
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start();//开始计时
            SortedDictionary<string, ICategory> m_Categories = loadData(train_emotion2);//这个时间需要近两秒，这是不可饶恕的
            //stopwatch.Stop();
            //TimeSpan timespan = stopwatch.Elapsed;
            //double minutes = timespan.TotalMinutes;  // 总分钟
            //double seconds = timespan.TotalSeconds;  //  总秒数
            //double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
            //Console.WriteLine("loadData总秒数:" + seconds);
            
            System.Diagnostics.Stopwatch stopwatch1 = new System.Diagnostics.Stopwatch();
            stopwatch1.Start();//开始计时
            Dictionary<string, double> score = makePrediction(fileContent, m_Categories);
            stopwatch1.Stop();
            TimeSpan timespan1 = stopwatch1.Elapsed;
            double minutes1 = timespan1.TotalMinutes;  // 总分钟
            double seconds1 = timespan1.TotalSeconds;  //  总秒数
            double milliseconds1 = timespan1.TotalMilliseconds;  //  总毫秒数
            Console.WriteLine("makePrediction总秒数:" + seconds1);
            //Dictionary<string, double> score = m_Classifier.Classify(new System.IO.StreamReader(prediction));

            foreach (string c in score.Keys)
            {
                Console.WriteLine(c + ":" + score[c].ToString());
            }
             
        }

        
        //<summary>
        //内容分类借口:传进参数是已经分好词的内容
        //</summary>
        public int contentClassifierAPI(String content)
        {
           // string train_content2 = @"..\..\train_content2.txt";
            String fileContent = CSegment.SegmentApi.SegmentAPI_content(ref content);
            //SortedDictionary<string, ICategory> m_Categories_content = loadData(train_content2);//这个时间需要近0.18秒，这是不可饶恕的
            Dictionary<string, double> score = makePrediction(fileContent, m_Categories_content);
            
            double max=-10000000000000;
            string flag=null;
            foreach (string c in score.Keys)
            {
                if (score[c]>max)
                {
                    max=score[c];
                    flag=c;
                }
            }
            return Content_INT[flag];
        }

        //0 : 愤怒 //1 : 搞笑 //2 : 难过 //3 : 无聊 //4 : 新奇
        //<summary>
        //情感内容分类借口
        //</summary>
        public int EmotionClassifierAPI(String emotionContent)
        {
            //string train_emotion2 = @"..\..\train_emotion2.txt";
            String fileContent = CSegment.SegmentApi.SegmentAPI_content(ref emotionContent);
            //SortedDictionary<string, ICategory> m_Categories_emotion = loadData(train_emotion2);//这个时间需要近0.08秒，这是不可饶恕的
            Dictionary<string, double> score = makePrediction(fileContent, m_Categories_emotion);

         
            
            double max=-10000000000000;
            string flag=null;
            foreach (string c in score.Keys)
            {
                if (score[c]>max)
                {
                    max=score[c];
                    flag=c;
                }
            }
            return Emotion_INT[flag];
        }
    }
}