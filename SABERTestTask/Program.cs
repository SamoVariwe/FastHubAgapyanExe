using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace SABERTestTask
{
    class Program
    {
        private  static  string[] testData = {"a","b","c","d","e","f","g"};
        private static string filePath = @"E:\c#Projects\SABERTestTask\SABERTestTask\serializationTest.txt";
        static void Main(string[] args)
        {
            ListRandom serializableList = new ListRandom();
            ListRandom deserializedList=new ListRandom();
            foreach (string data in testData)
            {
                serializableList.Add(data);
            }

            using (FileStream fs = new FileStream(filePath,FileMode.Open,FileAccess.Write))
            {
                serializableList.Serialize(fs);

            }
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                deserializedList.Deserialize(fs);
            }

            Console.WriteLine("Serializable list: ");
            serializableList.Print();
            Console.WriteLine("De-serializable list: ");
            deserializedList.Print();

            
            
                
        }
    }

     class ListNode
    {
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random; 
        public string Data;
        public ListNode(string data)
        {
            this.Data = data;
        }
    }


    class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(Stream s)
        {
            
            s.SetLength(0);
            foreach (ListNode node in this)
            {
                int randomNodeNumber = 0;
                ListNode currentRandomNode = node.Random;
                foreach (ListNode randomNode in this)
                {
                    if (currentRandomNode == randomNode) break;
                    randomNodeNumber++;
                }
                string data = node.Data;
                string tail;
                if (node==Tail)
                {
                     tail = "." + randomNodeNumber;
                }
                else
                {
                    tail = "." + randomNodeNumber + '\n';
                }
                
                data = data + tail;

                s.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
            }
                
            

        }

        public void Deserialize(Stream s)
        {
            string[] dataStrings;
            
            int fileLength = (int)s.Length;
            byte[] readenData = new byte[fileLength];
            s.Read(readenData, 0, fileLength);
            dataStrings = Encoding.ASCII.GetString(readenData).Split('\n');
            Queue<int> randomNodeNumbers = new Queue<int>();
            foreach(string dataString in dataStrings)
            {
                var data = dataString.Substring(0,dataString.LastIndexOf('.'));
                int randomNodeNumber = int.Parse(dataString.Substring(dataString.LastIndexOf('.') + 1));
                this.Add(data);
                randomNodeNumbers.Enqueue(randomNodeNumber);
                

            }
            foreach(ListNode node in this)
            {
                node.Random = GetRandomNode(randomNodeNumbers.Dequeue());

            }
            

        }

        public void Add(string data)
        {
            ListNode node = new ListNode(data);

            if(Head==null)
            {
                Head = node;
                
            }
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
            }
            Tail = node;
            Tail.Random = GetRandomNode(random.Next(Count + 1)); 
            Count++;


        }

        public void Print()
        {

            foreach(ListNode node in this)
            {
                Console.WriteLine($"Data: {node.Data} , random node data: {node.Random.Data}");
            }
        }

        private ListNode GetRandomNode(int randomNodeNumber)
        {
            
            int nodeNumber = 0;
            foreach(ListNode node in this)
            {
                if (nodeNumber == randomNodeNumber) return node;
                else
                {
                    nodeNumber++;
                }
            }
            return null;

        }

        public IEnumerator GetEnumerator()
        {
            ListNode node = Head;
            while (node != null)
            {
                yield return node;
                node = node.Next;
            }
            yield break;
        }

        private Random random =new Random();
    }

    

}
