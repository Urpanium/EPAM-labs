using System.Collections.Generic;

namespace T0.Medias
{
    public class MediaPlayer
    {

        //public float PlayDelay { get; set; }

        private Queue<Media> Queue;

        public MediaPlayer()
        {
            Queue = new Queue<Media>();
        }
        public void PlayList(List<Media> medias)
        {
            //LINQ?????
            foreach (Media media in medias)
            {
                Queue.Enqueue(media);
            }
            
        }

        public void PlayAll()
        {
            foreach (Media media in Queue)
            {
                media.Play();
            }
        }
        
        
        public void Play(Media media)
        {
            Clear();
            Queue.Enqueue(media);
        }

        public void AddToQueue(Media media)
        {
            Queue.Enqueue(media);
        }

        public void Clear()
        {
            Queue.Clear();
        }
        
    }
}