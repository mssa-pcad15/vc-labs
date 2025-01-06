using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSATest.Queue
{
    [TestClass]
    public class QueueTest
    {
        [TestMethod]
        public void EnqueueTenThings() { 
            Queue<int> queue= new Queue<int>();
            for (int i = 0; i < 10; i++) { 
            queue.Enqueue(i);
            }
            Assert.AreEqual(10, queue.Count); //should be 10 in queue
            Assert.AreEqual(0,queue.Peek()); // peek return the first item without removing it
            Assert.AreEqual(10, queue.Count); //count is still ten
            Assert.AreEqual(0, queue.Dequeue()); //dequeue remove the head of the queue and return it
            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(2, queue.Dequeue());
            Assert.AreEqual(7, queue.Count); // now there are 7 things in the queue
        }
        [TestMethod]
        public async Task AsyncEnqueuLotsOfItemsAsync()
        {
            Queue<int> queue = new Queue<int>();

            Task t1 = InsertItemAsync(queue, 10);
            Task t2 = InsertItemAsync(queue, 10);
            Task t3 = InsertItemAsync(queue, 10);
            Task t4 = InsertItemAsync(queue, 10);
            Task t5 = InsertItemAsync(queue, 10);// this method is asynchronous

            //await keyword for a task cause it to block
          
            Assert.AreEqual(50,queue.Count);
        }

        [TestMethod]
        public async Task ConcurrentEnqueuLotsOfItemsAsync()
        {
            Queue<int> queue = new Queue<int>();

            Parallel.For(0, 5, loopCounter => InsertItemAsync(queue, 10)); 

            Assert.AreEqual(50, queue.Count);
        }
        private async Task InsertItemAsync(Queue<int> queue, int numberOfItem) {
            for (int i = 0; i < numberOfItem; i++)
            { 
                queue.Enqueue(i);
                await Task.Delay(10);
                
            }
        }
    }
}
