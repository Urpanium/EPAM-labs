using System.Collections.Generic;

namespace T0.Medias
{
    public class MediaPlayer
    {

        public float PlayDelay { get; set; }

        private Queue<Media> Queue;

        public void PlayList(List<Media> medias)
        {
            
            foreach (Media media in medias)
            {
                Queue.Enqueue(media);
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